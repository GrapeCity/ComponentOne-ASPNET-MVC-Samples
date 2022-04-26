c1.documentReady(function () {
    // a CollectionView based on the raw data
    var view = c1.getService("collectionView");

    // create a second CollectionView based on the first one
    var view2 = new wijmo.collections.CollectionView(view.items, {
        collectionChanged: function (s) {
            var cnt = document.getElementById('cnt');
            cnt.textContent = wijmo.format('{length:n0}', s.items)
        }
    });

    // bind a grid to the second CollectionView
    var theGrid = wijmo.Control.getControl('#theGrid');
    theGrid.itemsSource = view2;

    // update the filter on the first CollectionView when the text changes
    document.getElementById('theFilter').addEventListener('input', function (e) {

        // update first CollectionView's filter
        var filterText = e.target.value.toLowerCase();
        view.filter = function (item) {
            return filterText
            ? item.Country.toLowerCase().indexOf(filterText) > -1
            : true;
        }

        // update second collection view's sourceCollection
        view2.sourceCollection = view.items;
    });
});