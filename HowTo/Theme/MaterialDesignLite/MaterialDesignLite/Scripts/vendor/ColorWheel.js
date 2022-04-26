var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var wijmo;
(function (wijmo) {
    var material;
    (function (material) {
        /**
         * Implements a color wheel similar to the one in the Material Design Lite customization page:
         * http://www.getmdl.io/customize/index.html#cdn-code
         */
        var ColorWheel = /** @class */ (function (_super) {
            __extends(ColorWheel, _super);
            function ColorWheel(element, options) {
                var _this = _super.call(this, element, null, true) || this;
                _this.themeChanged = new wijmo.Event();
                // instantiate and apply template
                var tpl = _this.getTemplate();
                _this.applyTemplate('wj-control wj-colorwheel', tpl, {
                    _dSvg: 'dsvg',
                    _svg: 'svg'
                });
                // initialize tooltip used to show color names
                _this._tooltip = new wijmo.Tooltip({
                    showAtMouse: true,
                    gap: 12
                });
                // handle clicks to select primary/accent colors
                _this._svg.addEventListener('click', function (e) {
                    var g = wijmo.closest(e.target, 'g'), index = g ? g.getAttribute(ColorWheel._PALINDEX) : null;
                    if (index) {
                        _this._click(parseInt(index));
                    }
                });
                // initialize control options
                _this.initialize(options);
                return _this;
            }
            Object.defineProperty(ColorWheel.prototype, "palette", {
                // ** object model
                get: function () {
                    return this._palette;
                },
                set: function (value) {
                    this._palette = value;
                    this.refresh();
                },
                enumerable: true,
                configurable: true
            });
            Object.defineProperty(ColorWheel.prototype, "primary", {
                get: function () {
                    return wijmo.isArray(this._palette) && wijmo.isNumber(this._primary) && this._primary > -1
                        ? this._palette[this._primary].name
                        : null;
                },
                set: function (value) {
                    var index = this._paletteIndex(value);
                    if (index > -1 && index != this._primary) {
                        this._primary = index;
                        this._themeChanged();
                    }
                },
                enumerable: true,
                configurable: true
            });
            Object.defineProperty(ColorWheel.prototype, "accent", {
                get: function () {
                    return wijmo.isArray(this._palette) && wijmo.isNumber(this._accent) && this._accent > -1
                        ? this._palette[this._accent].name
                        : null;
                },
                set: function (value) {
                    var index = this._paletteIndex(value);
                    if (index > -1 && index != this._accent) {
                        this._accent = index;
                        this._themeChanged();
                    }
                },
                enumerable: true,
                configurable: true
            });
            ColorWheel.prototype.onThemeChanged = function (e) {
                this.themeChanged.raise(this);
            };
            // ** overrides
            ColorWheel.prototype.refresh = function (fullUpdate) {
                if (fullUpdate === void 0) { fullUpdate = true; }
                _super.prototype.refresh.call(this, fullUpdate);
                this._createWheel();
            };
            // ** implementation
            // gets the paletted index for a given color name
            ColorWheel.prototype._paletteIndex = function (color) {
                for (var i = 0; i < this._palette.length; i++) {
                    if (this._palette[i].name == color) {
                        return i;
                    }
                }
                return -1;
            };
            // craete the SVG elements that make up the wheel
            ColorWheel.prototype._createWheel = function () {
                // clear SVG element
                while (this._svg.firstChild) {
                    this._svg.removeChild(this._svg.firstChild);
                }
                // sanity
                if (!wijmo.isArray(this._palette) || this._palette.length < 4) {
                    return;
                }
                // get parameters
                var wid = this.hostElement.scrollWidth, hei = this.hostElement.scrollHeight;
                this._radius = Math.min(wid, hei) / 2 - ColorWheel._POINTERSIZE;
                this._cx = wid / 2,
                    this._cy = hei / 2;
                // more sanity
                if (this._radius < 10) {
                    return;
                }
                // add wheel sectors to main SVG element
                for (var i = 0; i < this._palette.length; i++) {
                    // create group for this entry
                    var entry = this._palette[i], group = this._createSvgElement('g');
                    this._svg.appendChild(group);
                    group.setAttribute(ColorWheel._PALINDEX, i.toString());
                    // set tooltip for the group
                    this._tooltip.setTooltip(group, entry.name);
                    // add transparent large element for hit-testing
                    var sector = this._createSector(this._radius * .4, this._radius * 1 + ColorWheel._POINTERSIZE, 'transparent');
                    group.appendChild(sector);
                    // add outer color segment
                    sector = this._createSector(this._radius * .4, this._radius * 1, entry.outer);
                    group.appendChild(sector);
                    // add inner color segment
                    sector = this._createSector(this._radius * .4, this._radius * .55, entry.inner);
                    group.appendChild(sector);
                    // apply rotation to group
                    group.setAttribute('transform', 'rotate(' +
                        (this._getSectorAngle(i)).toFixed(2) + ', ' +
                        this._cx.toFixed(2) + ' ' +
                        this._cy.toFixed(2) + ')');
                }
                // add primary and accent pointers
                this._primaryPtr = this._createPointer('1');
                this._accentPtr = this._createPointer('2');
                // update pointer positions
                this._themeChanged();
            };
            // select primary/accent entries on click
            ColorWheel.prototype._click = function (index) {
                if (this._primary != null && this._accent == null) {
                    if (!this._palette[index].primary) {
                        this._accent = index != this._primary ? index : null;
                    }
                }
                else {
                    this._primary = index;
                    this._accent = null;
                }
                this._themeChanged();
            };
            // create an SVG element using the proper namespace
            ColorWheel.prototype._createSvgElement = function (tag) {
                return document.createElementNS('http://www.w3.org/2000/svg', tag);
            };
            // create a sector of the color wheel
            ColorWheel.prototype._createSector = function (rStart, rEnd, color) {
                var p = this._createSvgElement('path'), angle = 2 * Math.PI / this._palette.length, cs = Math.cos(+angle / 2), ss = Math.sin(+angle / 2), ce = Math.cos(-angle / 2), se = Math.sin(-angle / 2), cx = this._cx, cy = this._cy;
                p.setAttribute('d', 'M' + (cx + rStart * cs).toFixed(2) + ' ' + (cy + rStart * ss).toFixed(2) + ' ' +
                    'L ' + (cx + rEnd * cs).toFixed(2) + ' ' + (cy + rEnd * ss).toFixed(2) + ' ' +
                    'L ' + (cx + rEnd * ce).toFixed(2) + ' ' + (cy + rEnd * se).toFixed(2) + ' ' +
                    'L ' + (cx + rStart * ce).toFixed(2) + ' ' + (cy + rStart * se).toFixed(2) + ' ' +
                    'z');
                p.style.fill = color;
                p.style.strokeLinejoin = 'round';
                return p;
            };
            // create a pointer to a sector
            ColorWheel.prototype._createPointer = function (caption) {
                // group to hold pointer balloon and text
                var g = this._createSvgElement('g');
                // add balloon to pointer group
                var p = this._createSvgElement('path'), size = ColorWheel._POINTERSIZE / 2, diag = size * Math.cos(Math.PI / 4), cx = this._cx, cy = this._cy;
                p.setAttribute('d', 'M' + (cx + this._radius).toFixed(2) + ' ' + cy.toFixed(2) + ' ' +
                    'l ' + diag.toFixed(2) + ' ' + -diag.toFixed(2) + ' ' +
                    'a ' + size + ' ' + size + ', 0 1 1, 0 ' + (2 * diag).toFixed(2) + ' ' +
                    'z');
                p.style.fill = '#c0c0c0';
                g.appendChild(p);
                // add sector outline to pointer group
                var sector = this._createSector(this._radius * .4, this._radius * 1, 'transparent');
                sector.style.strokeWidth = '2px';
                sector.style.stroke = 'rgba(0,0,0, 0.3)';
                sector.style.pointerEvents = 'none';
                g.appendChild(sector);
                // add text to pointer group
                var t = this._createSvgElement('text');
                t.setAttribute('class', 'wj-pointer');
                t.setAttribute('text-anchor', 'middle');
                t.textContent = caption;
                g.appendChild(t);
                // add pointer group to main svg element
                this._svg.appendChild(g);
                // done
                return g;
            };
            // gets the starting angle for a sector (in degrees)
            ColorWheel.prototype._getSectorAngle = function (index) {
                return (index + .5) / this._palette.length * 360 - 90;
            };
            // update UI to reflect current selection
            ColorWheel.prototype._themeChanged = function () {
                // update pointer positions/visibility
                this._updatePointer(this._primaryPtr, this._primary);
                this._updatePointer(this._accentPtr, this._accent);
                // enable/disable quadrants
                var disable = this._primary != null && this._accent == null, sectors = this._svg.querySelectorAll('g[' + ColorWheel._PALINDEX + ']');
                for (var i = 0; i < sectors.length; i++) {
                    if (this._palette[i].primary) {
                        var sector = sectors[i];
                        if (disable && i != this._primary) {
                            sector.setAttribute('opacity', '0.3');
                        }
                        else {
                            sector.removeAttribute('opacity');
                        }
                    }
                }
                // raise event
                this.onThemeChanged();
            };
            // update pointer position to indicate selection
            ColorWheel.prototype._updatePointer = function (ptr, index) {
                // if the index is null, hide the pointer
                if (index == null || index < 0) {
                    ptr.setAttribute('visibility', 'hidden');
                    return;
                }
                // get geometry
                var angle = this._getSectorAngle(index), cx = this._cx, cy = this._cy;
                // update pointer position
                var paths = ptr.querySelectorAll('path');
                for (var i = 0; i < paths.length; i++) {
                    paths[i].setAttribute('transform', 'rotate(' +
                        (angle).toFixed(2) + ', ' +
                        cx.toFixed(2) + ' ' +
                        cy.toFixed(2) + ')');
                }
                // get geometry
                var size = ColorWheel._POINTERSIZE / 2, diag = size * Math.cos(Math.PI / 4), radius = this._radius + size + diag - 6, sin = Math.sin(angle * Math.PI / 180), cos = Math.cos(angle * Math.PI / 180);
                // update pointer text
                var t = ptr.querySelector('text');
                t.setAttribute('x', (cx + radius * cos).toFixed(2));
                t.setAttribute('y', (cy + radius * sin).toFixed(2));
                t.setAttribute('dy', '.3em');
                // show the pointer
                ptr.removeAttribute('visibility');
            };
            ColorWheel._POINTERSIZE = 34;
            ColorWheel._PALINDEX = 'palette-index';
            /**
             * Gets or sets the template used to instantiate @see:Gauge controls.
             */
            ColorWheel.controlTemplate = '<div wj-part="dsvg" style="width:100%;height:100%">' +
                '<svg wj-part="svg" width="100%" height="100%" style="overflow:visible"></svg>' +
                '</div>';
            return ColorWheel;
        }(wijmo.Control));
        material.ColorWheel = ColorWheel;
    })(material = wijmo.material || (wijmo.material = {}));
})(wijmo || (wijmo = {}));
//# sourceMappingURL=ColorWheel.js.map