c1.documentReady(function () {
    var theGauge = wijmo.Control.getControl('#theGauge');
    var tickSpacing = wijmo.Control.getControl('#tickSpacing ');
    var tickColor = wijmo.Control.getControl('#tickColor ');
    var tickWidth = wijmo.Control.getControl('#tickWidth ');

    // customize tickmarks
    document.getElementById('showTicks').addEventListener('click', function (e) {
        theGauge.showTicks = e.target.checked;
    });

    tickSpacing.value = theGauge.tickSpacing;
    tickSpacing.valueChanged.addHandler(function (s, e) {
        theGauge.tickSpacing = s.value;
    });

    tickColor.valueChanged.addHandler(function (s, e) {
        var style = theGauge.hostElement.querySelector('.wj-ticks').style;
        style.stroke = s.value;
    });

    tickWidth.valueChanged.addHandler(function (s, e) {
        var style = theGauge.hostElement.querySelector('.wj-ticks').style;
        style.strokeWidth = s.value;
    });
});