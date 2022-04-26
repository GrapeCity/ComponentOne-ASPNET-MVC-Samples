c1.documentReady(function () {
    var theChart = wijmo.Control.getControl('#theChart');

    theChart.series[0].style = { fill: 'green', stroke: 'darkgreen', strokeWidth: 1 };
    theChart.series[1].style = { fill: 'red', stroke: 'darkred', strokeWidth: 1 };
    theChart.series[2].style = { stroke: 'orange', strokeWidth: 5 };
    theChart.series[2].symbolStyle = { fill: 'gold', stroke: 'gold' };
});