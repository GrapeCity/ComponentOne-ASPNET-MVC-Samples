var ctx = {
    flexSheet: null,
    isFrozen: false,
    frozenBtn: null
};

var _unFreezeTitle = 'UnFreeze';
var _freezeTitle = 'Freeze';

c1.documentReady(function () {
    var flexSheet;
    ctx.flexSheet = wijmo.Control.getControl('#frozenSheet');
    ctx.mergeBtn = wijmo.getElement('#frozenBtn');
    flexSheet = ctx.flexSheet;
    if (flexSheet) {
        for (colIdx = 0; colIdx < flexSheet.columns.length; colIdx++) {
            for (rowIdx = 0; rowIdx < flexSheet.rows.length; rowIdx++) {
                flexSheet.setCellData(rowIdx, colIdx, colIdx + rowIdx);
            }
        }
    }
});

function updateBtnTitle(unFreezeTitle, freezeTitle) {
    _unFreezeTitle = unFreezeTitle;
    _freezeTitle = freezeTitle;
}

function updateBtn() {
    ctx.mergeBtn.innerText = ctx.isFrozen ? _unFreezeTitle : _freezeTitle;
}

function freezeCells() {
    var flexSheet = ctx.flexSheet;
    if (flexSheet) {
        flexSheet.freezeAtCursor();
        updateFrozenState();
    }
}

function updateFrozenState() {
    var flexSheet = ctx.flexSheet;
    if (flexSheet) {
        if (ctx.flexSheet.frozenColumns > 0 || ctx.flexSheet.frozenRows > 0) {
            ctx.isFrozen = true;
        } else {
            ctx.isFrozen = false;
        }

        updateBtn();
    }
}
