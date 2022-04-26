c1.documentReady(function () {
    // update radial gauge when value changes
    var theInputRange = document.getElementById('theInputRange');
    theInputRange.addEventListener('input', function (e) {
        setValue(e.target.value * 1);
    });

    //LinearGauge with default settings
    var theLinearGauge = wijmo.Control.getControl('#theLinearGauge');
    theLinearGauge.isReadOnly = false;
    theLinearGauge.valueChanged.addHandler(function (s, e) {
        setValue(s.value);
    });

    //LinearGauge with some styling
    var theLinearGaugeCSS = wijmo.Control.getControl('#theLinearGaugeCSS');
    theLinearGaugeCSS.valueChanged.addHandler(function (s, e) {
        setValue(s.value);
    });

    // update all controls when the value changes
    var theValue = document.getElementById('theValue');
    setValue(50);
    function setValue(value) {
        theValue.textContent = value;
        theInputRange.value = value;
        theLinearGauge.value = value;
        theLinearGaugeCSS.value = value;
    }
});