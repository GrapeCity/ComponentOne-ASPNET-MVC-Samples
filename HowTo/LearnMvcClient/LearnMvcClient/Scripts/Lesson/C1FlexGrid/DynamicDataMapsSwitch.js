c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');
    theGrid.itemsSource = getData();
    theGrid.columns[0].dataMap = getCountries();
    theGrid.columns[1].dataMap = getCities();

    // update cities data map depending on country
    theGrid.beginningEdit.addHandler(function (s, e) {
        var col = s.columns[e.col];
        if (col.binding == 'city') {
            switch (s.rows[e.row].dataItem.country) {
                case 'US':
                    col.dataMap = ['Washington', 'Miami', 'Seattle'];
                    break;
                case 'UK':
                    col.dataMap = ['London', 'Oxford', 'Bath'];
                    break;
                case 'Japan':
                    col.dataMap = ['Tokyio', 'Sendai', 'Kobe'];
                    break;
                case 'Greece':
                    col.dataMap = ['Athens', 'Santorini', 'Thebes'];
                    break;
                case 'Germany':
                    col.dataMap = ['Bonn', 'Munich', 'Kiel'];
                    break;
                case 'Italy':
                    col.dataMap = ['Rome', 'Florence', 'Milan'];
                    break;
                default:
                    col.dataMap = ['Unknown Country!'];
                    break;
            }
        }
    });

    // some random data
    function getCountries() {
        return 'US,Germany,UK,Japan,Italy,Greece'.split(',');
    }
    function getCities() {
        return 'Washington,Bonn,London,Tokyo,Rome,Athens'.split(',');
    }
    function getData() {
        var countries = getCountries(),
          cities = getCities(),
          data = [];
        for (var i = 0; i < countries.length; i++) {
            data.push({
                country: countries[i],
                city: cities[i],
                downloads: Math.round(Math.random() * 20000),
                sales: Math.random() * 10000,
                expenses: Math.random() * 5000
            });
        }
        return data;
    }
});