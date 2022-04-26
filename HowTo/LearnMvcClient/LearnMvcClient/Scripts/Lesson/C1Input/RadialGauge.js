c1.documentReady(function () {
    // update radial gauge when value changes
    var theInputRange = document.getElementById('theInputRange');
    theInputRange.addEventListener('input', function (e) {
        setValue(e.target.value * 1);
    });

    // RadialGauge with default settings
    var theRadialGauge = wijmo.Control.getControl('#theRadialGauge');
    theRadialGauge.isReadOnly = false;
    theRadialGauge.valueChanged.addHandler(function (s, e) {
        setValue(s.value);
    });

    // RadialGauge with some styling
    var theRadialGaugeCSS = wijmo.Control.getControl('#theRadialGaugeCSS');
    theRadialGaugeCSS.valueChanged.addHandler(function (s, e) {
        setValue(s.value);
    });

    // update all controls when the value changes
    var theValue = document.getElementById('theValue');
    setValue(50);
    function setValue(value) {
        theValue.textContent = value;
        theInputRange.value = value;
        theRadialGauge.value = value;
        theRadialGaugeCSS.value = value;
    }
});