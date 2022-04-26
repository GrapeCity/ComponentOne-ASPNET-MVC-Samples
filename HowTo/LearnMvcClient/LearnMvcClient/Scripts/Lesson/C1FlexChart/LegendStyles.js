c1.documentReady(function () {
    var theChart = wijmo.Control.getControl('#theChart');

    // toggle custom styles
    document.getElementById('customLegend').addEventListener('click', function (e) {
        wijmo.toggleClass(theChart.hostElement, 'custom-legend', e.target.checked)
    });
});