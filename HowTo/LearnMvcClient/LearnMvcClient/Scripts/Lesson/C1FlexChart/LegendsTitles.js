c1.documentReady(function () {
    var theChart = wijmo.Control.getControl('#theChart');
    var header = wijmo.Control.getControl('#header');
    var footer = wijmo.Control.getControl('#footer');
    var xTitle = wijmo.Control.getControl('#xTitle');
    var yTitle = wijmo.Control.getControl('#yTitle');

    header.textChanged.addHandler(function (s, e) {
        theChart.header = s.text;
    });

    footer.textChanged.addHandler(function (s, e) {
        theChart.footer = s.text;
    });

    xTitle.textChanged.addHandler(function (s, e) {
        theChart.axisX.title = s.text;
    });

    yTitle.textChanged.addHandler(function (s, e) {
        theChart.axisY.title = s.text;
    });

    // initialize titles
    header.text = 'My Great Chart'
    footer.text = 'powered by ComponentOne\'s MVC FlexChart';
    xTitle.text = 'Country';
    yTitle.text = 'Values/Units';
});