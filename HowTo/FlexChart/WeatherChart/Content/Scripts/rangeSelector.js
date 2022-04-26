var chart, chart1, chart2, chart3;
    

function updateCharts(rs) {
    var min = rs.min, max = rs.max;
    [chart1, chart2, chart3].forEach(function (chart) {
        chart.axisX.min = min;
        chart.axisX.max = max;
        chart.invalidate();
        chart.rendered.addHandler(function () {
            chart.hostElement.querySelector('.wj-chart-linemarker').style.display = 'none';
        })
    });
}

