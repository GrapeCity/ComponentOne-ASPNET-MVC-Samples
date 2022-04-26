c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');
    var elLog = document.getElementById('log');
    var placeholder = elLog.innerHTML;

    theGrid.selectionChanged.addHandler(function (s, e) {
        var html = placeholder;
        if (!theGrid.selection.isSingleCell) {
            var stats = getSelectionStats(theGrid);
            var fmt = stats.sum != null
                ? 'Avg: <b>{avg:n2}</b>, Count: <b>{cnt:n0}</b>, Sum: <b>{sum:n2}</b>'
              : 'Count: {cnt:n2}';
            html = wijmo.format(fmt, stats);
        }
        elLog.innerHTML = html;
    });

    // get statistics for the current selection
    function getSelectionStats(grid) {
        var sel = grid.selection,
                cnt = 0,
            ncnt = 0,
            sum = 0;
        for (var r = sel.topRow; r <= sel.bottomRow; r++) {
            for (var c = sel.leftCol; c <= sel.rightCol; c++) {
                var val = grid.cells.getCellData(r, c, false);
                if (val != null) {
                    cnt++;
                    if (wijmo.isNumber(val)) {
                        ncnt++;
                        sum += val;
                    }
                }
            }
        }
        return {
            cnt: cnt,
            sum: ncnt > 0 ? sum : null,
            avg: ncnt > 0 ? sum / ncnt : null
        }
    }
});