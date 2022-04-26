var flexGrid = null;

function initialControls() {
    CreateCustomMergeManager(wijmo);
    flexGrid = wijmo.Control.getControl("#flexGrid");
    initCustomMerge();

    // center-align cells vertically and horizontally
    flexGrid.formatItem.addHandler(function (s, e) {
        if (e.cell.children.length == 0) {
            e.cell.innerHTML = '<div>' + e.cell.innerHTML + '</div>';
            wijmo.setCss(e.cell, {
                display: 'table',
                tableLayout: 'fixed'
            });
            wijmo.setCss(e.cell.children[0], {
                display: 'table-cell',
                textAlign: 'center',
                verticalAlign: 'middle'
            });
        }
    });    
}

function initCustomMerge() {
   flexGrid.mergeManager = new wijmo.grid.CustomMergeManager();

    // create rows and columns
    while (flexGrid.columns.length < 7) {
        flexGrid.columns.push(new wijmo.grid.Column());
    }
    while (flexGrid.rows.length < 5) {
        flexGrid.rows.push(new wijmo.grid.Row());
    }
    setData(flexGrid.columnHeaders, 0, ',Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday'.split(','));
    flexGrid.rowHeaders.columns[0].width = 80;
    flexGrid.rows.defaultSize = 40;
    flexGrid.isReadOnly = true;

    // add data
    setData(flexGrid.cells, 0, '12:00,Walker,Morning Show,Morning Show,Sport,Weather,N/A,N/A'.split(','));
    setData(flexGrid.cells, 1, '13:00,Today Show,Today Show,Sesame Street,Football,Market Watch,N/A,N/A'.split(','));
    setData(flexGrid.cells, 2, '14:00,Today Show,Today Show,Kid Zone,Football,Soap Opera,N/A,N/A'.split(','));
    setData(flexGrid.cells, 3, '15:00,News,News,News,News,News,N/A,N/A'.split(','));
    setData(flexGrid.cells, 4, '16:00,News,News,News,News,News,N/A,N/A'.split(','));
};

function setData(p, r, cells) {

    // first element in row header
    if (p.cellType == wijmo.grid.CellType.Cell) {
        p.grid.rowHeaders.setCellData(r, 0, cells[0]);
    }

    // other elements in row
    for (var i = 1; i < cells.length; i++) {
        p.setCellData(r, i - 1, cells[i]);
    }
}