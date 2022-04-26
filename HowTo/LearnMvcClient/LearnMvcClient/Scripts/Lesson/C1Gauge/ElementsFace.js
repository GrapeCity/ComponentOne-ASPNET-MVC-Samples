c1.documentReady(function () {
    var theGauge = wijmo.Control.getControl('#theGauge');
    var theColor = wijmo.Control.getControl('#theColor');
    var theThickness = wijmo.Control.getControl('#theThickness');

    theColor.value = theGauge.face.color;
    theColor.valueChanged.addHandler(function (s, e) {
        theGauge.face.color = s.value;
    });

    theThickness.value = theGauge.face.thickness;
    theThickness.valueChanged.addHandler(function (s, e){
        theGauge.face.thickness = s.value;
    });
});