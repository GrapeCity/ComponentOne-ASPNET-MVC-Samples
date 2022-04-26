c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');

    // autosize when pressing button
    document.getElementById('autoSizeColumns').addEventListener('click', function () {
        theGrid.autoSizeColumns();
    })
});