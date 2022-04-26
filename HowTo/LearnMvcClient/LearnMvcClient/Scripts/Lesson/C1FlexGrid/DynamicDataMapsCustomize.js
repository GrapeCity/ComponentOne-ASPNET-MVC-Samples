c1.documentReady(function () {
    // create data maps
    var countryMap = new wijmo.grid.DataMap(getCountries(), 'id', 'name');
    var cityMap = new wijmo.grid.DataMap(getCities(), 'id', 'name');

    // override cityMap's getDisplayValues method to get cities that
    // correspond to the current item
    cityMap.getDisplayValues = function (dataItem) {
        var arr = [],
            cities = getCities(),
            country = theGrid.collectionView.currentItem.country;
        for (var i = 0; i < cities.length; i++) {
            var city = cities[i];
            if (city.country == country) {
                arr.push(city.name);
            }
        }
        return arr;
    };

    var theGrid = wijmo.Control.getControl('#theGrid');
    theGrid.itemsSource = getData();
    theGrid.columns[0].dataMap = countryMap;
    theGrid.columns[1].dataMap = cityMap;

    // create some random data
    function getData() {
        var countries = getCountries(),
          cities = getCities(),
        data = [];
        for (var i = 0; i < cities.length; i++) {
            data.push({
                country: cities[i].country,
                city: cities[i].id,
                downloads: Math.round(Math.random() * 20000),
                sales: Math.random() * 10000,
                expenses: Math.random() * 5000
            });
        }
        return data;
    }
    function getCountries() {
        return [
            { id: 0, name: 'US' },
            { id: 1, name: 'Germany' },
            { id: 2, name: 'UK' },
            { id: 3, name: 'Japan' },
            { id: 4, name: 'Italy' },
            { id: 5, name: 'Greece' }
        ];
    }
    function getCities() {
        return [
            { id: 0, country: 0, name: 'Washington' },
            { id: 1, country: 0, name: 'Miami' },
            { id: 2, country: 0, name: 'Seattle' },
            { id: 3, country: 1, name: 'Bonn' },
            { id: 4, country: 1, name: 'Munich' },
            { id: 5, country: 1, name: 'Berlin' },
            { id: 6, country: 2, name: 'London' },
            { id: 7, country: 2, name: 'Oxford' },
            { id: 8, country: 2, name: 'Bath' },
            { id: 9, country: 3, name: 'Tokyo' },
            { id: 10, country: 3, name: 'Sendai' },
            { id: 11, country: 3, name: 'Kobe' },
            { id: 12, country: 4, name: 'Rome' },
            { id: 13, country: 4, name: 'Florence' },
            { id: 14, country: 4, name: 'Milan' },
            { id: 15, country: 5, name: 'Athens' },
            { id: 16, country: 5, name: 'Santorini' },
            { id: 17, country: 5, name: 'Thebes' }
        ];
    }
});