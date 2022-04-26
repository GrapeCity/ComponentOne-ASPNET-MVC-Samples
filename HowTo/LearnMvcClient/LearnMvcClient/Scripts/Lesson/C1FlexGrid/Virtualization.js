c1.documentReady(function () {
    // start with a small data set
    var data = getData(100);

    var theGrid = wijmo.Control.getControl('#theGrid');
    theGrid.updatedView.addHandler(function(s, e) {
        rowCount.textContent = s.rows.length;
        cellCount.textContent = s.hostElement.querySelectorAll('.wj-cell').length;
    });
    theGrid.scrollPositionChanged.addHandler(function (s, e) {

        // if we're close to the bottom, add 20 items
        if (s.viewRange.bottomRow >= s.rows.length - 1) {
            addData(data, 20);
            s.collectionView.refresh();
        }
    });
    theGrid.itemsSource = data;

    // get an array with random data
    function getData(cnt, start) {
        var data = [];
        var countries = 'USA,Germany,UK,Japan,Italy,Greece'.split(',');
        if (!start) start = 0;
        for (var i = 0; i < cnt; i++) {
            data.push({
                id: i + start,
                country: countries[i % countries.length],
                date: new Date(2014, i % 12, i % 28),
                amount: Math.random() * 10000,
                active: i % 4 === 0
            });
        }
        return data;
    }

    // add random data to an array
    function addData(data, cnt) {
        var more = getData(cnt, data.length);
        for (var i = 0; i < more.length; i++) {
            data.push(more[i])
        }
    }
});