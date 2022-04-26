c1.documentReady(function () {
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

    var theGrid = wijmo.Control.getControl('#theGrid');
    theGrid.itemsSource = data;

    // show the current item
    var selItemElement = document.getElementById('selectedItem');
    function updateCurrentInfo() {
        selItemElement.innerHTML = wijmo.format(
            'Country: <b>{country}</b>, Sales: <b>{sales:c0}</b> Expenses: <b>{expenses:c0}</b>',
          theGrid.collectionView.currentItem);
    }

    // update current item now and when the grid selection changes
    updateCurrentInfo();
    theGrid.collectionView.currentChanged.addHandler(updateCurrentInfo);

    // sort the data by country
    var sd = new wijmo.collections.SortDescription('country', true);
    theGrid.collectionView.sortDescriptions.push(sd);
});