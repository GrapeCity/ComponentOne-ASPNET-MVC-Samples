c1.documentReady(function () {
    var theChart = wijmo.Control.getControl('#theChart');
    var theNeckWidth = wijmo.Control.getControl('#neckWidth');
    var theNeckHeight = wijmo.Control.getControl('#neckHeight');
    var theNeckStyle = wijmo.Control.getControl('#neckStyle');

    theNeckWidth.valueChanged.addHandler(function (s, e) {
        theChart.options.funnel.neckWidth = s.value;
        theChart.refresh(true);
    });

    theNeckHeight.valueChanged.addHandler(function (s, e) {
        theChart.options.funnel.neckHeight = s.value;
        theChart.refresh(true);
    });

    theNeckStyle.textChanged.addHandler(function (s, e) {
        theChart.options.funnel.type = s.text.toLowerCase();
        theChart.refresh(true);
    });
});