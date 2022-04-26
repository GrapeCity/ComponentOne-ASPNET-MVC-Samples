c1.documentReady(function () {
    var cv = c1.getService("collectionView");
    for (var i = 0; i < cv.items.length; i++) {
        cv.items[i].Comment = i % 5 == 0 ? 'This item has a long comment so it will wrap in the cell.' : '';
    }

    // column properties
    var theGrid = wijmo.Control.getControl('#theGrid');
    theGrid.initialize({
        autoGenerateColumns: false,
        columns: [
            { binding: 'Id', header: 'ID', align: 'center', width: 50 },
            { binding: 'Country', header: 'Country', cssClass: 'cell-country' },
            { binding: 'Product', header: 'Product', cssClass: 'cell-product' },
            { binding: 'Comment', header: 'Comment', wordWrap: true, width: 200 },
            { binding: 'Sales', header: 'Sales', format: 'c0', align: 'center' },
            { binding: 'Expenses', header: 'Expenses', format: 'c0', align: 'center' },
        ],
    });

    // call autosizerows to show word-wrapping
    theGrid.autoSizeRows();

    // formatItem event
    var theGridFormatItem = wijmo.Control.getControl('#theGridFormatItem');
    theGridFormatItem.initialize({
        autoGenerateColumns: false,
        columns: [
            { binding: 'Country', header: 'Country' },
            { binding: 'Product', header: 'Product' },
            { binding: 'Sales', header: 'Sales', format: 'c0' },
            { binding: 'Expenses', header: 'Expenses', format: 'c0' },
        ],
        formatItem: function (s, e) {
            if (e.panel == s.cells) {
                var value = e.panel.getCellData(e.row, e.col);
                wijmo.toggleClass(e.cell, 'high-value', wijmo.isNumber(value) && value > 60000);
                wijmo.toggleClass(e.cell, 'low-value', wijmo.isNumber(value) && value < 20000);
            }
        }
    });
});