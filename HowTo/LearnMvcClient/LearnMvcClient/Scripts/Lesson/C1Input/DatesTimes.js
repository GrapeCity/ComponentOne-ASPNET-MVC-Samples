c1.documentReady(function () {
    // we can edit date without changing time
    var theInputDateOnly = wijmo.Control.getControl('#theInputDateOnly');
    theInputDateOnly.valueChanged.addHandler(function (s, e) {
        setDateTime(s.value);
    });

    // we can edit time without changing date
    var theInputTimeOnly = wijmo.Control.getControl('#theInputTimeOnly');
    theInputTimeOnly.valueChanged.addHandler(function (s, e) {
        setDateTime(s.value);
    });

    // or edit the whole thing
    var theInputDateTime = wijmo.Control.getControl('#theInputDateTime');
    theInputDateTime.valueChanged.addHandler(function (s, e) {
        setDateTime(s.value);
    });

    // initialize and update the date/time value
    var dt = new Date();
    dt.setHours(17,30);
    setDateTime(dt);
    function setDateTime(value) {
        var el = document.getElementById('dateTime');
        el.textContent = wijmo.Globalize.format(value, 'F')
        theInputDateOnly.value = value;
        theInputTimeOnly.value = value;
        theInputDateTime.value = value;
    }
});