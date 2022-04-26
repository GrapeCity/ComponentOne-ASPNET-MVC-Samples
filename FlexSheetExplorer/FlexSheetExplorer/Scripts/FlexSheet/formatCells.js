var applyFillColor = false,
    updatingSelection = false,
    formats = ['0', 'n2', 'p2', 'c2', '', 'd', 'D', 'f', 'F'],
    ctx = {
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

c1.documentReady(function () {
    initFlexSheet();
    initInputs();
});

function initInputs() {
    ctx.cboFontName = wijmo.Control.getControl('#cboFontName');
    ctx.cboFontSize = wijmo.Control.getControl('#cboFontSize');
    ctx.menuFormat = wijmo.Control.getControl('#menuFormat');
    initBtns();
    initColorPicker();
    setMenuHeader(ctx.menuFormat);
}

function initBtns() {
    ctx.boldBtn = wijmo.getElement('#boldBtn');
    ctx.italicBtn = wijmo.getElement('#italicBtn');
    ctx.underlineBtn = wijmo.getElement('#underlineBtn');
    ctx.leftBtn = wijmo.getElement('#leftBtn');
    ctx.centerBtn = wijmo.getElement('#centerBtn');
    ctx.rightBtn = wijmo.getElement('#rightBtn');
}

function updateBtns() {
    updateActiveState(ctx.selectionFormatState.isBold, ctx.boldBtn);
    updateActiveState(ctx.selectionFormatState.isItalic, ctx.italicBtn);
    updateActiveState(ctx.selectionFormatState.isUnderline, ctx.underlineBtn);
    updateActiveState(ctx.selectionFormatState.textAlign === 'left', ctx.leftBtn);
    updateActiveState(ctx.selectionFormatState.textAlign === 'center', ctx.centerBtn);
    updateActiveState(ctx.selectionFormatState.textAlign === 'right', ctx.rightBtn);
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

    ctx.flexSheet = wijmo.Control.getControl('#formatSheet');
    flexSheet = ctx.flexSheet;
    if (flexSheet) {
        flexSheet.selectionChanged.addHandler(function (sender, args) {
            updateSelection(args.range);
            ctx.selectionFormatState = flexSheet.getSelectionFormatState();
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
                    applyFillColor = false;
                    colorPicker.hostElement.style.display = 'none';
                }
            }, 0);
        });

        // Initialize the value changed event handler for the color picker control.
        colorPicker.valueChanged.addHandler(function () {
            if (applyFillColor) {
                ctx.flexSheet.applyCellsStyle({ backgroundColor: colorPicker.value });
            } else {
                ctx.flexSheet.applyCellsStyle({ color: colorPicker.value });
            }
        });
    }
}

function formatChanged(sender) {
    var flexSheet = ctx.flexSheet,
        menu = sender;
    if (menu.selectedValue) {
        ctx.format = menu.selectedValue.CommandParameter;
        setMenuHeader(menu);
        if (flexSheet && !updatingSelection) {
            flexSheet.applyCellsStyle({ format: ctx.format });
        }
    }
}

function setMenuHeader(menu) {
    menu.header = "Format:<b>" + menu.selectedValue.Header + "</b>";
}

function fontChanged(sender) {
    if (!updatingSelection && ctx.flexSheet) {
        ctx.flexSheet.applyCellsStyle({ fontFamily: ctx.cboFontName.selectedItem.Value });
    }
}

function fontSizeChanged(sender) {
    if (!updatingSelection && ctx.flexSheet) {
        ctx.flexSheet.applyCellsStyle({ fontSize: ctx.cboFontSize.selectedItem.Value });
    }
}

// apply the text alignment for the selected cells
function applyCellTextAlign(textAlign) {
    ctx.flexSheet.applyCellsStyle({ textAlign: textAlign });
    ctx.selectionFormatState.textAlign = textAlign;
    updateBtns();
};

// apply the bold font weight for the selected cells
function applyBoldStyle() {
    ctx.flexSheet.applyCellsStyle({ fontWeight: ctx.selectionFormatState.isBold ? 'none' : 'bold' });
    ctx.selectionFormatState.isBold = !ctx.selectionFormatState.isBold;
    updateBtns();
};

// apply the underline text decoration for the selected cells
function applyUnderlineStyle() {
    ctx.flexSheet.applyCellsStyle({ textDecoration: ctx.selectionFormatState.isUnderline ? 'none' : 'underline' });
    ctx.selectionFormatState.isUnderline = !ctx.selectionFormatState.isUnderline;
    updateBtns();
};

// apply the italic font style for the selected cells
function applyItalicStyle() {
    ctx.flexSheet.applyCellsStyle({ fontStyle: ctx.selectionFormatState.isItalic ? 'none' : 'italic' });
    ctx.selectionFormatState.isItalic = !ctx.selectionFormatState.isItalic;
    updateBtns();
};

// show the color picker control.
function showColorPicker(e, isFillColor) {
    var colorPicker = ctx.colorPicker,
        offset = cumulativeOffset(e.target),
        winWidth = document.body.clientWidth;

    if (colorPicker) {
        colorPicker.hostElement.style.display = 'inline';
        if (offset.left + colorPicker.hostElement.clientWidth > winWidth) {
            colorPicker.hostElement.style.left = 'auto';
            colorPicker.hostElement.style.right = '0px';
        } else {
            colorPicker.hostElement.style.right = 'auto';
            colorPicker.hostElement.style.left = offset.left + 'px';
        }
        colorPicker.hostElement.style.top = (offset.top - colorPicker.hostElement.clientHeight - 5) + 'px';
        colorPicker.hostElement.focus();
    }

    applyFillColor = isFillColor;
};

// Update the selection object of the scope.
function updateSelection(sel) {
    var flexSheet = ctx.flexSheet,
        row = flexSheet.rows[sel.row],
        rowCnt = flexSheet.rows.length,
        colCnt = flexSheet.columns.length,
        r,
        c,
        cellStyle,
        cellContent,
        cellFormat;

    updatingSelection = true;
    if (ctx.cboFontName && sel.row > -1 && sel.col > -1 && rowCnt > 0 && colCnt > 0
            && sel.col < colCnt && sel.col2 < colCnt
            && sel.row < rowCnt && sel.row2 < rowCnt) {
        r = sel.row >= rowCnt ? rowCnt - 1 : sel.row;
        c = sel.col >= colCnt ? colCnt - 1 : sel.col;
        cellContent = flexSheet.getCellData(sel.row, sel.col);
        cellStyle = flexSheet.selectedSheet.getCellStyle(sel.row, sel.col);
        if (cellStyle) {
            ctx.cboFontName.selectedIndex = checkFontfamily(cellStyle.fontFamily);
            ctx.cboFontSize.selectedIndex = checkFontSize(cellStyle.fontSize);
            cellFormat = cellStyle.format;
        } else {
            ctx.cboFontName.selectedIndex = 0;
            ctx.cboFontSize.selectedIndex = 5;
        }

        if (!!cellFormat) {
            ctx.format = cellFormat;
        } else {
            if (wijmo.isInt(cellContent)) {
                ctx.format = '0';
            } else if (wijmo.isNumber(cellContent)) {
                ctx.format = 'n2';
            } else if (wijmo.isDate(cellContent)) {
                ctx.format = 'd';
            }
        }
        ctx.selectionFormatState = flexSheet.getSelectionFormatState()
        ctx.menuFormat.selectedIndex = formats.indexOf(ctx.format);
        updateBtns();
    }
    updatingSelection = false;
};

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

// Get the absolute position of the dom element.
function cumulativeOffset(element) {
    var top = 0, left = 0, scrollTop = 0, scrollLeft = 0;

    do {
        top += element.offsetTop || 0;
        left += element.offsetLeft || 0;
        scrollTop += element.scrollTop || 0;
        scrollLeft += element.scrollLeft || 0;
        element = element.offsetParent;
    } while (element);

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