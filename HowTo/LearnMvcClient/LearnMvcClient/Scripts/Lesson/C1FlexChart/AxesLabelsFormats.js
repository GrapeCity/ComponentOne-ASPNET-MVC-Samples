c1.documentReady(function () {
    var theChart = wijmo.Control.getControl('#theChart');
    var labelAngle = wijmo.Control.getControl('#labelAngle');

    theChart.axisY.format = 'n0';
    theChart.axisY.title = 'US$ (thousands)';


    labelAngle.textChanged.addHandler(function (s, e) {
        theChart.axisX.labelAngle = s.selectedValue;
    });
});