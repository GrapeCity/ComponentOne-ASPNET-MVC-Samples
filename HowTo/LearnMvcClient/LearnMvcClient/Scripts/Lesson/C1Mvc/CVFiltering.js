c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');
    var view = theGrid.collectionView;

    // show filtered item count
    view.collectionChanged.addHandler(function () {
        var cnt = document.getElementById('cnt');
        cnt.textContent = wijmo.format('{length:n0}', view.items)
    })

    // initialize item count display
    view.onCollectionChanged();

    // change the filter
    document.addEventListener('change', function (e) {
        var filterType = e.target.value;
        view.filter = function (item) {

            switch (filterType) {
                case 'Country':
                    return item.Country == 'US';
                case 'Sales':
                    return item.Sales > 50000;
                case 'Downloads':
                    return item.Downloads > 150000;
                default:
                    return true;
            }
        }
    });
});