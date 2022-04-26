c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');

    // add 'button' to country cells
    theGrid.formatItem.addHandler(function (s, e) {
        if (e.panel == s.cells) {
            if (s.columns[e.col].binding == 'Country') {
                var html = '<span class="my-button">&#x2b24;</span> ' + e.cell.innerHTML;
                e.cell.innerHTML = html;
            }
        }
    });

    // monitor and log mouse moves
    var logEl = document.getElementById('log');
    theGrid.addEventListener(theGrid.hostElement, 'mousemove', function (e) {
        var ht = theGrid.hitTest(e);
        var logText = wijmo.format('panel <b>{cellType}</b> row <b>{row}</b> col <b>{col}</b>', {
            cellType: wijmo.grid.CellType[ht.cellType],
            row: ht.row,
            col: ht.col
        });
        if (e.target.classList.contains('my-button')) {
            logText += ' (fake button!)';
        } else if (e.target.tagName == 'INPUT' && e.target.type == 'checkbox') {
            logText += ' (checkbox!)';
        } else if (ht.panel == theGrid.cells) {
            if (theGrid.rows[ht.row] instanceof wijmo.grid.GroupRow) {
                logText += ' (group row)';
            } else {
                logText += ' (value: <b>' + theGrid.cells.getCellData(ht.row, ht.col, true) + '</b>)';
            }
        }
        logEl.innerHTML = logText;
    });
});