c1.documentReady(function () {
    var theInputDate = wijmo.Control.getControl('#theInputDate');
    theInputDate.valueChanged.addHandler(function (s, e) {
        setDateTime(s.value);
    });

    var theInputTime = wijmo.Control.getControl('#theInputTime');
    theInputTime.min = '9:00'; // list starts at 9am
    theInputTime.max = '17:00'; // until 5pm
    theInputTime.step = 30; // every 30 minutes
    theInputTime.format = 'h:mm tt'; // default format, with AM/PM
    theInputTime.isEditable = true; // user can enter values not on the list
    theInputTime.valueChanged.addHandler(function (s, e) {
        setDateTime(s.value);
    });

    // initialize and update the date/time value
    var dt = new Date();
    dt.setHours(17, 30);
    setDateTime(dt);
    function setDateTime(value) {
        var el = document.getElementById('dateTime');
        el.textContent = wijmo.Globalize.format(value, 'F')
        theInputDate.value = value;
        theInputTime.value = value;
    }
});