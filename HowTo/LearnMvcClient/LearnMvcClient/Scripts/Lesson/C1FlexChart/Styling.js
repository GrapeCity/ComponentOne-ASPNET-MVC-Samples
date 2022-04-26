c1.documentReady(function () {
    var theChart = wijmo.Control.getControl('#theChart');

    // toggle custom styles
    document.getElementById('customCSS').addEventListener('click', function (e) {
        wijmo.toggleClass(theChart.hostElement, 'custom-chart', e.target.checked);
    });
});