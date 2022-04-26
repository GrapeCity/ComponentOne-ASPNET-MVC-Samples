var ctx = {
    flexSheet: null
};

c1.documentReady(function () {
    var flexSheet;
    ctx.flexSheet = wijmo.Control.getControl('#headerSheet');
    flexSheet = ctx.flexSheet;
    if (flexSheet) {
        for (colIdx = 0; colIdx < flexSheet.columns.length; colIdx++) {
            for (rowIdx = 0; rowIdx < flexSheet.rows.length; rowIdx++) {
                flexSheet.setCellData(rowIdx, colIdx, colIdx + rowIdx);
            }
        }
    }
});

function addRowHeader() {
    var flexSheet = ctx.flexSheet;
    if (flexSheet) {
        flexSheet.rowHeaders.columns.push(new wijmo.grid.Column());
    }
}

function removeRowHeader() {
    var flexSheet = ctx.flexSheet,
        colCnt;
    if (flexSheet) {
        colCnt = flexSheet.rowHeaders.columns.length;
        flexSheet.rowHeaders.columns.removeAt(colCnt - 1);
    }
}

function addColumnHeader() {
    var flexSheet = ctx.flexSheet;
    if (flexSheet) {
        flexSheet.columnHeaders.rows.push(new wijmo.grid.Row());
    }
}

function removeColumnHeader() {
    var flexSheet = ctx.flexSheet,
        rowCnt;
    if (flexSheet) {
        rowCnt = flexSheet.columnHeaders.rows.length;
        flexSheet.columnHeaders.rows.removeAt(rowCnt - 1);
    }
}

