c1.documentReady(function () {
    var theSSN = wijmo.Control.getControl('#theSSN');
    var theZip = wijmo.Control.getControl('#theZip');
    var theZip4 = wijmo.Control.getControl('#theZip4');
    var thePhone = wijmo.Control.getControl('#thePhone');

    theSSN.valueChanged.addHandler(validateMask);
    theSSN.lostFocus.addHandler(lostFocus);

    theZip.valueChanged.addHandler(validateMask);
    theZip.lostFocus.addHandler(lostFocus);

    theZip4.valueChanged.addHandler(validateMask);
    theZip4.lostFocus.addHandler(lostFocus);

    thePhone.valueChanged.addHandler(validateMask);
    thePhone.lostFocus.addHandler(lostFocus);

    // update 'state-invalid' class
    function validateMask(s, e) {
        wijmo.toggleClass(s.hostElement, 'state-invalid', !s.maskFull);
    }
  
    // clear masks that are not filled when losing focus
    var clearIncomplete = document.getElementById('clearIncomplete');
    function lostFocus(s, e) {
        if (!s.maskFull && !s.isRequired && clearIncomplete.checked) {
            s.value = '';
        }
    }
});