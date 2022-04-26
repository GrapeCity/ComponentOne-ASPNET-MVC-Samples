c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');
    theGrid.deferResizing = true;
    theGrid.columns[0].allowResizing = false;
});