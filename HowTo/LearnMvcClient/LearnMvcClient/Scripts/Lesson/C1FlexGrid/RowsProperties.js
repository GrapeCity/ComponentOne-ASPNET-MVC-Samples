c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');
    theGrid.loadedRows.addHandler(function () {
        if (hideRows) {
            theGrid.rows.forEach(function (row, index) {
                row.visible = index % 2 == 0;
            });
        }
    });

    // toggle row visibility
    var hideRows = false;
    document.getElementById('btnToggleVis').addEventListener('click', function (e) {
        hideRows = !hideRows;
        theGrid.collectionView.refresh(); // force row re-load
    });
});