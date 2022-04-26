c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');

    theGrid.beginningEdit.addHandler(function(s, e) {
        var col = s.columns[e.col];
        if (col.binding != 'Overdue') {
            var item = s.rows[e.row].dataItem;
            if (item.Overdue) { // prevent editing overdue items
                e.cancel = true;
                showMessage('Overdue items cannot be edited');
            }
        }
    });
    theGrid.cellEditEnding.addHandler(function (s, e) {
        showMessage('&nbsp;');
        var col = s.columns[e.col];
        if (col.binding == 'Sales' || col.binding == 'Expenses') {
            var value = wijmo.changeType(s.activeEditor.value, wijmo.DataType.Number, col.format);
            if (!wijmo.isNumber(value) || value < 0) { // prevent negative sales/expenses
                e.cancel = true;
                showMessage('Please enter a positive amount');
            }
        }
    });

    // show log message
    var elLog = document.getElementById('log');
    function showMessage(msg) {
        elLog.innerHTML = msg;
    }
});