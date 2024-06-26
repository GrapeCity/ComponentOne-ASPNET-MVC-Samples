// Format Cells

var applyFillColor = false,
    updatingSelection = false,
    formats = ['0', 'n2', 'p2', 'c2', '', 'd', 'D', 'f', 'F'],
    ctxFormatCells = {
        format: '',
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
        sheetName: '',
        selectionFormatState: {},
    };

function loadFormatCells() {
    initFlexSheet();
    initInputs();
};

function initInputs() {
    ctxFormatCells.cboFontName = wijmo.Control.getControl('#cboFontName');
    ctxFormatCells.cboFontSize = wijmo.Control.getControl('#cboFontSize');
    ctxFormatCells.menuFormat = wijmo.Control.getControl('#fcMenuFormat');
    initBtns();
    initColorPicker();
    setMenuHeader(ctxFormatCells.menuFormat);
}

function initBtns() {
    ctxFormatCells.boldBtn = wijmo.getElement('#boldBtn');
    ctxFormatCells.italicBtn = wijmo.getElement('#italicBtn');
    ctxFormatCells.underlineBtn = wijmo.getElement('#underlineBtn');
    ctxFormatCells.leftBtn = wijmo.getElement('#leftBtn');
    ctxFormatCells.centerBtn = wijmo.getElement('#centerBtn');
    ctxFormatCells.rightBtn = wijmo.getElement('#rightBtn');
}

function formatCellsUpdateBtns() {
    updateActiveState(ctxFormatCells.selectionFormatState.isBold, ctxFormatCells.boldBtn);
    updateActiveState(ctxFormatCells.selectionFormatState.isItalic, ctxFormatCells.italicBtn);
    updateActiveState(ctxFormatCells.selectionFormatState.isUnderline, ctxFormatCells.underlineBtn);
    updateActiveState(ctxFormatCells.selectionFormatState.textAlign === 'left', ctxFormatCells.leftBtn);
    updateActiveState(ctxFormatCells.selectionFormatState.textAlign === 'center', ctxFormatCells.centerBtn);
    updateActiveState(ctxFormatCells.selectionFormatState.textAlign === 'right', ctxFormatCells.rightBtn);
}

function updateActiveState(condition, btn) {
    condition ? addClass(btn, "active") : removeClass(btn, "active");
}

function initFlexSheet() {
    var sheetIdx,
        sheetName,
        colIdx,
        rowIdx,
        date,
        flexSheet;

    ctxFormatCells.flexSheet = wijmo.Control.getControl('#fcFlexSheet');
    flexSheet = ctxFormatCells.flexSheet;
    if (flexSheet) {
        flexSheet.selectionChanged.addHandler(function (sender, args) {
            updateSelection(args.range);
            ctxFormatCells.selectionFormatState = flexSheet.getSelectionFormatState();
        });

        for (sheetIdx = 0; sheetIdx < flexSheet.sheets.length; sheetIdx++) {
            flexSheet.selectedSheetIndex = sheetIdx;
            sheetName = flexSheet.selectedSheet.name;
            for (colIdx = 0; colIdx < flexSheet.columns.length; colIdx++) {
                for (rowIdx = 0; rowIdx < flexSheet.rows.length; rowIdx++) {
                    if (sheetName === 'Number') {
                        flexSheet.setCellData(rowIdx, colIdx, colIdx + rowIdx);
                    } else {
                        date = new Date(2015, colIdx, rowIdx + 1);
                        flexSheet.setCellData(rowIdx, colIdx, date);
                    }
                }
            }
        }
        flexSheet.selectedSheetIndex = 0;
        updateSelection(flexSheet.selection);
    }
};

// initialize the colorPicker control.
function initColorPicker() {
    var colorPicker = ctxFormatCells.colorPicker = wijmo.Control.getControl('#fcColorPicker'),
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
                    applyFillColor = false;
                    colorPicker.hostElement.style.display = 'none';
                }
            }, 0);
        });

        // Initialize the value changed event handler for the color picker control.
        colorPicker.valueChanged.addHandler(function () {
            if (applyFillColor) {
                ctxFormatCells.flexSheet.applyCellsStyle({ backgroundColor: colorPicker.value });
            } else {
                ctxFormatCells.flexSheet.applyCellsStyle({ color: colorPicker.value });
            }
        });
    }
}

function fcMenuFormat_Changed(sender) {
    var flexSheet = ctxFormatCells.flexSheet,
        menu = sender;
    if (menu.selectedValue) {
        ctxFormatCells.format = menu.selectedValue.CommandParameter;
        setMenuHeader(menu);
        if (flexSheet && !updatingSelection) {
            flexSheet.applyCellsStyle({ format: ctxFormatCells.format });
        }
    }
}

function setMenuHeader(menu) {
    menu.header = "Format:<b>" + menu.selectedValue === null ? "" : menu.selectedValue.Header + "</b>";
}

function fontChanged(sender) {
    if (!updatingSelection && ctxFormatCells.flexSheet) {
        ctxFormatCells.flexSheet.applyCellsStyle({ fontFamily: ctxFormatCells.cboFontName.selectedItem.Value });
    }
}

function fontSizeChanged(sender) {
    if (!updatingSelection && ctxFormatCells.flexSheet) {
        ctxFormatCells.flexSheet.applyCellsStyle({ fontSize: ctxFormatCells.cboFontSize.selectedItem.Value });
    }
}

// apply the text alignment for the selected cells
function applyCellTextAlign(textAlign) {
    ctxFormatCells.flexSheet.applyCellsStyle({ textAlign: textAlign });
    ctxFormatCells.selectionFormatState.textAlign = textAlign;
    formatCellsUpdateBtns();
};

// apply the bold font weight for the selected cells
function applyBoldStyle() {
    ctxFormatCells.flexSheet.applyCellsStyle({ fontWeight: ctxFormatCells.selectionFormatState.isBold ? 'none' : 'bold' });
    ctxFormatCells.selectionFormatState.isBold = !ctxFormatCells.selectionFormatState.isBold;
    formatCellsUpdateBtns();
};

// apply the underline text decoration for the selected cells
function applyUnderlineStyle() {
    ctxFormatCells.flexSheet.applyCellsStyle({ textDecoration: ctxFormatCells.selectionFormatState.isUnderline ? 'none' : 'underline' });
    ctxFormatCells.selectionFormatState.isUnderline = !ctxFormatCells.selectionFormatState.isUnderline;
    formatCellsUpdateBtns();
};

// apply the italic font style for the selected cells
function applyItalicStyle() {
    ctxFormatCells.flexSheet.applyCellsStyle({ fontStyle: ctxFormatCells.selectionFormatState.isItalic ? 'none' : 'italic' });
    ctxFormatCells.selectionFormatState.isItalic = !ctxFormatCells.selectionFormatState.isItalic;
    formatCellsUpdateBtns();
};

// show the color picker control.
function showColorPicker(e, isFillColor) {
    var colorPicker = ctxFormatCells.colorPicker,
        offset = cumulativeOffset(e.target);

    if (colorPicker) {
        colorPicker.hostElement.style.display = 'inline';
        colorPicker.hostElement.style.left = offset.left + 'px';
        colorPicker.hostElement.style.top = (offset.top - colorPicker.hostElement.clientHeight - 5) + 'px';
        colorPicker.hostElement.focus();
    }

    applyFillColor = isFillColor;
};

// Update the selection object of the scope.
function updateSelection(sel) {
    var flexSheet = ctxFormatCells.flexSheet,
        row = flexSheet.rows[sel.row],
        rowCnt = flexSheet.rows.length,
        colCnt = flexSheet.columns.length,
        r,
        c,
        cellStyle,
        cellContent,
        cellFormat;

    updatingSelection = true;
    if (ctxFormatCells.cboFontName && sel.row > -1 && sel.col > -1 && rowCnt > 0 && colCnt > 0
            && sel.col < colCnt && sel.col2 < colCnt
            && sel.row < rowCnt && sel.row2 < rowCnt) {
        r = sel.row >= rowCnt ? rowCnt - 1 : sel.row;
        c = sel.col >= colCnt ? colCnt - 1 : sel.col;
        cellContent = flexSheet.getCellData(sel.row, sel.col);
        cellStyle = flexSheet.selectedSheet.getCellStyle(sel.row, sel.col);
        if (cellStyle) {
            ctxFormatCells.cboFontName.selectedIndex = checkFontfamily(cellStyle.fontFamily);
            ctxFormatCells.cboFontSize.selectedIndex = checkFontSize(cellStyle.fontSize);
            cellFormat = cellStyle.format;
        } else {
            ctxFormatCells.cboFontName.selectedIndex = 0;
            ctxFormatCells.cboFontSize.selectedIndex = 5;
        }

        if (!!cellFormat) {
            ctxFormatCells.format = cellFormat;
        } else {
            if (wijmo.isInt(cellContent)) {
                ctxFormatCells.format = '0';
            } else if (wijmo.isNumber(cellContent)) {
                ctxFormatCells.format = 'n2';
            } else if (wijmo.isDate(cellContent)) {
                ctxFormatCells.format = 'd';
            }
        }
        ctxFormatCells.selectionFormatState = flexSheet.getSelectionFormatState()
        ctxFormatCells.menuFormat.selectedIndex = formats.indexOf(ctxFormatCells.format);
        formatCellsUpdateBtns();
    }
    updatingSelection = false;
};

// check font family for the font name combobox of the ribbon.
function checkFontfamily(fontFamily) {
    var fonts = ctxFormatCells.cboFontName.itemsSource.items,
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
    var sizeList = ctxFormatCells.cboFontSize.itemsSource.items,
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

// Get the absolute position of the dom element.
function cumulativeOffset(element) {
    var top = 0, left = 0, scrollTop = 0, scrollLeft = 0;

    do {
        top += element.offsetTop || 0;
        left += element.offsetLeft || 0;
        scrollTop += element.scrollTop || 0;
        scrollLeft += element.scrollLeft || 0;
        element = element.offsetParent;
    } while (element && !(element instanceof HTMLBodyElement));

    scrollTop += document.body.scrollTop || document.documentElement.scrollTop;
    scrollLeft += document.body.scrollLeft || document.documentElement.scrollLeft;

    return {
        top: top - scrollTop,
        left: left - scrollLeft
    };
};

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

