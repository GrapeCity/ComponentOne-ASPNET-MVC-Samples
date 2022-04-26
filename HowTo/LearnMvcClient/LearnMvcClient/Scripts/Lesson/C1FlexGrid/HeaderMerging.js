c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');
    theGrid.allowMerging = 'ColumnHeaders';

    // create extra header row
    var extraRow = new wijmo.grid.Row();
    extraRow.allowMerging = true;

    // add extra header row to the grid
    var panel = theGrid.columnHeaders;
    panel.rows.splice(0, 0, extraRow);

    // populate the extra header row
    for (col = 1; col <= 2; col++) {
        panel.setCellData(0, col, 'Amounts');
    }

    // merge "Country" and "Active" headers vertically
    ['Country', 'Active'].forEach(function (binding) {
        col = theGrid.getColumn(binding);
        col.allowMerging = true;
        panel.setCellData(0, col.index, col.header);
    });

    // center-align merged header cells
    theGrid.formatItem.addHandler(function (s, e) {
        if (e.panel == s.columnHeaders && e.range.rowSpan > 1) {
            var html = e.cell.innerHTML;
            e.cell.innerHTML = '<div class="v-center">' + html + '</div>';
        }
    });
});