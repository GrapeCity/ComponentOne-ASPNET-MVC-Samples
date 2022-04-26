var multiRow, footer;

c1.documentReady(function () {
    multiRow = wijmo.Control.getControl('#purchaseSlip');
    footer = wijmo.Control.getControl('#gridFooter');
    
    if (multiRow) {
        var cv = multiRow.collectionView;
        cv.collectionChanged.addHandler(function (sender, e) {
            var quantity, unitCost;
            if (e.action === wijmo.collections.NotifyCollectionChangedAction.Change && !!e.item) {
                quantity = +e.item.Quantity;
                unitCost = +e.item.UnitCost;
                if (!isNaN(quantity) && !isNaN(unitCost)) {
                    e.item.Cost = quantity * unitCost;
                    e.item.Price = e.item.Cost * 1.35;
                    updateSummary(cv);
                }
            }
        });
        multiRow._root.style.overflowX = 'hidden';

        multiRow.resizedColumn.addHandler(function (sender, e) {
            for (var i = 0; i < footer.columns.length; i++) {
                footer.columns[i].width = multiRow.columns[i].width;
            }
        });

        if (footer) {
            footer.columnLayout = multiRow.columnLayout;
            for (var i = 0; i < footer.columns.length; i++) {
                footer.columns[i].isReadOnly = true;
                footer.columns[i].cssClass = 'summary-footer';
                if (i === 3 || i === 6) {
                    footer.columns[i].cssClass += ' summary-footer-title';
                }
            }
            footer.columns[4].align = 'right';
            footer.rows.push(new wijmo.grid.Row());
            footer.columnHeaders.rows.clear();
            footer.setCellData(0, 3, fields.summary);
            footer.setCellData(0, 6, fields.amountSummary);

            updateSummary(multiRow.collectionView);

            footer.scrollPositionChanged.addHandler(function () {
                if (multiRow) {
                    multiRow.scrollPosition = footer.scrollPosition;
                }
            })
        }
    }
});

// Export the records of current page to xlsx file.
function exportToExcel () {
    var mainWorkbook = wijmo.grid.xlsx.FlexGridXlsxConverter.save(multiRow),
        footerWOrkbook = wijmo.grid.xlsx.FlexGridXlsxConverter.save(footer);

    mainWorkbook.sheets[0].rows.push(footerWOrkbook.sheets[0].rows[0]);
    mainWorkbook.save('PurchaseSlip.xlsx');
}

// Save the records of current page to PDF file.
function exportToPDF () {
    var isJapanese = culture === 'ja';

    var allowAddNew = multiRow.allowAddNew;
    multiRow.allowAddNew = false;
    mergeFooter();
    exportService.exportPdf(multiRow, 'PurchaseSlip.pdf', isJapanese, null);
    multiRow.columnFooters.rows.pop();
    multiRow.allowAddNew = allowAddNew;
}

// Update the summary info for the MultiRow control.
function updateSummary(cv) {
    var qtySum = wijmo.getAggregate(wijmo.Aggregate.Sum, cv.items, 'Quantity'),
        costSum = wijmo.getAggregate(wijmo.Aggregate.Sum, cv.items, 'Cost'),
        priceSum = wijmo.getAggregate(wijmo.Aggregate.Sum, cv.items, 'Price');

    footer.setCellData(0, 4, qtySum);
    footer.setCellData(0, 7, wijmo.Globalize.format(costSum, 'c'));
    footer.setCellData(0, 8, wijmo.Globalize.format(priceSum, 'c'));

}

// Merge the footer to the multiRow control for exporting pdf.
function mergeFooter() {
    var mrgFooter = multiRow.columnFooters,
        rowCnt = mrgFooter.rows.length,
        newRow = new wijmo.grid.Row();
    
    newRow.recordIndex = 0;
    mrgFooter.rows.push(newRow);
    mrgFooter.setCellData(rowCnt, 3, footer.getCellData(0, 3));
    mrgFooter.setCellData(rowCnt, 4, footer.getCellData(0, 4));
    mrgFooter.setCellData(rowCnt, 6, footer.getCellData(0, 6), false);
    mrgFooter.setCellData(rowCnt, 7, footer.getCellData(0, 7));
    mrgFooter.setCellData(rowCnt, 8, footer.getCellData(0, 8));
}