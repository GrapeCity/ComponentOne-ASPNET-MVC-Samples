c1.documentReady(function () {
    var theChart = wijmo.Control.getControl('#theChart');
    var legendPosition = wijmo.Control.getControl('#legendPosition ');

    legendPosition.textChanged.addHandler(function (s, e) {
        theChart.legend.position = s.text;
    });
});