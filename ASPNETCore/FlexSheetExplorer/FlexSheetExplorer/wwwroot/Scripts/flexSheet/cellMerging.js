var ctx = {
    flexSheet: null,
    selectionFormatState: {},
    mergeBtn: null
};

var _unMergeTitle = 'UnMerge';
var _mergeTitle = 'Merge';

c1.documentReady(function () {
    var flexSheet;
    ctx.flexSheet = wijmo.Control.getControl('#mergeSheet');
    ctx.mergeBtn = wijmo.getElement('#mergeBtn');
    flexSheet = ctx.flexSheet;
    if (flexSheet) {
        for (colIdx = 0; colIdx < flexSheet.columns.length; colIdx++) {
            for (rowIdx = 0; rowIdx < flexSheet.rows.length; rowIdx++) {
                flexSheet.setCellData(rowIdx, colIdx, colIdx + rowIdx);
            }
        }
        flexSheet.selectionChanged.addHandler(function () {
            ctx.selectionFormatState = flexSheet.getSelectionFormatState();
            updateBtn();
        });
    }
});

function updateBtnTitle(unMergeTitle, mergeTitle) {
    _unMergeTitle = unMergeTitle;
    _mergeTitle = mergeTitle;
}

function updateBtn() {
    ctx.mergeBtn.innerText = ctx.selectionFormatState.isMergedCell ? _unMergeTitle : _mergeTitle;
}

function mergeCells() {
    var flexSheet = ctx.flexSheet;

    if (flexSheet) {
        flexSheet.mergeRange();
        ctx.selectionFormatState = flexSheet.getSelectionFormatState();
        updateBtn();
    }
}
