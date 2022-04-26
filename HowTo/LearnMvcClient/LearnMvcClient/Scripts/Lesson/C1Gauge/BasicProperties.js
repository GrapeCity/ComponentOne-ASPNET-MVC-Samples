c1.documentReady(function () {
    var theRadialGauge = wijmo.Control.getControl('#theRadialGauge');
    var theLinearGauge = wijmo.Control.getControl('#theLinearGauge');
    var theBulletGraph = wijmo.Control.getControl('#theBulletGraph');
    var minCtl = wijmo.Control.getControl('#min');
    var maxCtl = wijmo.Control.getControl('#max');
    var valueCtl = wijmo.Control.getControl('#value');
    var showTextCtl = wijmo.Control.getControl('#showText');
    var stepCtl = wijmo.Control.getControl('#step');

    theRadialGauge.valueChanged.addHandler(function (s, e) {
        valueCtl.value = s.value;
    });

    theLinearGauge.valueChanged.addHandler(function (s, e) {
        valueCtl.value = s.value;
    });

    theBulletGraph.valueChanged.addHandler(function (s, e) {
        valueCtl.value = s.value;
    });

    function getGaugeProp(prop) {
        return theRadialGauge[prop];
    }
    function setGaugeProp(prop, value) {
        theRadialGauge[prop] = value;
        theLinearGauge[prop] = value;
        theBulletGraph[prop] = value;
    }

    minCtl.value = getGaugeProp('min');
    minCtl.valueChanged.addHandler(function (s, e) {
        setGaugeProp('min', s.value);
    });

    maxCtl.value = getGaugeProp('max');
    maxCtl.valueChanged.addHandler(function (s, e) {
        setGaugeProp('max', s.value);
    });

    valueCtl.value = getGaugeProp('value');
    valueCtl.valueChanged.addHandler(function (s, e) {
        setGaugeProp('value', s.value);
    });

    showTextCtl.text = getGaugeProp('showText');
    showTextCtl.textChanged.addHandler(function (s, e) {
        setGaugeProp('showText', s.text)
    });

    document.getElementById('isReadOnly').addEventListener('click', function (e) {
        setGaugeProp('isReadOnly', e.target.checked);
    });

    stepCtl.value = getGaugeProp('step');
    stepCtl.valueChanged.addHandler(function (s, e) {
        setGaugeProp('step', s.value);
    });

    document.getElementById('isAnimated').addEventListener('click', function (e) {
        setGaugeProp('isAnimated', e.target.checked);
    });
});