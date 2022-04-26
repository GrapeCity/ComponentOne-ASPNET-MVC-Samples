c1.documentReady(function () {
    var theChart = wijmo.Control.getControl('#theChart');

    // save chart image to file
    document.getElementById('saveButtons').addEventListener('click', function (e) {
        if (e.target instanceof HTMLButtonElement) {
            theChart.saveImageToFile('FlexChart.' + e.target.textContent);
        }
    });
});