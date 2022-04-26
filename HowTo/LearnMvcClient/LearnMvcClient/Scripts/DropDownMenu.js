/// <reference path="ts/wijmo.d.ts" />
var c1;
(function (c1) {
    var sample;
    (function (sample) {
        var DropDownMenu = (function () {
            function DropDownMenu(element, parent) {
                if (element instanceof String) {
                    element = document.querySelector(element);
                }
                if (element) {
                    this._menu = element;
                }
                if (parent instanceof String) {
                    parent = document.querySelector(parent);
                }
                if (parent) {
                    this._parent = parent;
                }
                this._init();
            }
            Object.defineProperty(DropDownMenu.prototype, "visible", {
                get: function () {
                    var display = this._menu.style.display || '';
                    if (display.toLowerCase() != 'none') {
                        return true;
                    }
                    return false;
                },
                enumerable: true,
                configurable: true
            });
            DropDownMenu.prototype._init = function () {
                var _this = this;
                this._menu.style.display = 'none';
                document.addEventListener('click', this._documentClick.bind(this));
                this._parent.style.cursor = "pointer";
                this._parent.addEventListener('click', function () {
                    if (_this.visible) {
                        _this.hide();
                    }
                    else {
                        _this.show();
                    }
                });
            };
            DropDownMenu.prototype._documentClick = function (event) {
                if (eventFrom(event, this._menu) || eventFrom(event, this._parent)) {
                    return;
                }
                this.hide();
            };
            ;
            DropDownMenu.prototype.hide = function () {
                if (this.visible) {
                    slideUp(this._menu);
                }
            };
            DropDownMenu.prototype.show = function () {
                if (!this.visible) {
                    slideDown(this._menu, this._parent);
                }
            };
            return DropDownMenu;
        }());
        sample.DropDownMenu = DropDownMenu;
    })(sample = c1.sample || (c1.sample = {}));
})(c1 || (c1 = {}));
