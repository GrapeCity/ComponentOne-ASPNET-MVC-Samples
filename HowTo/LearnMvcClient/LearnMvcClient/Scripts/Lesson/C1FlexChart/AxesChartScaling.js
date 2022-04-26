c1.documentReady(function () {
    var theChart = wijmo.Control.getControl('#theChart');

    // toggle log scale
    document.getElementById('logScale').addEventListener('click', function (e) {
        var logBase = e.target.checked ? 10 : null;
        theChart.axisX.logBase = logBase;
        theChart.axisY.logBase = logBase;
    });
});