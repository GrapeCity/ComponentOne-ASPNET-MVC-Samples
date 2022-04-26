// Excel I/O
var ctxcExcelIO = {
    fileName: '',
    flexSheet: null,
    fileNameInput: null,
    fileInput: null
};

function loadExcelIO() {
    var flexSheet;
    ctxcExcelIO.flexSheet = wijmo.Control.getControl('#excelIOSheet');
    ctxcExcelIO.fileNameInput = wijmo.getElement('#fileName');
    ctxcExcelIO.fileInput = wijmo.getElement('#importFile');
    flexSheet = ctxcExcelIO.flexSheet;
    if (flexSheet) {
        for (sheetIdx = 0; sheetIdx < flexSheet.sheets.length; sheetIdx++) {
            flexSheet.selectedSheetIndex = sheetIdx;
            sheetName = flexSheet.selectedSheet.name;
            if (sheetName === 'Unbound') {
                for (colIdx = 0; colIdx < flexSheet.columns.length; colIdx++) {
                    for (rowIdx = 0; rowIdx < flexSheet.rows.length; rowIdx++) {
                        flexSheet.setCellData(rowIdx, colIdx, colIdx + rowIdx);
                    }
                }
            } else {
                applyDataMap(flexSheet);
            }
        }
        flexSheet.selectedSheetIndex = 0;
    }
};

excelIOLoad = function () {
    var flexSheet = ctxcExcelIO.flexSheet,
        fileInput = ctxcExcelIO.fileInput;
    if (flexSheet && fileInput.files[0]) {
        flexSheet.load(fileInput.files[0]);
    }
}

excelIOSave = function () {
    var flexSheet = ctxcExcelIO.flexSheet,
        fileName;
    if (flexSheet) {
        if (!!ctxcExcelIO.fileName) {
            fileName = ctxcExcelIO.fileName;
        } else {
            fileName = 'FlexSheet.xlsx';
        }
        flexSheet.save(fileName);
    }
}

function fileNameChanged() {
    ctxcExcelIO.fileName = ctxcExcelIO.fileNameInput.value;
}

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