c1.documentReady(function () {
    var flexSheet = wijmo.Control.getControl('#boundSheet');
    if (flexSheet) {
        applyDataMap(flexSheet);
    }
});

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