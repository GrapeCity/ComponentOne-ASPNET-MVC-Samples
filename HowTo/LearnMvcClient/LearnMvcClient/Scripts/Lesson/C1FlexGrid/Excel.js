c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');

    // export grid to XLSX
    document.getElementById('btnExport').addEventListener('click', function () {

        // create book with current view
        var book = wijmo.grid.xlsx.FlexGridXlsxConverter.save(theGrid, {
            includeColumnHeaders: true,
            includeRowHeaders: true
        });
        book.sheets[0].name = 'Learn MVC Client';

        // save the book
        book.save('LearnMvcClient.xlsx');
    })
});