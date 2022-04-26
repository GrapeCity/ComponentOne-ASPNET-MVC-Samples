c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');
    theGrid.allowAddNew = true;
    theGrid.allowDelete = true;

    // toggle new row position
    document.getElementById('newRowAtTop').addEventListener('click', function (e) {
        theGrid.newRowAtTop = e.target.checked;
    })
});