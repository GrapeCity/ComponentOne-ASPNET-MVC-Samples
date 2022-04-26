c1.documentReady(function () {
    // create some random data
    var countries = 'US,Germany,UK,Japan,Italy,Greece'.split(','),
      data = [];
    for (var i = 0; i < 200; i++) {
        data.push({
            id: i,
            country: countries[i % countries.length],
            active: i % 5 != 0,
            downloads: Math.round(Math.random() * 200000),
            sales: Math.random() * 100000,
            expenses: Math.random() * 50000
        });
    }

    // default grid
    var gridDefault = wijmo.Control.getControl('#gridDefault');
    gridDefault.itemsSource = data;

    // customize columns
    var gridCols = wijmo.Control.getControl('#gridCols');
    gridCols.initialize({
        autoGenerateColumns: false,
        itemsSource: data,
        columns: [
            { header: 'Country', binding: 'country', width: '*' },
            { header: 'Downloads (k)', binding: 'downloads', format: 'n1,' },
            { header: 'Sales (k)', binding: 'sales', format: 'n1,' },
            { header: 'Expenses (k)', binding: 'expenses', format: 'c1,' },
        ]
    });

    // customize properties
    var gridProps = wijmo.Control.getControl('#gridProps');
    gridProps.itemsSource = data;

    var selectionMode = wijmo.Control.getControl('#selectionMode');
    selectionMode.selectedIndexChanged.addHandler(function () {
        gridProps.selectionMode = selectionMode.text
    });
    var allowSorting = wijmo.Control.getControl('#allowSorting');
    allowSorting.selectedIndexChanged.addHandler(function () {
        gridProps.allowSorting = allowSorting.text == 'True';
    });
    var allowDragging = wijmo.Control.getControl('#allowDragging');
    allowDragging.selectedIndexChanged.addHandler(function () {
        gridProps.allowDragging = allowDragging.text;
    });
    var isReadOnly = wijmo.Control.getControl('#isReadOnly');
    isReadOnly.selectedIndexChanged.addHandler(function () {
        gridProps.isReadOnly = isReadOnly.text == 'True';
    });
    var headersVisibility = wijmo.Control.getControl('#headersVisibility');
    headersVisibility.selectedIndexChanged.addHandler(function () {
        gridProps.headersVisibility = headersVisibility.text;
    });
});