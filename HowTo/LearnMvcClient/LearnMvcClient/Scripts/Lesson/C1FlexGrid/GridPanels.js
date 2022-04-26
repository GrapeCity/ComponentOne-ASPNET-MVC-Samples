c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');

    // add a footer row to show all panels
    theGrid.columnFooters.rows.push(new wijmo.grid.GroupRow());
    theGrid.bottomLeftCells.setCellData(0, 0, 'Σ');

    // use tooltip to show hit-test information
    var tt = new wijmo.Tooltip();
    var tip = '';
    theGrid.hostElement.addEventListener('mousemove', function (e) {
        var ht = theGrid.hitTest(e);
        if (ht.panel) {
            var newTip = wijmo.format('cellType: <b>{panel}</b><br/>row: <b>{row}</b><br/>column: <b>{col}</b><br>value: <b>{val}</b>', {
                panel: wijmo.grid.CellType[ht.cellType],
                row: ht.row,
                col: ht.col,
                val: ht.panel.getCellData(ht.row, ht.col, true)
            });
            if (newTip != tip) {
                tip = newTip;
                tt.show(ht.panel.hostElement.parentElement, tip, ht.panel.getCellBoundingRect(ht.row, ht.col));
            }
        } else {
            tt.hide();
            tip = '';
        }
    });
    theGrid.hostElement.addEventListener('mouseleave', function () {
        tt.hide();
        tip = '';
    })

    // show the effect of the headersVisibility property
    var headersVisibility = wijmo.Control.getControl('#headersVisibility');
    headersVisibility.selectedIndexChanged.addHandler(function (s, e) {
        theGrid.headersVisibility = s.text;
    });

    // add a 'grid-panel' class to all grid panel hosts
    var panels = 'topLeftCells,columnHeaders,rowHeaders,cells,bottomLeftCells,columnFooters'.split(',');
    panels.forEach(function (p) {
        theGrid[p].hostElement.parentElement.classList.add('grid-panel');
    });
});