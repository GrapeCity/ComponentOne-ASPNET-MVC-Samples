c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');

    applyRowsStyle();
    theGrid.loadedRows.addHandler(function (s, e) {
        applyRowsStyle();
    });

    function applyRowsStyle()
    {
        // apply cssClass to rows after loading them
        for (var i = 0; i < theGrid.rows.length; i++) {
            var row = theGrid.rows[i];
            var item = row.dataItem;
            if (item.Sales > 60000) {
                row.cssClass = 'high-value';
            } else if (item.Sales < 10000) {
                row.cssClass = 'low-value';
            }
        }
    }

    // toggle showAlternatingRows
    document.getElementById('showAlternatingRows').addEventListener('click', function (e) {
        theGrid.alternatingRowStep = e.target.checked ? 1 : 0;
    })
});