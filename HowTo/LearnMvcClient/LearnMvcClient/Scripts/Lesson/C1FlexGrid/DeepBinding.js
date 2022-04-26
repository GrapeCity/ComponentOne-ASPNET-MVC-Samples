c1.documentReady(function () {
    // create some random data
    var countries = 'US,Germany,UK,Japan,Sweden,Norway,Denmark'.split(',');
    var customers = [
      { id: 0, first: 'Paul', last: 'Smith' },
      { id: 1, first: 'Susan', last: 'Johnson' },
      { id: 2, first: 'Joan', last: 'Roberts' },
      { id: 3, first: 'Don', last: 'Davis' }
    ];
    var data = [];
    for (var i = 0; i < countries.length; i++) {
        data.push({
            id: i,
            country: countries[i],
            customer: customers[i % customers.length],
            active: i % 5 != 0,
            downloads: Math.round(Math.random() * 200000),
            sales: Math.random() * 100000,
            expenses: Math.random() * 50000
        });
    }

    var theGrid = wijmo.Control.getControl('#theGrid');
    theGrid.initialize({
        autoGenerateColumns: false,
        columns: [
            { binding: 'country', header: 'Country', width: '*' },
          { binding: 'customer.first', header: 'First', width: '*' },
          { binding: 'customer.last', header: 'Last', width: '*' },
          { binding: 'downloads', header: 'Downloads (k)', format: 'n0,' }, // thousands
        ],
        itemsSource: data,
    });

    // save deep bound values when editing starts
    var itemData = {};
    theGrid.rowEditStarted.addHandler(function (s, e) {
        var item = s.collectionView.currentEditItem;
        itemData = {};
        s.columns.forEach(function (col) {
            if (col.binding.indexOf('.') > -1) {
                var binding = new wijmo.Binding(col.binding);
                itemData[col.binding] = binding.getValue(item);
            }
        })
    });

    // restore deep bound values when edits are canceled
    theGrid.rowEditEnding.addHandler(function (s, e) {
        if (e.cancel && document.getElementById('enableDeepUndo').checked) {
            var item = s.collectionView.currentEditItem;
            // var item = s.rows[e.row].dataItem; // same thing
            for (var k in itemData) {
                var binding = new wijmo.Binding(k);
                binding.setValue(item, itemData[k]);
            }
        }
        itemData = {};
    });
});