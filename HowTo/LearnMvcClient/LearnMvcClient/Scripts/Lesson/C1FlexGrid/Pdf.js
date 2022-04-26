c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');

    // export grid to PDF
    document.getElementById('btnExport').addEventListener('click', function () {
        wijmo.grid.pdf.FlexGridPdfConverter.export(theGrid, 'LearnMvcClient.pdf', {
            maxPages: 10,
            scaleMode: wijmo.grid.pdf.ScaleMode.PageWidth,
            documentOptions: {
                compress: true,
                header: { declarative: { text: '\t&[Page] of &[Pages]' } },
                footer: { declarative: { text: '\t&[Page] of &[Pages]' } },
                info: { author: 'C1', title: 'Learn Wijmo' }
            },
            styles: {
                cellStyle: { backgroundColor: '#ffffff', borderColor: '#c6c6c6' },
                altCellStyle: { backgroundColor: '#f9f9f9' },
                groupCellStyle: { backgroundColor: '#dddddd' },
                headerCellStyle: { backgroundColor: '#eaeaea' }
            }
        });
    });

    // create a PDF document with the grid and some other content
    document.getElementById('btnCreateDoc').addEventListener('click', function () {

        // create the document
        var doc = new wijmo.pdf.PdfDocument({
            compress: true,
            header: { declarative: { text: '\t&[Page]\\&[Pages]' } },
            ended: function (sender, args) {
                wijmo.pdf.saveBlob(args.blob, 'LearnMvcClientDoc.pdf');
            }
        });

        // add some content
        doc.drawText('This is a FlexGrid');

        // add the grid (400pt wide)
        wijmo.grid.pdf.FlexGridPdfConverter.draw(theGrid, doc, 400);

        // finish document
        doc.end();
    });
});