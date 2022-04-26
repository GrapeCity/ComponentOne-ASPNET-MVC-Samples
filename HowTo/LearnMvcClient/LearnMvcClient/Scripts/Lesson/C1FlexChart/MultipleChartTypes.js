c1.documentReady(function () {
    var theChart = wijmo.Control.getControl('#theChart');
    var theChartType = wijmo.Control.getControl('#chartType');

    theChartType.textChanged.addHandler(function (s, e) {
        theChart.series[2].chartType = s.text;
    });
});