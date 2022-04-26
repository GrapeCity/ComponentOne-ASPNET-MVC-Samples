c1.documentReady(function () {
    var theChart = wijmo.Control.getControl('#theChart');

    // reverse Y axis
    document.getElementById('reverseY').addEventListener('click', function (e) {
        theChart.axisY.reversed = e.target.checked;
    });
});