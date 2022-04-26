c1.documentReady(function () {
    var theComboString = wijmo.Control.getControl('#theComboString');
    var theComboObject = wijmo.Control.getControl('#theComboObject');

    // select a country (string)
    theComboString.selectedIndexChanged.addHandler(function (s, e) {
        showSelectedString(s);
    });

    // select an item (object)
    theComboObject.selectedIndexChanged.addHandler(function (s, e) {
        showSelectedObject(s);
    });

    showSelectedString(theComboString);
    showSelectedObject(theComboObject);

    function showSelectedString(s) {
        setText('theComboStringCurrent', s.text);
    }

    function showSelectedObject(s) {
        var text = wijmo.format('{Country} (sales: {Sales:c0}, expenses {Expenses:c0})', s.selectedValue);
        setText('theComboObjectCurrent', text);
    }

    // show text in an element on the page
    function setText(id, value) {
        document.getElementById(id).textContent = value;
    }
});