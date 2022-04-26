// Formulas
var ctxFormulas = {
    flexSheet: null,
    currentCellData:null
};

function loadFormulasSheet() {
    ctxFormulas.flexSheet = wijmo.Control.getControl('#formulaSheet');
    flexSheet = ctxFormulas.flexSheet;
    flexSheet.selectionChanged.addHandler(function (sender, args) {
        var selection = args.range;
        if (selection.isValid) {
            ctxFormulas.currentCellData = ctxFormulas.flexSheet.getCellData(selection.row, selection.col, true);
            document.getElementById('dvCurrentCellData').innerText = ctxFormulas.currentCellData;
        }
    });
    flexSheet.deferUpdate(function () {
        generateExpenceReport(flexSheet);
    });
};

// Set content for the use case template sheet.
function generateExpenceReport(flexSheet) {
    flexSheet.setCellData(1, 1, 'Expense Report');
    flexSheet.setCellData(3, 1, 'Date');
    flexSheet.setCellData(3, 2, 'Fuel');
    flexSheet.setCellData(3, 3, 'Parking(per hour)');
    flexSheet.setCellData(3, 4, 'Parking(hours)');
    flexSheet.setCellData(3, 5, 'Total');;
    flexSheet.setCellData(9, 1, 'Total');
    flexSheet.setCellData(10, 4, 'Subtotal');
    flexSheet.setCellData(11, 4, 'Cash Advances');
    flexSheet.setCellData(12, 4, 'Total');

    setExpenseData(flexSheet);

    applyStyleForExpenceReport(flexSheet);
}

// set expense detail data for the use case template sheet.
function setExpenseData(flexSheet) {
    var rowIndex,
        colIndex,
        value;

    for (rowIndex = 4; rowIndex <= 8; rowIndex++) {
        for (colIndex = 2; colIndex <= 5; colIndex++) {
            if (colIndex === 5) {
                flexSheet.setCellData(rowIndex, colIndex, '=C' + (rowIndex + 1) + ' + Product(C' + (rowIndex + 1) + ':D' + (rowIndex + 1) + ')');
            } else if (colIndex === 4) {
                value = parseInt(7 * Math.random()) + 1;
                flexSheet.setCellData(rowIndex, colIndex, value);
            } else if (colIndex === 3) {
                flexSheet.setCellData(rowIndex, colIndex, 3.75);
            } else {
                value = 200 * Math.random();
                flexSheet.setCellData(rowIndex, colIndex, value);
            }
        }
    }

    flexSheet.setCellData(4, 1, '2015-3-1');
    flexSheet.setCellData(5, 1, '2015-3-3');
    flexSheet.setCellData(6, 1, '2015-3-7');
    flexSheet.setCellData(7, 1, '2015-3-11');
    flexSheet.setCellData(8, 1, '2015-3-18');
    flexSheet.setCellData(9, 2, '=Sum(C5:C9)');
    flexSheet.setCellData(9, 4, '=Sum(Product(D5:E5), Product(D6:E6), Product(D7:E7), Product(D8:E8), Product(D9:E9))');
    flexSheet.setCellData(9, 5, '=Sum(F5:F9)');
    flexSheet.setCellData(10, 5, '=F13-F12');
    flexSheet.setCellData(11, 5, 800);
    flexSheet.setCellData(12, 5, '=F10');
}

// Apply styles for the use case template sheet.
function applyStyleForExpenceReport(flexSheet) {
    flexSheet.columns[0].width = 10;
    flexSheet.columns[1].width = 90;
    flexSheet.columns[2].width = 80;
    flexSheet.columns[3].width = 140;
    flexSheet.columns[4].width = 120;
    flexSheet.columns[5].width = 80;
    for (var i = 2; i <= 3; i++) {
        flexSheet.columns[i].format = 'c2';
    }
    flexSheet.columns[5].format = 'c2';
    flexSheet.rows[1].height = 45;
    flexSheet.applyCellsStyle({
        fontSize: '24px',
        fontWeight: 'bold',
        color: '#696964'
    }, [new wijmo.grid.CellRange(1, 1, 1, 3)]);
    flexSheet.mergeRange(new wijmo.grid.CellRange(1, 1, 1, 3));
    flexSheet.applyCellsStyle({
        fontWeight: 'bold',
        backgroundColor: '#FAD9CD',
    }, [new wijmo.grid.CellRange(3, 1, 3, 5),
        new wijmo.grid.CellRange(9, 1, 9, 5)]);
    flexSheet.applyCellsStyle({
        textAlign: 'center'
    }, [new wijmo.grid.CellRange(3, 1, 3, 5)]);
    flexSheet.applyCellsStyle({
        format: 'c2'
    }, [new wijmo.grid.CellRange(9, 4, 9, 4)]);
    flexSheet.applyCellsStyle({
        backgroundColor: '#F4B19B'
    }, [new wijmo.grid.CellRange(4, 1, 8, 5)]);
    flexSheet.applyCellsStyle({
        fontWeight: 'bold',
        textAlign: 'right'
    }, [new wijmo.grid.CellRange(10, 4, 12, 4)]);
}