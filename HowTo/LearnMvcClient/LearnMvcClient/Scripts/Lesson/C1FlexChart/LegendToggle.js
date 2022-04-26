c1.documentReady(function () {
    var theChart = wijmo.Control.getControl('#theChart');

    // change legendToggle
    document.getElementById('legendToggle').addEventListener('click', function (e) {
        theChart.legendToggle = e.target.checked;
    })
});