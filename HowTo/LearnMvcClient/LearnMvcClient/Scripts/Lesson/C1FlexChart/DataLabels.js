c1.documentReady(function () {
    var theChart = wijmo.Control.getControl('#theChart');
    var labelPosition = wijmo.Control.getControl('#labelPosition');
    var downloadsOnly = false;

    theChart.dataLabel.content = '{value:0}';
    theChart.dataLabel.position = "Top"; //None,Left,Top,Right,Bottom,Center
    theChart.dataLabel.rendering.addHandler(function (s, e) {
        if (downloadsOnly && e.hitTestInfo.series.binding != 'Downloads') {
            e.cancel = true; // labels only for the "downloads" series
        }
    });

    // customize the data labels
    labelPosition.textChanged.addHandler(function (s, e) {
        theChart.dataLabel.position = s.text;
    });
    document.getElementById('linesAndBorders').addEventListener('click', function (e) {
        var dl = theChart.dataLabel;
        dl.connectingLine = dl.border = e.target.checked;
    });
    document.getElementById('downloadsOnly').addEventListener('click', function (e) {
        downloadsOnly = e.target.checked;
        theChart.invalidate();
    });
});