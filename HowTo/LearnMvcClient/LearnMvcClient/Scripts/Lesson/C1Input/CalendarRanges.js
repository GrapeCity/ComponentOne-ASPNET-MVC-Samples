c1.documentReady(function () {
    // Calendar with a range restriction
    var theCalendar = wijmo.Control.getControl('#theCalendar');
    var curr = new Date(),
        firstDay = new Date(curr.setDate(curr.getDate() - curr.getDay())),
        lastDay = new Date(curr.setDate(curr.getDate() - curr.getDay() + 6));
    theCalendar.min = firstDay;
    theCalendar.max = lastDay;
});