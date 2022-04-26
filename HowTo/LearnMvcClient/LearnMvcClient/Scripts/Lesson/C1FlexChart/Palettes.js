c1.documentReady(function () {
    var theChart = wijmo.Control.getControl('#theChart');
    var thePalette = wijmo.Control.getControl('#thePalette');

    thePalette.selectedIndexChanged.addHandler(function (s, e) {
        var pal = s.text.toLowerCase();
        theChart.palette = wijmo.chart.Palettes[pal];
    });
});