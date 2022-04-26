c1.documentReady(function () {
    var theGridLeft = wijmo.Control.getControl('#theGridLeft');
    theGridLeft.itemsSource = getData();
    theGridLeft.scrollPositionChanged.addHandler(function (s, e) {
        syncGrids(s, theGridRight)
    });

    var theGridRight = wijmo.Control.getControl('#theGridRight');
    theGridRight.itemsSource = theGridLeft.collectionView;
    theGridRight.scrollPositionChanged.addHandler(function (s, e) {
        syncGrids(s, theGridLeft)
    });

    // support wheel scrolling on both grids
    theGridLeft.hostElement.addEventListener('wheel', applyWheel);
    theGridRight.hostElement.addEventListener('wheel', applyWheel);
    function applyWheel(e) {
        var root = wijmo.closest(e.target, '[wj-part="root"]');
        if (root) {
            root.scrollTop += 32 * (e.deltaY < 0 ? -1 : +1);
            e.preventDefault();
        }
    }

    // synchronize the vertical scrolling position
    // NOTE: the selection is synchronized automatically since both
    // grids are bound to the same CollectionView
    function syncGrids(main, sub) {
        sub.scrollPosition = new wijmo.Point(sub.scrollPosition.x, main.scrollPosition.y);
    }

    // move the last two columns to the grid on the right
    theGridRight.columns.clear();
    for (var i = 0; i < 2; i++) {
        var lastCol = theGridLeft.columns[theGridLeft.columns.length - 1];
        theGridLeft.columns.remove(lastCol);
        theGridRight.columns.push(lastCol);
    }

    // get some random data
    function getData() {
        var countries = 'US,Germany,UK,Japan,Italy,Greece,Austria,Belgium,China,Korea'.split(','),
                data = [];
        for (var i = 0; i < 200; i++) {
            data.push({
                id: i,
                country: countries[i % countries.length],
                downloads: Math.round(Math.random() * 20000),
                sales: Math.random() * 10000,
                expenses: Math.random() * 5000,
                amount1: Math.random() * 5000,
                amount2: Math.random() * 5000,
                amount3: Math.random() * 5000,
                amount4: Math.random() * 5000,
                amount5: Math.random() * 5000,
                amount6: Math.random() * 5000,
                amount7: Math.random() * 5000,
                amount8: Math.random() * 5000,
                important: i % 5 == 0
            });
        }
        return data;
    }
});