c1.documentReady(function () {
    var theChart = wijmo.Control.getControl('#theChart');
    var theInterval = wijmo.Control.getControl('#theInterval');

    theChart.itemsSource = getData(200);

    // start changing the data
    function addPoints() {

        // append new points, remove old points
        var arr = theChart.collectionView.sourceCollection;
        var pt = arr[arr.length - 1];
        var y = pt.y;
        for (var i = 0; i < 10; i++) {
            y += Math.random() - .5;
            arr.push({ x: pt.x + i + 1, y: y });
            arr.splice(0, 1);
        }

        // update chart
        theChart.collectionView.refresh();
        if (theInterval.selectedItem < 50) {
            theChart.refresh(); // force a refresh if the interval is small
        }

        // and keep updating
        setTimeout(function () {
            addPoints();
        }, theInterval.selectedItem)
    }
    addPoints();

    // get some random data
    function getData(cnt) {
        var arr = [],
        y = 0;
        for (var i = 0; i < cnt; i++) {
            arr.push({ x: i, y: y });
            y += Math.random() - .5;
        }
        return arr;
    }
});