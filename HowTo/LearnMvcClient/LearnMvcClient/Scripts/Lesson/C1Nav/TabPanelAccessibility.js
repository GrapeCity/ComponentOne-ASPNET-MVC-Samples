c1.documentReady(function () {
    var theTabPanel = wijmo.Control.getControl('#rtlTabPanel');

    // toggle autoSwitch property
    document.getElementById('autoSwitch').addEventListener('click', function (e) {
        theTabPanel.autoSwitch = e.target.checked;
    });
});