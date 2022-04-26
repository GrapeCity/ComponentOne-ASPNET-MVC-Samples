c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');

    var countries = 'US,Germany,UK,Japan,Sweden,Norway,Denmark'.split(',');
    theGrid.columns[1].dataMap = countries;
    theGrid.gotFocus.addHandler(function(s, e) {
        s.startEditing(false); // quick mode
    });
    theGrid.selectionChanged.addHandler(function (s, e) {
        setTimeout(function () {
            s.startEditing(false); // quick mode
        }, 50); // let the grid update first
    });
});