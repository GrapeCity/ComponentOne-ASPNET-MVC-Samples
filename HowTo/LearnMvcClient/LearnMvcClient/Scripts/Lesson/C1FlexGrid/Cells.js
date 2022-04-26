c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');
    theGrid.formatItem.addHandler(function (s, e) {
        if (e.panel == s.cells && s.columns[e.col].binding == 'Sales') {
            var item = s.rows[e.row].dataItem;
            wijmo.toggleClass(e.cell, 'low', item.Sales < 10000);
            wijmo.toggleClass(e.cell, 'high', item.Sales > 50000);
        }
    });

    // demonstrate the getCellData method
    document.getElementById('getCellData').addEventListener('click', function () {
        var sel = theGrid.selection;
        var raw = theGrid.cells.getCellData(sel.row, sel.col, false);
        var formatted = theGrid.cells.getCellData(sel.row, sel.col, true);
        alert('The selected cell contains this data: ' + raw + '\n' +
                  'which is formatted as "' + formatted + '"')
    });
});