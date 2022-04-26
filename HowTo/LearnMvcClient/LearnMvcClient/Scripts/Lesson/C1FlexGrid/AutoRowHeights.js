c1.documentReady(function () {
    // create some random data
    var countries = 'Austria,Belgium,Canada,Denmark,Estonia,Finland,Germany,Hungary,Ireland,Japan,Korea,Lebanon,Mexico,Norway,Portugal,Qatar,Romania,Spain,Turkey,Ukraine,Venezuela,Zaire'.split(',');
    var data = [];
    for (var i = 0; i < 100; i++) {
        data.push({
            id: i,
            countries: getCountries(),
            sales: Math.random() * 10000,
            expenses: Math.random() * 5000
        });
    }
    function getCountries() {
        var c = [];
        var cnt = Math.round(Math.random() * 12) + 1;
        var start = Math.round(Math.random() * countries.length);
        for (var i = 0; i < cnt; i++) {
            c.push(countries[(i + start) % countries.length]);
        }
        return c.join(', ');
    }

    // show the data in a grid with fixed height
    var theGrid = wijmo.Control.getControl('#theGrid');
    theGrid.loadedRows.addHandler(function (s, e) { // rows loaded/reloaded
        setTimeout(function () {
            s.autoSizeRows();
        }, 50);
    });
    theGrid.resizedColumn.addHandler(function (s, e) { // column resized
        if (s.columns[e.col].wordWrap) {
            s.autoSizeRows();
        }
    });
    theGrid.cellEditEnded.addHandler(function (s, e) { // cell edited
        if (s.columns[e.col].wordWrap) {
            s.autoSizeRow(e.row);
        }
    });
    theGrid.rowEditEnded.addHandler(function (s, e) { // whole row undo
        s.autoSizeRow(e.row);
    });

    theGrid.itemsSource = data;
});