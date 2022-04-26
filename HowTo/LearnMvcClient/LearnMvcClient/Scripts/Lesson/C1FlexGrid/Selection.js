c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');
    theGrid.selectionChanged.addHandler(function (s, e) {
        currSel.textContent = wijmo.format(
        '({row},{col})-({row2},{col2})',
        theGrid.selection);
    });
    theGrid.onSelectionChanged(); // initialize selection display

    // pick selectionMode
    var selectionMode = wijmo.Control.getControl('#selectionMode');
    selectionMode.textChanged.addHandler(function (s, e) {
        theGrid.selectionMode = selectionMode.text
    });

    // select first four cells in the grid
    document.getElementById('btnSelect').addEventListener('click', function () {
        theGrid.selection = new wijmo.grid.CellRange(0, 0, 1, 1);
    });

    // select rows 0, 2, and 4
    document.getElementById('btnListSelect').addEventListener('click', function () {
        selectionMode.text = 'ListBox';
        [0, 2, 4].forEach(function (index) {
            theGrid.rows[index].isSelected = true;
        })
    });
});