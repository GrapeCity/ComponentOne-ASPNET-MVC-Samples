c1.documentReady(function () {
    var theChart = wijmo.Control.getControl('#theChart');

    // toggle the custom range
    var warning = document.getElementById('warning');
    document.getElementById('customRange').addEventListener('click', function (e) {
        if (e.target.checked) {
            theChart.axisY.min = 150000;
            theChart.axisY.max = 160000;
            warning.style.display = '';
        } else {
            theChart.axisY.min = theChart.axisY.max = null;
            warning.style.display = 'none';
        }
    });
});