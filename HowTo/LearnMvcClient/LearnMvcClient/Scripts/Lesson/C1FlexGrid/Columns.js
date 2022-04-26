c1.documentReady(function () {
    var cv = c1.getService("collectionView");

    // grid with extra fixed column and auto-generated scrollable columns
    var theFirstGrid = wijmo.Control.getControl('#theFirstGrid');
    theFirstGrid.rowHeaders.columns.push(new wijmo.grid.Column); // extra fixed column
    theFirstGrid.itemsSource = cv.items; // auto-generate scrollable columns

    // grid with no fixed columns and custom scrollable columns
    var theSecondGrid = wijmo.Control.getControl('#theSecondGrid');
    theSecondGrid.rowHeaders.columns.splice(0, 1); // no extra columns
    theSecondGrid.autoGenerateColumns = false; // custom scrollable columns
    theSecondGrid.itemsSource = cv.items;
    var cols = theSecondGrid.columns;
    cols.push(new wijmo.grid.Column({ binding: 'Country', header: 'Country' }));
    cols.push(new wijmo.grid.Column({ binding: 'Sales', header: 'Sales' }));
    cols.push(new wijmo.grid.Column({ binding: 'Expenses', header: 'Expenses' }));
});