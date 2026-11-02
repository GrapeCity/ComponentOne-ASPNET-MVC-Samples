var ctx = {
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

c1.documentReady(function () {
    var flexSheet;
    ctx.flexSheet = wijmo.Control.getControl('#tableSheet');
    flexSheet = ctx.flexSheet;

    ctx.tableOptions = document.getElementById('tableOptions');
    ctx.headerRow = document.getElementById('headerRow');
    ctx.totalRow = document.getElementById('totalRow');
    ctx.bandedRows = document.getElementById('bandedRows');
    ctx.bandedColumns = document.getElementById('bandedColumns');
    ctx.firstColumn = document.getElementById('firstColumn');
    ctx.lastColumn = document.getElementById('lastColumn');
    ctx.builtInStyles = wijmo.Control.getControl('#builtInStyles');

    updateTableProperty(ctx.headerRow, "showHeaderRow");
    updateTableProperty(ctx.totalRow, "showTotalRow");
    updateTableProperty(ctx.bandedRows, "showBandedRows");
    updateTableProperty(ctx.bandedColumns, "showBandedColumns");
    updateTableProperty(ctx.firstColumn, "alterFirstColumn");
    updateTableProperty(ctx.lastColumn, "alterLastColumn");

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
    ctx.builtInStyles.itemsSource = tableStyleNames;
    ctx.builtInStyles.selectedIndexChanged.addHandler(function (sender) {
        // apply the table style for the selected table.
        if (ctx.selectedTable) {
            var tableStyle = ctx.flexSheet.getBuiltInTableStyle(sender.selectedValue);
            ctx.selectedTable.style = tableStyle;
        }
    });

    if (flexSheet) {
        flexSheet.selectionChanged.addHandler(function (sender, args) {
            var selection = args.range;
            if (selection.isValid) {
                getSelectedTable(selection, flexSheet);
            } else {
                ctx.selectedTable = null;
            }
        });

        flexSheet.updatedLayout.addHandler(function () {
            if (flexSheet.selection && flexSheet.selection.isValid) {
                getSelectedTable(flexSheet.selection, flexSheet);
            } else {
                ctx.selectedTable = null;
            }
        });
    }
});

// Get selected table in FlexSheet.
function getSelectedTable(seletion, flexSheet) {
    if (flexSheet && flexSheet.selectedSheet != null) {
        ctx.selectedTable = flexSheet.selectedSheet.findTable(seletion.row, seletion.col);
    }

    updateControls();
}

function updateControls() {
    if (ctx.selectedTable == null) {
        ctx.tableOptions.style.display = "none";
    } else {
        ctx.tableOptions.style.display = "";

        ctx.headerRow.checked = ctx.selectedTable.showHeaderRow;
        ctx.totalRow.checked = ctx.selectedTable.showTotalRow;
        ctx.bandedRows.checked = ctx.selectedTable.showBandedRows;
        ctx.bandedColumns.checked = ctx.selectedTable.showBandedColumns;
        ctx.firstColumn.checked = ctx.selectedTable.alterFirstColumn;
        ctx.lastColumn.checked = ctx.selectedTable.alterLastColumn;

        var tableStyle = ctx.flexSheet.getBuiltInTableStyle(ctx.selectedTable.style.name);
        ctx.builtInStyles.selectedValue = tableStyle.name;
    }
}

function updateTableProperty(input, property) {
    input.addEventListener("click", function (e) {
        if (ctx.selectedTable) {
            ctx.selectedTable[property] = e.target.checked;
        }
    });
}