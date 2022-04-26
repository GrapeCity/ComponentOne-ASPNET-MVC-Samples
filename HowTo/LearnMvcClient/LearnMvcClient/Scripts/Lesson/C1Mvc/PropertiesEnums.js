c1.documentReady(function () {
    var theChart = wijmo.Control.getControl('#theChart');

    // try out different chart types
    var currentType = document.getElementById('currentType');
    var chartType = wijmo.Control.getControl('#chartType');
    chartType.textChanged.addHandler(function (s, e) {
        updateChartType();
    });
    updateChartType();

    function updateChartType() {
        theChart.chartType = chartType.selectedItem;
        currentType.textContent = theChart.chartType +
          ' (\'' + wijmo.chart.ChartType[theChart.chartType] + '\')';
    }
});