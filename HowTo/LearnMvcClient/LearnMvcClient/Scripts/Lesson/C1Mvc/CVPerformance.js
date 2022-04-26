c1.documentReady(function () {
    // create some random data
    // Note that we are using an ObservableArray rather than a regular
    // JavaScript array. The ObservableArray raises events when items
    // are added or removed.
    // If we used a regular array, we would have to call the
    // CollectionView.refresh() method to update the view after 
    // adding or removing items.
    var countries = 'US,Germany,UK,Japan,Italy,Greece'.split(',');
    var data = new wijmo.collections.ObservableArray();

    function getItem(i) {
        return {
            country: countries[i % countries.length],
            downloads: Math.round(Math.random() * 20000),
            sales: Math.random() * 10000,
            expenses: Math.random() * 5000,
            active: i % 4 == 0
        }
    }

    // create a CollectionView
    var view = new wijmo.collections.CollectionView(data, {
        sortDescriptions: ['country'],
        newItemCreator: function () {
            return getItem(view.sourceCollection.length);
        }
    });

    // add items with addNew
    document.getElementById('btnAddNew').addEventListener('click', function (e) {
        var start = Date.now();
        view.sourceCollection.clear();
        for (var i = 0; i < 1000; i++) {
            view.addNew();
            view.commitNew();
        }
        alert('addNew done in ' + (Date.now() - start) + 'ms');
    });

    // add items with push
    document.getElementById('btnPush').addEventListener('click', function (e) {
        var start = Date.now();
        view.sourceCollection.clear();
        for (var i = 0; i < 1000; i++) {
            view.sourceCollection.push(getItem(i));
        }
        alert('push done in ' + (Date.now() - start) + 'ms');
    });

    // add items with pushDefer
    document.getElementById('btnPushDefer').addEventListener('click', function (e) {
        var start = Date.now();
        view.sourceCollection.clear();
        view.deferUpdate(function () {
            for (var i = 0; i < 1000; i++) {
                view.sourceCollection.push(getItem(i));
            }
        })
        alert('pushDefer done in ' + (Date.now() - start) + 'ms');
    });
});