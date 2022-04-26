c1.documentReady(function () {
    var theChart = wijmo.Control.getControl('#theChart');

    theChart.itemsSource = getData();

    // create Waterfall series for 'sales' and add it to the chart
    var sales = new wijmo.chart.analytics.Waterfall();
    sales.name = 'Sales';
    sales.binding = 'sales';
    sales.styles = {
        start: { fill: 'blue', stroke: 'blue' },
        total: { fill: 'yellow', stroke: 'yellow' },
        falling: { fill: 'red', stroke: 'red' },
        rising: { fill: 'green', stroke: 'green' },
        connectorLines: { stroke: 'blue', 'stroke-dasharray': '3, 1' }
    };
    theChart.series.push(sales);

    // customize the Waterfall series
    document.getElementById('connectorLines').addEventListener('click', function (e) {
        sales.connectorLines = e.target.checked;
    });
    document.getElementById('showTotal').addEventListener('click', function (e) {
        sales.showTotal = e.target.checked;
    });

    // randomize the data
    document.getElementById('btnRandomize').addEventListener('click', function () {
        theChart.itemsSource = getData();
    });

    // create some data
    function getData() {
        var data = []
        date = new Date();
        for (var month = 0; month < 12; month++) {
            data.push({
                date: date,
                sales: (Math.random() - .4) * 1000
            });
            date = wijmo.DateTime.addMonths(date, 1);
        }
        return data;
    }
});