c1.documentReady(function () {
    var theChart = wijmo.Control.getControl('#theChart');

    theChart.itemsSource = getData();

    // create BoxWhisker series for 'sales' and add it to the chart
    var sales = new wijmo.chart.analytics.BoxWhisker();
    sales.name = 'Sales';
    sales.binding = 'sales';
    sales.groupWidth = .7;
    sales.gapWidth = .2;
    theChart.series.push(sales);

    // create BoxWhisker series for 'expenses' and add it to the chart
    var expenses = new wijmo.chart.analytics.BoxWhisker();
    expenses.name = 'Expenses';
    expenses.binding = 'expenses';
    expenses.groupWidth = .7;
    expenses.gapWidth = .2;
    theChart.series.push(expenses);

    // customize the BoxWhisker series
    document.getElementById('innerPoints').addEventListener('click', function (e) {
        theChart.series.forEach(function (series) {
            series.showInnerPoints = e.target.checked;
        });
    });
    document.getElementById('outliers').addEventListener('click', function (e) {
        theChart.series.forEach(function (series) {
            series.showOutliers = e.target.checked;
        });
    });

    // randomize the data
    document.getElementById('btnRandomize').addEventListener('click', function () {
        theChart.itemsSource = getData();
    });

    // create some random data
    // the data items contain arrays of values rather than single values
    function getData() {
        var countries = 'US,Canada,Mexico,Germany,UK,France,Japan,Korea,China'.split(','),
            data = [];
        for (var i = 0; i < countries.length; i++) {
            data.push({
                country: countries[i],
                sales: getRandomArray(20, 10000),
                expenses: getRandomArray(20, 5000),
            });
        }
        return data;
    }
    function getRandomArray(count, max) {
        var arr = [];
        for (var i = 0; i < count; i++) {
            arr.push(
            Math.random() * max / 3 +
            Math.random() * max / 3 +
            Math.random() * max / 3);
        }
        return arr;
    }
});