c1.documentReady(function () {
    var theChart = wijmo.Control.getControl('#theChart');
    var lines = wijmo.Control.getControl('#lines');
    var interaction = wijmo.Control.getControl('#interaction');

    theChart.itemsSource = getData();

    // add a LineMarker
    var lm = new wijmo.chart.LineMarker(theChart, {
        isVisible: false,
        lines: 'Both',
        interaction: 'Move',
        content: function (ht) {
            return ht.item
            ? wijmo.format('The value on <b>{date:MMM d, yyyy}</b><br/>is <b>{value:c}</b>', ht.item)
            : 'No items here...';
        }
    });

    // show the marker when the mouse is over the chart
    theChart.addEventListener(theChart.hostElement, 'mouseenter', function () {
        lm.isVisible = true;
    });
    theChart.addEventListener(theChart.hostElement, 'mouseleave', function () {
        lm.isVisible = false;
    });

    // configure the LineMarker
    lines.textChanged.addHandler(function (s, e) {
        lm.lines = s.text;
    });
    interaction.textChanged.addHandler(function (s, e) {
        lm.interaction = s.text;
    });

    // create some random data
    function getData() {
        var arr = [],
                value = 100,
                    date = new Date();
        for (var i = 0; i < 100; i++) {
            arr.push({
                date: date,
                value: value + Math.random() * 10 - 4
            });
            date = wijmo.DateTime.addDays(date, -1);
        }
        return arr;
    }
});