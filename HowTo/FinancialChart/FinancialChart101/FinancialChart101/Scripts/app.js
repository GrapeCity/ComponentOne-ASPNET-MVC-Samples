$(document).ready(function () {
    //Chart Types
    tpChart = wijmo.Control.getControl("#tpChart");
    tpCombo = wijmo.Control.getControl("#tpCombo");
    tpCombo.itemsSource = chartTypesList;

    //Range Selector
    stChart = wijmo.Control.getControl("#stChart");
    rsChart = wijmo.Control.getControl("#rsChart");
    
    //Trend Lines
    tlChart = wijmo.Control.getControl('#tlChart');
    inPeriod = wijmo.Control.getControl('#inPeriod');
    cmbMAType = wijmo.Control.getControl('#cmbMAType');

    inPeriod.valueChanged.addHandler(function () {
        if (inPeriod.value < inPeriod.min || inPeriod.value > inPeriod.max) {
            return;
        }
        tlChart.series[1].period = inPeriod.value;
    });
});

//Chart Types
var bindingYs = { 0: 'close', 2: 'close', 5: 'high,low,open,close', 6: 'high,low,open,close', 7: 'high,low,open,close', 8: 'high,low,open,close', 9: 'high,low,open,close', 10: 'high,low,open,close', 11: 'close,volume', 12: 'high,low,open,close,volume', 13: 'high,low,open,close,volume', 14: 'high,low,open,close,volume' }
    , chartTypesList = [{ "value": "2", "text": "Line" }
                        , { "value": "0", "text": "Column" }
                        , { "value": "5", "text": "Candlestick" }
                        , { "value": "6", "text": "HighLowOpenClose" }
                        , { "value": "7", "text": "HeikinAshi" }
                        , { "value": "8", "text": "LineBreak" }
                        , { "value": "9", "text": "Renko" }
                        , { "value": "10", "text": "Kagi" }
                        , { "value": "11", "text": "ColumnVolume" }
                        , { "value": "12", "text": "EquiVolume" }
                        , { "value": "13", "text": "CandleVolume" }
                        , { "value": "14", "text": "ArmsCandleVolume" }]
    , tpChart = null
    , tpCombo = null;

function tpChart_tooltipContent(ht) {
    var dateStr = 'Date: ' + wijmo.Globalize.format(ht.item.date,'MM/dd/yyyy') + '<br/>',
        hlocStr = 'Open: ' + wijmo.Globalize.format(ht.item.open, 'n2') + '<br/>' +
                  'High: ' + wijmo.Globalize.format(ht.item.high, 'n2') + '<br/>' +
                  'Low: ' + wijmo.Globalize.format(ht.item.low, 'n2') + '<br/>' +
                  'Close: ' + wijmo.Globalize.format(ht.item.close, 'n2') + '<br/>',
        closeStr = 'Close: ' + wijmo.Globalize.format(ht.item.close, 'n2'),
        volStr = 'Volume: ' + wijmo.Globalize.format(ht.item.volume, 'n0'),
        toolTipStr;
    switch (tpCombo.selectedItem.text) {
        case 'Line':
        case 'Column':
            toolTipStr = dateStr + closeStr;
            break;
        case 'ColumnVolume':
            toolTipStr = dateStr + closeStr + '<br/>' + volStr;
            break;
        case 'EquiVolume':
        case 'CandleVolume':
        case 'ArmsCandleVolume':
            toolTipStr = dateStr + hlocStr + volStr;
            break;
        default:
            toolTipStr = dateStr + hlocStr;
            break;
    }
    return toolTipStr;
};


//var menu = new wijmo.input.Menu('#tpMenu');
function tpCombo_SelectedIndexChanged(sender) {
    var arg = wijmo.changeType(sender.selectedValue, wijmo.DataType.Number);
    // check if the conversion was successful
    if (wijmo.isNumber(arg) && tpChart && tpChart.series) 
    {
        // update the value
        tpChart.chartType = arg;
        tpChart.series[0].binding = bindingYs[arg];
        switch (sender.selectedItem.text) {
            case 'LineBreak':
                tpChart.options = {
                    lineBreak: {
                        newLineBreaks: 3
                    }
                };
                break;
            case 'Renko':
                tpChart.options = {
                    renko: {
                        boxSize: 2,
                        rangeMode: 'Fixed',
                        fields: 'Close'
                    }
                };
                break;
            case 'Kagi':
                tpChart.options = {
                    kagi: {
                        reversalAmount: 1,
                        rangeMode: 'Fixed',
                        fields: 'Close'
                    }
                };
                break;
            default:
                break;
        }
    }
};

//Marker
function lineMarkerContent(ht, pt) {
    var item = ht.series.collectionView.items[ht.pointIndex];
    if (item) {
        return 'Date: ' + wijmo.Globalize.format(ht.x, 'MMM-dd') + '<br/>' +
                            'High: ' + item.high.toFixed() + '<br/>' +
                            'Low: ' + item.low.toFixed() + '<br/>' +
                            'Open: ' + item.open.toFixed() + '<br/>' +
                            'Close: ' + item.close.toFixed();

    }
};

//Range Selector
function stChart_tooltipContent(ht) {
    var toolTipStr = 'Date: ' + wijmo.Globalize.format(ht.item.date, 'MM/dd/yyyy') + '<br/>' +
                        'Open: ' + wijmo.Globalize.format(ht.item.open, 'n2') + '<br/>' +
                        'High: ' + wijmo.Globalize.format(ht.item.high, 'n2') + '<br/>' +
                        'Low: ' + wijmo.Globalize.format(ht.item.low, 'n2') + '<br/>' +
                        'Close: ' + wijmo.Globalize.format(ht.item.close, 'n2') + '<br/>';
    return toolTipStr;
};

function rsChart_OnClientRangeChanged(sender, e) {
    if (stChart && rsChart) {
        var rangeSelector = c1.getExtender(rsChart, 'RangeSelector');
        if (rangeSelector) {
            stChart.axisX.min = rangeSelector.min;
            stChart.axisX.max = rangeSelector.max;
            stChart.invalidate();
        }
    }
};

//declare variables
var stChart = null
    , rsChart = null;

//Trend Lines
var tlChart = null
    , movingAverage = null
    , inPeriod = null
    , cmbMAType = null;

function cmbMAType_SelectedIndexChanged(sender) {
    var arg = sender.selectedValue;
    if (tlChart && tlChart.series && tlChart.series.length > 0 && tlChart.series[1]) {
        tlChart.series[1].type = wijmo.chart.analytics.MovingAverageType[arg];
        // update name for legend item
        tlChart.series[1].name = sender.text + ' Moving Average';
        tlChart.invalidate();
    }
};
