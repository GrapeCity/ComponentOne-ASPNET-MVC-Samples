var wijmo;
(function (wijmo) {
    var chart;
    (function (chart) {
        'use strict';
        /**
         * Render to canvas.
         */
        var _CanvasRenderEngine = (function () {
            function _CanvasRenderEngine(element, applyCSSStyles) {
                if (applyCSSStyles === void 0) { applyCSSStyles = false; }
                this._strokeWidth = 1;
                this._fontSize = null;
                this._fontFamily = null;
                this._applyCanvasClip = function (ctx, clipID) {
                    var clipRect = this._canvasRect[clipID];
                    if (!clipRect) {
                        return;
                    }
                    ctx.beginPath();
                    ctx.rect(clipRect.left, clipRect.top, clipRect.width, clipRect.height);
                    ctx.clip();
                    ctx.closePath();
					
                };
                this._applyCanvasStyles = function (ele, style, className, enableFill, enableFont) {
                    var self = this, ctx = self._canvas.getContext('2d'), font, eleStyles, stroke = self.stroke, fill = self.fill, strokeWidth = self.strokeWidth;
                    if (style && style.stroke !== undefined) {
                        stroke = style.stroke;
                    }
                    if (style && style.fill !== undefined) {
                        fill = style.fill;
                    }
                    //style can be set in tag name, so always get element style.
                    if (ele) {
                        //if (style != null || className != null) {
                        //if eleStyles is not null, use eleStyles
                        eleStyles = window.getComputedStyle(ele);
                    }
                    if (enableFont) {
                        if (eleStyles) {
                            ctx.fillStyle = eleStyles.color;
                            font = eleStyles.fontSize + ' ' + eleStyles.fontFamily;
                            ctx.font = font;
                            if (ctx.font.replace(/\"/g, "'") !== font.replace(/\"/g, "'")) {
                                font = eleStyles.fontSize + ' ' + (ctx.font.split(' ')[1] || 'sans-serif');
                                ctx.font = font;
                            }
                        }
                        else if (self.fontSize && self.fontFamily) {
                            ctx.fillStyle = self.textFill;
                            ctx.font = self.fontSize + ' ' + self.fontFamily;
                        }
                        else if (self._canvasDefaultFont) {
                            ctx.fillStyle = self._canvasDefaultFont.textFill;
                            font = self._canvasDefaultFont.fontSize + ' ' + self._canvasDefaultFont.fontFamily;
                            ctx.font = font;
                            if (ctx.font.replace(/\"/g, "'") !== font.replace(/\"/g, "'")) {
                                font = self._canvasDefaultFont.fontSize + ' ' + (ctx.font.split(' ')[1] || 'sans-serif');
                                ctx.font = font;
                            }
                        }
                    }
                    else {
                        if (eleStyles) {
                            stroke = (eleStyles.stroke && eleStyles.stroke !== 'none') ? eleStyles.stroke : stroke;
                            fill = (eleStyles.fill && eleStyles.fill !== 'none') ? eleStyles.fill : fill;
                            strokeWidth = eleStyles.strokeWidth ? eleStyles.strokeWidth : strokeWidth;
                        }
                        if (stroke !== 'none' && stroke != null) {
                            ctx.strokeStyle = stroke;
                            ctx.lineWidth = strokeWidth;
                            ctx.stroke();
                        }
                        if (enableFill && fill != null && fill !== 'transparent' && fill !== 'none') {
                            ctx.fillStyle = fill;
                            ctx.fill();
                        }
                    }
                };
                var self = this;
                self._element = element;
                self._canvas = document.createElement('canvas');
                self._svgEngine = new chart._SvgRenderEngine(element);
                self._element.appendChild(self._canvas);
                self._applyCSSStyles = applyCSSStyles;
            }
            _CanvasRenderEngine.prototype.beginRender = function () {
                var self = this, svg = self._svgEngine.element, ele = self._element, style;
                if (self._applyCSSStyles) {
                    self._svgEngine.beginRender();
                    ele = svg;
                }
                self._element.appendChild(svg);
                //clear and initialize _canvasRect, _canvasDefaultFont
                self._canvasRect = {};
                style = window.getComputedStyle(ele);
                self._canvasDefaultFont = {
                    fontSize: style.fontSize,
                    fontFamily: style.fontFamily,
                    textFill: style.color
                };
            };
            _CanvasRenderEngine.prototype.endRender = function () {
                if (this._applyCSSStyles) {
                    this._svgEngine.endRender();
                }
                this._svgEngine.element.parentNode.removeChild(this._svgEngine.element);
            };
            _CanvasRenderEngine.prototype.setViewportSize = function (w, h) {
                var self = this, canvas = self._canvas, ctx = canvas.getContext('2d'), f = self.fill, color, stroke;
                if (self._applyCSSStyles) {
                    self._svgEngine.setViewportSize(w, h);
                }
                canvas.width = w;
                canvas.height = h;
                color = window.getComputedStyle(self._element).backgroundColor;
                stroke = self.stroke;
                if (color && color != "rgba(0, 0, 0, 0)") {//C1WEB 27540 ignore exception
                    self.stroke = null;
                    self.fill = color;
                    self.drawRect(0, 0, w, h);
                    self.fill = f;
                    self.stroke = stroke;
                }
            };
            Object.defineProperty(_CanvasRenderEngine.prototype, "element", {
                get: function () {
                    return this._canvas;
                },
                enumerable: true,
                configurable: true
            });
            Object.defineProperty(_CanvasRenderEngine.prototype, "fill", {
                get: function () {
                    return this._fill;
                },
                set: function (value) {
                    this._svgEngine.fill = value;
                    this._fill = value;
                },
                enumerable: true,
                configurable: true
            });
            Object.defineProperty(_CanvasRenderEngine.prototype, "fontSize", {
                get: function () {
                    return this._fontSize;
                },
                set: function (value) {
                    this._svgEngine.fontSize = value;
                    this._fontSize = value;
                },
                enumerable: true,
                configurable: true
            });
            Object.defineProperty(_CanvasRenderEngine.prototype, "fontFamily", {
                get: function () {
                    return this._fontFamily;
                },
                set: function (value) {
                    this._svgEngine.fontFamily = value;
                    this._fontFamily = value;
                },
                enumerable: true,
                configurable: true
            });
            Object.defineProperty(_CanvasRenderEngine.prototype, "stroke", {
                get: function () {
                    return this._stroke;
                },
                set: function (value) {
                    this._svgEngine.stroke = value;
                    this._stroke = value;
                },
                enumerable: true,
                configurable: true
            });
            Object.defineProperty(_CanvasRenderEngine.prototype, "strokeWidth", {
                get: function () {
                    return this._strokeWidth;
                },
                set: function (value) {
                    this._svgEngine.strokeWidth = value;
                    this._strokeWidth = value;
                },
                enumerable: true,
                configurable: true
            });
            Object.defineProperty(_CanvasRenderEngine.prototype, "textFill", {
                get: function () {
                    return this._textFill;
                },
                set: function (value) {
                    this._svgEngine.textFill = value;
                    this._textFill = value;
                },
                enumerable: true,
                configurable: true
            });
            _CanvasRenderEngine.prototype.addClipRect = function (clipRect, id) {
                if (clipRect && id) {
                    if (this._applyCSSStyles) {
                        this._svgEngine.addClipRect(clipRect, id);
                    }
                    this._canvasRect[id] = clipRect;
                }
            };
            _CanvasRenderEngine.prototype.drawEllipse = function (cx, cy, rx, ry, className, style) {
                var el, ctx = this._canvas.getContext('2d');
                if (this._applyCSSStyles) {
                    el = this._svgEngine.drawEllipse(cx, cy, rx, ry, className, style);
                }
                ctx.save();
                ctx.beginPath();
                if (ctx.ellipse) {
                    ctx.ellipse(cx, cy, rx, ry, 0, 0, 2 * Math.PI);
                }
                else {
                    ctx.translate(cx, cy);
                    ctx.scale(1, ry / rx);
                    ctx.translate(-cx, -cy);
                    ctx.arc(cx, cy, rx, 0, 2 * Math.PI);
                    ctx.scale(1, 1);
                }
                this._applyCanvasStyles(el, style, className, true);
                ctx.restore();
                return el;
            };
            _CanvasRenderEngine.prototype.drawRect = function (x, y, w, h, className, style, clipPath) {
                var el, ctx = this._canvas.getContext('2d');
                if (this._applyCSSStyles) {
                    el = this._svgEngine.drawRect(x, y, w, h, className, style, clipPath);
                }
                ctx.save();
                this._applyCanvasClip(ctx, clipPath);
                ctx.beginPath();
                ctx.rect(x, y, w, h);
                this._applyCanvasStyles(el, style, className, true);
                ctx.restore();
                return el;
            };
            _CanvasRenderEngine.prototype.drawLine = function (x1, y1, x2, y2, className, style) {
                var el, ctx = this._canvas.getContext('2d');
                if (this._applyCSSStyles) {
                    el = this._svgEngine.drawLine(x1, y1, x2, y2, className, style);
                }
                ctx.save();
                ctx.beginPath();
                ctx.moveTo(x1, y1);
                ctx.lineTo(x2, y2);
                this._applyCanvasStyles(el, style, className);
                ctx.restore();
                return el;
            };
            _CanvasRenderEngine.prototype.drawLines = function (xs, ys, className, style, clipPath) {
                if (!xs || !ys || xs.length === 0 || ys.length === 0) {
                    return;
                }
                var ctx = this._canvas.getContext('2d'), len = Math.min(xs.length, ys.length), el, i;
                if (this._applyCSSStyles) {
                    el = this._svgEngine.drawLines([0, 1], [1, 0], className, style, clipPath);
                }
                ctx.save();
                this._applyCanvasClip(ctx, clipPath);
                ctx.beginPath();
                ctx.moveTo(xs[0], ys[0]);
                for (i = 1; i < len; i++) {
                    ctx.lineTo(xs[i], ys[i]);
                }
                this._applyCanvasStyles(el, style, className);
                ctx.restore();
                return el;
            };
            _CanvasRenderEngine.prototype.drawSplines = function (xs, ys, className, style, clipPath) {
                if (!xs || !ys || xs.length === 0 || ys.length === 0) {
                    return;
                }
                var ctx = this._canvas.getContext('2d'), spline = new chart._Spline(xs, ys), s = spline.calculate(), sx = s.xs, sy = s.ys, len = Math.min(sx.length, sy.length), el, i;
                if (this._applyCSSStyles) {
                    el = this._svgEngine.drawSplines([0, 1], [1, 0], className, style, clipPath);
                }
                ctx.save();
                this._applyCanvasClip(ctx, clipPath);
                ctx.beginPath();
                ctx.moveTo(sx[0], sy[0]);
                for (i = 1; i < len; i++) {
                    ctx.lineTo(sx[i], sy[i]);
                }
                this._applyCanvasStyles(el, style, className);
                ctx.restore();
                return el;
            };
            _CanvasRenderEngine.prototype.drawPolygon = function (xs, ys, className, style, clipPath) {
                if (!xs || !ys || xs.length === 0 || ys.length === 0) {
                    return;
                }
                var ctx = this._canvas.getContext('2d'), len = Math.min(xs.length, ys.length), el, i;
                if (this._applyCSSStyles) {
                    el = this._svgEngine.drawPolygon([0, 1, 1], [1, 0, 1], className, style, clipPath);
                }
                ctx.save();
                this._applyCanvasClip(ctx, clipPath);
                ctx.beginPath();
                ctx.moveTo(xs[0], ys[0]);
                for (i = 1; i < len; i++) {
                    ctx.lineTo(xs[i], ys[i]);
                }
                ctx.closePath();
                this._applyCanvasStyles(el, style, className, true);
                ctx.restore();
                return el;
            };
            _CanvasRenderEngine.prototype.drawPieSegment = function (cx, cy, r, startAngle, sweepAngle, className, style, clipPath) {
                var el, ctx = this._canvas.getContext('2d'), sta = startAngle, swa = startAngle + sweepAngle;
                if (this._applyCSSStyles) {
                    el = this._svgEngine.drawPieSegment(cx, cy, r, startAngle, sweepAngle, className, style, clipPath);
                }
                ctx.save();
                this._applyCanvasClip(ctx, clipPath);
                ctx.beginPath();
                ctx.moveTo(cx, cy);
                ctx.arc(cx, cy, r, sta, swa, false);
                ctx.lineTo(cx, cy);
                this._applyCanvasStyles(el, style, className, true);
                ctx.restore();
                return el;
            };
            _CanvasRenderEngine.prototype.drawDonutSegment = function (cx, cy, radius, innerRadius, startAngle, sweepAngle, className, style, clipPath) {
                var ctx = this._canvas.getContext('2d'), sta = startAngle, swa = startAngle + sweepAngle, el, p1, p2;
                if (this._applyCSSStyles) {
                    el = this._svgEngine.drawDonutSegment(cx, cy, radius, innerRadius, startAngle, sweepAngle, className, style, clipPath);
                }
                p1 = new wijmo.Point(cx, cy);
                p1.x += innerRadius * Math.cos(sta);
                p1.y += innerRadius * Math.sin(sta);
                p2 = new wijmo.Point(cx, cy);
                p2.x += innerRadius * Math.cos(swa);
                p2.y += innerRadius * Math.sin(swa);
                ctx.save();
                this._applyCanvasClip(ctx, clipPath);
                ctx.beginPath();
                ctx.moveTo(p1.x, p1.y);
                ctx.arc(cx, cy, radius, sta, swa, false);
                ctx.lineTo(p2.x, p2.y);
                ctx.arc(cx, cy, innerRadius, swa, sta, true);
                this._applyCanvasStyles(el, style, className, true);
                ctx.restore();
                return el;
            };
            _CanvasRenderEngine.prototype.drawString = function (s, pt, className, style) {
                var el, ctx = this._canvas.getContext('2d');
                if (this._applyCSSStyles) {
                    el = this._svgEngine.drawString(s, pt, className, style);
                }
                ctx.save();
                ctx.textBaseline = 'bottom';
                this._applyCanvasStyles(el, style, className, true, true);
                ctx.fillText(s, pt.x, pt.y);
                ctx.restore();
                return el;
            };
            _CanvasRenderEngine.prototype.drawStringRotated = function (s, pt, center, angle, className, style) {
                var el, ctx = this._canvas.getContext('2d'), metric = ctx.measureText(s);
                if (this._applyCSSStyles) {
                    el = this._svgEngine.drawStringRotated(s, pt, center, angle, className, style);
                }
                ctx.save();
                ctx.textBaseline = 'bottom';
                ctx.translate(center.x, center.y);
                ctx.rotate(Math.PI / 180 * angle);
                ctx.translate(-center.x, -center.y);
                this._applyCanvasStyles(el, style, className, true, true);
                ctx.fillText(s, pt.x, pt.y);
                ctx.restore();
                return el;
            };
            _CanvasRenderEngine.prototype.measureString = function (s, className, groupName, style) {
                var ctx = ctx = this._canvas.getContext('2d'), width;
                this._applyCanvasStyles(null, null, className, true, true);
                width = ctx.measureText(s).width;
                return new wijmo.Size(width, parseInt(ctx.font) * 1.5);
            };
            _CanvasRenderEngine.prototype.startGroup = function (className, clipPath, createTransform) {
                if (createTransform === void 0) { createTransform = false; }
                var el, ctx = this._canvas.getContext('2d');
                if (this._applyCSSStyles) {
                    el = this._svgEngine.startGroup(className, clipPath, createTransform);
                }
                ctx.save();
                this._applyCanvasClip(ctx, clipPath);
                return el;
            };
            _CanvasRenderEngine.prototype.endGroup = function () {
                if (this._applyCSSStyles) {
                    this._svgEngine.endGroup();
                }
                this._canvas.getContext('2d').restore();
            };
            _CanvasRenderEngine.prototype.drawImage = function (imageHref, x, y, w, h) {
                var el, ctx = this._canvas.getContext('2d'), img = new Image;
                if (this._applyCSSStyles) {
                    el = this._svgEngine.drawImage(imageHref, x, y, w, h);
                }
                img.onload = function () {
                    ctx.drawImage(img, x, y, w, h);
                };
                img.src = imageHref;
                return el;
            };
            return _CanvasRenderEngine;
        })();
        chart._CanvasRenderEngine = _CanvasRenderEngine;
        //export chart using _CanvasRenderEngine.
        if (wijmo.chart.FlexChartBase && wijmo.chart.FlexChartBase.prototype && wijmo.chart.FlexChartBase.prototype._exportToImage) {
            var _exportFn = wijmo.chart.FlexChartBase.prototype._exportToImage;
            wijmo.chart.FlexChartBase.prototype._exportToImage = function (extension, processDataURI) {
                if (extension === 'svg') {
                    _exportFn.call(this, extension, processDataURI);
                    return;
                }
                var canvasEngine = new wijmo.chart._CanvasRenderEngine(this.hostElement, true), dataUrl, canvas;
                this._render(canvasEngine);
                canvas = canvasEngine.element;
                canvas.parentNode.removeChild(canvas);
                dataUrl = canvas.toDataURL('image/' + extension);
                processDataURI.call(null, dataUrl);
                canvas = null;
                canvasEngine = null;
            };
        }
    })(chart = wijmo.chart || (wijmo.chart = {}));
})(wijmo || (wijmo = {}));
//# sourceMappingURL=CanvasRenderEngine.js.map
