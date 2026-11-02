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
    var chart;
    (function (chart) {
        var research;
        (function (research) {
            "use strict";
            var AggregateSeries = /** @class */ (function (_super) {
                __extends(AggregateSeries, _super);
                function AggregateSeries() {
                    var _this = _super.call(this) || this;
                    // backing fields
                    _this._autoInterval = false;
                    _this._autoGroupIntervals = ["ss", "10ss", "30ss", "mm", "10mm", "30mm", "hh", "DD", "WW", "MM", "YYYY"];
                    _this._autoMaxGroupings = 150;
                    _this._groupInterval = null;
                    _this._groupAggregate = wijmo.Aggregate.Avg;
                    _this._isGrouped = false;
                    _this._rcHandlerAdded = false;
                    // event to notify subscribers that the event changed
                    _this.groupChanged = new wijmo.Event();
                    return _this;
                }
                Object.defineProperty(AggregateSeries.prototype, "autoInterval", {
                    // gets/sets whether the interval is calculated automatically based on the displayed range
                    // currently, we try to display the largest amount of data possible for the displayed range
                    // that meets the autoMaxGroupings requirement
                    get: function () {
                        return this._autoInterval;
                    },
                    set: function (value) {
                        if (this._autoInterval !== value) {
                            this._autoInterval = wijmo.asBoolean(value, false);
                            this._clearValues();
                            this._invalidate();
                        }
                    },
                    enumerable: true,
                    configurable: true
                });
                Object.defineProperty(AggregateSeries.prototype, "autoGroupIntervals", {
                    // gets/sets the group intervals when autoInterval=true
                    // note that these are calculated in advance and should be limited to
                    // what's necessary for performance reasons.  these values should also make sense
                    // for the provided data set: it doesn't make sense to group by minutes or seconds
                    // when providing daily or weekly data
                    get: function () {
                        return this._autoGroupIntervals;
                    },
                    set: function (value) {
                        var _this = this;
                        if (this._autoGroupIntervals !== value) {
                            value = wijmo.asArray(value, false);
                            value.forEach(function (val) { return wijmo.assert(_this._isValidInterval(wijmo.asString(val, false)), "Invalid autoGroupIntervals"); }, this);
                            this._autoGroupIntervals = value;
                            this._clearValues();
                            this._invalidate();
                        }
                    },
                    enumerable: true,
                    configurable: true
                });
                Object.defineProperty(AggregateSeries.prototype, "autoMaxGroupings", {
                    // gets/sets the maximum number of groupings, as a positive integer, when autoInterval=true
                    // when this value is exceeded, the next interval is applied
                    // if set to zero, no grouping will be applied
                    get: function () {
                        return this._autoMaxGroupings;
                    },
                    set: function (value) {
                        if (this._autoMaxGroupings !== value) {
                            this._autoMaxGroupings = wijmo.asInt(value, false, true);
                            this._clearValues();
                            this._invalidate();
                        }
                    },
                    enumerable: true,
                    configurable: true
                });
                Object.defineProperty(AggregateSeries.prototype, "groupInterval", {
                    // gets/sets the group interval when autoInterval=false
                    // null or "null" string literal will clear the groups
                    get: function () {
                        return this._groupInterval;
                    },
                    set: function (value) {
                        if (this._groupInterval !== value) {
                            if (value !== null && value !== "null") {
                                wijmo.assert(this._isValidInterval(wijmo.asString(value, true)), "Invalid groupInterval");
                            }
                            this._groupInterval = wijmo.asString(value, true);
                            this._clearValues();
                            this._invalidate();
                        }
                    },
                    enumerable: true,
                    configurable: true
                });
                Object.defineProperty(AggregateSeries.prototype, "groupAggregate", {
                    // gets/sets the aggregate for groups
                    get: function () {
                        return this._groupAggregate;
                    },
                    set: function (value) {
                        if (this._groupAggregate !== value) {
                            this._groupAggregate = wijmo.asEnum(value, wijmo.Aggregate, false);
                            this._clearValues();
                            this._invalidate();
                        }
                    },
                    enumerable: true,
                    configurable: true
                });
                // fires group changed event
                AggregateSeries.prototype.onGroupChanged = function () {
                    var lbls = this._allAxisLabels[this._currentInterval], len = lbls.length;
                    var args = {
                        aggregate: this.groupAggregate,
                        interval: this._currentInterval,
                        count: len,
                        start: lbls[0],
                        end: lbls[len - 1]
                    };
                    if (this.groupChanged.hasHandlers) {
                        this.groupChanged.raise(this, args);
                    }
                };
                AggregateSeries.prototype.getValues = function (dim) {
                    this._addRangeChangedHandler();
                    this._group();
                    // verify grouping has been applied and return as normal if not
                    if (!this._isGrouped || !this._currentInterval) {
                        return _super.prototype.getValues.call(this, dim);
                    }
                    var values = this._allValues[this._currentInterval], propName = dim === 0 ? this._getBinding(0) : this.bindingX;
                    return values.map(function (item) {
                        if (wijmo.isDate(item[propName])) {
                            return item[propName].valueOf();
                        }
                        else {
                            return item[propName];
                        }
                    });
                };
                AggregateSeries.prototype._getBindingValues = function (index) {
                    this._addRangeChangedHandler();
                    this._group();
                    // verify grouping has been applied and return as normal if not
                    if (!this._isGrouped || !this._currentInterval) {
                        return _super.prototype._getBindingValues.call(this, index);
                    }
                    var values = this._allValues[this._currentInterval], propName = this._getBinding(index);
                    return values.map(function (item) { return item[propName]; });
                };
                AggregateSeries.prototype._clearValues = function () {
                    this._isGrouped = false;
                    this._allValues = null;
                    this._allAxisLabels = null;
                    this._currentInterval = null;
                    // remove axisX.rangeChanged handler and reset axisX.itemsSource
                    var ax = this._getAxisX();
                    if (ax && this._rcHandlerAdded) {
                        ax.rangeChanged.removeHandler(this._rangeChangedHandler, this);
                        this._rcHandlerAdded = false;
                    }
                    if (ax && ax.itemsSource) {
                        ax.itemsSource = null;
                    }
                    // restore bounds
                    var ay = this._getAxisY();
                    if (ay) {
                        ay.min = null;
                        ay.max = null;
                    }
                };
                // override for hit testing and financial chart types
                AggregateSeries.prototype._getItem = function (index) {
                    this._addRangeChangedHandler();
                    this._group();
                    var retval = null;
                    if (this._isGrouped && this._allValues[this._currentInterval].length > index) {
                        retval = this._allValues[this._currentInterval][index];
                    }
                    else {
                        retval = _super.prototype._getItem.call(this, index);
                    }
                    return retval;
                };
                // helper to add range changed event handler
                AggregateSeries.prototype._addRangeChangedHandler = function () {
                    if (!this._rcHandlerAdded) {
                        var ax = this._getAxisX();
                        if (!ax) {
                            return;
                        }
                        // add handler and update boolean flag
                        ax.rangeChanged.addHandler(this._rangeChangedHandler, this);
                        this._rcHandlerAdded = true;
                    }
                };
                // event handler for axisX.rangeChanged when autoInterval=true
                AggregateSeries.prototype._rangeChangedHandler = function (sender) {
                    if (this.autoInterval) {
                        var min = sender.actualMin, max = sender.actualMax;
                        // get ms representation of dates
                        if (wijmo.isDate(min)) {
                            min = min.valueOf();
                        }
                        if (wijmo.isDate(max)) {
                            max = max.valueOf();
                        }
                        this._selectInterval(min, max);
                    }
                };
                // acts as a controller to apply groupings and etc. based on autoInterval state
                AggregateSeries.prototype._group = function () {
                    if (this._isGrouped || !this._canGroup()) {
                        return;
                    }
                    var ax = this._getAxisX();
                    this._allValues = {};
                    this._allAxisLabels = {};
                    if (this.autoInterval) {
                        var i = 0, interval;
                        for (i = 0; i < this.autoGroupIntervals.length; i++) {
                            interval = this.autoGroupIntervals[i];
                            this._applyGroup(interval, this.groupAggregate);
                        }
                    }
                    else {
                        this._applyGroup(this.groupInterval, this.groupAggregate);
                        this._currentInterval = this.groupInterval;
                    }
                    this._isGrouped = true;
                    if (this.autoInterval) {
                        ax.onRangeChanged();
                    }
                    else {
                        this._updateAxes(false);
                        this.onGroupChanged();
                    }
                };
                // responsible for creating a single group when autoInterval=false
                AggregateSeries.prototype._applyGroup = function (key, aggregate) {
                    var cv = new wijmo.collections.CollectionView(this.collectionView.items), match, gd, bindings = this.binding.split(","), interval, subInterval = 1, row = 0, col = 0, item;
                    // split interval string
                    match = this._splitIntervalString(key);
                    if (match.length > 1) {
                        subInterval = parseInt(match[0]);
                        interval = match[1];
                    }
                    else {
                        subInterval = 1;
                        interval = match[0];
                    }
                    // handle grouping values
                    this._allValues[key] = [];
                    gd = this._getGroupDescription(this.bindingX, interval, subInterval);
                    cv.groupDescriptions.push(gd);
                    for (row = 0; row < cv.groups.length; row++) {
                        item = {};
                        // handle y bindings
                        for (col = 0; col < bindings.length; col++) {
                            item[bindings[col]] = cv.groups[row].getAggregate(aggregate, bindings[col]);
                        }
                        // handle x binding - dates only at the moment
                        item[this.bindingX] = new Date(cv.groups[row].name);
                        this._allValues[key].push(item);
                    }
                    // handle axis labels
                    this._allAxisLabels[key] = [];
                    for (row = 0; row < this._allValues[key].length; row++) {
                        this._allAxisLabels[key].push({
                            value: this._allValues[key][row][this.bindingX].valueOf(),
                            text: wijmo.Globalize.formatDate(this._allValues[key][row][this.bindingX], this._getFormatX(interval))
                        });
                    }
                };
                // gets the appropriate key (i.e. derived data set) based on given range
                AggregateSeries.prototype._selectInterval = function (xmin, xmax) {
                    if (!this._autoInterval || !this._isGrouped || (!isFinite(xmin) || !wijmo.isNumber(xmin)) || (!isFinite(xmax) || !wijmo.isNumber(xmax))) {
                        return;
                    }
                    var len = this.autoGroupIntervals.length, rangeVals, labels, counts = {}, key, i = 0;
                    // find visible range for each grouped set
                    for (i = 0; i < len; i++) {
                        key = this._autoGroupIntervals[i];
                        labels = this._allAxisLabels[key];
                        rangeVals = this._getValuesInXRange(labels, "value", xmin, xmax);
                        counts[key] = rangeVals.length;
                    }
                    key = null;
                    // find largest visible range less than maxGroupings
                    var temp = 0;
                    for (i = 0; i < len; i++) {
                        if (counts[this._autoGroupIntervals[i]] > temp && counts[this._autoGroupIntervals[i]] <= this.autoMaxGroupings) {
                            temp = counts[this._autoGroupIntervals[i]];
                            key = this._autoGroupIntervals[i];
                        }
                    }
                    // change current key
                    if (key && key !== this._currentInterval) {
                        this._currentInterval = key;
                        // fire event
                        this.onGroupChanged();
                    }
                    // update axes
                    if (this._currentInterval) {
                        this._updateAxes(true);
                    }
                };
                // updates axisX.itemsSource and axisY.[min/max]
                // subset determines if the data is filtered to the current range or not
                AggregateSeries.prototype._updateAxes = function (subset) {
                    var ay = this._getAxisY(), ax = this._getAxisX(), ymin = Number.MAX_VALUE, ymax = Number.MIN_VALUE, xmin = ax.actualMin, xmax = ax.actualMax, bindings = this.binding.split(','), values, propVal, i = 0;
                    values = subset
                        ? this._getValuesInXRange(this._allValues[this._currentInterval], this.bindingX, xmin, xmax)
                        : this._allValues[this._currentInterval];
                    // find ymin/ymax for visible x range for all bound y-values
                    for (i = 0; i < values.length; i++) {
                        for (var j = 0; j < bindings.length; j++) {
                            propVal = values[i][bindings[j]];
                            if (propVal < ymin) {
                                ymin = propVal;
                            }
                            if (ymax < propVal) {
                                ymax = propVal;
                            }
                        }
                    }
                    this.chart.beginUpdate();
                    // update axisY limits
                    if (isFinite(ymin) && wijmo.isNumber(ymin) && ymin !== Number.MAX_VALUE) {
                        ay.min = ymin;
                    }
                    if (isFinite(ymax) && wijmo.isNumber(ymax) && ymax !== Number.MIN_VALUE) {
                        ay.max = ymax;
                    }
                    // update axisX.itemsSource for current interval
                    this._getAxisX().itemsSource = this._allAxisLabels[this._currentInterval];
                    this.chart.endUpdate();
                };
                // helper to filter _allValues and _allAxisLabels for visible x-range
                AggregateSeries.prototype._getValuesInXRange = function (values, propName, xmin, xmax) {
                    if (wijmo.isDate(xmin)) {
                        xmin = xmin.valueOf();
                    }
                    if (wijmo.isDate(xmax)) {
                        xmax = xmax.valueOf();
                    }
                    var prop;
                    return values.filter(function (item) {
                        prop = item[propName];
                        if (wijmo.isDate(prop)) {
                            prop = prop.valueOf();
                        }
                        return xmin <= prop && prop <= xmax;
                    });
                };
                // gets the group description
                AggregateSeries.prototype._getGroupDescription = function (bindingX, interval, subInterval) {
                    if (subInterval === void 0) { subInterval = 1; }
                    var fn = null;
                    subInterval = subInterval || 1;
                    switch (interval) {
                        case "YYYY":
                            fn = function (item, propName) {
                                var year = wijmo.asDate(item[propName]).getFullYear();
                                return new Date(year, 0, 1);
                            };
                            break;
                        case "MM":
                            fn = function (item, propName) {
                                var d = wijmo.asDate(item[propName]), month = d.getMonth(), year = d.getFullYear();
                                return new Date(year, month, 1);
                            };
                            break;
                        case "WW":
                            fn = function (item, propName) {
                                var d = wijmo.asDate(item[propName]), month = d.getMonth(), dayOfWeek = d.getDay(), day = d.getDate(), year = d.getFullYear();
                                return new Date(year, month, day - dayOfWeek);
                            };
                            break;
                        case "DD":
                            fn = function (item, propName) {
                                var d = wijmo.asDate(item[propName]), month = d.getMonth(), date = d.getDate(), year = d.getFullYear();
                                return new Date(year, month, date);
                            };
                            break;
                        case "hh":
                            fn = function (item, propName) {
                                var d = wijmo.asDate(item[propName]), month = d.getMonth(), date = d.getDate(), year = d.getFullYear(), hour = d.getHours();
                                return new Date(year, month, date, hour - (hour % subInterval));
                            };
                            break;
                        case "mm":
                            fn = function (item, propName) {
                                var d = wijmo.asDate(item[propName]), month = d.getMonth(), date = d.getDate(), year = d.getFullYear(), hour = d.getHours(), minute = d.getMinutes();
                                return new Date(year, month, date, hour, minute - (minute % subInterval));
                            };
                            break;
                        case "ss":
                            fn = function (item, propName) {
                                var d = wijmo.asDate(item[propName]), month = d.getMonth(), date = d.getDate(), year = d.getFullYear(), hour = d.getHours(), minute = d.getMinutes(), second = d.getSeconds();
                                return new Date(year, month, date, hour, minute, second - (second % subInterval));
                            };
                            break;
                        default:
                            wijmo.assert(false, "Invalid groupInterval");
                            break;
                    }
                    return new wijmo.collections.PropertyGroupDescription(bindingX, fn);
                };
                // parse a given interval string, returning interval (ex. seconds) and sub-interval (ex. 30 seconds);
                AggregateSeries.prototype._splitIntervalString = function (value) {
                    return value ? value.match(/[a-zA-Z\.]+|[0-9?(\.0-9)]+/g) : [];
                };
                // determines whether grouping can be applied based on the current series configuration
                AggregateSeries.prototype._canGroup = function () {
                    var retval = wijmo.isString(this.binding) && wijmo.isString(this.bindingX) && this.groupAggregate !== wijmo.Aggregate.None && this.collectionView !== null && !wijmo.isUndefined(this.collectionView);
                    if (this.autoInterval) {
                        retval = retval && this.autoGroupIntervals && this.autoMaxGroupings ? true : false;
                    }
                    else {
                        retval = retval && this._isValidInterval(this.groupInterval);
                    }
                    return retval;
                };
                // determine if a given interval (and optional sub-interval) is valid
                AggregateSeries.prototype._isValidInterval = function (interval) {
                    var match = this._splitIntervalString(interval), subInterval = 1;
                    if (match.length !== 1 && match.length !== 2) {
                        return false;
                    }
                    interval = match.length > 1 ? match[1] : match[0];
                    subInterval = match.length > 1 ? +match[0] : subInterval;
                    return ["ss", "mm", "hh", "DD", "WW", "MM", "YYYY"].indexOf(interval) >= 0 && wijmo.isInt(subInterval);
                };
                // gets the format string for a given interval unless axisX.format is set
                AggregateSeries.prototype._getFormatX = function (interval) {
                    var retval = this._getAxisX().format;
                    if (retval) {
                        return retval;
                    }
                    switch (interval) {
                        case "YYYY":
                            retval = "yyyy";
                            break;
                        case "MM":
                            retval = "MMM yyyy";
                            break;
                        case "hh":
                        case "mm":
                            retval = "g";
                            break;
                        case "ss":
                            retval = "G";
                            break;
                        case "DD":
                        case "WW":
                        default:
                            retval = "d";
                            break;
                    }
                    return retval;
                };
                return AggregateSeries;
            }(chart.Series));
            research.AggregateSeries = AggregateSeries;
        })(research = chart.research || (chart.research = {}));
    })(chart = wijmo.chart || (wijmo.chart = {}));
})(wijmo || (wijmo = {}));
//# sourceMappingURL=AggregateSeries.js.map