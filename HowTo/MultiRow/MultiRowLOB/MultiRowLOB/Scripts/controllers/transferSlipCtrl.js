var multiRow, slipDate, slipNo, slipSettlement, debtorSum, creditorSum, balance;

c1.documentReady(function () {
    multiRow = wijmo.Control.getControl('#transferSlip');
    if (multiRow) {
        cv = multiRow.collectionView;
        updateSummary(cv);

        multiRow.cellEditEnded.addHandler(function () {
            updateSummary(cv);
        });
        cv.pageChanged.addHandler(function () {
            updateSummary(cv);
        });
        cv.collectionChanged.addHandler(function (sender, e) {
            var debtorAmt, creditorAmt;
            if (e.action === wijmo.collections.NotifyCollectionChangedAction.Change && !!e.item) {
                debtorAmt = +e.item.DebtorAmt;
                creditorAmt = +e.item.CreditorAmt;
                if (!isNaN(debtorAmt)) {
                    e.item.DebtorTax = e.item.DebtorAmt * 0.09;
                }
                if (!isNaN(creditorAmt)) {
                    e.item.CreditorTax = e.item.CreditorAmt * 0.09;
                }
            }
        });
    }
});

// Update summary info for the footer of the multirow control.v
function updateSummary(cv){
    debtorSumVal = wijmo.getAggregate(wijmo.Aggregate.Sum, cv.items, 'DebtorAmt');
    creditorSumVal = wijmo.getAggregate(wijmo.Aggregate.Sum, cv.items, 'CreditorAmt');
    balanceVal = debtorSumVal - creditorSumVal;

    document.getElementById('debtorSum').innerText = debtorSum = wijmo.Globalize.format(debtorSumVal, 'c');
    document.getElementById('creditorSum').innerText = creditorSum =  wijmo.Globalize.format(creditorSumVal, 'c');
    document.getElementById('balance').innerText = balance =  wijmo.Globalize.format(balanceVal, 'c');
}

function updateSlipData() {
    var inputDate = wijmo.Control.getControl('#slipDate');
    slipDate = wijmo.Globalize.format(inputDate.value, 'd');
    slipNo = document.getElementById('slipNo').value;
    slipSettlement = document.getElementById('slipSettlement').value;
}

// Export the records of current page to xlsx file.
function exportToExcel() {
    updateSlipData();

    var workbook = wijmo.grid.xlsx.FlexGridXlsxConverter.save(multiRow);
    var workbookRow = new wijmo.xlsx.WorkbookRow();
    var workbookFill = new wijmo.xlsx.WorkbookFill();
    workbookFill.color = '#8080FF';
    var workbookFont = new wijmo.xlsx.WorkbookFont();
    workbookFont.bold = true;
    var workbookStyle = new wijmo.xlsx.WorkbookStyle();
    workbookStyle.fill = workbookFill;
    workbookStyle.font = workbookFont;
    workbookStyle.hAlign = wijmo.xlsx.HAlign.Center;
    var workbookCell = new wijmo.xlsx.WorkbookCell();
    workbookCell.value = fields.date;
    workbookCell.style = workbookStyle;
    workbookRow.cells.push(workbookCell);
    workbookCell = new wijmo.xlsx.WorkbookCell();
    workbookCell.value = slipDate;
    workbookRow.cells.push(workbookCell);
    workbookCell = new wijmo.xlsx.WorkbookCell();
    workbookCell.value = fields.slipNo;
    workbookCell.style = workbookStyle;
    workbookRow.cells.push(workbookCell);
    workbookCell = new wijmo.xlsx.WorkbookCell();
    workbookCell.value = slipNo;
    workbookRow.cells.push(workbookCell);
    workbookCell = new wijmo.xlsx.WorkbookCell();
    workbookCell.value = fields.settlementTitle;
    workbookCell.style = workbookStyle;
    workbookRow.cells.push(workbookCell);
    workbookCell = new wijmo.xlsx.WorkbookCell();
    workbookCell.value = slipSettlement;
    workbookRow.cells.push(workbookCell);
    workbook.sheets[0].rows.splice(0, 0, workbookRow);
    workbook.sheets[0].frozenPane.rows = 3;

    workbookRow = new wijmo.xlsx.WorkbookRow();
    workbookFill = new wijmo.xlsx.WorkbookFill();
    workbookFill.color = '#99B4D1';
    workbookStyle = new wijmo.xlsx.WorkbookStyle();
    workbookStyle.fill = workbookFill;
    workbookStyle.hAlign = wijmo.xlsx.HAlign.Center;
    workbookCell = new wijmo.xlsx.WorkbookCell();
    workbookCell.value = fields.debtorSumTitle;
    workbookCell.style = workbookStyle;
    workbookRow.cells.push(workbookCell);
    workbookCell = new wijmo.xlsx.WorkbookCell();
    workbookCell.value = debtorSum;
    workbookCell.style = workbookStyle;
    workbookRow.cells.push(workbookCell);
    workbookCell = new wijmo.xlsx.WorkbookCell();
    workbookCell.value = fields.creditorSumTitle;
    workbookCell.style = workbookStyle;
    workbookRow.cells.push(workbookCell);
    workbookCell = new wijmo.xlsx.WorkbookCell();
    workbookCell.value = creditorSum;
    workbookCell.style = workbookStyle;
    workbookRow.cells.push(workbookCell);
    workbookCell = new wijmo.xlsx.WorkbookCell();
    workbookCell.value = fields.debtorCreditorBalanceTitle;
    workbookCell.style = workbookStyle;
    workbookRow.cells.push(workbookCell);
    workbookCell = new wijmo.xlsx.WorkbookCell();
    workbookCell.value = balance;
    workbookCell.style = workbookStyle;
    workbookRow.cells.push(workbookCell);
    workbook.sheets[0].rows.push(workbookRow);
    workbook.save('TransferSlip.xlsx');
}

// Save the records of current page to PDF file.
function exportToPDF () {
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
            wijmo.pdf.saveBlob(args.blob, 'TransferSlip.pdf')
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
        }, font, drawTextSetting, thinPen = new wijmo.pdf.PdfPen('#000000', 0.5);

    // Set Japanese font for Japanese culture.
    if (culture === 'ja') {
        doc.registerFont({
            source: '/content/fonts/ipaexg00201/ipaexg.ttf',
            name: 'ipaexg',
            style: 'normal',
            weight: 'normal',
            sansSerif: true
        });
        font = new wijmo.pdf.PdfFont('ipaexg');
        settings.styles.cellStyle.font = font;
        drawTextSetting = {
            font: font
        };
    }

    updateSlipData();

    // Draw header of the transfer slip.
    doc.paths
       .rect(0.5, 0.5, 50, 21)
       .fill('#8080FF')
       .moveTo(0, 0).lineTo(334, 0)
       .moveTo(334, 0).lineTo(334, 22)
       .moveTo(0, 22).lineTo(334, 22)
       .moveTo(0, 0).lineTo(0, 22).stroke(thinPen);
    doc.drawText(fields.date, 3.5, 5.5, drawTextSetting);
    doc.drawText(wijmo.Globalize.format(slipDate, 'd'), 53.5, 5.5, drawTextSetting);
    doc.paths
       .rect(130.5, 0.5, 50, 21)
       .fill('#8080FF');
    doc.drawText(fields.slipNo, 133.5, 5.5, drawTextSetting);
    doc.drawText(slipNo, 183.5, 5.5, drawTextSetting);
    doc.paths
       .rect(230.5, 0.5, 50, 21)
       .fill('#8080FF');
    doc.drawText(fields.settlementTitle, 233.5, 5.5, drawTextSetting);
    doc.drawText(slipSettlement, 283.5, 5.5, drawTextSetting);
    doc.moveDown();

    // Draw the body of the transfer slip.
    wijmo.grid.pdf.FlexGridPdfConverter.draw(multiRow, doc, null, null, settings);

    // Draw the footer of the transfer slip.
    doc.paths
      .rect(0.5, 274.5, 380, 21)
      .fill('#99B4D1')
      .moveTo(0, 274).lineTo(381, 274)
      .moveTo(381, 274).lineTo(381, 296)
      .moveTo(0, 296).lineTo(381, 296)
      .moveTo(0, 274).lineTo(0, 296)
      .moveTo(60, 274).lineTo(60, 296)
      .moveTo(120, 274).lineTo(120, 296)
      .moveTo(180, 274).lineTo(180, 296)
      .moveTo(240, 274).lineTo(240, 296)
      .moveTo(320, 274).lineTo(320, 296).stroke(thinPen);
    doc.drawText(fields.debtorSumTitle, 3.5, 279.5, drawTextSetting);
    doc.drawText(debtorSum, 63.5, 279.5, drawTextSetting);
    doc.drawText(fields.creditorSumTitle, 123.5, 279.5, drawTextSetting);
    doc.drawText(creditorSum, 183.5, 279.5, drawTextSetting);
    doc.drawText(fields.debtorCreditorBalanceTitle, 243.5, 279.5, drawTextSetting);
    doc.drawText(balance, 323.5, 279.5, drawTextSetting);

    doc.end();
}