c1.documentReady(function () {
    // create some random data
    var countries = 'Austria,Belgium,Canada,Denmark,Estonia,Finland,Germany,Hungary,Ireland,Japan,Korea,Lebanon,Mexico,Norway,Portugal,Qatar,Romania,Spain,Turkey,Ukraine,Venezuela,Zaire'.split(',');
    var data = [];
    for (var i = 0; i < 10; i++) {
        data.push({
            id: i,
            countries: getCountries(),
            sales: Math.random() * 10000,
            expenses: Math.random() * 5000
        });
    }
    function getCountries() {
        var c = [];
        var cnt = Math.round(Math.random() * 2) + 1;
        var start = Math.round(Math.random() * countries.length);
        for (var i = 0; i < cnt; i++) {
            c.push(countries[(i + start) % countries.length]);
        }
        return c.join(', ');
    }

    // show the data in a grid with fixed height
    var theGrid = wijmo.Control.getControl('#theGrid');
    theGrid.loadedRows.addHandler(function (s, e) { // rows loaded/reloaded
        s.autoSizeColumns();
    });
    theGrid.cellEditEnded.addHandler(function (s, e) { // cell edited
        s.autoSizeColumn(e.col);
    });
    theGrid.rowEditEnded.addHandler(function (s, e) { // whole row undo
        s.autoSizeColumns();
    });

    theGrid.itemsSource = data;
});