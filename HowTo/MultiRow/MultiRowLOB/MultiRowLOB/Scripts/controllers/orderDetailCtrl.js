var multiRow;

c1.documentReady(function () {
    multiRow = wijmo.Control.getControl('#orderDetail');
});

// Export the records of current page to xlsx file.
function exportToExcel () {
    exportService.exportXlsx(multiRow, 'OrderDetail.xlsx');
}

// Save the records of current page to PDF file.
function exportToPDF() {
    var styles = {
        cellStyle: {
            backgroundColor: '#ffffff',
            borderColor: '#c6c6c6'
        },
        altCellStyle: {
            backgroundColor: '#C0FFC0'
        },
        headerCellStyle: {
            backgroundColor: '#eaeaea'
        }
    }, isJapanese = culture === 'ja';

    exportService.exportPdf(multiRow, 'OrderDetail.pdf', isJapanese, styles);
}
