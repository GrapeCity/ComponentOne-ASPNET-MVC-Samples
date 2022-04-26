c1.documentReady(function () {
    var theChart = wijmo.Control.getControl("#theChart");
    var theGrid = wijmo.Control.getControl("#theGrid");
    var selectionMode = wijmo.Control.getControl("#selectionMode");
    var chartType = wijmo.Control.getControl("#chartType");

    theChart.selectionChanged.addHandler(function (s, e) {
        var selectedSeries = s.selection;
        if (!selectedSeries) {
            theGrid.hostElement.style.display = 'none';
        } else {
            theGrid.hostElement.style.display = '';
            theGrid.columns.forEach(function(col) {
                col.visible = col.binding == selectedSeries.binding || col.binding == 'Country';
                col.width = '*';
            })
        }
    });

    // change the selectionMode
    selectionMode.textChanged.addHandler(function (s, e) {
        theChart.selectionMode = s.text;
    });

    // change the chartType
    chartType.textChanged.addHandler(function (s, e) {
        theChart.chartType = s.text;
    });
});