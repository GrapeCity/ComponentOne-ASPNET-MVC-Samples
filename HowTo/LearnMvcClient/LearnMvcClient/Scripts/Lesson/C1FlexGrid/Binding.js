c1.documentReady(function () {
    var cv = c1.getService("collectionView");
    var data = cv.items;

    // auto-generated columns
    var theGridAutoGen = wijmo.Control.getControl('#theGridAutoGen');
    theGridAutoGen.itemsSource = data;

    // explicit columns 
    var theGridExplicitCols = wijmo.Control.getControl('#theGridExplicitCols');
    theGridExplicitCols.initialize({
        autoGenerateColumns: false,
        columns: [
            { binding: 'Country', header: 'Country', width: '*' },
            { binding: 'Downloads', header: 'Downloads (k)', format: 'n0,' }, // thousands
            { binding: 'Sales', header: 'Sales', format: 'n2' },
            { binding: 'Expenses', header: 'Expenses', format: 'n2' },
            { binding: 'Active', header: 'Active' }
        ],
        itemsSource: data,
    });
});