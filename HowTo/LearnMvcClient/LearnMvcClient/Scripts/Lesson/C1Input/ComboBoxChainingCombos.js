c1.documentReady(function () {
    var cmbCountry = wijmo.Control.getControl('#cmbCountry');
    var cmbCity = wijmo.Control.getControl('#cmbCity');

    cmbCountry.itemsSource = getCountries();
    cmbCity.itemsSource = getCities();

    // filter city view based on currently selected country
    cmbCountry.collectionView.currentChanged.addHandler(function (s, e) {
        cmbCity.collectionView.filter = function (city) {
            return city.country == cmbCountry.collectionView.currentItem.id;
        }
    });

    // the data
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
            { id: 8, country: 2, name: 'Manchester' },
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