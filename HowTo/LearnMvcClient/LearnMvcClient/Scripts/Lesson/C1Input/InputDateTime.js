c1.documentReady(function () {
    var theInputDateTime = wijmo.Control.getControl('#theInputDateTime');
    theInputDateTime.valueChanged.addHandler(function (s, e) {
        showDateTime(s.value);
    });

    // initialize the date/time value
    var dt = new Date();
    dt.setHours(17, 30, 0);
    theInputDateTime.value = dt;

    // show changes
    function showDateTime(value) {
        var el = document.getElementById('dateTime');
        el.textContent = wijmo.Globalize.format(value, 'F')
    }
});