c1.documentReady(function () {
    var theChart = wijmo.Control.getControl('#theChart');
    theChart.series[0].style = { fill: 'l(0, 0, 0, 1)#ff0000-#ff0000:1:0', stroke: 'darkred', strokeWidth: 1 };
    theChart.series[1].style = { fill: 'l(0, 0, 0, 1)#00b050-#00b050:1:0', stroke: 'darkgreen', strokeWidth: 1 };
});