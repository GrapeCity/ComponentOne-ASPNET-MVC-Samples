c1.documentReady(function () {
    var theRadialGauge = wijmo.Control.getControl('#theRadialGauge');
    var theLinearGauge = wijmo.Control.getControl('#theLinearGauge');

    theRadialGauge.valueChanged.addHandler(function (s, e) {
        theLinearGauge.value = s.value;
    });

    theLinearGauge.valueChanged.addHandler(function (s, e) {
        theRadialGauge.value = s.value;
    });

    document.getElementById('showRanges').addEventListener('click', function (e) {
        if (e.target.checked) {
            theRadialGauge.showRanges = theLinearGauge.showRanges = true;
            theRadialGauge.pointer.thickness = .2;
            theLinearGauge.pointer.thickness = .4;
        } else {
            theRadialGauge.showRanges = theLinearGauge.showRanges = false;
            theRadialGauge.pointer.thickness = 1;
            theLinearGauge.pointer.thickness = 1;
        }
    });
});