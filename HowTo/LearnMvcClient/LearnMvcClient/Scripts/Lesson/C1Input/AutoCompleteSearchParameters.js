c1.documentReady(function () {
    var theAutoComplete = wijmo.Control.getControl('#theAutoComplete');
    var theDelay = wijmo.Control.getControl('#theDelay');
    var theMinLength = wijmo.Control.getControl('#theMinLength');
    var theMaxItems = wijmo.Control.getControl('#theMaxItems');

    theDelay.value = theAutoComplete.delay;
    theDelay.valueChanged.addHandler(function (s, e) {
        theAutoComplete.delay = s.value;
    });

    theMinLength.value = theAutoComplete.minLength;
    theMinLength.valueChanged.addHandler(function (s, e) {
        theAutoComplete.minLength = s.value;
    });

    theMaxItems.value = theAutoComplete.maxItems;
    theMaxItems.valueChanged.addHandler(function (s, e) {
        theAutoComplete.maxItems = s.value;
    });
});