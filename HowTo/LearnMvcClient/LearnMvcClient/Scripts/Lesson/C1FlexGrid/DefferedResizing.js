c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');
    theGrid.deferResizing = true;

    // toggle the deferResizing property
    document.getElementById('deferResizing').addEventListener('click', function (e) {
        theGrid.deferResizing = e.target.checked;
    });
});