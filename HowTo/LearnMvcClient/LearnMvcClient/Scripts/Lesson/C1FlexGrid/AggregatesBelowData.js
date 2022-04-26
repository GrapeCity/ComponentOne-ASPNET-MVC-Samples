c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');

    theGrid.getColumn('Sales').aggregate = 'Sum';
    theGrid.getColumn('Expenses').aggregate = 'Sum';

    theGrid.columnFooters.rows.push(new wijmo.grid.GroupRow());
    theGrid.bottomLeftCells.setCellData(0, 0, 'Σ');
});