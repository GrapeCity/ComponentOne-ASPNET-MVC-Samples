c1.documentReady(function () {
    // create some random data
    var countries = 'US,Germany,UK,Japan,Italy,Greece,Spain,Portugal,Australia'.split(',');
    var data = [];
    for (var i = 0; i < countries.length; i++) {
        data.push({
            id: i,
            country: countries[i],
            downloads: Math.round(Math.random() * 20000),
            sales: Math.random() * 10000,
            expenses: Math.random() * 5000
        });
    }

    // create a CollectionView
    var view = new wijmo.collections.CollectionView(data, {
        currentChanged: updateDetails,
        sortDescriptions: ['country']
    });

    // country combo
    var cmbCountry = wijmo.Control.getControl('#cmbCountry');
    cmbCountry.itemsSource = view;
    cmbCountry.displayMemberPath = 'country';

    // update details
    updateDetails();
    function updateDetails() {
        updateDetail('downloads', 'n0');
        updateDetail('sales', 'c');
        updateDetail('expenses', 'c');
    }
    function updateDetail(prop, fmt) {
        var text = '';
        if (view.currentItem) {
            text = wijmo.Globalize.format(view.currentItem[prop], fmt);
        }
        document.getElementById(prop).textContent = text;
    }
});