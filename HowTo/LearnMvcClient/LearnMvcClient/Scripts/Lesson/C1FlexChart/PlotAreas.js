c1.documentReady(function () {
    var theChart = wijmo.Control.getControl('#theChart');

    // create a plot area for amounts
    var p = new wijmo.chart.PlotArea();
    p.row = theChart.plotAreas.length;
    p.name = 'amounts';
    p.height = '2*';
    theChart.plotAreas.push(p)

    // create a spacer plot area
    p = new wijmo.chart.PlotArea();
    p.row = theChart.plotAreas.length;
    p.name = 'spacer';
    p.height = 25;
    theChart.plotAreas.push(p)

    // create a plot area for quantities
    p = new wijmo.chart.PlotArea();
    p.row = theChart.plotAreas.length;
    p.name = 'quantities';
    p.height = '*';
    var axisYQty = new wijmo.chart.Axis(wijmo.chart.Position.Left);
    axisYQty.plotArea = p;
    theChart.series[2].axisY = axisYQty;
    theChart.plotAreas.push(p);
});