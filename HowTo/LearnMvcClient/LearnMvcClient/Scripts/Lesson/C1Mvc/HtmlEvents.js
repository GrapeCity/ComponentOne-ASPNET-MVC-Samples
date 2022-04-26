c1.documentReady(function () {
    theComboBox = wijmo.Control.getControl('#theComboBox');
    theComboBox.addEventListener(theComboBox.hostElement, 'mouseenter', function () {
        log.textContent = 'mouseenter on ComboBox';
    });
    theComboBox.addEventListener(theComboBox.hostElement, 'mouseleave', function () {
        log.textContent = 'mouseleave on ComboBox';
    });

    var theDate = wijmo.Control.getControl('#theDate');
    theDate.addEventListener(theDate.hostElement, 'mouseenter', function () {
        log.textContent = 'mouseenter on InputDate';
    });
    theDate.addEventListener(theDate.hostElement, 'mouseleave', function () {
        log.textContent = 'mouseleave  on InputDate';
    });
});