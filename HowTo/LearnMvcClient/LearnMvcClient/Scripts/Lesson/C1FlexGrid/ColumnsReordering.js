c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');

    // prevent reordering the first column
    theGrid.getColumn('Id').allowDragging = false;
});