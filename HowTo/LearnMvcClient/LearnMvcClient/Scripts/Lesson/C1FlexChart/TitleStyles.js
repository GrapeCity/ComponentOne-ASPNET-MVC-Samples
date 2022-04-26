c1.documentReady(function () {
    var theChart = wijmo.Control.getControl('#theChart');

    // toggle custom styles
    document.getElementById('customTitles').addEventListener('click', function (e) {
        wijmo.toggleClass(theChart.hostElement, 'custom-titles', e.target.checked)
    });
});