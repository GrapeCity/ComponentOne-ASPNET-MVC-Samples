c1.documentReady(function () {
    // basic grouping
    var theGrid = wijmo.Control.getControl('#theGrid');
    theGrid.collectionView.groupDescriptions.push(new wijmo.collections.PropertyGroupDescription("Country"));
    theGrid.collectionView.groupDescriptions.push(new wijmo.collections.PropertyGroupDescription("Product"));

    // select first item (after sorting/grouping)
    theGrid.select(new wijmo.grid.CellRange(0, 0), true);

    var theGridHideCols = wijmo.Control.getControl('#theGridHideCols');
    theGridHideCols.getColumn('Country').visible = false;
    theGridHideCols.getColumn('Product').visible = false;
    theGridHideCols.collectionView.groupDescriptions.push(new wijmo.collections.PropertyGroupDescription("Country"));
    theGridHideCols.collectionView.groupDescriptions.push(new wijmo.collections.PropertyGroupDescription("Product"));

    // select first item (after sorting/grouping)
    theGridHideCols.select(new wijmo.grid.CellRange(0, 2), true);
});