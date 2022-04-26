c1.documentReady(function () {
    var theGauge = wijmo.Control.getControl('#theGauge');
    var value = wijmo.Control.getControl('#value');
    var startAngle = wijmo.Control.getControl('#startAngle');
    var sweepAngle = wijmo.Control.getControl('#sweepAngle');

    theGauge.valueChanged.addHandler(function (s, e) {
        value.value = s.value;
    });

    value.min = theGauge.min;
    value.max = theGauge.max;
    value.step = theGauge.step;
    value.value = theGauge.value;
    value.valueChanged.addHandler(function (s, e) {
        theGauge.value = s.value;
    });

    startAngle.value = theGauge.startAngle;
    startAngle.valueChanged.addHandler(function (s, e) {
        theGauge.startAngle = s.value;
    });

    sweepAngle.value = theGauge.sweepAngle;
    sweepAngle.valueChanged.addHandler(function (s, e) {
        theGauge.sweepAngle = s.value;
    });

    document.getElementById('autoScale').addEventListener('click', function (e) {
        theGauge.autoScale = e.target.checked;
    });
});