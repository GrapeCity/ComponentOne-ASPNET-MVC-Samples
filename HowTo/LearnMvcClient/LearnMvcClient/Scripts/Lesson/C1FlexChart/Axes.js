c1.documentReady(function () {
    var theChart = wijmo.Control.getControl('#theChart');

    var axisX = theChart.axisX;
    axisX.format = 'MMM dd';
    axisX.majorGrid = true; // show major gridlines
    axisX.majorTickMarks = 'Cross'; // None,Outside,Inside,Cross
    axisX.majorUnit = 7;
    axisX.minorGrid = true; // show minor gridlines
    axisX.minorTickMarks = 'None'; // None,Outside,Inside,Cross
    axisX.minorUnit = 1;

    var axisY = theChart.axisY;
    axisY.min = 790;
    axisY.max = 860;
    axisY.format = 'c0';
    axisY.axisLine = true; // show axis line
    axisY.majorGrid = true; // show major gridlines
    axisY.majorTickMarks = 'Cross'; // None,Outside,Inside,Cross
    axisY.majorUnit = 20;
    axisY.minorGrid = true; // show minor gridlines
    axisY.minorTickMarks = 'None'; // None,Outside,Inside,Cross
    axisY.minorUnit = 5;

    // toggle axes
    document.getElementById('showAxes').addEventListener('click', function (e) {
        var show = e.target.checked;
        theChart.axisX.position = show ? 'Bottom' : 'None';
        theChart.axisY.position = show ? 'Left' : 'None';
    });
});