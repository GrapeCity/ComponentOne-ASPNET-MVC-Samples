var multiRow;

c1.documentReady(function () {
    multiRow = wijmo.Control.getControl('#salesSlip');
    if (multiRow) {
        var cv = multiRow.collectionView;
        cv.collectionChanged.addHandler(function (sender, e) {
            var unitPrice, profitUnitPrice, quantity;
            if (e.action === wijmo.collections.NotifyCollectionChangedAction.Change && !!e.item) {
                unitPrice = +e.item.UnitPrice;
                profitUnitPrice = +e.item.ProfitUnitPrice;
                quantity = +e.item.Quantity;
                if (!isNaN(quantity)) {
                    if (!isNaN(unitPrice)) {
                        e.item.SalesAmount = unitPrice * quantity;
                    }
                    if (!isNaN(profitUnitPrice)) {
                        e.item.TotalProfit = profitUnitPrice * quantity;
                    }
                    if (!isNaN(unitPrice) && !isNaN(profitUnitPrice)) {
                        e.item.ProfitRate = e.item.TotalProfit / e.item.SalesAmount;
                    }
                }
            }
        });
    }
});

// Export the records of current page to xlsx file.
function exportToExcel() {
    exportService.exportXlsx(multiRow, 'SalesSlip.xlsx');
}

// Save the records of current page to PDF file.
function exportToPDF() {
    var isJapanese = culture === 'ja';

    var formatItem = function (args) {
        // Change the background color of the regular cells of "Country" column.
        if (args.panel.cellType === wijmo.grid.CellType.Cell) {
            var column = multiRow.getBindingColumn(args.panel, args.row, args.col);
            if (column.binding === "TotalProfit" || column.binding === "ProfitRate") {
                args.style.backgroundColor = '#FFFFC0';
                args.style.color = 'red';
            }
        }
    }

    exportService.exportPdf(multiRow, 'SalesSlip.pdf', isJapanese, null, formatItem);
}