c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');
    theGrid.beginningEdit.addHandler(function (s, e) {
        if (e.data && e.data.type == 'keypress' &&
            !document.getElementById('quickEdit').checked) {
            e.cancel = true;
        }
    });
});