c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');
    theGrid.beginningEdit.addHandler(function (s, e) {
        var item = s.rows[e.row].dataItem;
        if (item.Overdue) { // prevent editing overdue items
            e.cancel = true;
        }
    });
});