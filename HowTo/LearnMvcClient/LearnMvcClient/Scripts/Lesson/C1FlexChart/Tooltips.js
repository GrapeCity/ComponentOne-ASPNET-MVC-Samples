c1.documentReady(function () {
    var theChart = wijmo.Control.getControl('#theChart');
    var tooltip = wijmo.Control.getControl('#tooltip');

    tooltip.selectedIndexChanged.addHandler(function (s, e) {
        theChart.tooltip.content = s.selectedItem.Value;
    });
});