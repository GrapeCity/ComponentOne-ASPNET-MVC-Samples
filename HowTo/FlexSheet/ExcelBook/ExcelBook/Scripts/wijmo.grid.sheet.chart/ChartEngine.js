var wijmo;
(function (wijmo) {
    var grid;
    (function (grid) {
        var sheet;
        (function (sheet) {
            var chart;
            (function (chart_1) {
                'use strict';
                /*
                 * Resize mode for the chart.
                 */
                var ResizeMode;
                (function (ResizeMode) {
                    /* No resizing */
                    ResizeMode[ResizeMode["none"] = 0] = "none";
                    /* Resizing towards left */
                    ResizeMode[ResizeMode["left"] = 2] = "left";
                    /* Resizing towards top */
                    ResizeMode[ResizeMode["top"] = 4] = "top";
                    /* Resizing towards right */
                    ResizeMode[ResizeMode["right"] = 8] = "right";
                    /* Resizing towards bottom */
                    ResizeMode[ResizeMode["bottom"] = 16] = "bottom";
                    /* Resizing towards left and top */
                    ResizeMode[ResizeMode["leftTop"] = 6] = "leftTop";
                    /* Resizing towards left and bottom */
                    ResizeMode[ResizeMode["leftBottom"] = 18] = "leftBottom";
                    /* Resizing towards right and top */
                    ResizeMode[ResizeMode["rightTop"] = 12] = "rightTop";
                    /* Resizing towards right and bottom */
                    ResizeMode[ResizeMode["rightBottom"] = 24] = "rightBottom";
                })(ResizeMode || (ResizeMode = {}));
                /**
                 * Defines the @see:ChartEngine class.
                 *
                 * The @see:ChartEngine class processes the @see:FlexChart for the @see:FlexSheet control.
                 */
                var ChartEngine = /** @class */ (function () {
                    /**
                     * Initializes a new instance of the @see:ChartEngine class.
                     *
                     * @param owner The @see: FlexSheet control that the <b>ChartEngine</b> works for.
                     */
                    function ChartEngine(owner) {
                        this._charts = [];
                        this._chartMousePosition = new wijmo.Point();
                        this._chartOriginalBounds = new wijmo.Rect(0, 0, 0, 0);
                        this._containerOriginalBounds = new wijmo.Rect(0, 0, 0, 0);
                        this._movingChart = false;
                        this._resizingChart = false;
                        this._lastScrollPosition = new wijmo.Point();
                        this._undoing = false;
                        this._owner = owner;
                        this._init();
                    }
                    ;
                    /**
                     *  Add chart into FlexSheet
                     *
                     * @param chartType the type of the chart.
                     * @param data the data used to render the chart.
                     * @param chartRect the bounds rectangle to locate the chart.
                     */
                    ChartEngine.prototype.addChart = function (chartType, data, chartRect) {
                        if (chartType == null) {
                            chartType = wijmo.chart.ChartType.Column;
                        }
                        if (chartRect == null) {
                            chartRect = this._getChartRect();
                        }
                        return this._appendChart(chartType, data, chartRect);
                    };
                    /**
                     * Remove the specified chart in the FlexSheet.
                     * If the chart is not specified, it will remove the selected chart in FlexSheet.
                     *
                     * @param chart The specified chart to remove.
                     */
                    ChartEngine.prototype.removeChart = function (chart) {
                        var delChart = chart ? chart : this._selectedChart, chartObj, i, undoAction;
                        if (delChart && this._charts.length > 0) {
                            for (i = 0; i < this._charts.length; i++) {
                                chartObj = this._charts[i];
                                if (delChart === chartObj.chart) {
                                    if (!this._undoing) {
                                        undoAction = new chart_1._ChartRemoveAction(this, chartObj);
                                        this._owner.undoStack._addAction(undoAction);
                                    }
                                    this._charts.splice(i, 1);
                                    this._chartHost.removeChild(delChart.hostElement.parentElement);
                                    delChart._hideToolTip();
                                    delChart.dispose();
                                    delChart = null;
                                    break;
                                }
                            }
                        }
                        this._owner.hostElement.focus();
                    };
                    // Initialize the chart engine.
                    ChartEngine.prototype._init = function () {
                        var self = this;
                        self._currentSheetIndex = self._owner.selectedSheetIndex;
                        self._chartHost = document.createElement('div');
                        wijmo.setCss(self._chartHost, {
                            position: 'relative',
                            zIndex: 100,
                            top: '0px',
                            left: '0px',
                            height: '0px'
                        });
                        self._owner.hostElement.insertBefore(self._chartHost, self._owner.hostElement.firstChild);
                        self._owner.selectedSheetChanged.addHandler(function () {
                            var chartObj;
                            // Show/hide chart for current sheet
                            for (var i = 0; i < self._charts.length; i++) {
                                chartObj = self._charts[i];
                                if (chartObj.sheetIndex === self._owner.selectedSheetIndex) {
                                    chartObj.chart.hostElement.parentElement.style.display = 'inline';
                                    chartObj.chart.refresh();
                                }
                                else {
                                    chartObj.chart.hostElement.parentElement.style.display = 'none';
                                }
                            }
                            self._lastScrollPosition = self._owner.selectedSheet._scrollPosition;
                            self._currentSheetIndex = self._owner.selectedSheetIndex;
                        });
                        self._owner.cellEditEnded.addHandler(function (sender, args) {
                            var chartObj, cellRange;
                            if (args.data && (args.data.keyCode === 46 || args.data.keyCode === 8)) {
                                return;
                            }
                            // Updated the charts the cell ranges contains the editted cell.
                            for (var i = 0; i < self._charts.length; i++) {
                                chartObj = self._charts[i];
                                if (chartObj.sheetIndex === self._owner.selectedSheetIndex) {
                                    for (var j = 0; j < chartObj.cellRanges.length; j++) {
                                        cellRange = chartObj.cellRanges[j];
                                        if (cellRange.contains(args.range)) {
                                            self._updateChart(chartObj);
                                            break;
                                        }
                                    }
                                }
                            }
                        });
                        // Adjust the position of the charts in current sheet after scrolling current sheet.
                        self._owner.scrollPositionChanged.addHandler(function () {
                            var deltaScrollX = self._owner.scrollPosition.x - self._lastScrollPosition.x, deltaScrollY = self._owner.scrollPosition.y - self._lastScrollPosition.y, chartObj, i;
                            if (self._currentSheetIndex !== self._owner.selectedSheetIndex) {
                                return;
                            }
                            for (i = 0; i < self._charts.length; i++) {
                                chartObj = self._charts[i];
                                if (chartObj.sheetIndex === self._owner.selectedSheetIndex) {
                                    self._scrollChart(chartObj.chart, deltaScrollX, deltaScrollY);
                                }
                            }
                            self._lastScrollPosition = self._owner.scrollPosition;
                        });
                        // Clear all the charts when the FlexSheet was cleared.
                        self._owner.sheetCleared.addHandler(function () {
                            var chartObj, delChart, i;
                            for (i = 0; i < self._charts.length; i++) {
                                chartObj = self._charts[i];
                                delChart = chartObj.chart;
                                self._chartHost.removeChild(delChart.hostElement.parentElement);
                                delChart._hideToolTip();
                                delChart.dispose();
                                delChart = null;
                            }
                            self._charts.splice(0, self._charts.length);
                        });
                        self._owner.updatedView.addHandler(function () {
                            if (self._toRefresh) {
                                clearTimeout(self._toRefresh);
                                self._toRefresh = null;
                            }
                            self._toRefresh = setTimeout(function () {
                                var chartObj;
                                // Updated the charts after refreshing current sheet.
                                for (var i = 0; i < self._charts.length; i++) {
                                    chartObj = self._charts[i];
                                    if (chartObj.sheetIndex === self._owner.selectedSheetIndex) {
                                        self._updateChart(chartObj);
                                    }
                                }
                            }, 100);
                        });
                        self._owner.prepareChangingRow.addHandler(function () {
                            self._undoAction = new chart_1._ChartRowsChangedAction(self);
                        });
                        self._owner.rowChanged.addHandler(function (sender, args) {
                            var chartObj, currentAction;
                            for (var i = 0; i < self._charts.length; i++) {
                                chartObj = self._charts[i];
                                if (chartObj.sheetIndex === self._owner.selectedSheetIndex) {
                                    self._updateChartRange(chartObj, args.index, args.count, args.added, true);
                                }
                            }
                            currentAction = self._owner.undoStack._pop();
                            if (currentAction instanceof sheet._RowsChangedAction) {
                                self._undoAction._affectedFormulas = currentAction._affectedFormulas;
                                self._undoAction._affectedDefinedNameVals = currentAction._affectedDefinedNameVals;
                                self._undoAction._deletedTables = currentAction._deletedTables;
                                self._undoAction.saveNewState();
                                self._owner.undoStack._addAction(self._undoAction);
                                self._undoAction = null;
                            }
                            else {
                                self._owner.undoStack._addAction(currentAction);
                            }
                        });
                        self._owner.prepareChangingColumn.addHandler(function () {
                            self._undoAction = new chart_1._ChartColumnsChangedAction(self);
                        });
                        self._owner.columnChanged.addHandler(function (sender, args) {
                            var chartObj, currentAction;
                            for (var i = 0; i < self._charts.length; i++) {
                                chartObj = self._charts[i];
                                if (chartObj.sheetIndex === self._owner.selectedSheetIndex) {
                                    self._updateChartRange(chartObj, args.index, args.count, args.added, false);
                                }
                            }
                            currentAction = self._owner.undoStack._pop();
                            if (currentAction instanceof sheet._ColumnsChangedAction) {
                                self._undoAction._affectedFormulas = currentAction._affectedFormulas;
                                self._undoAction._affectedDefinedNameVals = currentAction._affectedDefinedNameVals;
                                self._undoAction._deletedTables = currentAction._deletedTables;
                                self._undoAction.saveNewState();
                                self._owner.undoStack._addAction(self._undoAction);
                                self._undoAction = null;
                            }
                            else {
                                self._owner.undoStack._addAction(currentAction);
                            }
                        });
                        document.addEventListener('mousedown', function () {
                            if (self._selectedChart && !self._movingChart && !self._resizingChart) {
                                self._selectedChart.hostElement.style.borderStyle = 'none';
                                self._selectedChart = null;
                            }
                        });
                        document.addEventListener('keydown', function (e) {
                            if (self._selectedChart && e.keyCode === 46) {
                                self.removeChart();
                                self._selectedChart = null;
                                e.preventDefault();
                                e.stopPropagation();
                            }
                        }, true);
                    };
                    // Generate the data for the adding chart
                    ChartEngine.prototype._generateChartData = function (chartType, cellRanges) {
                        var selections = (cellRanges ? cellRanges : this._owner.selectedSheet.selectionRanges), selection, datas, index, startRowIndex, item, seriesBinding, seriesIndex, lastSeriesIndex, needSeriesBinding, xIndex, mergedCells, cellVal;
                        if (this._owner.rows.length === 0 || this._owner.columns.length === 0) {
                            return null;
                        }
                        datas = [];
                        seriesIndex = 0;
                        selections = selections.length > 0 ? selections : [this._owner.selection];
                        needSeriesBinding = this._checkSeriesBinding(selections);
                        xIndex = this._checkBindingX(needSeriesBinding, selections, chartType);
                        for (var i = 0; i < selections.length; i++) {
                            selection = selections[i];
                            if (!selection.isValid) {
                                continue;
                            }
                            startRowIndex = selection.topRow + (needSeriesBinding ? 1 : 0);
                            index = 0;
                            lastSeriesIndex = seriesIndex;
                            for (var rowIndex = startRowIndex; rowIndex <= selection.bottomRow; rowIndex++) {
                                if (this._owner.rows[rowIndex].visible) {
                                    seriesIndex = lastSeriesIndex;
                                    if (!datas[index]) {
                                        item = {};
                                        datas[index] = item;
                                    }
                                    else {
                                        item = datas[index];
                                    }
                                    for (var colIndex = selection.leftCol; colIndex <= selection.rightCol; colIndex++) {
                                        if (this._owner.columns[colIndex].visible) {
                                            mergedCells = this._owner.getMergedRange(this._owner.cells, rowIndex, colIndex);
                                            if (colIndex === xIndex) {
                                                if (mergedCells == null || (rowIndex === mergedCells.topRow && colIndex === mergedCells.leftCol)) {
                                                    if (chartType === wijmo.chart.ChartType.Scatter) {
                                                        if (!wijmo.isNumber(this._owner.getCellValue(rowIndex, colIndex))) {
                                                            item.x = index + 1;
                                                        }
                                                        else {
                                                            item.x = this._owner.getCellValue(rowIndex, colIndex, true);
                                                        }
                                                    }
                                                    else {
                                                        item.x = this._owner.getCellValue(rowIndex, colIndex, true);
                                                    }
                                                }
                                                else {
                                                    if (chartType === wijmo.chart.ChartType.Scatter) {
                                                        item = null;
                                                    }
                                                    else {
                                                        item.x = '';
                                                    }
                                                }
                                            }
                                            else if (item) {
                                                if (mergedCells == null || (rowIndex === mergedCells.topRow && colIndex === mergedCells.leftCol)) {
                                                    cellVal = this._owner.getCellValue(rowIndex, colIndex);
                                                }
                                                else {
                                                    if (chartType === wijmo.chart.ChartType.Scatter) {
                                                        cellVal = null;
                                                    }
                                                    else {
                                                        cellVal = 0;
                                                    }
                                                }
                                                if (cellVal != null && !wijmo.isNumber(cellVal)) {
                                                    cellVal = 0;
                                                }
                                                if (needSeriesBinding) {
                                                    seriesBinding = this._owner.getCellValue(startRowIndex - 1, colIndex);
                                                    item[seriesBinding] = cellVal;
                                                }
                                                else {
                                                    seriesIndex++;
                                                    item['series' + seriesIndex] = cellVal;
                                                }
                                            }
                                        }
                                    }
                                    if (item && !item.x && xIndex !== -1) {
                                        item.x = '';
                                    }
                                    index++;
                                }
                            }
                        }
                        return datas;
                    };
                    // Gets the rectangle for the adding chart.
                    ChartEngine.prototype._getChartRect = function () {
                        var root = this._owner['_root'], cells = this._owner['_eCt'], rect = new wijmo.Rect(0, 0, 468, 284), elementWidth, elementHeight;
                        elementWidth = root.clientWidth > cells.clientWidth ? cells.clientWidth : root.clientWidth;
                        elementHeight = root.clientHeight > cells.clientHeight ? cells.clientHeight : root.clientHeight;
                        rect.left = cells.offsetLeft + (elementWidth - rect.width) / 2;
                        rect.top = cells.offsetTop + (elementHeight - rect.height) / 2;
                        return rect;
                    };
                    // Appends the chart into FlexSheet.
                    ChartEngine.prototype._appendChart = function (chartType, data, chartRect) {
                        var self = this, chartContainer = document.createElement('div'), hostElement = document.createElement('div'), mouseUp = function () {
                            document.removeEventListener('mousemove', mouseMove);
                            document.removeEventListener('mouseup', mouseUp);
                            self._mouseUp();
                        }, mouseMove = function (e) {
                            self._mouseMove(e);
                        }, cellRanges, range, chart, undoAction, chartObj;
                        if (data == null) {
                            data = self._generateChartData(chartType);
                            if (!data || data.length === 0) {
                                return;
                            }
                            cellRanges = [];
                            if (self._owner.selectedSheet.selectionRanges.length > 0) {
                                for (var i = 0; i < self._owner.selectedSheet.selectionRanges.length; i++) {
                                    range = self._owner.selectedSheet.selectionRanges[i];
                                    cellRanges.push(range.clone());
                                }
                            }
                            else {
                                cellRanges.push(self._owner.selection.clone());
                            }
                        }
                        wijmo.setCss(hostElement, {
                            position: 'absolute',
                            top: '0px',
                            left: '0px',
                            height: chartRect.height + 'px',
                            width: chartRect.width + 'px',
                            borderWidth: '2px',
                            borderStyle: 'dashed'
                        });
                        hostElement.focus();
                        chartContainer.appendChild(hostElement);
                        wijmo.setCss(chartContainer, {
                            position: 'absolute',
                            display: 'inline',
                            overflow: 'hidden',
                            zIndex: '1',
                            top: chartRect.top + 'px',
                            left: chartRect.left + 'px',
                            height: chartRect.height + 'px',
                            width: chartRect.width + 'px'
                        });
                        self._chartHost.appendChild(chartContainer);
                        chart = new wijmo.chart.FlexChart(hostElement);
                        chart.chartType = chartType;
                        chart.itemsSource = data;
                        hostElement.addEventListener('mousedown', function (e) {
                            var i, chartObj;
                            document.addEventListener('mousemove', mouseMove);
                            document.addEventListener('mouseup', mouseUp);
                            self._mouseDown(e, hostElement);
                            self._selectedChart = chart;
                            for (i = 0; i < self._charts.length; i++) {
                                chartObj = self._charts[i];
                                if (chartObj.chart !== self._selectedChart) {
                                    chartObj.chart.hostElement.style.borderStyle = 'none';
                                }
                            }
                            e.preventDefault();
                        }, true);
                        hostElement.addEventListener('click', function (e) {
                            e.stopPropagation();
                        });
                        hostElement.addEventListener('contextmenu', function (e) {
                            e.preventDefault();
                        });
                        hostElement.addEventListener('mousemove', function (e) {
                            if (self._resizingChart || self._movingChart) {
                                return;
                            }
                            self._hoverChart(e, chart);
                        });
                        if (data && data.length > 0) {
                            self._setChartSeries(chart, data[0]);
                        }
                        self._selectedChart = chart;
                        if (!self._undoing) {
                            undoAction = new chart_1._ChartInsertAction(self, chartType, data, chartRect, cellRanges);
                            self._owner.undoStack._addAction(undoAction);
                        }
                        chartObj = {
                            sheetIndex: self._owner.selectedSheetIndex,
                            cellRanges: cellRanges,
                            chart: chart
                        };
                        self._charts.push(chartObj);
                        self._owner.hostElement.focus();
                        return chartObj;
                    };
                    // Check whether the chart data contains series binding.
                    ChartEngine.prototype._checkSeriesBinding = function (selections) {
                        var selection, startRowIndex, cellData, needSeriesBinding = true;
                        for (var i = 0; i < selections.length; i++) {
                            selection = selections[i];
                            if (selection.isValid) {
                                startRowIndex = selection.topRow;
                                for (var colIndex = selection.leftCol; colIndex <= selection.rightCol; colIndex++) {
                                    cellData = this._owner.getCellValue(startRowIndex, colIndex);
                                    if (!wijmo.isString(cellData) || !cellData) {
                                        needSeriesBinding = false;
                                        break;
                                    }
                                }
                            }
                            else {
                                needSeriesBinding = false;
                            }
                        }
                        return needSeriesBinding;
                    };
                    // Check whether the chart data contains binding x.
                    ChartEngine.prototype._checkBindingX = function (needSeriesBinding, selections, chartType) {
                        var xIndex = -1, selection, colIndex, startRowIndex, cellData;
                        selectionLoop: for (var i = 0; i < selections.length; i++) {
                            selection = selections[i];
                            if (selection.isValid) {
                                colIndex = selection.leftCol;
                                if (this._owner.columns[colIndex].dataMap != null) {
                                    if (xIndex === -1 || xIndex < colIndex) {
                                        xIndex = colIndex;
                                    }
                                }
                                else {
                                    startRowIndex = selection.topRow + (needSeriesBinding ? 1 : 0);
                                    for (var rowIndex = startRowIndex; rowIndex <= selection.bottomRow; rowIndex++) {
                                        cellData = this._owner.getCellValue(rowIndex, colIndex);
                                        if (((typeof cellData === 'string' && !!cellData) || cellData instanceof Date) && (xIndex === -1 || xIndex < colIndex)) {
                                            xIndex = colIndex;
                                            continue selectionLoop;
                                        }
                                    }
                                }
                            }
                        }
                        if (chartType === wijmo.chart.ChartType.Scatter && xIndex === -1) {
                            for (var j = 0; j < selections.length; j++) {
                                selection = selections[j];
                                if (selection.isValid) {
                                    if (xIndex === -1 || xIndex > selection.leftCol) {
                                        xIndex = selection.leftCol;
                                    }
                                }
                            }
                        }
                        return xIndex;
                    };
                    // Set the series for the adding/updating chart.
                    ChartEngine.prototype._setChartSeries = function (chart, item) {
                        Object.keys(item).forEach(function (key) {
                            var series = new wijmo.chart.Series();
                            if (key === 'x') {
                                chart.bindingX = key;
                            }
                            else {
                                series.name = key;
                                series.binding = key;
                                chart.series.push(series);
                            }
                        });
                    };
                    // Update the chart in the FlexSheet.
                    ChartEngine.prototype._updateChart = function (chartObj) {
                        if (!!chartObj.cellRanges) {
                            var data = this._generateChartData(chartObj.chart.chartType, chartObj.cellRanges);
                            chartObj.chart.itemsSource = data;
                            chartObj.chart.series.clear();
                            if (data && data.length > 0) {
                                this._setChartSeries(chartObj.chart, data[0]);
                            }
                        }
                    };
                    // Indicates resize mode for the chart.
                    ChartEngine.prototype._hoverChart = function (e, chart) {
                        var hostElement = chart.hostElement, offset = this._cumulativeOffset(hostElement), right = offset.x + hostElement.clientWidth, bottom = offset.y + hostElement.clientHeight;
                        if (chart !== this._selectedChart) {
                            hostElement.style.cursor = 'move';
                            return;
                        }
                        this._resizeMode = ResizeMode.none;
                        hostElement.style.cursor = 'move';
                        if (e.clientX < offset.x || e.clientX > right || e.clientY < offset.y || e.clientY > bottom) {
                            return;
                        }
                        if (e.clientX > offset.x && e.clientX <= offset.x + 10) {
                            this._resizeMode = this._resizeMode | ResizeMode.left;
                        }
                        if (e.clientX >= right - 10 && e.clientX < right) {
                            this._resizeMode = this._resizeMode | ResizeMode.right;
                        }
                        if (e.clientY > offset.y && e.clientY <= offset.y + 10) {
                            this._resizeMode = this._resizeMode | ResizeMode.top;
                        }
                        if (e.clientY >= bottom - 10 && e.clientY < bottom) {
                            this._resizeMode = this._resizeMode | ResizeMode.bottom;
                        }
                        if (this._resizeMode & ResizeMode.right) {
                            if (this._resizeMode & ResizeMode.bottom) {
                                hostElement.style.cursor = 'se-resize';
                            }
                            else if (this._resizeMode & ResizeMode.top) {
                                hostElement.style.cursor = 'ne-resize';
                            }
                            else {
                                hostElement.style.cursor = 'e-resize';
                            }
                            return;
                        }
                        if (this._resizeMode & ResizeMode.left) {
                            if (this._resizeMode & ResizeMode.bottom) {
                                hostElement.style.cursor = 'sw-resize';
                            }
                            else if (this._resizeMode & ResizeMode.top) {
                                hostElement.style.cursor = 'nw-resize';
                            }
                            else {
                                hostElement.style.cursor = 'w-resize';
                            }
                            return;
                        }
                        if (this._resizeMode & ResizeMode.bottom) {
                            hostElement.style.cursor = 's-resize';
                        }
                        else if (this._resizeMode & ResizeMode.top) {
                            hostElement.style.cursor = 'n-resize';
                        }
                    };
                    // Event handler for mouse move event to indicate to move chart or reszie chart.
                    ChartEngine.prototype._mouseMove = function (e) {
                        var hostEle, deltaX, deltaY, left, top;
                        if (!this._selectedChart) {
                            return;
                        }
                        hostEle = this._selectedChart.hostElement;
                        deltaX = e.clientX - this._chartMousePosition.x;
                        deltaY = e.clientY - this._chartMousePosition.y;
                        left = this._containerOriginalBounds.left + deltaX;
                        top = this._containerOriginalBounds.top + deltaY;
                        this._owner.hostElement.style.cursor = '';
                        if (this._movingChart) {
                            this._moveChart(e, left, top, deltaX, deltaY);
                        }
                        else if (this._resizingChart) {
                            this._owner.hostElement.style.cursor = hostEle.style.cursor;
                            this._resizeChart(e, left, top, deltaX, deltaY);
                            this._selectedChart.refresh();
                        }
                    };
                    // Event handler for mouse down event.
                    ChartEngine.prototype._mouseDown = function (e, hostElement) {
                        var root = this._owner['_root'], cells = this._owner['_eCt'], rowHeader = this._owner['_eRHdr'], columnHeader = this._owner['_eCHdr'];
                        hostElement.style.borderStyle = 'dashed';
                        hostElement.focus();
                        this._containerRect = new wijmo.Rect(cells.offsetLeft, cells.offsetTop, root.clientWidth > cells.clientWidth ? cells.clientWidth : (root.clientWidth - rowHeader.clientWidth), root.clientHeight > cells.clientHeight ? cells.clientHeight : (root.clientHeight - columnHeader.clientHeight));
                        this._chartMousePosition.x = e.clientX;
                        this._chartMousePosition.y = e.clientY;
                        this._containerOriginalBounds.left = hostElement.parentElement.offsetLeft;
                        this._containerOriginalBounds.top = hostElement.parentElement.offsetTop;
                        this._containerOriginalBounds.width = hostElement.parentElement.offsetWidth;
                        this._containerOriginalBounds.height = hostElement.parentElement.offsetHeight;
                        this._chartOriginalBounds.left = hostElement.offsetLeft;
                        this._chartOriginalBounds.top = hostElement.offsetTop;
                        this._chartOriginalBounds.width = hostElement.offsetWidth;
                        this._chartOriginalBounds.height = hostElement.offsetHeight;
                        if (hostElement.style.cursor === 'move') {
                            this._movingChart = true;
                        }
                        else if (this._resizeMode !== ResizeMode.none) {
                            this._resizingChart = true;
                        }
                    };
                    // Event handler for mouse up event to reset the move chart and resize chart status.
                    ChartEngine.prototype._mouseUp = function () {
                        this._movingChart = false;
                        this._resizingChart = false;
                        this._resizeMode = ResizeMode.none;
                    };
                    // Move the chart in the FlexSheet.
                    ChartEngine.prototype._moveChart = function (e, left, top, deltaX, deltaY) {
                        var hostElement = this._selectedChart.hostElement, container = hostElement.parentElement, width = 0, height = 0, innerDelta;
                        // Adjust the horizontal postion and width of the chart, if the horizontal part of the chart is partial visible with the sheet scrolling.
                        if (this._containerOriginalBounds.width < this._chartOriginalBounds.width) {
                            if (hostElement.offsetLeft < 0) {
                                //Adjust the chart if the left part of the chart is invisible.
                                if (deltaX > 0) {
                                    innerDelta = this._chartOriginalBounds.left + deltaX;
                                    if (innerDelta > 0) {
                                        width = this._chartOriginalBounds.width;
                                        hostElement.style.left = '0px';
                                        left = innerDelta;
                                        this._chartOriginalBounds.left = 0;
                                        this._containerOriginalBounds.width = width;
                                        this._chartMousePosition.x = e.clientX;
                                    }
                                    else {
                                        width = this._containerOriginalBounds.width + deltaX;
                                        hostElement.style.left = innerDelta + 'px';
                                        left = 0;
                                    }
                                }
                            }
                            else {
                                //Adjust the chart if the right part of the chart is invisible.
                                if (deltaX < 0) {
                                    if (this._containerOriginalBounds.width - deltaX > this._chartOriginalBounds.width) {
                                        width = this._chartOriginalBounds.width;
                                        this._containerOriginalBounds.width = width;
                                    }
                                    else {
                                        width = this._containerOriginalBounds.width - deltaX;
                                    }
                                }
                            }
                        }
                        // Adjust the vertical postion and height of the chart, if the vertical part of the chart is partial visible with the sheet scrolling.
                        if (this._containerOriginalBounds.height < this._chartOriginalBounds.height) {
                            if (hostElement.offsetTop < 0) {
                                //Adjust the chart if the top part of the chart is invisible.
                                if (deltaY > 0) {
                                    innerDelta = this._chartOriginalBounds.top + deltaY;
                                    if (innerDelta > 0) {
                                        height = this._chartOriginalBounds.height;
                                        hostElement.style.top = '0px';
                                        top = innerDelta;
                                        this._chartOriginalBounds.top = 0;
                                        this._containerOriginalBounds.height = height;
                                        this._chartMousePosition.y = e.clientY;
                                    }
                                    else {
                                        height = this._containerOriginalBounds.height + deltaY;
                                        hostElement.style.top = innerDelta + 'px';
                                        top = 0;
                                    }
                                }
                            }
                            else {
                                //Adjust the chart if the bottom part of the chart is invisible.
                                if (deltaY < 0) {
                                    if (this._containerOriginalBounds.height - deltaY > this._chartOriginalBounds.height) {
                                        height = this._chartOriginalBounds.height;
                                        this._containerOriginalBounds.height = height;
                                    }
                                    else {
                                        height = this._containerOriginalBounds.height - deltaY;
                                    }
                                }
                            }
                        }
                        if (left < this._containerRect.left) {
                            left = this._containerRect.left;
                        }
                        else if (left + container.clientWidth > this._containerRect.left + this._containerRect.width) {
                            left = this._containerRect.left + this._containerRect.width - container.clientWidth;
                        }
                        if (top < this._containerRect.top) {
                            top = this._containerRect.top;
                        }
                        else if (top + container.clientHeight > this._containerRect.top + this._containerRect.height) {
                            top = this._containerRect.top + this._containerRect.height - container.clientHeight;
                        }
                        if (width > 0) {
                            container.style.width = width + 'px';
                        }
                        if (height > 0) {
                            container.style.height = height + 'px';
                        }
                        container.style.left = left + 'px';
                        container.style.top = top + 'px';
                    };
                    // Resize the chart in the FlexSheet.
                    ChartEngine.prototype._resizeChart = function (e, left, top, deltaX, deltaY) {
                        var hostEle = this._selectedChart.hostElement, container = hostEle.parentElement, cells = this._owner['_eCt'], offset = this._cumulativeOffset(cells);
                        if (!!(this._resizeMode & ResizeMode.right) && e.clientX < offset.x + this._containerRect.width) {
                            hostEle.style.width = this._chartOriginalBounds.width + deltaX + 'px';
                            container.style.width = this._containerOriginalBounds.width + deltaX + 'px';
                        }
                        if (!!(this._resizeMode & ResizeMode.left) && e.clientX >= offset.x) {
                            container.style.left = left + 'px';
                            hostEle.style.width = this._chartOriginalBounds.width - deltaX + 'px';
                            container.style.width = this._containerOriginalBounds.width - deltaX + 'px';
                        }
                        if (!!(this._resizeMode & ResizeMode.bottom) && e.clientY < offset.y + this._containerRect.height) {
                            hostEle.style.height = this._chartOriginalBounds.height + deltaY + 'px';
                            container.style.height = this._containerOriginalBounds.height + deltaY + 'px';
                        }
                        if (!!(this._resizeMode & ResizeMode.top) && e.clientY >= offset.y) {
                            container.style.top = top + 'px';
                            hostEle.style.height = this._chartOriginalBounds.height - deltaY + 'px';
                            container.style.height = this._containerOriginalBounds.height - deltaY + 'px';
                        }
                    };
                    // Adjust the position of chart with scrolling.
                    ChartEngine.prototype._scrollChart = function (chart, deltaScrollX, deltaScrollY) {
                        var hostElement = chart.hostElement, container = hostElement.parentElement;
                        if (deltaScrollX < 0) {
                            this._scrollRightDownChart(hostElement, container, deltaScrollX, true);
                            return;
                        }
                        if (deltaScrollY < 0) {
                            this._scrollRightDownChart(hostElement, container, deltaScrollY);
                            return;
                        }
                        if (deltaScrollX > 0) {
                            this._scrollLeftUpChart(hostElement, container, deltaScrollX, true);
                            return;
                        }
                        if (deltaScrollY > 0) {
                            this._scrollLeftUpChart(hostElement, container, deltaScrollY);
                            return;
                        }
                    };
                    // Adjust the position of the chart with right\down scroll direction.
                    ChartEngine.prototype._scrollRightDownChart = function (hostElement, container, delta, isHorizontal) {
                        if (isHorizontal === void 0) { isHorizontal = false; }
                        var root = this._owner['_root'], cells = this._owner['_eCt'], direction = isHorizontal ? 'Left' : 'Top', widthOrHeight = isHorizontal ? 'Width' : 'Height', containerSize = container['offset' + widthOrHeight], chartSize = hostElement['offset' + widthOrHeight], innerDelta = 0, pos;
                        if (container['offset' + direction] === root['offset' + direction] + root['client' + widthOrHeight]) {
                            innerDelta = hostElement['offset' + direction] + delta;
                            if (innerDelta > 0) {
                                hostElement.style[direction.toLowerCase()] = innerDelta + 'px';
                                pos = container['offset' + direction];
                            }
                            else {
                                hostElement.style[direction.toLowerCase()] = '0px';
                                container.style[widthOrHeight.toLowerCase()] = containerSize - innerDelta + 'px';
                                pos = container['offset' + direction] + innerDelta;
                            }
                        }
                        else {
                            pos = container['offset' + direction] + delta;
                            if (containerSize < chartSize && container['offset' + direction] !== cells['offset' + direction]) {
                                innerDelta = containerSize - delta;
                                if (innerDelta > chartSize) {
                                    container.style[widthOrHeight.toLowerCase()] = chartSize + 'px';
                                }
                                else {
                                    container.style[widthOrHeight.toLowerCase()] = innerDelta + 'px';
                                }
                            }
                            else {
                                if (pos < cells['offset' + direction]) {
                                    innerDelta = cells['offset' + direction] - pos;
                                    pos = cells['offset' + direction];
                                }
                                if (innerDelta > 0) {
                                    if (innerDelta <= container['offset' + widthOrHeight]) {
                                        container.style[widthOrHeight.toLowerCase()] = container['offset' + widthOrHeight] - innerDelta + 'px';
                                    }
                                    else {
                                        container.style[widthOrHeight.toLowerCase()] = '0px';
                                    }
                                    hostElement.style[direction.toLowerCase()] = hostElement['offset' + direction] - innerDelta + 'px';
                                }
                            }
                        }
                        container.style[direction.toLowerCase()] = pos + 'px';
                    };
                    // Adjust the position of the chart with left\up scroll direction.
                    ChartEngine.prototype._scrollLeftUpChart = function (hostElement, container, delta, isHorizontal) {
                        if (isHorizontal === void 0) { isHorizontal = false; }
                        var root = this._owner['_root'], cells = this._owner['_eCt'], direction = isHorizontal ? 'Left' : 'Top', widthOrHeight = isHorizontal ? 'Width' : 'Height', containerSize = container['offset' + widthOrHeight], chartSize = hostElement['offset' + widthOrHeight], innerDelta = 0, pos;
                        if (container['offset' + direction] === cells['offset' + direction]) {
                            if (containerSize < chartSize) {
                                innerDelta = hostElement['offset' + direction] + delta;
                                if (innerDelta < 0) {
                                    if (chartSize + innerDelta > 0) {
                                        container.style[widthOrHeight.toLowerCase()] = chartSize + innerDelta + 'px';
                                    }
                                    hostElement.style[direction.toLowerCase()] = innerDelta + 'px';
                                    pos = cells['offset' + direction];
                                }
                                else {
                                    hostElement.style[direction.toLowerCase()] = '0px';
                                    container.style[widthOrHeight.toLowerCase()] = chartSize + 'px';
                                    pos = cells['offset' + direction] + innerDelta;
                                }
                            }
                            else {
                                pos = container['offset' + direction] + delta;
                            }
                        }
                        else if (container['offset' + direction] === root['offset' + direction] + root['client' + widthOrHeight]) {
                            hostElement.style[direction.toLowerCase()] = hostElement['offset' + direction] + delta + 'px';
                            pos = root['offset' + direction] + root['client' + widthOrHeight];
                        }
                        else {
                            pos = container['offset' + direction] + delta;
                            if (pos + containerSize > root['offset' + direction] + root['client' + widthOrHeight]) {
                                innerDelta = pos + containerSize - root['offset' + direction] - root['client' + widthOrHeight];
                                if (innerDelta > containerSize) {
                                    pos = root['offset' + direction] + root['client' + widthOrHeight];
                                    container.style[widthOrHeight.toLowerCase()] = '0px';
                                    hostElement.style[direction.toLowerCase()] = hostElement['offset' + direction] + innerDelta - containerSize + 'px';
                                }
                                else {
                                    container.style[widthOrHeight.toLowerCase()] = container['offset' + widthOrHeight] - innerDelta + 'px';
                                }
                            }
                        }
                        container.style[direction.toLowerCase()] = pos + 'px';
                    };
                    // Gets the absolute offset for the element.
                    ChartEngine.prototype._cumulativeOffset = function (element) {
                        var top = 0, left = 0;
                        do {
                            top += element.offsetTop || 0;
                            left += element.offsetLeft || 0;
                            element = element.offsetParent;
                        } while (element);
                        return new wijmo.Point(left, top);
                    };
                    // Update the cell range of the chart.
                    ChartEngine.prototype._updateChartRange = function (chartObj, index, count, isAdd, isRow) {
                        var selection, max, min;
                        for (var i = 0; i < chartObj.cellRanges.length; i++) {
                            selection = chartObj.cellRanges[i];
                            if (!selection.isValid) {
                                continue;
                            }
                            if (isAdd) {
                                if (isRow) {
                                    if (index <= selection.bottomRow) {
                                        selection.row2 += count;
                                        if (index <= selection.topRow) {
                                            selection.row += count;
                                        }
                                    }
                                }
                                else {
                                    if (index <= selection.rightCol) {
                                        selection.col2 += count;
                                        if (index <= selection.leftCol) {
                                            selection.col += count;
                                        }
                                    }
                                }
                            }
                            else {
                                if (isRow) {
                                    if (index <= selection.bottomRow) {
                                        if (index + count <= selection.bottomRow) {
                                            max = selection.bottomRow - count;
                                        }
                                        else {
                                            max = index - 1;
                                        }
                                        min = selection.topRow;
                                        if (index <= selection.topRow) {
                                            if (index + count <= selection.topRow) {
                                                min = selection.topRow - count;
                                            }
                                            else {
                                                min = index;
                                            }
                                        }
                                        if (max < min) {
                                            if (min === index) {
                                                min = -1;
                                            }
                                            max = min;
                                        }
                                        selection.row = min;
                                        selection.row2 = max;
                                    }
                                }
                                else {
                                    if (index <= selection.rightCol) {
                                        if (index + count <= selection.rightCol) {
                                            max = selection.rightCol - count;
                                        }
                                        else {
                                            max = index - 1;
                                        }
                                        min = selection.leftCol;
                                        if (index <= selection.leftCol) {
                                            if (index + count <= selection.leftCol) {
                                                min = selection.leftCol - count;
                                            }
                                            else {
                                                min = index;
                                            }
                                        }
                                        if (max < min) {
                                            if (min === index) {
                                                min = -1;
                                            }
                                            max = min;
                                        }
                                        selection.col = min;
                                        selection.col2 = max;
                                    }
                                }
                            }
                        }
                    };
                    // Get the settings of all charts in current FlexSheet.
                    ChartEngine.prototype._getChartSettings = function () {
                        var charts = [], chartObj, chartHost, chartContainer;
                        for (var i = 0; i < this._charts.length; i++) {
                            chartObj = this._charts[i];
                            chartHost = chartObj.chart.hostElement;
                            chartContainer = chartHost.parentElement;
                            charts[i] = {
                                sheetIndex: chartObj.sheetIndex,
                                cellRanges: chartObj.cellRanges ? this._cloneChartRanges(chartObj.cellRanges) : null,
                                chartType: chartObj.chart.chartType,
                                chartRect: new wijmo.Rect(chartHost.offsetLeft, chartHost.offsetTop, chartHost.offsetWidth, chartHost.offsetHeight),
                                chartContainerRect: new wijmo.Rect(chartContainer.offsetLeft, chartContainer.offsetTop, chartContainer.offsetWidth, chartContainer.offsetHeight)
                            };
                        }
                        return charts;
                    };
                    // Clone the cell ranges of the specific chart.
                    ChartEngine.prototype._cloneChartRanges = function (ranges) {
                        var cloneRanges = [];
                        for (var i = 0; i < ranges.length; i++) {
                            cloneRanges[i] = ranges[i].clone();
                        }
                        return cloneRanges;
                    };
                    return ChartEngine;
                }());
                chart_1.ChartEngine = ChartEngine;
            })(chart = sheet.chart || (sheet.chart = {}));
        })(sheet = grid.sheet || (grid.sheet = {}));
    })(grid = wijmo.grid || (wijmo.grid = {}));
})(wijmo || (wijmo = {}));
//# sourceMappingURL=ChartEngine.js.map