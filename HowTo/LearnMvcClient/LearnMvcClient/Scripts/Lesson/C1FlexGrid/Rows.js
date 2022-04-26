c1.documentReady(function () {
    // grid with extra fixed row and a footer row
    var theGrid = wijmo.Control.getControl('#theGrid');
    theGrid.columnHeaders.rows.push(new wijmo.grid.Row()); // add a header row
    theGrid.columnFooters.rows.push(new wijmo.grid.Row()); // add a footer row
    theGrid.bottomLeftCells.setCellData(0, 0, 'x'); // show some data on the first cell
});