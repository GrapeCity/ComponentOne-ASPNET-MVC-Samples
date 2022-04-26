c1.documentReady(function () {
    var theChart = wijmo.Control.getControl('#theChart');
    var period = wijmo.Control.getControl('#period');
    var type = wijmo.Control.getControl('#type');

    theChart.itemsSource = getData();

    // create a MovingAverage and add it to the Chart series collection
    var movingAvg = new wijmo.chart.analytics.MovingAverage();
    movingAvg.name = 'Moving Average';
    movingAvg.itemsSource = theChart.itemsSource;
    movingAvg.binding = 'sales';
    movingAvg.period = 6;
    movingAvg.style = { stroke: 'darkred', strokeWidth: 3 };
    theChart.series.push(movingAvg);

    // select moving average period
    period.valueChanged.addHandler(function (s, e) {
        movingAvg.period = s.value;
    });

    // select moving average type
    type.textChanged.addHandler(function (s, e) {
        movingAvg.type = s.text;
    });

    // generate some random data
    function getData() {
        var arr = [],
                today = new Date(),
            sales = 1000;
        for (var i = 100; i >= 0; i--) {
            arr.push({
                date: wijmo.DateTime.addDays(today, -i),
                sales: sales
            });
            sales += Math.round(Math.random() * 300 - 130);
        }
        return arr;
    }
});