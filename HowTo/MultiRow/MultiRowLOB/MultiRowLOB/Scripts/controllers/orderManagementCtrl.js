var multiRow;

c1.documentReady(function () {
    multiRow = wijmo.Control.getControl('#orderManagement');
    if (multiRow) {
        var cv = multiRow.collectionView;
        cv.collectionChanged.addHandler(function (sender, e) {
            var unitPrice, quantity;
            if (e.action === wijmo.collections.NotifyCollectionChangedAction.Change && !!e.item) {
                unitPrice = +e.item.UnitPrice;
                quantity = +e.item.Quantity;
                if (!isNaN(unitPrice) && !isNaN(quantity)) {
                    e.item.Amount = unitPrice * quantity;
                }
            }
        });
    }
});

// Export the records of current page to xlsx file.
function exportToExcel() {
    exportService.exportXlsx(multiRow, 'OrderManagement.xlsx');
}

// Save the records of current page to PDF file.
function exportToPDF() {
    var isJapanese = culture === 'ja';

    exportService.exportPdf(multiRow, 'OrderManagement.pdf', isJapanese, null);
}