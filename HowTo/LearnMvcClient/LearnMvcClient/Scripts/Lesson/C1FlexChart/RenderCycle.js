c1.documentReady(function () {
    var theChart = wijmo.Control.getControl('#theChart');

    theChart.rendering.addHandler(function (s, e) {
        var pt = theChart.dataToPoint(0, theChart.axisY.actualMax);
        e.engine.drawString('rendering ' + Date.now(), pt, 'annotation');
    });

    theChart.rendered.addHandler(function (s, e) {
        var pt = theChart.dataToPoint(0, theChart.axisY.actualMin);
        e.engine.drawString('rendered ' + Date.now(), pt, 'annotation');
    })

    // refresh the chart
    document.getElementById('btnRefresh').addEventListener('click', function () {
        theChart.itemsSource = getData();
    });
});

// get some random data
function getData() {
    var countries = 'US,Germany,UK,Japan,Italy,Greece'.split(','),
      data = [];
    for (var i = 0; i < countries.length; i++) {
        data.push({
            Country: countries[i],
            Sales: Math.random() * 10000,
            Expenses: Math.random() * 5000,
        });
    }
    return data;
}