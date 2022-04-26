c1.documentReady(function () {
    var theAutoComplete = wijmo.Control.getControl('#theAutoComplete');
    var theAutoCompleteCustom = wijmo.Control.getControl('#theAutoCompleteCustom');
    var theCollectionView = c1.getService('theCollectionView');
    var allItems = theCollectionView.items;

    theAutoCompleteCustom.itemsSourceFunction = function (query, max, callback) {

        // empty query? no results
        if (!query) {
            callback(null);
            return;
        }

        // find items that start with the user input
        var queryItems = [],
            rx = new RegExp('^' + query, 'i');
        for (var i = 0; i < allItems.length && queryItems.length < max; i++) {
            if (rx.test(allItems[i].Country)) {
                queryItems.push(allItems[i]);
            }
        }
        callback(queryItems);
    };
});