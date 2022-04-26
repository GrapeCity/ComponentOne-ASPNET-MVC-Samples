// Frozen Cells
var ctxfrozenCells = {
    flexSheet: null,
    isFrozen: false,
    frozenBtn: null
};

function loadFrozenCells() {
    var flexSheet;
    ctxfrozenCells.flexSheet = wijmo.Control.getControl('#frozenSheet');
    ctxfrozenCells.mergeBtn = wijmo.getElement('#frozenBtn');
    flexSheet = ctxfrozenCells.flexSheet;
    if (flexSheet) {
        for (colIdx = 0; colIdx < flexSheet.columns.length; colIdx++) {
            for (rowIdx = 0; rowIdx < flexSheet.rows.length; rowIdx++) {
                flexSheet.setCellData(rowIdx, colIdx, colIdx + rowIdx);
            }
        }
    }
};

function frozenCellsUpdateBtn() {
    ctxfrozenCells.mergeBtn.innerText = ctxfrozenCells.isFrozen ? 'UnFreeze' : 'Freeze';
}

function freezeCells() {
    var flexSheet = ctxfrozenCells.flexSheet;
    if (flexSheet) {
        flexSheet.freezeAtCursor();
        frozenSheet_updateFrozenState();
    }
}

function frozenSheet_updateFrozenState() {
    var flexSheet = ctxfrozenCells.flexSheet;
    if (flexSheet) {
        if (ctxfrozenCells.flexSheet.frozenColumns > 0 || ctxfrozenCells.flexSheet.frozenRows > 0) {
            ctxfrozenCells.isFrozen = true;
        } else {
            ctxfrozenCells.isFrozen = false;
        }

        frozenCellsUpdateBtn();
    }
}
