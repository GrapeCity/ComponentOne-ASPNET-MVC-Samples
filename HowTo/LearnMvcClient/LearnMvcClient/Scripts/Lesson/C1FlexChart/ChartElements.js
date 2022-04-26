c1.documentReady(function(){
    var theChart = wijmo.Control.getControl('#theChart');

    // use tooltip to show hit-test information
    var tt = new wijmo.Tooltip(),
        tip = '';
    theChart.hostElement.addEventListener('mousemove', function (e) {
        // build tooltip text
        var ht = theChart.hitTest(e),
            elem = ht.chartElement,
          series = (ht.series && [1, 2, 3].indexOf(elem) < 0) ? ht.series : null,
          index = (ht.pointIndex != null && series) ? ht.pointIndex : null,
          newTip = wijmo.format('chartElement: <b>{elem}</b><br/>series: <b>{series}</b><br/>pointIndex: <b>{index}</b>', {
              elem: wijmo.chart.ChartElement[elem],
              series: series ? series.name : 'none',
              index: index != null ? index : 'none'
          });

        // update tooltip      
        if (newTip != tip) {
            tip = newTip;
            tt.show(e.target, tip, new wijmo.Rect(e.clientX, e.clientY, 0, 0));
        }
    });
    theChart.hostElement.addEventListener('mouseleave', function (e) {
        tt.hide();
        tip = '';
    });
});