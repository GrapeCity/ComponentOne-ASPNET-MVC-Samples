var ctx = {
    flexSheet: null,
    sheets: [],
    sheetsSelect: null
}

c1.documentReady(function () {
    var column, s;
    ctx.flexSheet = wijmo.Control.getControl('#standaloneSheet');
    ctx.sheetsSelect = wijmo.getElement('#sheetsSelect');
    s = ctx.flexSheet;
    for (var i = 0; i < s.sheets.length; i++) {
        s.sheets.selectedIndex = i;
        if (s.sheets[i].name === 'Country') {
            applyDataMap(s);
        }
        ctx.sheets.push(s.sheets[i].name);
    }
    s.selectedSheetIndex = 0;
    s.refresh();
    updateSheetsSelect();
});

function updateSheetsSelect() {
    var select = ctx.sheetsSelect, option, i = 0;

    option = select.querySelector('option');

    while (select && option) {
        select.remove(option);
        option = select.querySelector('option')
    }

    for (; i < ctx.sheets.length; i++) {
        option = document.createElement('option');
        option.value = i;
        option.innerHTML = ctx.sheets[i];
        option.selected = ctx.flexSheet.selectedIndex == i;
        select.appendChild(option);
    }

}

function loaded() {
    var flexSheet = ctx.flexSheet;

    ctx.sheets = [];

    for (var i = 0; i < flexSheet.sheets.length; i++) {
        ctx.sheets.push(flexSheet.sheets[i].name);
    }

    updateSheetsSelect();
}

function changeSelectedSheet() {
    ctx.flexSheet.selectedSheetIndex = ctx.sheetsSelect.value;
}

// export 
function save() {
    var flexSheet = ctx.flexSheet;

    if (flexSheet) {
        flexSheet.save('StandaloneFlexSheet.xlsx');
    }
};

// import
function load() {
    var flexSheet = ctx.flexSheet;

    if (flexSheet && $('#importFile')[0].files[0]) {
        flexSheet.load($('#importFile')[0].files[0]);
    }
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