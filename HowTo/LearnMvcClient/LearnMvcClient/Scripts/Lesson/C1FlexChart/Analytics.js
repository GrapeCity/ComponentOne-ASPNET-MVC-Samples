c1.documentReady(function () {
    var theChart = wijmo.Control.getControl('#theChart');

    theChart.itemsSource = getData();

    // create a TrendLine and add it to the chart
    var trendLine = new wijmo.chart.analytics.TrendLine();
    trendLine.name = 'TrendLine';
    trendLine.binding = 'y';
    trendLine.fitType = 'Polynomial';
    trendLine.order = 2;
    trendLine.style = { stroke: 'darkred', strokeWidth: 3 };
    theChart.series.push(trendLine);
  
    // create a MovingAverage and add it to the chart
    var movAvg = new wijmo.chart.analytics.MovingAverage();
    movAvg.name = 'MovingAverage';
    movAvg.binding = 'y';
    movAvg.period = 6;
    movAvg.style = { stroke: 'orange', strokeWidth: 3 };
    theChart.series.push(movAvg);

    // data sources
    function getData() {
        var arr = [], 
                cnt = 50,
            a = Math.random(),
            b = .2 + Math.random(),
                x = 1;
        for (var i = 1; i < cnt; i++) {
            arr.push({
                x: i,
                y: a + i * b + i * (Math.random() - .5)
            });
        }
        return arr;
    }
});