c1.documentReady(function () {
    var theChart = wijmo.Control.getControl('#theChart');

    theChart.rendering.addHandler(function (s, e) {
        // behind the regular chart elements
        var xMin = s.axisX.actualMin.valueOf(),
            xMax = s.axisX.actualMax.valueOf(),
            yMin = s.axisY.actualMin,
            yMax = s.axisY.actualMax;
        drawAlarmZone(s, e.engine, xMin, 855, xMax, yMax, 'sell-zone');
        drawAlarmZone(s, e.engine, xMin, yMin, xMax, 815, 'buy-zone');
    });

    // draw an alarm zone into the chart
    function drawAlarmZone(chart, engine, xMin, yMin, xMax, yMax, className) {
        var pt1 = chart.dataToPoint(xMin, yMin);
        var pt2 = chart.dataToPoint(xMax, yMax);
        engine.drawRect(
            Math.min(pt1.x, pt2.x), Math.min(pt1.y, pt2.y),
            Math.abs(pt2.x - pt1.x), Math.abs(pt2.y - pt1.y),
          className);
    }
});