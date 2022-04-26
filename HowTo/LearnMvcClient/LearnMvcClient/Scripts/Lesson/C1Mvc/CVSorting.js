c1.documentReady(function () {
    // get grid and its CollectionView
    var theGrid = wijmo.Control.getControl('#theGrid');
    var view = theGrid.collectionView;

    // change the sort
    document.addEventListener('change', function (e) {

        // remove the old sort
        view.sortDescriptions.clear();

        // add the new sorts
        var props = e.target.value.split(',');
        for (var i = 0; i < props.length; i++) {
            var asc = props[i] == 'Country'; // country in ascending order, other in descending order
            var sd = new wijmo.collections.SortDescription(props[i], asc);
            view.sortDescriptions.push(sd);
        }
    });
});