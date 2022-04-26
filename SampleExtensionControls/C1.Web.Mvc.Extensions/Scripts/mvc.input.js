var c1;
(function (c1) {
    var mvc;
    (function (mvc) {
        var input;
        (function (input_1) {
            var _MultiColumnComboBoxWrapper = /** @class */ (function (_super) {
                __extends(_MultiColumnComboBoxWrapper, _super);
                function _MultiColumnComboBoxWrapper() {
                    return _super !== null && _super.apply(this, arguments) || this;
                }
                Object.defineProperty(_MultiColumnComboBoxWrapper.prototype, "_controlType", {
                    get: function () {
                        return MultiColumnComboBox;
                    },
                    enumerable: true,
                    configurable: true
                });
                Object.defineProperty(_MultiColumnComboBoxWrapper.prototype, "_initializerType", {
                    get: function () {
                        return c1.mvc.input._ItemsInputInitializer;
                    },
                    enumerable: true,
                    configurable: true
                });
                return _MultiColumnComboBoxWrapper;
            }(c1.mvc.input._DropDownWrapper));
            input_1._MultiColumnComboBoxWrapper = _MultiColumnComboBoxWrapper;
            var MultiColumnComboBox = /** @class */ (function (_super) {
                __extends(MultiColumnComboBox, _super);
                function MultiColumnComboBox() {
                    var _this = _super !== null && _super.apply(this, arguments) || this;
                    // property storage
                    _this._editable = false;
                    return _this;
                }
                MultiColumnComboBox.prototype.initialize = function (options) {
                    var _this = this;
                    if (options) {
                        this.itemsSource = options.itemsSource;
                        if (options.validationAttributes) {
                            this._validationAttributes = options.validationAttributes;
                            delete options.validationAttributes;
                        }
                        // Handle initial selectedValue for RemoteCollectionView
                        var cv = options.itemsSource instanceof wijmo.collections.CollectionView
                            ? options.itemsSource : null;
                        var selectedValue, selectedIndex, selectedItem;
                        if (mvc.DataSourceManager._isRemoteSource(cv)) {
                            var needProcessSelections = false;
                            if (typeof (options.selectedValue) != 'undefined') {
                                selectedValue = options.selectedValue;
                                delete options.selectedValue;
                                needProcessSelections = true;
                            }
                            if (typeof (options.selectedItem) != 'undefined') {
                                selectedItem = options.selectedItem;
                                delete options.selectedItem;
                                needProcessSelections = true;
                            }
                            if (typeof (options.selectedIndex) != 'undefined') {
                                selectedIndex = options.selectedIndex;
                                delete options.selectedIndex;
                                needProcessSelections = true;
                            }
                            if (needProcessSelections) {
                                var processSelections = function () {
                                    if (typeof (selectedValue) != 'undefined') {
                                        _this.selectedValues = selectedValue;
                                    }
                                    if (typeof (selectedItem) != 'undefined') {
                                        _this.selectedItems = selectedItem;
                                    }
                                    if (typeof (selectedIndex) != 'undefined') {
                                        _this._pageSelectedIndexes = selectedIndex;
                                    }
                                    setTimeout(function () {
                                        cv.collectionChanged.removeHandler(processSelections);
                                    }, 0);
                                };
                                cv.collectionChanged.addHandler(processSelections);
                            }
                        }
                    }
                    this._query = this.text;
                    _super.prototype.initialize.call(this, options);
                    // handle hidden field
                    this._handleHiddenField();
                };
                Object.defineProperty(MultiColumnComboBox.prototype, "isEditable", {
                    /**
                     * Gets or sets a value that enables or disables editing of the text
                     * in the input element of the @see:ComboBox (defaults to false).
                     */
                    get: function () {
                        return this._editable;
                    },
                    set: function (value) {
                        if (value !== this._editable) {
                            this._editable = wijmo.asBoolean(value);
                            this._updateHiddenInput();
                        }
                    },
                    enumerable: true,
                    configurable: true
                });
                MultiColumnComboBox.prototype._handleHiddenField = function () {
                    var self = this, name = self.hostElement.getAttribute("wj-name"), hidden = self._hiddenInput;
                    if (!hidden) {
                        hidden = document.createElement('input');
                        hidden.type = 'text';
                        hidden.name = name;
                        hidden.style.visibility = "hidden";
                        hidden.style.position = "absolute";
                        self._tbx.parentNode.insertBefore(hidden, self._tbx);
                        mvc.Utils.forwardValidationEvents(self, self._tbx, hidden, true);
                        self._hiddenInput = hidden;
                    }
                    if (self._validationAttributes) {
                        for (var att in self._validationAttributes) {
                            hidden.setAttribute(att, self._validationAttributes[att]);
                        }
                    }
                    self._updateHiddenInput();
                };
                MultiColumnComboBox.prototype._onTextchanged = function () {
                    var self = this;
                    if (!self._hiddenInput) {
                        return;
                    }
                    self._hiddenInput.value = self.text;
                    if (self.isEditable) {
                        mvc.Utils.triggerValidationChangeEvent(self._hiddenInput);
                    }
                };
                MultiColumnComboBox.prototype._onSelectedIndexChanged = function () {
                    var self = this;
                    if (!self._hiddenInput) {
                        return;
                    }
                    self._hiddenInput.value = self.selectedValues;
                    mvc.Utils.triggerValidationChangeEvent(self._hiddenInput);
                };
                MultiColumnComboBox.prototype._updateHiddenInput = function () {
                    var self = this, hidden = self._hiddenInput;
                    if (!hidden) {
                        return;
                    }
                    if (self.isEditable) {
                        hidden.value = self.text;
                        if (self.selectedIndexChanged.hasHandlers) {
                            self.selectedIndexChanged.removeHandler(self._onSelectedIndexChanged, self);
                        }
                        self.textChanged.addHandler(self._onTextchanged, self);
                    }
                    else {
                        hidden.value = self.selectedValues;
                        if (self.textChanged.hasHandlers) {
                            self.textChanged.removeHandler(self._onTextchanged, self);
                        }
                        self.selectedIndexChanged.addHandler(self._onSelectedIndexChanged, self);
                    }
                };
                return MultiColumnComboBox;
            }(c1.input.MultiColumnComboBox || {}));
            input_1.MultiColumnComboBox = MultiColumnComboBox;
            var _MultiColumnComboBoxInitializer = /** @class */ (function (_super) {
                __extends(_MultiColumnComboBoxInitializer, _super);
                function _MultiColumnComboBoxInitializer() {
                    var _this = _super !== null && _super.apply(this, arguments) || this;
                    _this._textPortToSelectedValue = false;
                    return _this;
                }
                _MultiColumnComboBoxInitializer.prototype._beforeInitializeControl = function (options) {
                    _super.prototype._beforeInitializeControl.call(this, options);
                    if (!options) {
                        return;
                    }
                    this._processOptions(options);
                    this._handleHiddenElement();
                };
                _MultiColumnComboBoxInitializer.prototype._processOptions = function (options) {
                    var actionUrl = options.itemsSourceAction;
                    if (actionUrl) {
                        options.itemsSourceFunction = function (query, max, callback) {
                            mvc.Utils.ajaxGet({
                                url: actionUrl,
                                success: function (response) {
                                    callback(response);
                                },
                                data: { query: query, max: max }
                            });
                        };
                        delete options.itemsSourceAction;
                    }
                    if (options.validationAttributes) {
                        this._validationAttributes = options.validationAttributes;
                        delete options.validationAttributes;
                    }
                    // the value is serialized as text if editable
                    var editable = options.isEditable == undefined || options.isEditable === true;
                    if (editable && options.displayMemberPath != options.selectedValuePath) {
                        if (options.text != undefined) {
                            this._textPortToSelectedValue = true;
                            options.selectedValue = options.text;
                            delete options.text;
                        }
                    }
                };
                _MultiColumnComboBoxInitializer.prototype._createHiddenElement = function () {
                    var ele = document.createElement('input');
                    ele['type'] = 'text';
                    ele.name = this.control.hostElement.getAttribute('wj-name');
                    ele.style.visibility = "hidden";
                    ele.style.position = "absolute";
                    return ele;
                };
                _MultiColumnComboBoxInitializer.prototype._handleHiddenElement = function () {
                    var wijContrl = this.control, controlInput = wijContrl._tbx;
                    if (this._hiddenElement) {
                        return;
                    }
                    this._hiddenElement = this._createHiddenElement();
                    if (this._validationAttributes) {
                        for (var att in this._validationAttributes) {
                            this._hiddenElement.setAttribute(att, this._validationAttributes[att]);
                        }
                    }
                    controlInput.parentNode.insertBefore(this._hiddenElement, controlInput);
                    mvc.Utils.forwardValidationEvents(wijContrl, controlInput, this._hiddenElement);
                };
                _MultiColumnComboBoxInitializer.prototype._updateHiddenField = function () {
                    var input = this._hiddenElement, autoComplete = this.control;
                    autoComplete.selectedIndexChanged.removeHandler(this._onSelectedIndexChanged, this);
                    autoComplete.textChanged.removeHandler(this._onTextchanged, this);
                    if (autoComplete.isEditable) {
                        input.value = autoComplete.text;
                        autoComplete.selectedIndexChanged.addHandler(this._onSelectedIndexChanged, this);
                        autoComplete.textChanged.addHandler(this._onTextchanged, this);
                    }
                    else {
                        input.value = autoComplete.selectedValue;
                        autoComplete.selectedIndexChanged.addHandler(this._onSelectedIndexChanged, this);
                    }
                };
                _MultiColumnComboBoxInitializer.prototype._onTextchanged = function () {
                    var input = this._hiddenElement, autoComplete = this.control;
                    if (!input) {
                        return;
                    }
                    if (autoComplete.selectedIndex > -1) {
                        if (autoComplete.text != autoComplete.getDisplayText()) {
                            // on typing
                            input.value = autoComplete.text;
                        }
                        else {
                            input.value = autoComplete.selectedValue;
                        }
                    }
                    else {
                        input.value = autoComplete.text;
                    }
                    if (autoComplete.isEditable) {
                        mvc.Utils.triggerValidationChangeEvent(input);
                    }
                };
                _MultiColumnComboBoxInitializer.prototype._onSelectedIndexChanged = function () {
                    var input = this._hiddenElement, autoComplete = this.control;
                    if (!input) {
                        return;
                    }
                    if (autoComplete.isEditable) {
                        if (autoComplete.selectedIndex > -1) {
                            input.value = autoComplete.selectedValue;
                        }
                        else {
                            input.value = autoComplete.text;
                        }
                    }
                    else {
                        input.value = autoComplete.selectedValue;
                    }
                    mvc.Utils.triggerValidationChangeEvent(input);
                };
                _MultiColumnComboBoxInitializer.prototype._afterInitializeControl = function (options) {
                    _super.prototype._afterInitializeControl.call(this, options);
                    // handle hidden field
                    if (this._hiddenElement) {
                        this._updateHiddenField();
                    }
                    var autoComplete = this.control;
                    if (autoComplete.isEditable) {
                        if (options.selectedValue != undefined && this._textPortToSelectedValue) {
                            if (autoComplete.selectedIndex == -1) {
                                autoComplete.text = options.selectedValue;
                            }
                        }
                        var text = autoComplete.text;
                        if (autoComplete.selectedIndex == 0 && text != autoComplete.getDisplayText()) {
                            autoComplete.selectedIndex = -1;
                            autoComplete.text = text;
                        }
                        if (this._hiddenElement && autoComplete.selectedIndex > -1) {
                            var input = this._hiddenElement;
                            input.value = autoComplete.selectedValue;
                        }
                    }
                };
                return _MultiColumnComboBoxInitializer;
            }(c1.mvc.input._ItemsInputInitializer));
            input_1._MultiColumnComboBoxInitializer = _MultiColumnComboBoxInitializer;            
        })(input = mvc.input || (mvc.input = {}));
    })(mvc = c1.mvc || (c1.mvc = {}));
})(c1 || (c1 = {}));