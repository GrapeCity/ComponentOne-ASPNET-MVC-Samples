ctx = jQuery.extend(ctx, {
    isEditing: false,
    flexSheet: null,
    hub: null,
    userId: '',
    stateBar: null,
    shelveDatas: [],
    shelveCellStyles: [],
    oldCellValue: null,
    sheetCount: 0,
    shouldSendServer: true,
});

$(function () {
    var userIdInput = $('#userId');
    ctx.flexSheet = wijmo.Control.getControl('#signalRSheet');
    ctx.sheetCount = ctx.flexSheet.sheets.length;
    ctx.stateBar = $('#stateBar');
    //Init userId
    ctx.userId = getDefaultUserId();
    userIdInput.val(ctx.userId);
    userIdInput.change(function () {
        ctx.userId = userIdInput.val();
    });
    initFlexSheet();
    initInputs();

    // Declare a proxy to reference the hub
    ctx.hub = $.connection.flexSheetHub;
    registerClientMethods(ctx.hub);
    // Start Hub
    $.connection.hub.start().done(function () {
        ctx.stateBar.html('Connected!');
    });
});

function beginningEdit(sender, args) {
    ctx.oldCellValue = sender.getCellData(args.row, args.col);
    ctx.isEditing = true;
}

//If user ended editing, should handle the receiving data and send the changes.
function cellEditEnded(sender, args) {
    var cellData = sender.getCellData(args.row, args.col),
        isDate = cellData instanceof (Date);
    ctx.isEditing = false;
    handleShelveDatas();
    if (ctx.oldCellValue != cellData) {
        send({
            type: 'cellValue',
            sheet: sender.selectedSheetIndex,
            row: args.row,
            col: args.col,
            value: cellData,
            isDate: isDate
        });
    }
}

function sheetChanged(sender, args) {
    handleShelveCellStyles();
    if (!ctx.shouldSendServer) {
        ctx.shouldSendServer = true;
        return;
    }
    var flexSheet = ctx.flexSheet, sheetCount = ctx.sheetCount;
    if (flexSheet.selectedSheetIndex === flexSheet.sheets.length - 1 &&
        flexSheet.sheets.length - sheetCount === 1) {
        send({
            type: 'addSheet',
            value: flexSheet.sheets[flexSheet.selectedSheetIndex].name
        });
        ctx.sheetCount = flexSheet.sheets.length;
    }
}

//Send the cell format/style data.
function sendCellsStyle(formatType, value) {
    var selection = ctx.flexSheet.selection;
    send({
        type: 'cellsStyle',
        sheet: ctx.flexSheet.selectedSheetIndex,
        row: selection.row,
        col: selection.col,
        row2: selection.row2,
        col2: selection.col2,
        formatType: formatType,
        value: value
    });
}

//Begin send data through signalR.
function send(data) {
    ctx.stateBar.val('Updating...');
    ctx.hub.server.update(ctx.userId, data);
}

//Register client method for signalR.
function registerClientMethods(hub) {
    hub.client.onUpdated = function (result, data) {
        var msg = result ? (data.type !== 'addSheet' ?
            'Updated cell ' + getCellPositon(data.row, data.col) + '.' : 'Added sheet ' + data.value + '.')
            : 'Update failed!';
        ctx.stateBar.html(msg);

        //Ensure your change won't be covered by others.
        if (result && data.type === 'cellValue') {
            updateSheet(data);
        }
    };

    hub.client.update = function (id, userId, data) {
        var sheetName;
        if (data.type !== 'addSheet') {
            sheetName = ctx.flexSheet.sheets[data.sheet].name;
            if (ctx.isEditing) {
                ctx.shelveDatas.push({ id: id, userId: userId, data: data });
            } else {
                updateSheet(data);
                ctx.stateBar.html(userId + ' has updated cell '
                    + getCellPositon(data.row, data.col) + ' of ' + sheetName + '.');
            }
        } else {
            ctx.shouldSendServer = false;
            ctx.flexSheet.sheets.insert(ctx.flexSheet.sheets.length,
                new wijmo.grid.sheet.Sheet(data.value));
            ctx.sheetCount = ctx.flexSheet.sheets.length;
            ctx.stateBar.html(userId + ' has added the new sheet "' + data.value + '".');
        }
    };
}

//Handle the shelve cell datas, enable effects.
function handleShelveDatas() {
    var i = 0, shelveDatas = ctx.shelveDatas, msg = '', data, sheetName;

    for (; i < shelveDatas.length; i++) {
        data = shelveDatas[i].data;
        sheetName = ctx.flexSheet.sheets[data.sheet].name;
        updateSheet(data);
        msg += shelveDatas[i].userId + ' has updated cell '
            + getCellPositon(data.row, data.col) + ' of ' + sheetName + '. ';
    }

    ctx.shelveDatas = [];

    if (msg) {
        ctx.stateBar.html(msg);
    }
}

//Handle the cell formats for entire sheet.
function handleShelveCellStyles() {
    var flexSheet = ctx.flexSheet,
        sheetIndex = flexSheet.selectedSheetIndex,
        datas = ctx.shelveCellStyles[sheetIndex],
        i = 0;

    if (datas) {
        for (; i < datas.length; i++) {
            applyCellStyles(datas[i]);
        }

        ctx.shelveCellStyles[sheetIndex] = null;
    }
}

//Set the cell range styles.
function applyCellStyles(data) {
    var formatData = {}, flexSheet = ctx.flexSheet,
        sel = flexSheet.selection,
        cellRange = new wijmo.grid.CellRange(data.row, data.col, data.row2, data.col2);

    if (data.formatType === 'format') {
        data.value = wijmo.xlsx.Workbook.fromXlsxFormat(data.value)[0];
    }

    formatData[data.formatType] = data.value;
    flexSheet.applyCellsStyle(formatData,
           [cellRange]);
    flexSheet.refresh();

    adjustCellRange(sel);
    adjustCellRange(cellRange);

    //Whether updated cellRange contains the selection.
    if (sel && sel.row >= cellRange.row && sel.col >= cellRange.col &&
        sel.row2 <= cellRange.row2 && sel.col2 <= cellRange.col2) {
        updateSelection(sel);
    }
}

function adjustCellRange(cellRange) {
    if (cellRange.row > cellRange.row2) {
        //Exchange values.
        cellRange.row = cellRange.row ^ cellRange.row2;
        cellRange.row2 = cellRange.row ^ cellRange.row2;
        cellRange.row = cellRange.row ^ cellRange.row2;
    }

    if (cellRange.col > cellRange.col2) {
        cellRange.col = cellRange.col ^ cellRange.col2;
        cellRange.col2 = cellRange.col ^ cellRange.col2;
        cellRange.col = cellRange.col ^ cellRange.col2;
    }
}

//Begin to update FlexSheet values or styles according to the receiving data.
function updateSheet(data) {
    var flexSheet = ctx.flexSheet,
        sheetIndex = flexSheet.selectedSheetIndex,
        isCurrentSheet = data.sheet === sheetIndex,
        grid, sheetCellStyles;
    switch (data.type) {
        case 'cellValue':
            grid = isCurrentSheet ? flexSheet : flexSheet.sheets[data.sheet].grid;
            grid.setCellData(data.row, data.col,
                data.isDate ? new Date(data.value) : data.value);
            break;
        case 'cellsStyle':
            if (isCurrentSheet) {
                applyCellStyles(data);
            } else {
                sheetCellStyles = ctx.shelveCellStyles;
                if (sheetCellStyles[data.sheet]) {
                    sheetCellStyles[data.sheet].push(data);
                } else {
                    sheetCellStyles[data.sheet] = [data];
                }
            }
    }
}

//Distribute a defualt user ID.
function getDefaultUserId() {
    return 'User-' + Math.random().toString(36).substr(2, 6);
}

//Get the cell position string like 'B2'.
function getCellPositon(row, col) {
    return String.fromCharCode(65 + col) + (row + 1);
}