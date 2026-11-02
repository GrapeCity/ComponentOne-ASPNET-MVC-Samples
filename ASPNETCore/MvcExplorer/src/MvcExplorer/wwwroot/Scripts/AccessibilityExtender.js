var wijmo;
(function (wijmo) {
    var grid;
    (function (grid_1) {
        var accessibility;
        (function (accessibility) {
            /**
             * Class that adds accessibility features to a FlexGrid control.
             */
            var AccessibilityExtender = /** @class */ (function () {
                /**
                 * Initializes a new instance of an AccessibilityExtender.
                 *
                 * @param grid FlexGrid that will be extended by this AccessibilityExtender.
                 */
                function AccessibilityExtender(grid) {
                    this._step = 5; // resize step
                    this._g = grid;
                    // keyboard-based column sizing
                    var host = grid.hostElement;
                    grid.addEventListener(host, 'keydown', this._keydown.bind(this), true);
                    grid.addEventListener(host, 'keypress', this._keypress.bind(this), true);
                    // add alert element to grid
                    this._alert = document.createElement('div');
                    this._alert.setAttribute('role', 'alert');
                    this._alert.setAttribute('aria-live', 'assertive');
                    wijmo.setCss(this._alert, {
                        position: 'fixed',
                        top: -1000,
                        left: -1000
                    });
                    host.appendChild(this._alert);
                }
                Object.defineProperty(AccessibilityExtender.prototype, "polite", {
                    /**
                     * Gets or sets a value that determines whether ARIA alerts should
                     * be 'polite' or 'assertive'.
                     */
                    get: function () {
                        return this._alert.getAttribute('aria-live') == 'polite';
                    },
                    set: function (value) {
                        this._alert.setAttribute('aria-live', value ? 'polite' : 'assertive');
                    },
                    enumerable: true,
                    configurable: true
                });
                Object.defineProperty(AccessibilityExtender.prototype, "resizeStep", {
                    /**
                     * Gets or sets the size of each resizing step applied when the user
                     * presses the alt-left or alt-right keys.
                     */
                    get: function () {
                        return this._step;
                    },
                    set: function (value) {
                        this._step = value;
                    },
                    enumerable: true,
                    configurable: true
                });
                /**
                 * Issues an ARIA alert.
                 * @param msg Message to be issued.
                 */
                AccessibilityExtender.prototype.alert = function (msg) {
                    this._alert.textContent = msg;
                };
                // ** implementation
                // resize columns when the user presses the alt-left or alt-right keys
                AccessibilityExtender.prototype._keydown = function (e) {
                    var g = this._g;
                    this._ignoreKeyPress = false;
                    if (e.altKey && (g.allowResizing & grid_1.AllowResizing.Columns)) {
                        switch (e.keyCode) {
                            case wijmo.Key.End: // alt-end auto-sizes selection
                            case wijmo.Key.Left: // alt-left decreases width
                            case wijmo.Key.Right:// alt-right increases width
                                var sel = g.selection;
                                if (sel.col > -1) {
                                    var cols = g.columns, col = cols[sel.col], wid = col.renderWidth, step = this._step * (e.shiftKey ? 2 : 1);
                                    if (e.keyCode == wijmo.Key.End) {
                                        g.autoSizeColumns(sel.leftCol, sel.rightCol);
                                    }
                                    else {
                                        if (e.keyCode == wijmo.Key.Left) {
                                            wid = Math.round(Math.max(step, wid - step));
                                        }
                                        else if (e.keyCode == wijmo.Key.Right) {
                                            wid = Math.round(wid + step);
                                        }
                                        for (var i = sel.leftCol; i <= sel.rightCol; i++) {
                                            if (cols[i].allowResizing) {
                                                cols[i].width = wid;
                                            }
                                        }
                                    }
                                }
                                this._ignoreKeyPress = true;
                                e.preventDefault();
                                break;
                        }
                    }
                };
                // prevent alt+num key composition
                AccessibilityExtender.prototype._keypress = function (e) {
                    if (this._ignoreKeyPress) {
                        e.preventDefault();
                        this._ignoreKeyPress = false;
                    }
                };
                return AccessibilityExtender;
            }());
            accessibility.AccessibilityExtender = AccessibilityExtender;
        })(accessibility = grid_1.accessibility || (grid_1.accessibility = {}));
    })(grid = wijmo.grid || (wijmo.grid = {}));
})(wijmo || (wijmo = {}));
//# sourceMappingURL=AccessibilityExtender.js.map