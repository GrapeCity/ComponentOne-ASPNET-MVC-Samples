c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');

    document.getElementById('filter').addEventListener('input', function (e) {
        var filter = e.target.value.toLowerCase();
        theGrid.collectionView.filter = function (item) {
            return filter.length == 0 || item.Country.toLowerCase().indexOf(filter) > -1
        }
    });
});