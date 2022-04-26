c1.documentReady(function () {
    var theChart = wijmo.Control.getControl('#theChart');

    theChart.axisY.itemFormatter = function (engine, label) {
        if (label.val >= 4000) {
            label.cls = 'large-value';
        }
        return label;
    }
});