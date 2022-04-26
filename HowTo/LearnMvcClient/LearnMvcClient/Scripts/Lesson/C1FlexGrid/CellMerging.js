c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');
    theGrid.allowMerging = 'Cells';
    theGrid.getColumn('Country').allowMerging = true;
    theGrid.getColumn('Active').allowMerging = true;
});