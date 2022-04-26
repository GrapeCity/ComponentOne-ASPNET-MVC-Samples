c1.documentReady(function () {
    theComboBox = wijmo.Control.getControl('#theComboBox');
    theComboBox.selectedIndexChanged.addHandler(function (s, e) {
        alert('You selected a new item: ' + s.selectedValue);
    })

    var theDate = wijmo.Control.getControl('#theDate');
    theDate.valueChanged.addHandler(function (s, e) {
        alert('You selected a new date: ' + s.value)
    })
});