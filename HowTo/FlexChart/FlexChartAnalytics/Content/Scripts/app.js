
function InitialControls() {
    var waterfallChart = wijmo.Control.getControl("#waterfallChart"),
        relativeData = document.getElementById('relativeData'),
        connectorLines = document.getElementById('connectorLines'),
        showTotal = document.getElementById('showTotal'),
        showIntermediateTotal = document.getElementById('showIntermediateTotal');

    var waterfall = new wijmo.chart.analytics.Waterfall();
    waterfall.relativeData = true;
    waterfall.connectorLines = true;
    waterfall.showTotal = true;
    waterfall.start = 1000;
    waterfall.showIntermediateTotal = true;
    waterfall.intermediateTotalPositions = [3, 6, 9, 12];
    waterfall.intermediateTotalLabels = ['Q1', 'Q2', 'Q3', 'Q4'];
    waterfall.name = 'Increase,Decrease,Total';
    waterfall.styles = {
        connectorLines: {
            stroke: '#333',
            'stroke-dasharray': '5 5'
        },
        start: {
            fill: '#7dc7ed'
        },
        falling: {
            fill: '#dd2714',
            stroke: '#a52714'
        },
        rising: {
            fill: '#0f9d58',
            stroke: '#0f9d58'
        },
        intermediateTotal: {
            fill: '#7dc7ed'
        },
        total: {
            fill: '#7dc7ed'
        }
    };
    waterfallChart.series.push(waterfall);

    waterfallChart.tooltip.content = function (ht) {
        if (ht) {
            return '<b>' + ht.x + '</b><br/>value: ' + ht.y;
        }
    }

    // relativeData - initialize checkbox properties
    relativeData.checked = waterfall.relativeData;
    relativeData.addEventListener('change', function () {
        waterfall.relativeData = this.checked;
    });

    // connectorLines - initialize checkbox properties
    connectorLines.checked = waterfall.connectorLines;
    connectorLines.addEventListener('change', function () {
        waterfall.connectorLines = this.checked;
    });

    // showTotal - initialize checkbox properties
    showTotal.checked = waterfall.showTotal;
    showTotal.addEventListener('change', function () {
        waterfall.showTotal = this.checked;
    });

    // showIntermediateTotal - initialize checkbox properties
    showIntermediateTotal.checked = waterfall.showIntermediateTotal;
    showIntermediateTotal.addEventListener('change', function () {
        waterfall.showIntermediateTotal = this.checked;
    });
}

//TrendLine
function fitTypeMenu_SelectedIndexChanged(sender) {
    if (sender.selectedValue) {
        var trendLineChart = wijmo.Control.getControl("#trendLineChart");
        trendLineChart.series[1].fitType = parseInt(sender.selectedIndex);
    }
}

//MovingAverage
function movingAverageTypeMenu_SelectedIndexChanged(sender) {
    if (sender.selectedValue) {
        var movingAverageChart = wijmo.Control.getControl("#movingAverageChart");
        movingAverageChart.series[1].type = parseInt(sender.selectedIndex);
    }
}

function movingAveragePeriodInput_ValueChanged(sender) {
    if (!checkValue(sender)) {
        return;
    }

    var movingAverageChart = wijmo.Control.getControl("#movingAverageChart");
    movingAverageChart.series[1].period = sender.value;
}

//YFunctionSeries
function yFuncSeriesFunc(value) {
    return Math.sin(4 * value) * Math.cos(3 * value);
}

//ParametricFunctionSeries
function paramFuncSeriesXFunc(value) {
    return Math.cos(value * 5); 
}

function paramFuncSeriesYFunc(value) {
    return Math.sin(value * 7);
}

function checkValue(number) {
    return number.value >= number.min && number.value <= number.max;
}