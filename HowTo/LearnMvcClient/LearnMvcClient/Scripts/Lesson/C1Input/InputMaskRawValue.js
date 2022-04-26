c1.documentReady(function () {
    var theSSN = wijmo.Control.getControl('#theSSN');
    var theZip = wijmo.Control.getControl('#theZip');
    var theZip4 = wijmo.Control.getControl('#theZip4');
    var thePhone = wijmo.Control.getControl('#thePhone');

    theSSN.valueChanged.addHandler(function (s, e) {
        showRaw(s);
    });
    theZip.valueChanged.addHandler(function (s, e) {
        showRaw(s);
    });
    theZip4.valueChanged.addHandler(function (s, e) {
        showRaw(s);
    });
    thePhone.valueChanged.addHandler(function (s, e) {
        showRaw(s);
    });

    function showRaw(s) {
        var el = document.getElementById(s.hostElement.id + 'Raw');
        el.textContent = ' rawValue: ' + s.rawValue;
    }
});