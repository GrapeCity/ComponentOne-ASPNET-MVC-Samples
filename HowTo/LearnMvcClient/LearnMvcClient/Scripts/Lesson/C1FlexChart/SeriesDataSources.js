c1.documentReady(function () {
    var theChart = wijmo.Control.getControl('#theChart');

    // toggle axis origin
    document.getElementById('axisOrigin').addEventListener('click', function (e) {
        var origin = e.target.checked ? 0 : null;
        theChart.axisX.origin = origin;
        theChart.axisY.origin = origin;
    });
});