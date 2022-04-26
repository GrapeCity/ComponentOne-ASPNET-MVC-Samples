c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');

    // generate some random data
    var view = theGrid.collectionView;
    for (var i = 0; i < view.items.length; i++) {
        view.items[i].Rating = Math.round(Math.random() * 5);
    }

    // FlexGridFilter client-side filtering
    var filter = c1.getExtender(theGrid, "theGridFilter");

    // ratings are values from 0 to 5
    var filterRating = filter.getColumnFilter('Rating');
    filterRating.valueFilter.uniqueValues = [0, 1, 2, 3, 4, 5];

    // limit number of values shown in sales filter
    var filterSales = filter.getColumnFilter('Sales');
    filterSales.valueFilter.maxValues = 20;

    // filter expenses only by condition
    var filterExpenses = filter.getColumnFilter('Expenses');
    filterExpenses.filterType = 'Condition';
});