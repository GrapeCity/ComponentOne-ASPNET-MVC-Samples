c1.documentReady(function () {
    var theChart = wijmo.Control.getControl('#theChart');
    var theChartSelector = wijmo.Control.getControl('#theChartSelector');

    var rangeSelector = new wijmo.chart.interaction.RangeSelector(theChartSelector, {
        max: theChart.itemsSource.items[0].Date.valueOf(), // now
        min: theChart.itemsSource.items[30].Date.valueOf(), // a month ago
        minScale: .05, // restrict selection to between 5% and
        maxScale: .75, // 75% of the data
        rangeChanged: function (s, e) {
            theChart.axisX.min = s.min;
            theChart.axisX.max = s.max;
        }
    });

    //force to refresh the chart to make sure the RangeSelector updates the min/max values.
    theChartSelector.refresh();
});