c1.documentReady(function () {
    var theCombo = wijmo.Control.getControl('#theCombo');

    // select an item
    setSelectedInfo(theCombo);
    theCombo.selectedIndexChanged.addHandler(function (s, e) {
        setSelectedInfo(s);
    });

    function setSelectedInfo(s) {
        setText('theComboText', s.text);
        setText('theComboIndex', s.selectedIndex);
        setText('theComboValue', wijmo.Globalize.format(s.selectedValue, 'c'));
    }

    // show text in an element on the page
    function setText(id, value) {
        document.getElementById(id).textContent = value;
    }
});