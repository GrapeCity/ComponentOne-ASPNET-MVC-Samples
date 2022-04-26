c1.documentReady(function () {
    var theChart = wijmo.Control.getControl('#theChart');
    var theGrid = wijmo.Control.getControl('#theGrid');

    // auto-generate series
    var item = theChart.itemsSource.items[0];
    for (var k in item) {
        if (wijmo.isNumber(item[k])) {
            var series = new wijmo.chart.Series();
            series.binding = k;
            series.name = wijmo.toHeaderCase(k);
            series['visible'] = true; // add 'visible' property for binding
            theChart.series.push(series);
        }
    }

    theGrid.cellEditEnded.addHandler(function (s, e) {
        theChart.refresh(true);
    });

    // show the chart's series in a grid
    theGrid.itemsSource = theChart.series;
    theGrid.columns.push(new wijmo.grid.Column({ binding: 'binding', header: 'Binding', isReadOnly: true }));
    theGrid.columns.push(new wijmo.grid.Column({ binding: 'name', header: 'Name', isRequired: true }));
    theGrid.columns.push(new wijmo.grid.Column({ binding: 'visibility', header: 'Visibility', dataMap: getDataMap(wijmo.chart.SeriesVisibility) }));
    theGrid.columns.push(new wijmo.grid.Column({ binding: 'chartType', header: 'Chart Type', dataMap: getDataMap(wijmo.chart.ChartType), isRequired: false }));

    // build a DataMap for a given enum
    function getDataMap(enumClass) {
        var pairs = [];
        for (var key in enumClass) {
            var val = parseInt(key);
            if (!isNaN(val)) {
                pairs.push({ key: val, name: enumClass[val] });
            }
        }
        return new wijmo.grid.DataMap(pairs, 'key', 'name');
    }
});