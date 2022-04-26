var ctx = {
    flexSheet: null,
    sortManager: null,
    moveup: null,
    movedown: null,
    tbody: null,
    columns: null
}

c1.documentReady(function () {
    ctx.flexSheet = wijmo.Control.getControl('#sortSheet');
    ctx.sortManager = ctx.flexSheet.sortManager;
    ctx.moveup = wijmo.getElement('#moveup');
    ctx.movedown = wijmo.getElement('#movedown');
    ctx.tbody = wijmo.getElement('#sortTable tbody');
    ctx.columns = getColumns();
 
    ctx.flexSheet.selectedSheetChanged.addHandler(function (sender, args) {
        ctx.columns = getColumns();
        ctx.sortManager = ctx.flexSheet.sortManager;
        updateSortTable();
    });

    ctx.flexSheet.columns.collectionChanged.addHandler(function (sender, args) {
        ctx.columns = getColumns();
        updateSortTable();
    });

    ctx.flexSheet.loaded.addHandler(function (sender, args) {
        ctx.columns = getColumns();
        updateSortTable();
    });

    ctx.sortManager.sortDescriptions.collectionChanged.addHandler(function (sender, args) {
        updateSortTable();       
    });

    ctx.sortManager.sortDescriptions.currentChanged.addHandler(function (sender, args) {
        updateSortTable();
    });
    
    updateSortTable();
    applyDataMap(ctx.flexSheet);
});

function changeBtnState() {
    moveup.disabled = ctx.sortManager.sortDescriptions.currentPosition <= 0;
    movedown.disabled = ctx.sortManager.sortDescriptions.currentPosition >= ctx.sortManager.sortDescriptions.itemCount - 1;
}

function updateSortTable() {
    var i, j, html='', tr, sortDescriptions = ctx.sortManager.sortDescriptions,
        items = sortDescriptions.items;
    for (i = 0; i < items.length; i++) {
        tr = '<tr onclick="moveCurrentTo(' + i + ')" ' +
                (sortDescriptions.currentItem == items[i] ? 'class="success"' : '') + '>' +
                '<td>' +
                '<select class="form-control" onchange="columnIndexChanged(this, ' + i + ')">' +
                '<option value=-1></option>';

        for (j = 0; j < ctx.columns.length; j++) {
            tr += '<option value="' + j + '" ' + (j == items[i].columnIndex ? 'selected="selected"' : '') +
                '>' + ctx.columns[j] + '</option>';
        }

        tr += '</select></td>' +
            '<td>' +
            '<select class="form-control" onchange="ascendingChanged(this, ' + i + ')">' +
            '<option value="0" ' + (items[i].ascending ? 'selected="selected"' : '') + '>Ascending</option>' +
            '<option value="1" ' + (!items[i].ascending ? 'selected="selected"' : '') + '>Descending</option>' +
            '</select></td></tr>';
        html += tr;
    }
    ctx.tbody.innerHTML = html;
    changeBtnState();
}

function moveCurrentTo(index) {
    var items = ctx.sortManager.sortDescriptions.items, i=0;
    ctx.sortManager.sortDescriptions.moveCurrentTo(items[index]);
    for (; i < ctx.tbody.children.length; i++) {
        ctx.tbody.children[i].className = index == i ? 'success' : '';
    }
    changeBtnState();
}

function columnIndexChanged(ele, index) {
    ctx.sortManager.sortDescriptions.items[index].columnIndex = +ele.value;
}

function ascendingChanged(ele, index) {
    ctx.sortManager.sortDescriptions.items[index].ascending = ele.value=="0";
}

// commit the sorts
function commitSort() {
    ctx.sortManager.commitSort();
};

// cancel the sorts
function cancelSort() {
    ctx.sortManager.cancelSort();
    updateSortTable();
};

// add new sort level
function addSortLevel() {
    ctx.sortManager.addSortLevel();
    updateSortTable();
};

// delete current sort level
function deleteSortLevel() {
    ctx.sortManager.deleteSortLevel();
    updateSortTable();
};

// copy a new sort level by current sort level setting.
function copySortLevel() {
    ctx.sortManager.copySortLevel();
    updateSortTable();
};

// move the sort level
function moveSortLevel(offset) {
    ctx.sortManager.moveSortLevel(offset);
    updateSortTable();
};
// get the columns with the column header text for the column selection for sort setting.
function getColumns() {
    var columns = [],
        flex = ctx.flexSheet,
        i = 0;

    if (flex) {
        for (; i < flex.columns.length; i++) {
            columns.push('Column ' + wijmo.grid.sheet.FlexSheet.convertNumberToAlpha(i));
        }
    }

    return columns;
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