c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');
    var theColumn=null;

    // prevent 'sales' column from being dragged to index 0
    theGrid.draggingColumn.addHandler(function (s, e) {
        theColumn = s.columns[e.col].binding;
    });
    theGrid.draggingColumnOver.addHandler(function (s, e) {
        if (theColumn == 'Country') {
            if (e.col == 0 || e.col == s.columns.length - 1) {
                e.cancel = true;
            }
        }
    });
});