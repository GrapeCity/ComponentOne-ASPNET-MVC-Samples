c1.documentReady(function () {
    // a regular input date
    var theDate = wijmo.Control.getControl('#theDate');
    showCurrentDate();
    theDate.valueChanged.addHandler(function (s, e) {
        showCurrentDate();
    });

    // you can clear this one
    var theDateNotRequired = wijmo.Control.getControl('#theDateNotRequired');
    theDateNotRequired.isRequired = false;
    theDateNotRequired.value = null;

    // show the currently selected date
    function showCurrentDate() {
        var el = document.getElementById('theDateOutput');
        el.textContent = wijmo.Globalize.format(theDate.value, 'D');
    }
});