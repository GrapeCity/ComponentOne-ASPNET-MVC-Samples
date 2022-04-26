var ctx = {
    flexSheet: null,
    fillingRange: null,
    selection: null,
    fillingState: null,
    stateSpans: null
};

c1.documentReady(function () {
    ctx.flexSheet = wijmo.Control.getControl('#autoFillSheet');
    ctx.fillingState = document.getElementById("fillingState");
    ctx.stateSpans = ctx.fillingState.querySelectorAll("span");
    filling(new wijmo.grid.CellRange(1, 1, 2, 3), 5);
    ctx.flexSheet.setCellData(2, 2, 6);
    ctx.flexSheet.setCellData(2, 3, 7);
    
    ctx.flexSheet.hostElement.addEventListener("mousedown", function (e) {
        ctx.selection = ctx.flexSheet.selection;
    });
});

function filling(range, value) {
    for (var r = range.row; r < range.row2 + 1; r++)
        for (var c = range.col; c < range.col2 + 1; c++) {
            ctx.flexSheet.setCellData(r, c, value);
        }
    ctx.flexSheet.selection = range;
}

function sumRange(range) {
    if (!range) return 0;
    var sum = 0;
    for (var r = range.row; r < range.row2 + 1; r++)
        for (var c = range.col; c < range.col2 + 1; c++) {
            var v = ctx.flexSheet.getCellData(r, c, false);
            if (v && !isNaN(v)) sum += parseFloat(v);      
        }
    return sum;
}

function styleRange(range, color) {
    if (!range) return;
    if (color)
        ctx.flexSheet.applyCellsStyle({ backgroundColor: color }, [range]);
    else
        ctx.flexSheet.applyCellsStyle({ backgroundColor: "none" }, [range]);
}

function getColChar(col) {
    return String.fromCharCode('A'.charCodeAt(0) + col);
}

function getAddress(r) {
    return "(" + getColChar(r.col) + (r.row + 1) + ":" + getColChar(r.col2) + (r.row2 + 1) + ")";
}

function _onAutoFilling(sender, args) {
    styleRange(ctx.fillingRange);
    ctx.fillingRange = args.range;
    ctx.stateSpans[0].innerHTML = getAddress(args.range);
    ctx.stateSpans[1].innerHTML = sumRange(args.range);
}

function _onAutoFilled(sender, args) {
    styleRange(ctx.fillingRange, "#d2ffd2");
    styleRange(ctx.selection, "#a5ffa5");
    ctx.stateSpans[2].innerHTML = sumRange(args.range);
}