c1.documentReady(function () {
    var theComboNoSrc = wijmo.Control.getControl('#theComboNoSrc');
    var theCombo = wijmo.Control.getControl('#theCombo');
    var theComboCustom = wijmo.Control.getControl('#theComboCustom');

    setText('theComboText', theComboNoSrc.text);
    theComboNoSrc.textChanged.addHandler(function (s, e) {
        setText('theComboText', s.text);
    });

    setText('theComboValue', theCombo.selectedValue);
    theCombo.textChanged.addHandler(function (s, e) {
        setText('theComboValue', s.selectedValue);
    });

    setText('theComboCustomValue', theComboCustom.text);
    theComboCustom.textChanged.addHandler(function (s, e) {
        setText('theComboCustomValue', s.text);
    });

    // handle checkboxes
    document.getElementById('isRequired').addEventListener('click', function (e) {
        theComboCustom.isRequired = e.target.checked;
    })
    document.getElementById('isEditable').addEventListener('click', function (e) {
        theComboCustom.isEditable = e.target.checked;
    })

    // show text in an element on the page
    function setText(id, value) {
        document.getElementById(id).textContent = value;
    }
});