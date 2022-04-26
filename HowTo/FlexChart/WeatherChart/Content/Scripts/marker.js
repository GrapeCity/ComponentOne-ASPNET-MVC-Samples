var props = ['MeanPressure', 'Precipitation'], pt = new wijmo.Point();

function lineMarkerContent(hitTestInfo, point) {
    return getMarkercontent(new wijmo.Point(point.x, NaN));
}

function lineMarkerPositionChanged(marker, point) {
    var chart = marker.chart;
    pt = point;
    changeMarker(chart, marker);
}

function changeMarker(curChart, marker) {
    var curHost = curChart.hostElement,
        vline = curHost.querySelector('.wj-chart-linemarker-vline');

    [chart1, chart2, chart3].forEach(function (chart) {
        if (chart == null) return;
        if (chart === curChart) {
            chart.hostElement.querySelector('.wj-chart-linemarker').style.display = 'block';
        } else {
            chart.hostElement.querySelector('.wj-chart-linemarker').style.display = 'none';
        }
    });

    vline.style.height = 326 + 'px';
}

function getMarkercontent(point) {
    if (chart1 == null) return "";

    var chart = chart1, ht = chart.series[0].hitTest(point),
        item = chart.itemsSource.items[ht.pointIndex], content = '';

    for (var i = 0; i < chart.series.length; i++) {
        var series = chart.series[i];
        // find series lines to get its color
        var polyline = series.hostElement.querySelector('polyline');

        // add series info to the marker content
        if (ht.x && ht.y !== null) {
            if (i == 0) {
                content += wijmo.Globalize.formatDate(ht.x, 'dd-MMM');
            }
            content += '<div style="color:' + polyline.getAttribute('stroke') + '">' + series.name + ' = ' + item[series.name].toFixed() + '</div>';
        }

    }
    for (var i = 0; i < props.length; i++) {
        content += '<div>' + props[i] + ' = ' + item[props[i]].toFixed() + '</div>';
    }
    return content;
}