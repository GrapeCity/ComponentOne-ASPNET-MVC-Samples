c1.documentReady(function () {
    // generate some random data
    var countries = 'US,Germany,UK,Japan,Italy,Greece'.split(','),
        data = [];
    for (var i = 0; i < 200; i++) {
        data.push({
            id: i,
            country: countries[i % countries.length],
            downloads: Math.round(Math.random() * 20000),
            sales: Math.random() * 10000,
            expenses: Math.random() * 5000,
            num1: Math.random() * 5000,
            num2: Math.random() * 5000,
            num3: Math.random() * 5000,
            num4: Math.random() * 5000,
            num5: Math.random() * 5000,
            num6: Math.random() * 5000,
            num7: Math.random() * 5000,
            num8: Math.random() * 5000,
            num9: Math.random() * 5000,
            num10: Math.random() * 5000,
        });
    }

    var theGrid = wijmo.Control.getControl('#theGrid');
    theGrid.itemsSource = data;
    theGrid.frozenRows = 2;
    theGrid.frozenColumns = 1;

    // toggle frozen rows
    document.getElementById('btnFreezeRows').addEventListener('click', function () {
        theGrid.frozenRows = theGrid.frozenRows > 0 ? 0 : 2;
    });
    document.getElementById('btnFreezeCols').addEventListener('click', function () {
        theGrid.frozenColumns = theGrid.frozenColumns > 0 ? 0 : 1;
    });
});