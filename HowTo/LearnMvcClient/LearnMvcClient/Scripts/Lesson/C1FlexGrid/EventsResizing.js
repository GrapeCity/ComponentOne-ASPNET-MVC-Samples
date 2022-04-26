c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');
    var tt = new wijmo.Tooltip();

    theGrid.resizingColumn.addHandler(function(s, e) {
        var tip = wijmo.format('Column <b>{col}</b>, width <b>{wid:n0}px</b>', {
            col: s.columns[e.col].header,
            wid: s.columns[e.col].width
        })
        tt.show(theGrid.hostElement, tip);
    });
    theGrid.resizedColumn.addHandler(function (s, e) {
        tt.hide();
    });
});