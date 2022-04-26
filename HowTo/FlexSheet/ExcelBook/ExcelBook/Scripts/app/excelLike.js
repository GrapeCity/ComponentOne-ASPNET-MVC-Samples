var ctx = {
    applyFillColor: false,
    cellStyleApplying: false,
    updatingSelection: false,
    appliedClass: '',
    isFrozen: false,
    menuFormat: '',
    flexSheet: null,
    cboFontName: null,
    cboFontSize: null,
    boldBtn: null,
    italicBtn: null,
    underlineBtn: null,
    leftBtn: null,
    centerBtn: null,
    rightBtn: null,
    mergeBtn: null,
    frozenBtn: null,
    undoBtn: null,
    redoBtn: null,
    chartTypesBtn: null,
    lbChartTypes: null,
    undoStack: null,
    sortManager: null,
    columns: null,
    moveup: null,
    movedown: null,
    tbody: null,
    selectionFormatState: {},
    selection: {},
    chartEngine: null,
    selectedTable: null,
    tableOptions: null,
    tableHeaderRow: null,
    tableTotalRow: null,
    tableBandedRows: null,
    tableFirstColumn: null,
    tableLastColumn: null,
    tableBandedColumns: null,
    cboTableStyles: null,
};

c1.documentReady(function () {
    initInputs();
    initTableOptions();
    initFlexSheet();
    initSort();
    updateSortTable();
    initChartEngine();
    document.body.addEventListener('keydown', function(e) {
        var lbChartTypes = ctx.lbChartTypes;
        if (lbChartTypes) {
            if (e.keyCode === 27) {
                lbChartTypes.hostElement.style.display = 'none';
            }
            if (e.keyCode === 13 && lbChartTypes.hostElement.style.display !== 'none') {
                addChart();
            }
        }

        hidePopup(e);
    });

    document.body.addEventListener('click', function (e) {
        var lbChartTypes = ctx.lbChartTypes;
        if (lbChartTypes) {
            lbChartTypes.hostElement.style.display = 'none';
        }

        hidePopup(e);
    }, true);
});

function initChartEngine() {
    ctx.chartEngine = new wijmo.grid.sheet.chart.ChartEngine(ctx.flexSheet);
    var lbChartTypes = ctx.lbChartTypes = wijmo.Control.getControl("#lbChartTypes");
    if (lbChartTypes) {
        lbChartTypes.hostElement.addEventListener('click', function() {
            addChart();
        });
    }
}

function addChart() {
    var lbChartTypes = ctx.lbChartTypes, chartType = null;
    if (lbChartTypes.selectedValue != null) {
        chartType = wijmo.asEnum(lbChartTypes.selectedValue, wijmo.chart.ChartType);
    }
    ctx.chartEngine.addChart(chartType);
    lbChartTypes.hostElement.style.display = 'none';
}

function initInputs() {
    ctx.cboFontName = wijmo.Control.getControl('#cboFontName');
    ctx.cboFontSize = wijmo.Control.getControl('#cboFontSize');
    initBtns();
    initColorPicker();
}

function initTableOptions() {
    ctx.tableOptions = document.getElementById('tableOptionsPanel');
    ctx.tableHeaderRow = document.getElementById('tableHeaderRow');
    ctx.tableTotalRow = document.getElementById('tableTotalRow');
    ctx.tableBandedRows = document.getElementById('tableBandedRows');
    ctx.tableBandedColumns = document.getElementById('tableBandedColumns');
    ctx.tableFirstColumn = document.getElementById('tableFirstColumn');
    ctx.tableLastColumn = document.getElementById('tableLastColumn');

    updateTableProperty(ctx.tableHeaderRow, "showHeaderRow");
    updateTableProperty(ctx.tableTotalRow, "showTotalRow");
    updateTableProperty(ctx.tableBandedRows, "showBandedRows");
    updateTableProperty(ctx.tableBandedColumns, "showBandedColumns");
    updateTableProperty(ctx.tableFirstColumn, "alterFirstColumn");
    updateTableProperty(ctx.tableLastColumn, "alterLastColumn");

    initTableStyles();
}

function updateTableOptions() {
    if (ctx.selectedTable == null) {
        var needAdjustSize = ctx.tableOptions.style.display != "none";
        ctx.tableOptions.style.display = "none";
        if (needAdjustSize) {
            adjustSize();
            ctx.flexSheet.refresh();
        }
    } else {
        var needAdjustSize = ctx.tableOptions.style.display != "";
        ctx.tableOptions.style.display = "";
        if (needAdjustSize) {
            adjustSize();
            ctx.flexSheet.refresh();
        }

        ctx.tableHeaderRow.checked = ctx.selectedTable.showHeaderRow;
        ctx.tableTotalRow.checked = ctx.selectedTable.showTotalRow;
        ctx.tableBandedRows.checked = ctx.selectedTable.showBandedRows;
        ctx.tableBandedColumns.checked = ctx.selectedTable.showBandedColumns;
        ctx.tableFirstColumn.checked = ctx.selectedTable.alterFirstColumn;
        ctx.tableLastColumn.checked = ctx.selectedTable.alterLastColumn;

        var tableStyle = ctx.flexSheet.getBuiltInTableStyle(ctx.selectedTable.style.name);
        ctx.cboTableStyles.selectedValue = tableStyle.name;
    }
}

function updateTableProperty(input, property) {
    input.addEventListener("click", function (e) {
        if (ctx.selectedTable) {
            ctx.selectedTable[property] = e.target.checked;
        }
    });
}

function initTableStyles() {
    ctx.cboTableStyles = wijmo.Control.getControl('#cboTableStyles');

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

    ctx.cboTableStyles.itemsSource = tableStyleNames;

    ctx.cboTableStyles.selectedIndexChanged.addHandler(function () {
        // apply the table style for the selected table.
        if (ctx.selectedTable) {
            var tableStyle = ctx.flexSheet.getBuiltInTableStyle(ctx.cboTableStyles.selectedValue);
            ctx.selectedTable.style = tableStyle;
        }
    });
}

function initSort() {
    ctx.sortManager = ctx.flexSheet.sortManager;
    ctx.moveup = wijmo.getElement('#moveup');
    ctx.movedown = wijmo.getElement('#movedown');
    ctx.tbody = wijmo.getElement('#sortTable tbody');
    ctx.columns = getColumns();
    updateSortTable();
}

function initBtns() {
    ctx.boldBtn = wijmo.getElement('#boldBtn');
    ctx.italicBtn = wijmo.getElement('#italicBtn');
    ctx.underlineBtn = wijmo.getElement('#underlineBtn');
    ctx.leftBtn = wijmo.getElement('#leftBtn');
    ctx.centerBtn = wijmo.getElement('#centerBtn');
    ctx.rightBtn = wijmo.getElement('#rightBtn');
    ctx.frozenBtn = wijmo.getElement('#frozenBtn');
    ctx.mergeBtn = wijmo.getElement('#mergeBtn');
    ctx.undoBtn = wijmo.getElement('#undoBtn');
    ctx.redoBtn = wijmo.getElement('#redoBtn');
    ctx.chartTypesBtn = wijmo.getElement('#chartTypesBtn');
}

function updateBtns() {
    updateActiveState(ctx.selectionFormatState.isBold, ctx.boldBtn);
    updateActiveState(ctx.selectionFormatState.isItalic, ctx.italicBtn);
    updateActiveState(ctx.selectionFormatState.isUnderline, ctx.underlineBtn);
    updateActiveState(ctx.selectionFormatState.textAlign === 'left', ctx.leftBtn);
    updateActiveState(ctx.selectionFormatState.textAlign === 'center', ctx.centerBtn);
    updateActiveState(ctx.selectionFormatState.textAlign === 'right', ctx.rightBtn);
    ctx.mergeBtn.title = ctx.selectionFormatState.isMergedCell ? 'UnMerge' : 'Merge';
    ctx.mergeBtn.disabled = ctx.selectedTable != null;
    ctx.frozenBtn.querySelector(".text").innerText = ctx.isFrozen ? 'UnFreeze' : 'Freeze';
    ctx.undoBtn.disabled = !ctx.flexSheet.undoStack.canUndo;
    ctx.redoBtn.disabled = !ctx.flexSheet.undoStack.canRedo;
}

function updateActiveState(condition, btn) {
    condition ? addClass(btn, "active") : removeClass(btn, "active");
}

function initFlexSheet() {
    var flexSheet;
    ctx.flexSheet = wijmo.Control.getControl('#excelLikeSheet');
    flexSheet = ctx.flexSheet;
    if (flexSheet) {
        ctx.undoStack = flexSheet.undoStack;

        flexSheet.selectedSheetChanged.addHandler(function (sender, args) {
            ctx.sortManager = flexSheet.sortManager;

            if (flexSheet.frozenColumns > 0 || flexSheet.frozenRows > 0) {
                ctx.isFrozen = true;
            } else {
                ctx.isFrozen = false;
            }

            updateSortTable();
            updateSelection(flexSheet.selection);
            updateBtns();
        });

        flexSheet.undoStack.undoStackChanged.addHandler(function (sender, args) {
            updateSelection(flexSheet.selection);
            ctx.selectionFormatState = flexSheet.getSelectionFormatState();
            updateBtns();
        });

        flexSheet.selectionChanged.addHandler(function (sender, args) {
            updateSelection(flexSheet.selection);
            ctx.selectionFormatState = flexSheet.getSelectionFormatState();
            updateBtns();
        });

        flexSheet.columns.collectionChanged.addHandler(function () {
            ctx.columns = getColumns();
        });
        flexSheet.loaded.addHandler(function () {
            ctx.columns = getColumns();
            updateSortTable();
        });

        flexSheet.updatedLayout.addHandler(function () {
            if (flexSheet.selection && flexSheet.selection.isValid) {
                getSelectedTable(flexSheet.selection);
            } else {
                ctx.selectedTable = null;
            }
            updateTableOptions();
        });

        if (ctx.sortManager) {
            ctx.sortManager = flexSheet.sortManager;
        }

        initSheetData();
    }
}

function initSheetData() {
    var s = ctx.flexSheet, i = 0;
    for (; i < s.sheets.length; i++) {
        s.sheets.selectedIndex = i;
        switch (s.sheets[i].name) {
            case 'Country':
                applyDataMap(s);
                break;
            case 'Report':
                generateUseCaseTemplateSheet(s);
                break;
            case 'Formulas':
                generateFormulasSheet(s);
                break;
            case 'Table':
                generateTableSheet(s);
                break;
        }
    }

    s.sheets.selectedIndex = 0;
}

function initColorPicker() {
    var colorPicker = ctx.colorPicker = wijmo.Control.getControl('#colorPicker'),
        ua = window.navigator.userAgent,
        blurEvt;

    if (colorPicker) {
        // if the browser is firefox, we should bind the blur event. (TFS #124387)
        // if the browser is IE, we should bind the focusout event. (TFS #124500)
        blurEvt = /firefox/i.test(ua) ? 'blur' : 'focusout';
        // Hide the color picker control when it lost the focus.
        colorPicker.hostElement.addEventListener(blurEvt, function () {
            setTimeout(function () {
                if (!colorPicker.containsFocus()) {
                    ctx.applyFillColor = false;
                    colorPicker.hostElement.style.display = 'none';
                }
            }, 0);
        });

        colorPicker.hostElement.addEventListener('keydown', function (e) {
            if (e.keyCode === 27) {
                colorPicker.hostElement.style.display = 'none';
            }
        });

        // Initialize the value changed event handler for the color picker control.
        colorPicker.valueChanged.addHandler(function () {
            if (ctx.applyFillColor) {
                ctx.flexSheet.applyCellsStyle({ backgroundColor: colorPicker.value });
            } else {
                ctx.flexSheet.applyCellsStyle({ color: colorPicker.value });
            }
        });
    }
}

function fontChanged(sender) {
    if (!ctx.updatingSelection && ctx.flexSheet) {
        ctx.flexSheet.applyCellsStyle({ fontFamily: ctx.cboFontName.selectedItem.Value });
    }
}

function fontSizeChanged(sender) {
    if (!ctx.updatingSelection && ctx.flexSheet) {
        ctx.flexSheet.applyCellsStyle({ fontSize: ctx.cboFontSize.selectedItem.Value });
    }
}

// apply style for the selected cells
function applyCellStyle(className, cancelCellStyle) {
    if (cancelCellStyle) {
        if (ctx.cellStyleApplying) {
            ctx.cellStyleApplying = false;
        } else {
            ctx.flexSheet.applyCellsStyle();
        }
    } else {
        if (className) {
            ctx.appliedClass = className + '-style';
            ctx.flexSheet.applyCellsStyle({ className: ctx.appliedClass }, undefined, true);
        } else if (ctx.appliedClass) {
            ctx.flexSheet.applyCellsStyle({ className: ctx.appliedClass });
            ctx.appliedClass = '';
            ctx.cellStyleApplying = true;
        }
    }
};

// apply the text alignment for the selected cells
function applyCellTextAlign(textAlign) {
    ctx.flexSheet.applyCellsStyle({ textAlign: textAlign });
    ctx.selectionFormatState.textAlign = textAlign;
    updateBtns();
};

// apply the bold font weight for the selected cells
function applyBoldStyle() {
    var isBold = ctx.selectionFormatState.isBold;
    ctx.flexSheet.applyCellsStyle({ fontWeight: isBold ? 'none' : 'bold' });
    ctx.selectionFormatState.isBold = !isBold;
    updateBtns();
};

// apply the underline text decoration for the selected cells
function applyUnderlineStyle() {
    var isUnderline = ctx.selectionFormatState.isUnderline;
    ctx.flexSheet.applyCellsStyle({ textDecoration: isUnderline ? 'none' : 'underline' });
    ctx.selectionFormatState.isUnderline = !isUnderline;
    updateBtns();
};

// apply the italic font style for the selected cells
function applyItalicStyle() {
    var isItalic = ctx.selectionFormatState.isItalic;
    ctx.flexSheet.applyCellsStyle({ fontStyle: isItalic ? 'none' : 'italic' });
    ctx.selectionFormatState.isItalic = !isItalic;
    updateBtns();
};

// export 
function exportExcel() {
    ctx.flexSheet.save('FlexSheet.xlsx');
};

// import
function importExcel(element) {
    var flexSheet = ctx.flexSheet,
        grids = [],
        reader;

    if (flexSheet && element.files[0]) {
        flexSheet.load(element.files[0]);
        element.value = '';
    }
};

// New flexSheet
function newFile() {
    var flexSheet = ctx.flexSheet;
    if (flexSheet) {
        flexSheet.clear();
    }
};

function mergeCells() {
    var flexSheet = ctx.flexSheet;

    if (flexSheet) {
        flexSheet.mergeRange();
        ctx.selectionFormatState = flexSheet.getSelectionFormatState();
        updateBtns();
    }
}

function freeze() {
    var flexSheet = ctx.flexSheet;
    if (flexSheet) {
        if (flexSheet.columns.length > 0 && flexSheet.rows.length > 0) {
            flexSheet.freezeAtCursor();
        }
        if (flexSheet.frozenColumns > 0 || flexSheet.frozenRows > 0) {           
            ctx.isFrozen = true;
        } else {
            ctx.isFrozen = false;
        }
        updateBtns();
    }
}

// Excutes undo command.
function undo() {
    ctx.flexSheet.undo();
    updateBtns();
};

// Excutes redo command.
function redo() {
    ctx.flexSheet.redo();
    updateBtns();
};

function hidePopup(e) {
    var colorPicker = ctx.colorPicker,
        modals = document.querySelectorAll('.modal'),
        i;

    if (e.keyCode === 27) {
        if (modals && modals.length > 0) {
            for (i = 0; i < modals.length; i++) {
                $(modals[i]).modal('hide');
            }
        }
    }
}

// Show the column filter for the flexSheet control.
function showFilter() {
    ctx.flexSheet.showColumnFilter();
}

// show the color picker control.
function showColorPicker(e, isFillColor) {
    var colorPicker = ctx.colorPicker,
        offset = cumulativeOffset(e.target);

    if (colorPicker) {
        colorPicker.hostElement.style.display = 'inline';
        colorPicker.hostElement.style.left = offset.left + 'px';
        colorPicker.hostElement.style.top = (offset.top + e.target.clientHeight + 2) + 'px';
        colorPicker.hostElement.focus();
    }

    ctx.applyFillColor = isFillColor;
};

function applyDataMap(flexSheet) {
    var countries = ['US', 'Germany', 'UK', 'Japan', 'Italy', 'Greece'],
        products = ['Widget', 'Gadget', 'Doohickey'], column;
    // initialize the dataMap for the bound sheet.
    if (flexSheet) {
        column = flexSheet.columns.getColumn('Country');
        if (column && !column.dataMap) {
            column.dataMap = buildDataMap(countries);
        }
        column = flexSheet.columns.getColumn('Product');
        if (column && !column.dataMap) {
            column.dataMap = buildDataMap(products);
        }
    }
}

function buildDataMap(items) {
    var map = [];
    for (var i = 0; i < items.length; i++) {
        map.push({ key: i, value: items[i] });
    }
    return new wijmo.grid.DataMap(map, 'key', 'value');
}

// Update the selection object of the scope.
function updateSelection(sel) {
    var flexSheet = ctx.flexSheet,
        row = flexSheet.rows[sel.row],
        rowCnt = flexSheet.rows.length,
        colCnt = flexSheet.columns.length,
        r,
        c,
        cellStyle,
        cellRange,
        rangeSum,
        rangeAvg,
        rangeCnt;

    ctx.updatingSelection = true;
    if (sel.row > -1 && sel.col > -1 && rowCnt > 0 && colCnt > 0
            && sel.col < colCnt && sel.col2 < colCnt
            && sel.row < rowCnt && sel.row2 < rowCnt) {
        r = sel.row >= rowCnt ? rowCnt - 1 : sel.row;
        c = sel.col >= colCnt ? colCnt - 1 : sel.col;
        ctx.selection.content = flexSheet.getCellData(r, c, true);
        ctx.selection.position = wijmo.grid.sheet.FlexSheet.convertNumberToAlpha(sel.col) + (sel.row + 1);
        cellStyle = flexSheet.selectedSheet.getCellStyle(sel.row, sel.col);
        if (cellStyle) {
            ctx.cboFontName.selectedIndex = checkFontfamily(cellStyle.fontFamily);
            ctx.cboFontSize.selectedIndex = checkFontSize(cellStyle.fontSize);

        } else {
            ctx.cboFontName.selectedIndex = 0;
            ctx.cboFontSize.selectedIndex = 5;
        }

        if (sel.col !== -1 && sel.col2 !== -1 && sel.row !== -1 && sel.row2 !== -1) {
            cellRange = wijmo.grid.sheet.FlexSheet.convertNumberToAlpha(sel.leftCol) + (sel.topRow + 1) + ':' + wijmo.grid.sheet.FlexSheet.convertNumberToAlpha(sel.rightCol) + (sel.bottomRow + 1);
            rangeSum = flexSheet.evaluate('sum(' + cellRange + ')');
            rangeAvg = flexSheet.evaluate('average(' + cellRange + ')');
            rangeCnt = flexSheet.evaluate('count(' + cellRange + ')');

            $('.status').text(cellRange + ' Average: ' + rangeAvg + ' Count: ' + rangeCnt + ' Sum: ' + rangeSum);
        } else {
            $('.status').text('');
        }
        getSelectedTable(sel);
        updateTableOptions();
    } else {
        ctx.selection.content = '';
        ctx.selection.position = '';
        $('.status').text('');
    }

    ctx.updatingSelection = false;
};

// Get selected table in FlexSheet.
function getSelectedTable(selection) {
    var flexSheet = ctx.flexSheet,
        rowIndex, colIndx, selectedTable, tableStyle;
    if (flexSheet && flexSheet.selectedSheet != null) {
        for (rowIndex = selection.topRow; rowIndex <= selection.bottomRow; rowIndex++) {
            for (colIndx = selection.leftCol; colIndx <= selection.rightCol; colIndx++) {
                selectedTable = flexSheet.selectedSheet.findTable(rowIndex, colIndx);
                if (selectedTable) {
                    ctx.selectedTable = selectedTable;
                    return;
                }
            }
        }
    }
    ctx.selectedTable = null;
}

// check font family for the font name combobox of the ribbon.
function checkFontfamily(fontFamily) {
    var fonts = ctx.cboFontName.itemsSource.items,
        fontIndex = 0,
        font;

    if (!fontFamily) {
        return fontIndex;
    }

    for (; fontIndex < fonts.length; fontIndex++) {
        font = fonts[fontIndex];

        if (font.Name === fontFamily || font.Value === fontFamily) {
            return fontIndex;
        }
    }

    return 0;
}

// check font size for the font size combobox of the ribbon.
function checkFontSize(fontSize) {
    var sizeList = ctx.cboFontSize.itemsSource.items,
        index = 0,
        size;

    if (fontSize == undefined) {
        return 5;
    }

    for (; index < sizeList.length; index++) {
        size = sizeList[index];

        if (size.Value === fontSize || size.Name === fontSize) {
            return index;
        }
    }

    return 5;
}

function showChartTypes() {
    var lbChartTypes = ctx.lbChartTypes,
        parent = ctx.chartTypesBtn.parentElement,
        offset = cumulativeOffset(parent);

    if (lbChartTypes) {
        lbChartTypes.selectedIndex = -1;
        lbChartTypes.hostElement.style.display = 'inline';
        lbChartTypes.hostElement.style.left = offset.left + 'px';
        lbChartTypes.hostElement.style.top = (offset.top + parent.clientHeight + 2) + 'px';
        lbChartTypes.hostElement.focus();
    }
}

// Get the absolute position of the dom element.
function cumulativeOffset(element) {
    var top = 0, left = 0;

    do {
        top += element.offsetTop || 0;
        left += element.offsetLeft || 0;
        element = element.offsetParent;
    } while (element);

    return {
        top: top,
        left: left
    };
}

function hasClass(obj, cls) {
    return obj && obj.className.match(new RegExp('(\\s|^)' + cls + '(\\s|$)'));
}

function addClass(obj, cls) {
    if (!this.hasClass(obj, cls)) obj.className += " " + cls;
}

function removeClass(obj, cls) {
    if (hasClass(obj, cls)) {
        var reg = new RegExp('(\\s|^)' + cls + '(\\s|$)');
        obj.className = obj.className.replace(reg, ' ');
    }
}