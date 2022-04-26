c1.documentReady(function () {
    // InputDate with a range restriction
    var theInputDate = wijmo.Control.getControl('#theInputDate');

    var curr = new Date(),
        firstDay = new Date(curr.setDate(curr.getDate() - curr.getDay())),
        lastDay = new Date(curr.setDate(curr.getDate() - curr.getDay() + 6));

    theInputDate.min = firstDay;
    theInputDate.max = lastDay;
});