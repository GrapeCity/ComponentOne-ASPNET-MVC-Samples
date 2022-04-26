c1.documentReady(function () {
    // InputColor
    var theInputColor = wijmo.Control.getControl('#theInputColor');
    theInputColor.valueChanged.addHandler(function (s, e) {
        setBackground(s.value);
    });

    // ColorPicker
    var theColorPicker = wijmo.Control.getControl('#theColorPicker');
    theColorPicker.valueChanged.addHandler(function (s, e) {
        setBackground(s.value);
    });

    // show the color that was selected
    function setBackground(color) {
        document.getElementById('output').style.background = color;
        theInputColor.value = color;
        theColorPicker.value = color;
    }
});