var ctx = {
    flexSheet: null,
    filterBtn: null
};

c1.documentReady(function () {
    ctx.flexSheet = wijmo.Control.getControl('#filterSheet');
    ctx.filterBtn = wijmo.getElement('#filterBtn');
    if (ctx.flexSheet) {
        applyDataMap(ctx.flexSheet);
    }
});

function sheetChanged(sender, args) {
    ctx.filterBtn.disabled = !ctx.flexSheet.itemsSource;
}

function showFilter() {
    var flexSheet = ctx.flexSheet;

    if (flexSheet) {
        flexSheet.showColumnFilter();
    }
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