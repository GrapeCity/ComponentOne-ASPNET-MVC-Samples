//Table
var ctxTable = {
    flexSheet: null,
    selectedTable: null,
    tableOptions: null,
    headerRow: null,
    totalRow: null,
    bandedRows: null,
    bandedColumns: null,
    firstColumn: null,
    lastColumn: null,
    builtInStyles: null,
};

function loadTable() {
    var flexSheet;
    ctxTable.flexSheet = wijmo.Control.getControl('#tableSheet');
    flexSheet = ctxTable.flexSheet;

    ctxTable.tableOptions = document.getElementById('tableOptions');
    ctxTable.headerRow = document.getElementById('headerRow');
    ctxTable.totalRow = document.getElementById('totalRow');
    ctxTable.bandedRows = document.getElementById('bandedRows');
    ctxTable.bandedColumns = document.getElementById('bandedColumns');
    ctxTable.firstColumn = document.getElementById('firstColumn');
    ctxTable.lastColumn = document.getElementById('lastColumn');
    ctxTable.builtInStyles = wijmo.Control.getControl('#builtInStyles');

    updateTableProperty(ctxTable.headerRow, "showHeaderRow");
    updateTableProperty(ctxTable.totalRow, "showTotalRow");
    updateTableProperty(ctxTable.bandedRows, "showBandedRows");
    updateTableProperty(ctxTable.bandedColumns, "showBandedColumns");
    updateTableProperty(ctxTable.firstColumn, "alterFirstColumn");
    updateTableProperty(ctxTable.lastColumn, "alterLastColumn");

    var tableStyleNames = [];
    for (i = 1; i <= 21; i++) {
        tableStyleNames.push('TableStyleLight' + i);
    }
    for (i = 1; i <= 28; i++) {
        tableStyleNames.push('TableStyleMedium' + i);
    }
    for (i = 1; i <= 11; i++) {
        tableStyleNames.push('TableStyleDark' + i);
    }
    ctxTable.builtInStyles.itemsSource = tableStyleNames;
    ctxTable.builtInStyles.selectedIndexChanged.addHandler(function (sender) {
        // apply the table style for the selected table.
        if (ctxTable.selectedTable) {
            var tableStyle = ctxTable.flexSheet.getBuiltInTableStyle(sender.selectedValue);
            ctxTable.selectedTable.style = tableStyle;
        }
    });

    if (flexSheet) {
        flexSheet.selectionChanged.addHandler(function (sender, args) {
            var selection = args.range;
            if (selection.isValid) {
                getSelectedTable(selection, flexSheet);
            } else {
                ctxTable.selectedTable = null;
            }
        });

        flexSheet.updatedLayout.addHandler(function () {
            if (flexSheet.selection && flexSheet.selection.isValid) {
                getSelectedTable(flexSheet.selection, flexSheet);
            } else {
                ctxTable.selectedTable = null;
            }
        });
    }
}

// Get selected table in FlexSheet.
function getSelectedTable(seletion, flexSheet) {
    if (flexSheet && flexSheet.selectedSheet != null) {
        ctxTable.selectedTable = flexSheet.selectedSheet.findTable(seletion.row, seletion.col);
    }

    updateControls();
}

function updateControls() {
    if (ctxTable.selectedTable == null) {
        ctxTable.tableOptions.style.display = "none";
    } else {
        ctxTable.tableOptions.style.display = "";

        ctxTable.headerRow.checked = ctxTable.selectedTable.showHeaderRow;
        ctxTable.totalRow.checked = ctxTable.selectedTable.showTotalRow;
        ctxTable.bandedRows.checked = ctxTable.selectedTable.showBandedRows;
        ctxTable.bandedColumns.checked = ctxTable.selectedTable.showBandedColumns;
        ctxTable.firstColumn.checked = ctxTable.selectedTable.alterFirstColumn;
        ctxTable.lastColumn.checked = ctxTable.selectedTable.alterLastColumn;

        var tableStyle = ctxTable.flexSheet.getBuiltInTableStyle(ctxTable.selectedTable.style.name);
        ctxTable.builtInStyles.selectedValue = tableStyle.name;
    }
}

function updateTableProperty(input, property) {
    input.addEventListener("click", function (e) {
        if (ctxTable.selectedTable) {
            ctxTable.selectedTable[property] = e.target.checked;
        }
    });
}