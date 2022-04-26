c1.documentReady(function () {
    var theTree = wijmo.Control.getControl('#theTree');

    var theTreeSelected = wijmo.Control.getControl('#theTreeSelected');
    theTreeSelected.loadedItems.addHandler(function (s, e) {
        s.selectedItem = findItem(s.itemsSource, 'Solar Panel');
    });

    var theTreeCollapsed = wijmo.Control.getControl('#theTreeCollapsed');
    theTreeCollapsed.loadedItems.addHandler(function (s, e) {
        s.collapseToLevel(0);
    });

    var theTreeExpanded = wijmo.Control.getControl('#theTreeExpanded');
    theTreeExpanded.loadedItems.addHandler(function (s, e) {
        s.collapseToLevel(10);
    });

    // finds an item in a hierarchical array
    function findItem(arr, text) {
        for (var i = 0; arr && i < arr.length; i++) {
            if (arr[i].Header.indexOf(text) > -1) return arr[i];
            var item = findItem(arr[i].Items, text);
            if (item) return item;
        }
        return null;
    }
});