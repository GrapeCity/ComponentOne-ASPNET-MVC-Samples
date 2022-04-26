c1.documentReady(function () {
    var theCalendar = wijmo.Control.getControl('#theCalendar');
    theCalendar.valueChanged.addHandler(function (s, e) {
        showCurrentDate();
    });

    // show the currently selected date
    function showCurrentDate() {
        var el = document.getElementById('theCalendarOutput');
        el.textContent = wijmo.Globalize.format(theCalendar.value, 'D');
    }
    showCurrentDate();
});