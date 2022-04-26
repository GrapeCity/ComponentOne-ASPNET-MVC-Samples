c1.documentReady(function () {
    var theChart = wijmo.Control.getControl('#theChart');

    theChart.itemFormatter = function (engine, hitTestInfo, defaultFormat) {
        var ht = hitTestInfo,
            binding = 'Downloads';

        // check that this is the right series/element          
        if (ht.series.binding == binding && ht.pointIndex > 0 &&
            ht.chartElement == wijmo.chart.ChartElement.SeriesSymbol) {
            // get current and previous values
            var chart = ht.series.chart,
            items = chart.collectionView.items,
            valNow = items[ht.pointIndex][binding],
            valPrev = items[ht.pointIndex - 1][binding];

            // add line if value is increasing            
            if (valNow > valPrev) {
                var pt1 = chart.dataToPoint(ht.pointIndex, valNow),
                            pt2 = chart.dataToPoint(ht.pointIndex - 1, valPrev);
                engine.drawLine(pt1.x, pt1.y, pt2.x, pt2.y, null, {
                    stroke: 'gold',
                    strokeWidth: 6,
                });
            }
        }

        // render element as usual
        defaultFormat();
    }
});