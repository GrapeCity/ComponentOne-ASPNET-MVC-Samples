function getChartStartDate(selectedRange) {
    var dt = new Date();
    switch (selectedRange) {
        case 0:
            dt.setMonth(dt.getMonth() - 1);
            return dt;
        case 1:
            dt.setMonth(dt.getMonth() - 3);
            return dt;
        case 2:
            dt.setMonth(dt.getMonth() - 6);
            return dt;
        case 3:
            return new Date(dt.getFullYear(), 0, 1);
        case 4:
            dt.setFullYear(dt.getFullYear() - 1);
            return dt;
        case 5:
            dt.setFullYear(dt.getFullYear() - 2);
            return dt;
        case 6:
            dt.setFullYear(dt.getFullYear() - 3);
            return dt;
        case 7:
            return new Date(2005, 0, 1);
    }

    // unknown period, use the last 12 months
    dt.setFullYear(dt.getFullYear() - 1);
    return dt;
}

function filterItemsSource(min, max, itemsSource) {
    var filterResult = [];
    for (var i = 0; i < itemsSource.length; i++) {
        if (itemsSource[i].TimeSlab.valueOf() >= min && itemsSource[i].TimeSlab.valueOf() <= max) {
            filterResult.push(itemsSource[i]);
        }
    }
    return filterResult;
}

function filterAnalysisData(min, max, itemsSource) {
    var preLoadAnalysisData = false, aData = [];
    //retrieve the analysis data
    for (var i = 0; i < itemsSource.length; i++) {
        if (itemsSource[i].TimeSlab.valueOf() >= min && itemsSource[i].TimeSlab.valueOf() <= max) {
            if (!preLoadAnalysisData) {
                var idx;
                for (var j = 200; j > 0; j--) {
                    idx = i - j;
                    if (idx > 0) {
                        aData.push(itemsSource[i]);
                    }
                }
                preLoadAnalysisData = true;
            }
            aData.push(itemsSource[i]);
        }
    }
    return aData;
}

//update stock chart range
function updateStChartRange(min, max) {
    var layer = getAnnotationLayer(stChart);
    for (var i = 0; i < seriesLength; i++) {
        var itemsSource = filterItemsSource(min, max, stChartSeriesItems[i]),
            firstValue, volSeries, analysisData;
        if (!itemsSource[0]) {
            continue;
        }
        firstValue = itemsSource[0].Close;
        if (stChart.series[i] instanceof wijmo.chart.analytics.MovingAverage) {
            analysisData = filterAnalysisData(min, max, stChartSeriesItems[i]);
            if (analysisData) {
                maIdx = analysisData.length - stChart.series[0].itemsSource.length - periodInput.value + 1;
                analySource = analysisData.slice(maIdx >= 0 ? maIdx : 0, analysisData.length - 1);
            }
            for (var j = 0; j < analysisData.length; j++) {
                analysisData[j].HighChg = (analysisData[j].High - firstValue) / firstValue;
                analysisData[j].LowChg = (analysisData[j].Low - firstValue) / firstValue;
                analysisData[j].OpenChg = (analysisData[j].Open - firstValue) / firstValue;
                analysisData[j].CloseChg = (analysisData[j].Close - firstValue) / firstValue;
            }
            stChart.series[i].itemsSource = analysisData;
            continue;
        }

        if (seriesLength > 2 && i < seriesLength - 1 && itemsSource.length > 0) {
            for (var j = 0; j < itemsSource.length; j++) {
                itemsSource[j].HighChg = (itemsSource[j].High - firstValue) / firstValue;
                itemsSource[j].LowChg = (itemsSource[j].Low - firstValue) / firstValue;
                itemsSource[j].OpenChg = (itemsSource[j].Open - firstValue) / firstValue;
                itemsSource[j].CloseChg = (itemsSource[j].Close - firstValue) / firstValue;
            }
        }

        if (layer && i === 0 && stChart.series[0].binding == "CloseChg") {
            var items = layer.items;
            for (var j = 0; j < items.length; j++) {
                if (items[j] instanceof wijmo.chart.annotation.Rectangle) {
                    var dt = items[j].point.x, closeChg = findCloseChg(dt, itemsSource);
                    if (closeChg != null) {
                        items[j].point = new wijmo.chart.DataPoint(dt, closeChg);
                    }
                }
            }
        }

        stChart.series[i].itemsSource = itemsSource;

        if (stChart.series[i].binding == "Volume") {
            volSeries = stChart.series[i];
            volSeries.axisY.max = Math.max.apply(null, volSeries.getValues(0)) * 8;
        }
    }

    updateMovingAverage();
    stChart.invalidate();

    ranges[0].setAttribute("value", min);
    ranges[1].setAttribute("value", max);
    dateRangePanel.innerHTML = wijmo.Globalize.format(new Date(min), 'MMM,dd,yyyy') +
                    '-' + wijmo.Globalize.format(new Date(max), 'MMM,dd,yyyy');
    updateSeriesMarkers();
    setQuotesDetailInfo();

}

function findCloseChg(dt, itemsSource) {
    for (var i = 0; i < itemsSource.length; i++) {
        if (itemsSource[i].TimeSlab.getTime() == dt.getTime()) {
            return itemsSource[i].CloseChg;
        }
    }
    return null;
}
