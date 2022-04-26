c1.documentReady(function () {
    // create some random data
    var countries = 'US,Germany,UK,Japan,Sweden,Norway,Denmark'.split(',');
    var customers = [
      { id: 0, name: 'Paul Perkins', address: '123 Main St' },
      { id: 1, name: 'Susan Smith', address: '456 Grand Ave' },
      { id: 2, name: 'Joan Jett', address: '789 Broad St' },
      { id: 3, name: 'Don Davis', address: '321 Shattuck Ave' },
    ];
    var data = [];
    for (var i = 0; i < countries.length; i++) {
        data.push({
            id: i,
            country: countries[i],
            customer: customers[i % customers.length].id,
            active: i % 5 != 0,
            downloads: Math.round(Math.random() * 200000),
            sales: Math.random() * 100000,
            expenses: Math.random() * 50000
        });
    }

    // create customer data map
    var customerMap = new wijmo.grid.DataMap(customers, 'id', 'name');

    // show the data in a grid
    var theGrid = wijmo.Control.getControl('#theGrid');
    theGrid.itemsSource = data;
    theGrid.columns[0].dataMap = customerMap;
    theGrid.columns[1].dataMap = countries;

    // show customers as well
    var theGridCustomers = wijmo.Control.getControl('#theGridCustomers');
    theGridCustomers.itemsSource = customers;
    theGridCustomers.rowEditEnded.addHandler(function (s, e) {
        customerMap.collectionView.refresh(); // update customer names on the main grid
    });
});