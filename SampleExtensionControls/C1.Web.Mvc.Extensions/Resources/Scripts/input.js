var c1;
(function (c1) {
    var input;
    (function (input_1) {
        var MultiColumnComboBox = /** @class */ (function (_super) {
            __extends(MultiColumnComboBox, _super);
            function MultiColumnComboBox(element, options) {
                var _this = _super.call(this, element, options) || this;
                _this._pageSelectedIndexes = [];
                _this._suspendSelectionCount = 0;
                _this._initialized = false;
                _this._filtering = false;
                _this._queueSelectedValues = [];
                _this._query = "";
                _this._listKeydownEvent = new wijmo.Event();
                _this._toSearch = null;
                _this._gridItemFormater = "";
                // The reason we need to store selection of each page beacause FlexGrid does NOT support saving selection across the pages.
                /**
                 * Occurs when the value of the @see:selectedIndex property changes.
                 */
                _this.selectedIndexChanged = new wijmo.Event();
                // Initialize the control
                _this.initialize(options);
                // clear the default selection of the FlexGrid
                _this._initialized = true;
                if (_this._grid.collectionView) {
                    _this._clearDefaultFlexGridSelection();
                    _this._grid.collectionView.refresh();
                }
                return _this;
            }

            Object.defineProperty(MultiColumnComboBox.prototype, "gridItemFormater", {
                get: function () {
                    return this._gridItemFormater;
                },
                set: function (value) {
                    if (value) {                        
                        this._gridItemFormater = wijmo.asFunction(value);
                        // follow logic of FlexGrid.ts
                        this._grid._clearCells(); // clear all cells of grid and draw again.
                        this._grid.formatItem.addHandler(this._gridItemFormater, this);
                    }
                },
                enumerable: true,
                configurable: true
            });

            Object.defineProperty(MultiColumnComboBox.prototype, "listKeydownEvent", {
                /**
                 * Gets the keydown event handler list.
                 */
                get: function () {
                    return this._listKeydownEvent;
                },
                enumerable: true,
                configurable: true
            });
            Object.defineProperty(MultiColumnComboBox.prototype, "query", {
                /**
           * Gets or sets the current query string.
           */
                get: function () {
                    return this._query;
                },
                set: function (value) {
                    if (value == "")
                        value = null;
                    if (this._query != value) {
                        this._query = value;
                    }
                },
                enumerable: true,
                configurable: true
            });
            Object.defineProperty(MultiColumnComboBox.prototype, "itemsSource", {
                /**
                 * Gets or sets the array or @see:ICollectionView object that contains
                 * the items to select from.
                 */
                get: function () {
                    return this._grid.itemsSource;
                },
                set: function (value) {
                    if (this._grid.itemsSource != value) {
                        this._grid.itemsSource = value;
                        this._pager.collectionView = this._grid.collectionView;
                        this._addHandlers();
                    }
                },
                enumerable: true,
                configurable: true
            });
            /**
            * Add handlers for collection view on initializing.
            */
            MultiColumnComboBox.prototype._addHandlers = function () {
                this._grid.collectionView.pageChanged.addHandler(this._pageChanged, this);
                this._grid.collectionView.pageChanging.addHandler(this._pageChanging, this);
                this._grid.collectionView.filter = this._filter.bind(this);
                if (this._grid.collectionView.queryData) {
                    this._grid.collectionView.queryData.addHandler(this._queryData, this);
                    this._grid.collectionView.beginQuery.addHandler(this._updateMaskLayer, this);
                    this._grid.collectionView.endQuery.addHandler(this._updateMaskLayer, this);
                }
            };
            /**
            * Remove handlers for collection view on destroying.
            */
            MultiColumnComboBox.prototype._removeHandlers = function () {
                this._grid.collectionView.pageChanged.removeHandler(this._pageChanged);
                this._grid.collectionView.pageChanging.removeHandler(this._pageChanging);
                this._grid.collectionView.filter = null;
                if (this._grid.collectionView.queryData) {
                    this._grid.collectionView.queryData.removeHandler(this._queryData);
                    this._grid.collectionView.beginQuery.removeHandler(this._updateMaskLayer, this);
                    this._grid.collectionView.endQuery.removeHandler(this._updateMaskLayer, this);
                }
            };
            Object.defineProperty(MultiColumnComboBox.prototype, "itemsSourceChanged", {
                /**
                 * Occurs when the value of the @see:itemsSource property changes.
                 */
                get: function () {
                    return this._grid.itemsSourceChanged;
                },
                enumerable: true,
                configurable: true
            });
            /**
             * Override this method to put the FlexGrid to the dropdown.
             */
            MultiColumnComboBox.prototype._createDropDown = function () {
                if (typeof (this._grid) == "undefined") {
                    wijmo.toggleClass(this._dropDown, "grid_drop_down", true);
                    // Create grid
                    var gridHolder = document.createElement("DIV");
                    gridHolder.id = this.hostElement.id + "_grid";
                    this._dropDown.appendChild(gridHolder);
                    this._grid = new wijmo.grid.FlexGrid(gridHolder);
                    this._grid.isReadOnly = true;
                    this._grid.selectionMode = wijmo.grid.SelectionMode.ListBox;
                    this._grid.selectionChanged.addHandler(this._gridSelectionChanged, this);
                    if (this._gridItemFormater) {
                        this._grid.formatItem.addHandler(this._gridItemFormater, this);
                    }
                    this._grid.formatItem.addHandler(this._gridFormatItem, this);
                    gridHolder.addEventListener("mousedown", this._gridMouseDown.bind(this), false);
                    gridHolder.addEventListener("keydown", this._gridKeyDown.bind(this), false);
                    gridHolder.addEventListener("keydown", this._gridPreKeyDown.bind(this), true);
                    // Create pager
                    var pagerHolder = document.createElement("DIV");
                    pagerHolder.id = this.hostElement.id + "_pager";
                    this._dropDown.appendChild(pagerHolder);
                    this._pager = new c1.input.ExtendedPager(pagerHolder);
                    this._dropDown.setAttribute("tabIndex", "1");
                }
            };
            /**
            * Update the mask layer for loading.
            */
            MultiColumnComboBox.prototype._updateMaskLayer = function () {
                if (this._grid.collectionView._isQuerying()) {
                    this._showMaskLayer();
                }
                else {
                    this._hideMaskLayer();
                }
            };
            MultiColumnComboBox.prototype._showMaskLayer = function () {
                if (!this._maskLayer) {
                    this._maskLayer = this._createMaskLayer();
                }
                var parentElement = this._grid.cells.hostElement.parentElement.parentElement, parentSize, maskStyle;
                parentSize = wijmo.getElementRect(parentElement);
                maskStyle = this._maskLayer.style;
                maskStyle.left = '0px';
                maskStyle.top = '0px';
                maskStyle.width = parentSize.width + 'px';
                maskStyle.height = parentSize.height + 'px';
                maskStyle.lineHeight = parentSize.height + 'px';
                maskStyle.display = 'inline-block';
            };
            MultiColumnComboBox.prototype._hideMaskLayer = function () {
                if (this._maskLayer) {
                    this._maskLayer.style.display = 'none';
                }
            };
            MultiColumnComboBox.prototype._createMaskLayer = function () {
                // add the mask layer to the sibling of cells' container.
                var parentElement = this._grid.cells.hostElement.parentElement.parentElement, maskLayer, parentSize, maskStyle;
                if (!parentElement) {
                    throw 'the grid container doesn\'t exist.';
                }
                parentSize = wijmo.getElementRect(parentElement);
                maskLayer = document.createElement('div');
                maskStyle = maskLayer.style;
                maskStyle.left = '0px';
                maskStyle.top = '0px';
                maskStyle.width = parentSize.width + 'px';
                maskStyle.height = parentSize.height + 'px';
                maskStyle.lineHeight = parentSize.height + 'px';
                maskStyle.display = 'none';
                maskLayer.innerText = 'loading...';
                parentElement.appendChild(maskLayer);
                wijmo.addClass(maskLayer, 'c1-grid-mask');
                return maskLayer;
            };
            Object.defineProperty(MultiColumnComboBox.prototype, "selectedIndexes", {
                /**
                * Gets or sets the index of the currently selected item in
                * the drop-down list.
                */
                get: function () {
                    var seft = this;
                    if (this._isCallbackSource) {
                        // Just support selecting on a single page because the items source is not a full list.
                        return this._pageSelectedIndexes[this._currentPageIndex] || [];
                    }
                    else {
                        return this._pageSelectedIndexes.reduce(function (acc, val, pageIndex) {
                            val.forEach(function (row) {
                                var modelIndex = seft._convertToModelIndex(pageIndex, row);
                                acc.push(modelIndex);
                            });
                            return acc;
                        }, []);
                    }
                },
                set: function (value) {
                    this._pageSelectedIndexes = [];
                    if (value) {
                        value.forEach(function (index) {
                            this._addSelectedIndex(index);
                        });
                    }
                    else {
                        this._clearDefaultFlexGridSelection();
                    }
                    this._syncText();
                },
                enumerable: true,
                configurable: true
            });
            Object.defineProperty(MultiColumnComboBox.prototype, "selectedItems", {
                /**
                 * Gets or sets the item that is currently selected in
                 * the drop-down list.
                 */
                get: function () {
                    var seft = this;
                    if (this._isCallbackSource) {
                        // Just support selecting on a single page because the items source is not a full list.            
                        var acc_1 = [];
                        this.selectedIndexes.forEach(function (row) {
                            acc_1.push(seft._items[row]);
                        });
                        return acc_1;
                    }
                    else {
                        return this._pageSelectedIndexes.reduce(function (acc, val, pageIndex) {
                            val.forEach(function (row) {
                                var modelIndex = seft._convertToModelIndex(pageIndex, row);
                                acc.push(seft._items[modelIndex]);
                            });
                            return acc;
                        }, []);
                    }
                },
                set: function (value) {
                    this._pageSelectedIndexes = [];
                    var prop = this._selectedValuePath || this.displayMemberPath;
                    if (value) {
                        value.forEach(function (obj) {
                            if (prop != null) {
                                var pv_1 = obj[prop];
                                this._items.forEach(function (sourceValue, sourceIndex) {
                                    var sourcePropertyValue = sourceValue[prop];
                                    var pageIndex = Math.floor(sourceIndex / this.pageSize);
                                    var ps = this._pageSelectedIndexes[pageIndex] || [];
                                    if (sourcePropertyValue == pv_1) {
                                        var viewIndex = this._convertToViewIndex(sourceIndex);
                                        ps.push(viewIndex);
                                        if (this._isModelRowIsDisplayed(sourceIndex)) {
                                            this._selectRow(viewIndex);
                                        }
                                    }
                                    this._pageSelectedIndexes[pageIndex] = ps;
                                });
                            }
                            else {
                                throw "Not implemented";
                            }
                        });
                    }
                    else {
                        this._clearDefaultFlexGridSelection();
                    }
                    this._syncText();
                },
                enumerable: true,
                configurable: true
            });
            Object.defineProperty(MultiColumnComboBox.prototype, "selectedValues", {
                /**
                 * Gets or sets the value of the @see:selectedItem, obtained
                 * using the @see:selectedValuePath.
                 */
                get: function () {
                    if (this._pageSelectedIndexes && this._queueSelectedValues.length) {
                        return this._queueSelectedValues.pop();
                    }
                    // Combine all selected values from all pages
                    var selectedItems = this.selectedItems;
                    var selectedValues = [];
                    var prop = this._selectedValuePath || this.displayMemberPath;
                    if (selectedItems != null) {
                        if (prop != null) {
                            for (var _i = 0, selectedItems_1 = selectedItems; _i < selectedItems_1.length; _i++) {
                                var obj = selectedItems_1[_i];
                                selectedValues.push(obj[prop]);
                            }
                        }
                        else {
                            for (var _a = 0, selectedItems_2 = selectedItems; _a < selectedItems_2.length; _a++) {
                                var obj = selectedItems_2[_a];
                                selectedValues.push(obj.toString());
                            }
                        }
                    }
                    return selectedValues;
                },
                set: function (value) {
                    if (this._isCallbackSource && this.itemsSource._isQuerying()) {
                        this._queueSelectedValues.push(value);
                        if (value) {
                            this.inputElement.value = value.join(',');
                        }
                        var queryCompletedHandler = (function (e) {
                            this.selectedValues = this._queueSelectedValues.pop(); // Just support 1 item in queue first.
                            this.itemsSource.queryComplete.removeHandler(queryCompletedHandler);
                        }).bind(this);
                        this.itemsSource.queryComplete.addHandler(queryCompletedHandler);
                        return;
                    }
                    this._pageSelectedIndexes = [];
                    var vp = this._selectedValuePath || this.displayMemberPath;
                    if (value != null || value.length != 0) {
                        if (vp) {
                            var _loop_1 = function (s) {
                                index = this_1._items.findIndex(function (obj) {
                                    return obj[vp] == s;
                                });
                                if (index >= 0) {
                                    this_1._addSelectedIndex(index);
                                }
                            };
                            var this_1 = this, index;
                            for (var _i = 0, value_1 = value; _i < value_1.length; _i++) {
                                var s = value_1[_i];
                                _loop_1(s);
                            }
                        }
                        else {
                            throw "Not implemented";
                        }
                    }
                    else {
                        this._grid.select(-1, -1);
                    }
                    this._syncText();
                },
                enumerable: true,
                configurable: true
            });
            Object.defineProperty(MultiColumnComboBox.prototype, "selectedValuePath", {
                /**
                 * Gets or sets the name of the property used to get the
                 * @see:selectedValue from the @see:selectedItem.
                 */
                get: function () {
                    return this._selectedValuePath;
                },
                set: function (value) {
                    this._selectedValuePath = value;
                },
                enumerable: true,
                configurable: true
            });
            Object.defineProperty(MultiColumnComboBox.prototype, "displayMemberPath", {
                /**
                 * Gets or sets the name of the property to use as the visual
                 * representation of the items.
                 */
                get: function () {
                    return this._displayMemberPath;
                },
                set: function (value) {
                    this._displayMemberPath = value;
                    var text = this.getDisplayText();
                    if (this.text != text) {
                        this._setText(text, true);
                    }
                },
                enumerable: true,
                configurable: true
            });
            /**
             * Gets the displayed text for selected item(s).
             */
            MultiColumnComboBox.prototype.getDisplayText = function () {
                var selectedItems = this.selectedItems;
                var displayText = "";
                if (selectedItems != null) {
                    if (this.displayMemberPath != null) {
                        for (var _i = 0, selectedItems_3 = selectedItems; _i < selectedItems_3.length; _i++) {
                            var obj = selectedItems_3[_i];
                            displayText += obj[this.displayMemberPath] + ", ";
                        }
                        displayText = displayText.substr(0, displayText.length - 2);
                    }
                    else {
                        for (var _a = 0, selectedItems_4 = selectedItems; _a < selectedItems_4.length; _a++) {
                            var obj = selectedItems_4[_a];
                            displayText += obj.toString() + ", ";
                        }
                        displayText = displayText.substr(0, displayText.length - 2);
                    }
                }
                return displayText;
            };
            /**
             * Override text changed event handler to do filtering task.
             * @param {*} args
             */
            MultiColumnComboBox.prototype.onTextChanged = function (args) {
                _super.prototype.onTextChanged.call(this, args);
                if (this._initialized && this._grid.collectionView) {
                    this._query = this.text;
                    if (this._toSearch != null) {
                        clearTimeout(this._toSearch);
                    }
                    var seft = this;
                    this._toSearch = setTimeout(function () {
                        seft._filtering = true;
                        seft._applyFilter();
                        if (!seft.isDroppedDown)
                            seft.isDroppedDown = true;
                        // select the first items
                        if (seft._grid.collectionView.items.length > 0) {
                            seft._grid.select(0, 0);
                        }
                        seft._filtering = false;
                    }, 50);
                }
            };
            Object.defineProperty(MultiColumnComboBox.prototype, "_isCallbackSource", {
                get: function () {
                    return c1 != null && c1.mvc != null && c1.mvc.collections != null && this._isTypeOf(this.itemsSource, c1.mvc.collections.CallbackCollectionView);
                },
                enumerable: true,
                configurable: true
            });
            MultiColumnComboBox.prototype._isTypeOf = function (obj, type) {
                return obj != null && obj.__proto__.constructor == type;
            };
            Object.defineProperty(MultiColumnComboBox.prototype, "_items", {
                get: function () {
                    return this.itemsSource.items || this.itemsSource;
                },
                enumerable: true,
                configurable: true
            });
            MultiColumnComboBox.prototype._gridMouseDown = function (e) {
                var hi = this._grid.hitTest(e.clientX, e.clientY);
                if (hi != null && hi.cellType == wijmo.grid.CellType.Cell) {
                    if (!e.ctrlKey) {
                        var seft = this;
                        setTimeout(function () {
                            var pi = seft._currentPageIndex;
                            var cp = seft._pageSelectedIndexes[pi];
                            // Clear other page selection
                            seft._pageSelectedIndexes = [];
                            seft._pageSelectedIndexes[pi] = cp;
                            seft._syncText();
                            seft.isDroppedDown = false;
                        }, 0);
                    }
                }
            };
            /**
             * To override default of behavior to navigate among the pages.
             * @param {*} e
             */
            MultiColumnComboBox.prototype._gridKeyDown = function (e) {
                switch (e.keyCode) {
                    case wijmo.Key.PageDown:
                        this._pager._movePage("next");
                        break;
                    case wijmo.Key.PageUp:
                        this._pager._movePage("prev");
                    case wijmo.Key.Up:
                    case wijmo.Key.Down:
                        if (!e.ctrlKey) {
                            var ps = this._pageSelectedIndexes[this._currentPageIndex];
                            this._pageSelectedIndexes = [];
                            this._pageSelectedIndexes[this._currentPageIndex] = ps;
                            this._selectRow(ps);
                            this._updateGridActiveCell();
                            this._syncText();
                        }
                        break;
                    case wijmo.Key.Back:
                        var input = this.inputElement;
                        var text = input.value;
                        e.preventDefault();
                        input.focus();
                        wijmo.setSelectionRange(input, text.length, text.length);
                        break;
                }
            };
            /**
             * The keydown event at the capture phase to override the default FlexGrid behaviors.
             */
            MultiColumnComboBox.prototype._gridPreKeyDown = function (e) {
                switch (e.keyCode) {
                    case wijmo.Key.Enter:
                        this.isDroppedDown = false;
                        e.preventDefault();
                        break;
                    case wijmo.Key.Down:
                        var selection = this._grid.selection;
                        if (selection) {
                            var row = selection.row;
                            if (row == this._grid.rows.length - 1) {
                                e.preventDefault();
                                this._pager._movePage("next");
                            }
                        }
                        break;
                    case wijmo.Key.Up:
                        var selection = this._grid.selection;
                        if (selection) {
                            var row = selection.row;
                            if (row == 0) {
                                e.preventDefault();
                                this._pager._movePage("prev");
                            }
                        }
                        break;
                }
                this._listKeydownEvent.raise(this, e);
            };
            /**
             * Override to apply some key behavior.
             * @param {*} e
             */
            MultiColumnComboBox.prototype._keydown = function (e) {
                _super.prototype._keydown.call(this, e);
                switch (e.keyCode) {
                    case wijmo.Key.Enter:
                        this._syncText();
                        break;
                    case wijmo.Key.Up:
                    case wijmo.Key.Down:
                        if (!this.isDroppedDown)
                            this.isDroppedDown = true;
                        if (this._grid.selectedRows.length == 0) {
                            var row = e.keyCode == wijmo.Key.Down ? 0 : this._grid.rows.length - 1;
                            this._grid.select(row, 0);
                        }
                        this._updateGridActiveCell();
                        break;
                }
            };
            /**
             * Handle selection changed event to change selected text and process selected data logic.
             * @param {FlexGrid} sender
             * @param {*} args
             */
            MultiColumnComboBox.prototype._gridSelectionChanged = function (sender, args) {
                if (this._suspendSelectionCount == 0) {
                    var currentPage = this._currentPageIndex;
                    var tr = this._topRow;
                    var ps_1 = [];
                    this._grid.selectedRows.forEach(function (element) {
                        ps_1.push(element.index);
                    });
                    this._pageSelectedIndexes[currentPage] = ps_1;
                    if (!this._filtering) {
                        // Get selected display text to display in the textbox
                        this._syncText();
                    }
                }
            };
            MultiColumnComboBox.prototype._gridFormatItem = function (s, e) {
                if (this._query == null)
                    return;
                if (e.panel.cellType === wijmo.grid.CellType.Cell && e.panel.columns[e.col].dataType != wijmo.DataType.Boolean) {
                    var rxHighlight = new RegExp('(' + this._query.replace(/ /g, '|') + ')', 'ig');
                    if (this._query === "") {
                        var highlight = '<span>$1</span>';
                        e.cell.innerHTML = e.cell.innerHTML.replace(rxHighlight, highlight);
                    }
                    else {
                        var highlight = '<span class="wj-autocomplete-match">$1</span>';
                        e.cell.innerHTML = e.cell.innerHTML.replace(rxHighlight, highlight);
                    }
                }
            };
            Object.defineProperty(MultiColumnComboBox.prototype, "_topRow", {
                get: function () {
                    return this._isCallbackSource ? 0 : (this._grid.collectionView.pageIndex * this._grid.collectionView.pageSize);
                },
                enumerable: true,
                configurable: true
            });
            Object.defineProperty(MultiColumnComboBox.prototype, "_bottomRow", {
                get: function () {
                    return this._isCallbackSource ? (this._items.length - 1) : (this._topRow + this.pageSize - 1);
                },
                enumerable: true,
                configurable: true
            });
            Object.defineProperty(MultiColumnComboBox.prototype, "pageSize", {
                /*
                 * Gets or sets the page size.
                 */
                get: function () {
                    return this._grid.collectionView.pageSize;
                },
                set: function (value) {
                    this._grid.collectionView.pageSize = value;
                },
                enumerable: true,
                configurable: true
            });
            MultiColumnComboBox.prototype._syncText = function () {
                var displayText = this.getDisplayText();
                this.inputElement.value = displayText;
            };
            Object.defineProperty(MultiColumnComboBox.prototype, "_currentPageIndex", {
                get: function () {
                    return this._grid.collectionView.pageIndex;
                },
                enumerable: true,
                configurable: true
            });
            MultiColumnComboBox.prototype._pageChanged = function (args) {
                this._clearDefaultFlexGridSelection();
                this._applySelection();
                this._suspendSelectionCount--;
            };
            MultiColumnComboBox.prototype._pageChanging = function (args) {
                this._suspendSelectionCount++;
            };
            MultiColumnComboBox.prototype._selectRow = function (viewIndex) {
                var viewIndexes = typeof (viewIndex) == "number" ? [viewIndex] : (viewIndex || []);
                var seft = this;
                var first = true;
                viewIndexes.forEach(function (rowIndex) {
                    if (first) {
                        seft._grid.select(rowIndex, 0);
                        first = false;
                    }
                    else {
                        seft._grid.rows[rowIndex].isSelected = true;
                    }
                });
            };
            /**
             * Clear the default FlexGrid selection that we don't need.
             */
            MultiColumnComboBox.prototype._clearDefaultFlexGridSelection = function () {
                this._grid.select(-1, -1);
            };
            /**
             * Apply the selection with data from the page selection cache.
             */
            MultiColumnComboBox.prototype._applySelection = function () {
                var ps = this._pageSelectedIndexes[this._currentPageIndex];
                if (ps != null) {
                    this._selectRow(ps);
                }
                this._updateGridActiveCell();
            };
            MultiColumnComboBox.prototype._updateGridActiveCell = function () {
                var activeCell = this._grid.hostElement.querySelectorAll(".wj-cell.wj-state-selected");
                if (activeCell.length > 0) {
                    activeCell[0].focus();
                }
            };
            MultiColumnComboBox.prototype._addSelectedIndex = function (modelIndex) {
                var pi = Math.floor(modelIndex / this.pageSize);
                var ps = this._pageSelectedIndexes[pi] || [];
                var viewIndex = this._isCallbackSource ? modelIndex : this._convertToViewIndex(modelIndex);
                ps.push(viewIndex);
                this._pageSelectedIndexes[pi] = ps;
                if (this._isCallbackSource || this._isModelRowIsDisplayed(modelIndex)) {
                    this._selectRow(ps);
                }
            };
            MultiColumnComboBox.prototype._convertToViewIndex = function (modelIndex) {
                return modelIndex % this.pageSize;
            };
            MultiColumnComboBox.prototype._convertToModelIndex = function (pageIndex, viewIndex) {
                return pageIndex * this.pageSize + viewIndex;
            };
            MultiColumnComboBox.prototype._isModelRowIsDisplayed = function (modelIndex) {
                return modelIndex >= this._topRow && modelIndex <= this._bottomRow;
            };
            /**
             * Apply filtering to the internal FlexGrid collection view.
             */
            MultiColumnComboBox.prototype._applyFilter = function () {
                this._grid.collectionView.refresh();
            };
            /**
             * Do filtering on each list items.
             * @param {*} value
             * @param {*} index
             * @param {*} arr
             */
            MultiColumnComboBox.prototype._filter = function (value, index, arr) {
                if (!this._initialized && this._grid.collectionView)
                    return true;
                var propValue = value[this.displayMemberPath];
                var filteredText = this._query;
                if (filteredText != null && filteredText.length > 0 && value != null && propValue != null) {
                    // Simple search implementation.
                    return (propValue + "").toLowerCase().indexOf(filteredText.toLowerCase()) != -1;
                }
                else {
                    return true;
                }
            };
            MultiColumnComboBox.prototype._queryData = function (s, e) {
                if (this._query == null) {
                    if (s.pageIndex != 0) {
                        s.moveToFirstPage();
                    }
                    this._query = "";
                    return;
                }
                //if (this._query != "") {
                    if (e.extraRequestData == null) {
                        e.extraRequestData = {};
                    }
                    e.extraRequestData["AutoCompleteQuery"] = this._query;
                //}
            };
            return MultiColumnComboBox;
        }(wijmo.input.DropDown));
        input_1.MultiColumnComboBox = MultiColumnComboBox;
        var ExtendedPager = /** @class */ (function (_super) {
            __extends(ExtendedPager, _super);
            /**
             * Initializes a new instance of a @see:c1.input.Pager.
             *
             * @param element The DOM element that hosts the control, or a selector for the host element (e.g. '#theCtrl').
             * @param options The JavaScript object containing initialization data for the control.
             */
            function ExtendedPager(element, options) {
                var _this = _super.call(this, element) || this;
                _this._initElements();
                _this.initialize(options);
                return _this;
            }
            Object.defineProperty(ExtendedPager.prototype, "collectionView", {
                /**
                 * Gets or sets the @see:wijmo.collections.CollectionView that own this pager.
                 */
                get: function () {
                    return this._collectionView;
                },
                set: function (value) {
                    var cv = wijmo.tryCast(value, wijmo.collections.CollectionView), self = this;
                    if (cv && cv !== self._collectionView) {
                        if (self._collectionView) {
                            self._collectionView.collectionChanged.removeHandler(self._updatePager, self);
                        }
                        self._collectionView = cv;
                        self._collectionView.collectionChanged.addHandler(self._updatePager, self);
                        self._updatePager();
                    }
                },
                enumerable: true,
                configurable: true
            });
            /**
             * Refreshes the pager display.
             *
             */
            ExtendedPager.prototype.refresh = function () {
                _super.prototype.refresh.call(this, true);
                this._updateContent();
            };
            ExtendedPager.prototype._initElements = function () {
                var self = this, tpl = self.getTemplate(), btns = [];
                self.applyTemplate('wj-control wj-content wj-pager', tpl, {
                    _btnFirst: "btn-first",
                    _btnPrev: "btn-prev",
                    _btnNext: "btn-next",
                    _btnLast: "btn-last",
                    _inputPages: "pages",
                    _pagerHolder: "pager_holder"
                });
                btns.push(self._btnFirst, self._btnPrev, self._btnNext, self._btnLast);
                btns.forEach(function (item) {
                    return item.addEventListener('click', function () {
                        self._movePage(item.getAttribute('data-action'));
                    });
                });
            };
            ExtendedPager.prototype._movePage = function (type) {
                var self = this, cv = self.collectionView;
                if (!cv) {
                    return;
                }
                switch (type) {
                    case "first":
                        cv.moveToFirstPage();
                        break;
                    case "prev":
                        cv.moveToPreviousPage();
                        break;
                    case "next":
                        cv.moveToNextPage();
                        break;
                    case "last":
                        cv.moveToLastPage();
                        break;
                }
            };
            ExtendedPager.prototype._updatePager = function () {
                this.invalidate();
            };
            ExtendedPager.prototype._updateContent = function () {
                var self = this, cv = self.collectionView, enableBackwards, enableForwards;
                if (!cv) {
                    return;
                }
                // update the pager text
                self._inputPages.value = (cv.pageIndex + 1) + ' / ' + (cv.pageCount);
                // determine which pager buttons to enable/disable
                enableBackwards = cv.pageIndex <= 0;
                enableForwards = cv.pageIndex >= cv.pageCount - 1;
                // enable/disable pager buttons
                var ae = document.activeElement;
                if (((ae == self._btnFirst || ae == self._btnPrev) && enableBackwards) || ((ae == self._btnNext || ae == self._btnLast) && enableForwards)) {
                    self._pagerHolder.setAttribute("tabIndex", "1000");
                    self._pagerHolder.focus();
                }
                self._btnFirst.disabled = enableBackwards;
                self._btnPrev.disabled = enableBackwards;
                self._btnNext.disabled = enableForwards;
                self._btnLast.disabled = enableForwards;
            };
            ExtendedPager.controlTemplate = '<div class="wj-input-group" wj-part="pager_holder" style="outline:none;">' +
                '        <span class="wj-input-group-btn" >' +
                '            <button wj-part="btn-first" data-action="first" class="wj-btn wj-btn-default" type="button">' +
                '                <span class="wj-glyph-left" style="margin-right: -4px;"></span>' +
                '                <span class="wj-glyph-left"></span>' +
                '            </button>' +
                '        </span>' +
                '        <span class="wj-input-group-btn" >' +
                '            <button wj-part="btn-prev" data-action="prev" class="wj-btn wj-btn-default" type="button">' +
                '                <span class="wj-glyph-left"></span>' +
                '            </button>' +
                '        </span>' +
                '        <input wj-part="pages" type="text" class="wj-form-control" disabled />' +
                '        <span class="wj-input-group-btn" >' +
                '            <button wj-part="btn-next" data-action="next" class="wj-btn wj-btn-default" type="button">' +
                '                <span class="wj-glyph-right"></span>' +
                '            </button>' +
                '        </span>' +
                '        <span class="wj-input-group-btn" >' +
                '            <button wj-part="btn-last" data-action="last" class="wj-btn wj-btn-default" type="button">' +
                '                <span class="wj-glyph-right"></span>' +
                '                <span class="wj-glyph-right" style="margin-left: -4px;"></span>' +
                '            </button>' +
                '        </span>' +
                '    </div>';
            return ExtendedPager;
        }(wijmo.Control));
        input_1.ExtendedPager = ExtendedPager;
    })(input = c1.input || (c1.input = {}));
})(c1 || (c1 = {}));