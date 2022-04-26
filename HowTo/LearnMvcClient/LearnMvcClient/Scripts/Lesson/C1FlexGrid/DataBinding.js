c1.documentReady(function () {
    // the unbound grid with five rows and three columns
    var unboundGrid = wijmo.Control.getControl('#unboundGrid');
    for (var r = 0; r < 5; r++) {
        unboundGrid.rows.push(new wijmo.grid.Row());
    }
    for (var c = 0; c < 3; c++) {
        unboundGrid.columns.push(new wijmo.grid.Column({
            header: 'col ' + c
        }));
    }
    unboundGrid.setCellData(0, 0, 'This grid is unbound...');

    // create some random data
    var countries = 'US,Germany,UK,Japan,Italy,Greece'.split(',');
    var data = [];
    for (var i = 0; i < countries.length; i++) {
        data.push({
            id: i,
            country: countries[i],
            sales: Math.random() * 10000,
            expenses: Math.random() * 5000
        });
    }

    // automatically generated columns
    var autoBoundGrid = wijmo.Control.getControl('#autoBoundGrid');
    autoBoundGrid.itemsSource = data;

    // explicit columns
    var boundGrid = wijmo.Control.getControl('#boundGrid');
    boundGrid.autoGenerateColumns = false;
    boundGrid.columns.push(new wijmo.grid.Column({ binding: 'country', header: 'Country', width: '2*' }));
    boundGrid.columns.push(new wijmo.grid.Column({ binding: 'sales', header: 'Sales', width: '*', format: 'n0' }));
    boundGrid.columns.push(new wijmo.grid.Column({ binding: 'expenses', header: 'Expenses', width: '*', format: 'n0' }));
    boundGrid.itemsSource = data;
});