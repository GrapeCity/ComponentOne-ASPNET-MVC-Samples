c1.documentReady(function () {
    var theComboNumber = wijmo.Control.getControl('#theComboNumber');
    var theComboDate = wijmo.Control.getControl('#theComboDate');

    // select a number
    theComboNumber.selectedIndexChanged.addHandler(function (s, e) {
        setText('theComboNumberValue', s.selectedValue);
    });
    theComboNumber.itemsSource = [2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71];

    // select a date
    theComboDate.selectedIndexChanged.addHandler(function (s, e) {
        setText('theComboDateValue', s.text);
    });
    theComboDate.formatItem.addHandler(function (s, e) {
        e.item.textContent = wijmo.Globalize.format(e.data, 'd')
    });
    theComboDate.itemsSource = [new Date(2017, 0, 1), new Date(2017, 1, 12), new Date(2017, 1, 22), new Date(2017, 4, 13), new Date(2017, 4, 24), new Date(2017, 8, 19)];

    // show text in an element on the page
    function setText(id, value) {
        document.getElementById(id).textContent = value;
    }
});