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
    var grid;
    (function (grid) {
        var sheet;
        (function (sheet) {
            var chart;
            (function (chart_1) {
                'use strict';
                /*
                 * Defines the _ChartInsertAction class.
                 *
                 * It deals with the undo\redo for inserting chart in the @see:FlexSheet control.
                 */
                var _ChartInsertAction = /** @class */ (function (_super) {
                    __extends(_ChartInsertAction, _super);
                    /*
                     * Initializes a new instance of the @see:_ChartInsertAction class.
                     *
                     * @param chartEngine The @see: ChartEngine the engine to handle the @see:FlexChart in the @see:FlexSheet control.
                     * @param chartType the type of the chart.
                     * @param data the data used to render the chart.
                     * @param chartRect the bounds rectangle to locate the chart.
                     * @param cellRanges the cells to generate the @see:FlexChart in the @see:FlexSheet control.
                     */
                    function _ChartInsertAction(chartEngine, chartType, data, chartRect, cellRanges) {
                        var _this = _super.call(this, chartEngine._owner) || this;
                        _this._chartEngine = chartEngine;
                        _this._chartType = chartType;
                        _this._data = data;
                        _this._chartRect = chartRect;
                        _this._cellRnages = cellRanges;
                        return _this;
                    }
                    /*
                     * Overrides the undo method of its base class @see:_UndoAction.
                     */
                    _ChartInsertAction.prototype.undo = function () {
                        var chart, chartHost, chartContainer, chartCnt = this._chartEngine._charts.length;
                        if (chartCnt > 0) {
                            chart = this._chartEngine._charts[chartCnt - 1].chart;
                            chartHost = chart.hostElement;
                            chartContainer = chartHost.parentElement;
                            this._chartEngine._undoing = true;
                            this._chartRect = new wijmo.Rect(chartHost.offsetLeft, chartHost.offsetTop, chartHost.offsetWidth, chartHost.offsetHeight);
                            this._chartContainerRect = new wijmo.Rect(chartContainer.offsetLeft, chartContainer.offsetTop, chartContainer.offsetWidth, chartContainer.offsetHeight);
                            if (chart === this._chartEngine._selectedChart) {
                                this._chartEngine._selectedChart = null;
                            }
                            this._chartEngine.removeChart(chart);
                            this._chartEngine._undoing = false;
                        }
                    };
                    /*
                     * Overrides the redo method of its base class @see:_UndoAction.
                     */
                    _ChartInsertAction.prototype.redo = function () {
                        var chartObj, chart, chartContainer;
                        this._chartEngine._undoing = true;
                        chartObj = this._chartEngine.addChart(this._chartType, this._data, this._chartRect);
                        chartObj.cellRanges = this._cellRnages;
                        chart = chartObj.chart;
                        chartContainer = chart.hostElement.parentElement;
                        wijmo.setCss(chartContainer, {
                            top: this._chartContainerRect.top + 'px',
                            left: this._chartContainerRect.left + 'px',
                            height: this._chartContainerRect.height + 'px',
                            width: this._chartContainerRect.width + 'px'
                        });
                        this._chartEngine._undoing = false;
                    };
                    return _ChartInsertAction;
                }(sheet._UndoAction));
                chart_1._ChartInsertAction = _ChartInsertAction;
                /*
                 * Defines the _ChartRemoveAction class.
                 *
                 * It deals with the undo\redo for removing chart in the @see:FlexSheet control.
                 */
                var _ChartRemoveAction = /** @class */ (function (_super) {
                    __extends(_ChartRemoveAction, _super);
                    /*
                     * Initializes a new instance of the @see:_ChartRemoveAction class.
                     *
                     * @param chartEngine The @see: ChartEngine the engine to handle the @see:FlexChart in the @see:FlexSheet control.
                     * @param chartObj contains related information of the @see:FlexChart in the @see:FlexSheet control.
                     */
                    function _ChartRemoveAction(chartEngine, chartObj) {
                        var _this = _super.call(this, chartEngine._owner) || this;
                        var chart = chartObj.chart, chartHost = chartObj.chart.hostElement, chartContainer = chartHost.parentElement;
                        _this._chartEngine = chartEngine;
                        _this._chartType = chart.chartType;
                        _this._data = chart.itemsSource;
                        _this._cellRnages = chartObj.cellRanges;
                        _this._chartRect = new wijmo.Rect(chartHost.offsetLeft, chartHost.offsetTop, chartHost.offsetWidth, chartHost.offsetHeight);
                        _this._chartContainerRect = new wijmo.Rect(chartContainer.offsetLeft, chartContainer.offsetTop, chartContainer.offsetWidth, chartContainer.offsetHeight);
                        return _this;
                    }
                    /*
                     * Overrides the undo method of its base class @see:_UndoAction.
                     */
                    _ChartRemoveAction.prototype.undo = function () {
                        var chartObj, chart, chartContainer;
                        this._chartEngine._undoing = true;
                        chartObj = this._chartEngine.addChart(this._chartType, this._data, this._chartRect);
                        chartObj.cellRanges = this._cellRnages;
                        chart = chartObj.chart;
                        chartContainer = chart.hostElement.parentElement;
                        wijmo.setCss(chartContainer, {
                            top: this._chartContainerRect.top + 'px',
                            left: this._chartContainerRect.left + 'px',
                            height: this._chartContainerRect.height + 'px',
                            width: this._chartContainerRect.width + 'px'
                        });
                        this._chartEngine._undoing = false;
                    };
                    /*
                     * Overrides the redo method of its base class @see:_UndoAction.
                     */
                    _ChartRemoveAction.prototype.redo = function () {
                        var chart, chartCnt = this._chartEngine._charts.length;
                        if (chartCnt > 0) {
                            chart = this._chartEngine._charts[chartCnt - 1].chart;
                            this._chartEngine._undoing = true;
                            if (chart === this._chartEngine._selectedChart) {
                                this._chartEngine._selectedChart = null;
                            }
                            this._chartEngine.removeChart(chart);
                            this._chartEngine._undoing = false;
                        }
                    };
                    return _ChartRemoveAction;
                }(sheet._UndoAction));
                chart_1._ChartRemoveAction = _ChartRemoveAction;
                /*
                 * Defines the _ChartColumnsChangedAction class.
                 *
                 * It deals with the undo\redo for insert or delete column of the flexsheet.
                 */
                var _ChartColumnsChangedAction = /** @class */ (function (_super) {
                    __extends(_ChartColumnsChangedAction, _super);
                    /*
                     * Initializes a new instance of the @see:_ChartColumnsChangedAction class.
                     *
                     * @param chartEngine The @see: ChartEngine that the _ChartColumnsChangedAction works for.
                     */
                    function _ChartColumnsChangedAction(chartEngine) {
                        var _this = _super.call(this, chartEngine._owner) || this;
                        _this._chartEngine = chartEngine;
                        _this._oldCharts = chartEngine._getChartSettings();
                        return _this;
                    }
                    /*
                     * Overrides the undo method of its base class @see:_ColumnsChangedAction.
                     */
                    _ChartColumnsChangedAction.prototype.undo = function () {
                        var chartObj, chartSetting, data, i;
                        this._chartEngine._undoing = true;
                        _super.prototype.undo.call(this);
                        i = this._chartEngine._charts.length - 1;
                        for (; i >= 0; i--) {
                            chartObj = this._chartEngine._charts[i];
                            if (chartObj.sheetIndex === this['_sheetIndex']) {
                                this._chartEngine.removeChart(chartObj.chart);
                            }
                        }
                        for (i = 0; i < this._oldCharts.length; i++) {
                            chartSetting = this._oldCharts[i];
                            if (chartSetting.sheetIndex === this['_sheetIndex']) {
                                data = this._chartEngine._generateChartData(chartSetting.chartType, chartSetting.cellRanges);
                                chartObj = this._chartEngine.addChart(chartSetting.chartType, data, chartSetting.chartRect);
                                chartObj.cellRanges = chartSetting.cellRanges;
                                wijmo.setCss(chartObj.chart.hostElement.parentElement, {
                                    top: chartSetting.chartContainerRect.top + 'px',
                                    left: chartSetting.chartContainerRect.left + 'px',
                                    height: chartSetting.chartContainerRect.height + 'px',
                                    width: chartSetting.chartContainerRect.width + 'px'
                                });
                            }
                        }
                        this._chartEngine._undoing = false;
                    };
                    /*
                     * Overrides the redo method of its base class @see:_ColumnsChangedAction.
                     */
                    _ChartColumnsChangedAction.prototype.redo = function () {
                        var chartObj, chartSetting, data, i;
                        this._chartEngine._undoing = true;
                        _super.prototype.redo.call(this);
                        i = this._chartEngine._charts.length - 1;
                        for (; i >= 0; i--) {
                            chartObj = this._chartEngine._charts[i];
                            if (chartObj.sheetIndex === this['_sheetIndex']) {
                                this._chartEngine.removeChart(chartObj.chart);
                            }
                        }
                        for (i = 0; i < this._newCharts.length; i++) {
                            chartSetting = this._newCharts[i];
                            if (chartSetting.sheetIndex === this['_sheetIndex']) {
                                data = this._chartEngine._generateChartData(chartSetting.chartType, chartSetting.cellRanges);
                                chartObj = this._chartEngine.addChart(chartSetting.chartType, data, chartSetting.chartRect);
                                chartObj.cellRanges = chartSetting.cellRanges;
                                wijmo.setCss(chartObj.chart.hostElement.parentElement, {
                                    top: chartSetting.chartContainerRect.top + 'px',
                                    left: chartSetting.chartContainerRect.left + 'px',
                                    height: chartSetting.chartContainerRect.height + 'px',
                                    width: chartSetting.chartContainerRect.width + 'px'
                                });
                            }
                        }
                        this._chartEngine._undoing = false;
                    };
                    /*
                     * Overrides the saveNewState method of its base class @see:_ColumnsChangedAction.
                     */
                    _ChartColumnsChangedAction.prototype.saveNewState = function () {
                        this._newCharts = this._chartEngine._getChartSettings();
                        return _super.prototype.saveNewState.call(this);
                    };
                    return _ChartColumnsChangedAction;
                }(sheet._ColumnsChangedAction));
                chart_1._ChartColumnsChangedAction = _ChartColumnsChangedAction;
                /*
                 * Defines the _ChartRowsChangedAction class.
                 *
                 * It deals with the undo\redo for insert or delete column of the flexsheet.
                 */
                var _ChartRowsChangedAction = /** @class */ (function (_super) {
                    __extends(_ChartRowsChangedAction, _super);
                    /*
                     * Initializes a new instance of the @see:_ChartRowsChangedAction class.
                     *
                     * @param chartEngine The @see: ChartEngine that the _ChartRowsChangedAction works for.
                     */
                    function _ChartRowsChangedAction(chartEngine) {
                        var _this = _super.call(this, chartEngine._owner) || this;
                        _this._chartEngine = chartEngine;
                        _this._oldCharts = chartEngine._getChartSettings();
                        return _this;
                    }
                    /*
                     * Overrides the undo method of its base class @see:_RowsChangedAction.
                     */
                    _ChartRowsChangedAction.prototype.undo = function () {
                        var chartObj, chartSetting, data, i;
                        this._chartEngine._undoing = true;
                        _super.prototype.undo.call(this);
                        i = this._chartEngine._charts.length - 1;
                        for (; i >= 0; i--) {
                            chartObj = this._chartEngine._charts[i];
                            if (chartObj.sheetIndex === this['_sheetIndex']) {
                                this._chartEngine.removeChart(chartObj.chart);
                            }
                        }
                        for (i = 0; i < this._oldCharts.length; i++) {
                            chartSetting = this._oldCharts[i];
                            if (chartSetting.sheetIndex === this['_sheetIndex']) {
                                data = this._chartEngine._generateChartData(chartSetting.chartType, chartSetting.cellRanges);
                                chartObj = this._chartEngine.addChart(chartSetting.chartType, data, chartSetting.chartRect);
                                chartObj.cellRanges = chartSetting.cellRanges;
                                wijmo.setCss(chartObj.chart.hostElement.parentElement, {
                                    top: chartSetting.chartContainerRect.top + 'px',
                                    left: chartSetting.chartContainerRect.left + 'px',
                                    height: chartSetting.chartContainerRect.height + 'px',
                                    width: chartSetting.chartContainerRect.width + 'px'
                                });
                            }
                        }
                        this._chartEngine._undoing = false;
                    };
                    /*
                     * Overrides the redo method of its base class @see:_RowsChangedAction.
                     */
                    _ChartRowsChangedAction.prototype.redo = function () {
                        var chartObj, chartSetting, data, i;
                        this._chartEngine._undoing = true;
                        _super.prototype.redo.call(this);
                        i = this._chartEngine._charts.length - 1;
                        for (; i >= 0; i--) {
                            chartObj = this._chartEngine._charts[i];
                            if (chartObj.sheetIndex === this['_sheetIndex']) {
                                this._chartEngine.removeChart(chartObj.chart);
                            }
                        }
                        for (i = 0; i < this._newCharts.length; i++) {
                            chartSetting = this._newCharts[i];
                            if (chartSetting.sheetIndex === this['_sheetIndex']) {
                                data = this._chartEngine._generateChartData(chartSetting.chartType, chartSetting.cellRanges);
                                chartObj = this._chartEngine.addChart(chartSetting.chartType, data, chartSetting.chartRect);
                                chartObj.cellRanges = chartSetting.cellRanges;
                                wijmo.setCss(chartObj.chart.hostElement.parentElement, {
                                    top: chartSetting.chartContainerRect.top + 'px',
                                    left: chartSetting.chartContainerRect.left + 'px',
                                    height: chartSetting.chartContainerRect.height + 'px',
                                    width: chartSetting.chartContainerRect.width + 'px'
                                });
                            }
                        }
                        this._chartEngine._undoing = false;
                    };
                    /*
                     * Overrides the saveNewState method of its base class @see:_RowsChangedAction.
                     */
                    _ChartRowsChangedAction.prototype.saveNewState = function () {
                        this._newCharts = this._chartEngine._getChartSettings();
                        return _super.prototype.saveNewState.call(this);
                    };
                    return _ChartRowsChangedAction;
                }(sheet._RowsChangedAction));
                chart_1._ChartRowsChangedAction = _ChartRowsChangedAction;
            })(chart = sheet.chart || (sheet.chart = {}));
        })(sheet = grid.sheet || (grid.sheet = {}));
    })(grid = wijmo.grid || (wijmo.grid = {}));
})(wijmo || (wijmo = {}));
//# sourceMappingURL=_ChartUndoAction.js.map