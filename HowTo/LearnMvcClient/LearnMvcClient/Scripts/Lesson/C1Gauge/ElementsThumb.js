c1.documentReady(function () {
    var theGauge = wijmo.Control.getControl('#theGauge');
    var theColor = wijmo.Control.getControl('#theColor');
    var theThumbSize = wijmo.Control.getControl('#theThumbSize ');

    theColor.value = theGauge.pointer.color;
    theColor.valueChanged.addHandler(function (s, e) {
        theGauge.pointer.color = s.value;
    });

    theThumbSize.value = theGauge.thumbSize;
    theThumbSize.valueChanged.addHandler(function (s, e) {
        theGauge.thumbSize = s.value;
    });
});