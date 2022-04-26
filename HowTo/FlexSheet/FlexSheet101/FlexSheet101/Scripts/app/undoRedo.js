// Undo/Redo
var ctxuredoSheet = {
    flexSheet: null,
    undoStack: null
};


function loadUndoRedo() {
    var flexSheet;
    ctxuredoSheet.flexSheet = wijmo.Control.getControl('#uredoSheet');
    flexSheet = ctxuredoSheet.flexSheet;
    flexSheet.deferUpdate(function () {
        var colIdx,
            rowIdx;
        
        ctxuredoSheet.undoStack = flexSheet.undoStack;
        // initialize the dataMap for the bound sheet.
        if (flexSheet) {
            for (colIdx = 0; colIdx < flexSheet.columns.length; colIdx++) {
                for (rowIdx = 0; rowIdx < flexSheet.rows.length; rowIdx++) {
                    flexSheet.setCellData(rowIdx, colIdx, colIdx + rowIdx);
                }
            }
        }
    });
};

// Excutes undo command.
function undoFunc() {
    if (ctxuredoSheet.flexSheet)
        ctxuredoSheet.flexSheet.undo();
};

// Excutes redo command.
function redoFunc() {
    if (ctxuredoSheet.flexSheet)
        ctxuredoSheet.flexSheet.redo();
};