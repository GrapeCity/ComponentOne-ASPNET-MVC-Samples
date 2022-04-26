c1.documentReady(function () {
    var theGauge = wijmo.Control.getControl('#theGauge');
    var cmbRanges = wijmo.Control.getControl('#ranges');

    // customize
    document.getElementById('showRanges').addEventListener('click', function (e) {
        theGauge.showRanges = e.target.checked;
    });

    cmbRanges.selectedIndexChanged.addHandler(function (s, e) {
        createRanges(theGauge, s.selectedItem);
    });

    cmbRanges.selectedItem = 3;

    // populate gauge with ranges
    function createRanges(gauge, cnt) {
        gauge.ranges.clear();
        if (cnt) {
            var colorMin = new wijmo.Color('red'),
                colorMax = new wijmo.Color('green'),
                span = (gauge.max - gauge.min) / cnt;
            for (var i = 0; i < cnt; i++) {
                var rng = new wijmo.gauge.Range(),
                    color = wijmo.Color.interpolate(colorMin, colorMax, cnt > 1 ? i / (cnt - 1) : 0);
                rng.min = gauge.min + i * span;
                rng.max = rng.min + span;
                rng.color = color.toString();
                gauge.ranges.push(rng);
            }
        }
    }
});