c1.documentReady(function () {
    var theNumber = wijmo.Control.getControl('#theNumber');

    theNumber.valueChanged.addHandler(function (s, e) {
        document.getElementById('theNumberOutput').textContent = s.value;
    });
});