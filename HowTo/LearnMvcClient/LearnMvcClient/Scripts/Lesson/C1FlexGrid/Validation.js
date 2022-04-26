c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');
    var countries = 'US,Germany,UK,Japan,Sweden,Norway,Denmark'.split(',');

    theGrid.columns[1].dataMap = countries;
    theGrid.cellEditEnding.addHandler(function(s, e) {
        var col = s.columns[e.col];
        if (col.binding == 'Sales' || col.binding == 'Expenses') {
            var value = wijmo.changeType(s.activeEditor.value, wijmo.DataType.Number, col.format);
            if (!wijmo.isNumber(value) || value < 0) {
                e.cancel = true;
                alert('Please enter a positive amount.')
            }
        }
    });
});