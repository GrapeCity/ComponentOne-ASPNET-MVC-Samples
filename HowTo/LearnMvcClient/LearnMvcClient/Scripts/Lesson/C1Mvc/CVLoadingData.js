c1.documentReady(function () {
    // create some random data
    var countries = 'US,Germany,UK,Japan,Italy,Austria'.split(',');
    var data = [];
    for (var i = 0; i < countries.length; i++) {
        data.push({
            id: i,
            country: countries[i],
            sales: Math.random() * 10000,
            expenses: Math.random() * 5000
        });
    }

    // create a CollectionView based on the array
    var view = new wijmo.collections.CollectionView(data, {
        sortDescriptions: ['country'] // sort by country
    });

    // same thing, a little more verbose:
    //var view = new wijmo.collections.CollectionView();
    //view.sourceCollection = data;
    //view.sortDescriptions.push(new wijmo.collections.SortDescription('country', true));

    // show the CollectionView
    var firstList = document.getElementById('firstList');
    view.items.forEach(function (item) {
        var html = wijmo.format('<li><b>{country}</b> sales: {sales:n2} (id: {id})</li>', item);
        firstList.appendChild(wijmo.createElement(html));
    });

    // create a second, empty CollectionView
    var secondView = new wijmo.collections.CollectionView();
    secondView.collectionChanged.addHandler(function () {

        // show the second view's data
        var secondList = document.getElementById('secondList');
        secondList.innerHTML = '';
        secondView.items.forEach(function (item) {
            var html = wijmo.format('<li>{CategoryID} <b>{CategoryName}</b> {Description}</li>', item);
            secondList.appendChild(wijmo.createElement(html));
        });
    });

    // populate it with data from a server
    var url = 'https://services.odata.org/Northwind/Northwind.svc/Categories';
    var params = {
        $format: 'json',
        $select: 'CategoryID,CategoryName,Description'
    };
    wijmo.httpRequest(url, {
        data: params,
        success: function (xhr) {

            // got the data, assign it to the CollectionView
            var response = JSON.parse(xhr.response);
            var data = response.d ? response.d.results : response.value;

            // use the result as the sourceCollection
            secondView.sourceCollection = data;

            // append results to the sourceCollection
            // in case you want to load data in parts
            //secondView.deferUpdate(function () {
            //  data.forEach(function(item) {
            //		secondView.sourceCollection.push(item);
            //  });
            //});
        }
    });

    // get and show the CollectionViewService
    var service = c1.getService("collectionView");
    var thirdList = document.getElementById('thirdList');
    service.items.forEach(function (item) {
        var html = wijmo.format('<li><b>{Country}</b> sales: {Sales:n2} (id: {Id})</li>', item);
        thirdList.appendChild(wijmo.createElement(html));
    });
});