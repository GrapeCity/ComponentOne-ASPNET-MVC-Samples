c1.documentReady(function () {
    var theChart = wijmo.Control.getControl('#theChart');
    var theChartType = wijmo.Control.getControl('#chartType');
    var theStacking = wijmo.Control.getControl('#stacking');

    theChartType.selectedIndexChanged.addHandler(function (s, e) {
        theChart.chartType = s.text;
    });

    theStacking.selectedIndexChanged.addHandler(function (s, e) {
        theChart.stacking = s.text;
    });

    document.getElementById('rotated').addEventListener('click', function (e) {
        theChart.rotated = e.target.checked;
    });
});