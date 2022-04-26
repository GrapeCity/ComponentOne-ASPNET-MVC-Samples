var exportService = {
    exportXlsx: function (multiRow, fileName) {
        wijmo.grid.xlsx.FlexGridXlsxConverter.save(multiRow, null, fileName);
    },

    exportPdf: function (multiRow, fileName, isJapanese, customStyles, formatItem) {
        var doc = new wijmo.pdf.PdfDocument({
            header: {
                declarative: {
                    text: '\t&[Page]\\&[Pages]'
                }
            },
            footer: {
                declarative: {
                    text: '\t&[Page]\\&[Pages]'
                }
            },
            ended: function (sender, args) {
                wijmo.pdf.saveBlob(args.blob, fileName)
            }
        }),
        settings = {
            styles: {
                cellStyle: {
                    backgroundColor: '#ffffff',
                    borderColor: '#c6c6c6'
                },
                altCellStyle: {
                    backgroundColor: '#f9f9f9'
                },
                headerCellStyle: {
                    backgroundColor: '#eaeaea'
                }
            }
        };

        if (!!customStyles) {
            settings.styles = customStyles;
        }

        if (!!formatItem) {
            settings.formatItem = formatItem;
        }

        // Set Japanese font for Japanese culture.
        if (isJapanese) {
            doc.registerFont({
                source: '/content/fonts/ipaexg00201/ipaexg.ttf',
                name: 'ipaexg',
                style: 'normal',
                weight: 'normal',
                sansSerif: true
            });
            settings.styles.cellStyle.font = new wijmo.pdf.PdfFont('ipaexg');
        }

        wijmo.grid.pdf.FlexGridPdfConverter.draw(multiRow, doc, null, null, settings);
        doc.end();
    }
};