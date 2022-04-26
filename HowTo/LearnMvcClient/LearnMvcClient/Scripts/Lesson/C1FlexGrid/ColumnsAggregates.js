c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');
    theGrid.getColumn('Sales').aggregate = 'Sum';
    theGrid.getColumn('Expenses').aggregate = 'Sum';
});