c1.documentReady(function () {
    var theChart = wijmo.Control.getControl('#theChart');
    var theSeriesPicker = wijmo.Control.getControl('#theSeriesPicker');

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

    // the series picker
    theSeriesPicker.itemsSource = theChart.series;
    theSeriesPicker.lostFocus.addHandler(function (s, e) {
        wijmo.hidePopup(theSeriesPicker.hostElement);
    });
    theSeriesPicker.checkedItemsChanged.addHandler(function (s, e) {
        // map extra 'visible' property to 'Series.visibility' values
        theChart.series.forEach(function (series) {
            series.visibility = s.checkedItems.indexOf(series) > -1
                ? 'Visible'
                : 'Hidden';
        });
    });

    document.getElementById('pickerButton').addEventListener('click', function (e) {
        wijmo.showPopup(theSeriesPicker.hostElement, e.target, false, true, false);
        theSeriesPicker.focus();
        e.preventDefault();
    })
});