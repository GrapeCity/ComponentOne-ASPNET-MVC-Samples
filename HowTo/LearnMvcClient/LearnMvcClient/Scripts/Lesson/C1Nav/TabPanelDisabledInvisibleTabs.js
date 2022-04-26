c1.documentReady(function () {
    var theTabPanel = wijmo.Control.getControl('#theTabPanel');

    // toggle Europe's disabled state
    document.getElementById('disableEurope').addEventListener('click', function (e) {
        theTabPanel.getTab('tab-europe').isDisabled = e.target.checked;
    });

    // toggle Oceania's visibility
    document.getElementById('showOceania').addEventListener('click', function (e) {
        theTabPanel.getTab('tab-oceania').isVisible = e.target.checked;
    });
});