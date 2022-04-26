c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');

    // populate column picker
    var cols = [];
    theGrid.columns.forEach(function (col) {
        cols.push({
            header: col.header,
            binding: col.binding,
            visible: true
        });
    });

    var columnPicker = wijmo.Control.getControl('#columnPicker');
    columnPicker.itemsSource = cols;
    columnPicker.checkedItemsChanged.addHandler(function (s, e) {
        cols.forEach(function (item) {
            theGrid.getColumn(item.binding).visible = item.visible;
        })
    });

    // save/restore layout buttons
    document.getElementById('btnSave').addEventListener('click', function() {
        localStorage.setItem('myLayout', theGrid.columnLayout);
    });
    document.getElementById('btnRestore').addEventListener('click', function() {
        var layout = localStorage.getItem('myLayout');
        if (layout) {
            theGrid.columnLayout = layout;
        }
    });
});