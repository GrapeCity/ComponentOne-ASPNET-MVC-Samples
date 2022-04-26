c1.documentReady(function () {
    var tree = wijmo.Control.getControl('#theTree');
    var search = wijmo.Control.getControl('#search');

    search.itemsSource = getSearchList(tree.itemsSource);

    search.selectedIndexChanged.addHandler(function (s, e) {
        if (s.selectedItem) {
            tree.selectedItem = s.selectedItem.item;
        }
    });

    function getSearchList(items, searchList, path) {

        // set defaults
        if (searchList == null) searchList = [];
        if (path == null) path = '';

        // add items and sub-items
        for (var i = 0; i < items.length; i++) {
            var item = items[i];
            searchList.push({
                item: item,
                path: path + item.Header,
                keywords: item.Keywords
            });
            if (item.Items) {
                getSearchList(item.Items, searchList, path + item.Header + ' / ');
            }
        }
        return searchList;
    }
});