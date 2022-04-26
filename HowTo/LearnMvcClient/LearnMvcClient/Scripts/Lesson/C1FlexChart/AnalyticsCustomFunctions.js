c1.documentReady(function () {
    var theChart = wijmo.Control.getControl('#theChart');

    // create a YFunctionSeries and add it to the chart
    var yFun = new wijmo.chart.analytics.YFunctionSeries();
    yFun.name = 'y = f(x)';
    yFun.min = -10;
    yFun.max = 10;
    yFun.sampleCount = 300;
    yFun.func = function (x) {
        return Math.sin(4 * x) * Math.cos(3 * x);
    };
    theChart.series.push(yFun);

    // create a ParametricFunctionSeries and add it to the chart
    var pFun = new wijmo.chart.analytics.ParametricFunctionSeries();
    pFun.name = 'x = f(t); y = g(t)'
    pFun.min = 0;
    pFun.max = 2 * Math.PI;
    pFun.sampleCount = 1000;
    pFun.xFunc = function (t) {
        return 10 * Math.cos(5 * t);
    };
    pFun.yFunc = function (t) {
        return Math.sin(6 * t);
    };
    theChart.series.push(pFun);
});