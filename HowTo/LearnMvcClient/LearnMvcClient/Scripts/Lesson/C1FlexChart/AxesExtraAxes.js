c1.documentReady(function () {
    var theChart = wijmo.Control.getControl('#theChart');

    // create and apply extra Y axis for 'Downloads' series
    var axisY2 = new wijmo.chart.Axis();
    axisY2.position = 'Right';
    axisY2.title = 'Downloads (k)';
    axisY2.format = 'n0,';
    axisY2.min = 0;
    axisY2.axisLine = true;
    getSeries('Downloads').axisY = axisY2;

    // toggle extra axis
    document.getElementById('secondaryAxis').addEventListener('click', function (e) {
        getSeries('Downloads').axisY = e.target.checked ? axisY2 : null;
    });

    // get a series by its binding
    function getSeries(binding) {
        var s = theChart.series;
        for (var i = 0; i < s.length; i++) {
            if (s[i].binding == binding) {
                return s[i];
            }
        }
        return null;
    }
});