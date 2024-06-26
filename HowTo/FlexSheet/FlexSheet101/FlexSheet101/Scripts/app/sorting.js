// Sorting
var ctxSorting = {
    flexSheet: null,
    sortManager: null,
    moveup: null,
    movedown: null,
    tbody: null,
    columns: null
};

function loadSorting() {
    ctxSorting.flexSheet = wijmo.Control.getControl('#sortingFlexSheet');
    ctxSorting.sortManager = ctxSorting.flexSheet.sortManager;
    ctxSorting.moveup = wijmo.getElement('#moveup');
    ctxSorting.movedown = wijmo.getElement('#movedown');
    ctxSorting.tbody = wijmo.getElement('#sortTable tbody');
    ctxSorting.columns = getColumns();

    ctxSorting.flexSheet.selectedSheetChanged.addHandler(function (sender, args) {
        ctxSorting.columns = getColumns();
        ctxSorting.sortManager = ctxSorting.flexSheet.sortManager;
        updateSortTable();
    });

    ctxSorting.flexSheet.columns.collectionChanged.addHandler(function (sender, args) {
        ctxSorting.columns = getColumns();
        updateSortTable();
    });

    ctxSorting.flexSheet.loaded.addHandler(function (sender, args) {
        ctxSorting.columns = getColumns();
        updateSortTable();
    });

    ctxSorting.sortManager.sortDescriptions.collectionChanged.addHandler(function (sender, args){
        updateSortTable();
    });

    ctxSorting.sortManager.sortDescriptions.currentChanged.addHandler(function (sender, args){
        updateSortTable();
    });


    updateSortTable();
    applyDataMap(ctxSorting.flexSheet);
};

function changeBtnState() {
    moveup.disabled = ctxSorting.sortManager.sortDescriptions.currentPosition <= 0;
    movedown.disabled = ctxSorting.sortManager.sortDescriptions.currentPosition >= ctxSorting.sortManager.sortDescriptions.itemCount - 1;
}

function updateSortTable() {
    var i, j, html = '', tr, sortDescriptions = ctxSorting.sortManager.sortDescriptions,
        items = sortDescriptions.items;
    for (i = 0; i < items.length; i++) {
        tr = '<tr onclick="moveCurrentTo(' + i + ')" ' +
                (sortDescriptions.currentItem == items[i] ? 'class="success"' : '') + '>' +
                '<td>' +
                '<select class="form-control" onchange="columnIndexChanged(this, ' + i + ')">' +
                '<option value=-1></option>';

        for (j = 0; j < ctxSorting.columns.length; j++) {
            tr += '<option value="' + j + '" ' + (j == items[i].columnIndex ? 'selected="selected"' : '') +
                '>' + ctxSorting.columns[j] + '</option>';
        }

        tr += '</select></td>' +
            '<td>' +
            '<select class="form-control" onchange="ascendingChanged(this, ' + i + ')">' +
            '<option value="0" ' + (items[i].ascending ? 'selected="selected"' : '') + '>Ascending</option>' +
            '<option value="1" ' + (!items[i].ascending ? 'selected="selected"' : '') + '>Descending</option>' +
            '</select></td></tr>';
        html += tr;
    }
    ctxSorting.tbody.innerHTML = html;
    changeBtnState();
}

function moveCurrentTo(index) {
    var items = ctxSorting.sortManager.sortDescriptions.items, i = 0;
    ctxSorting.sortManager.sortDescriptions.moveCurrentTo(items[index]);
    for (; i < ctxSorting.tbody.children.length; i++) {
        ctxSorting.tbody.children[i].className = index == i ? 'success' : '';
    }
    changeBtnState();
}

function columnIndexChanged(ele, index) {
    if (ctxSorting.sortManager.sortDescriptions.items[index] != null)
        ctxSorting.sortManager.sortDescriptions.items[index].columnIndex = +ele.value;
}

function ascendingChanged(ele, index) {
    ctxSorting.sortManager.sortDescriptions.items[index].ascending = ele.value == "0";
}

// commit the sorts
function commitSort() {
    ctxSorting.sortManager.commitSort();
};

// cancel the sorts
function cancelSort() {
    ctxSorting.sortManager.cancelSort();
    updateSortTable();
};

// add new sort level
function addSortLevel() {
    ctxSorting.sortManager.addSortLevel();
    updateSortTable();
};

// delete current sort level
function deleteSortLevel() {
    ctxSorting.sortManager.deleteSortLevel();
    updateSortTable();
};

// copy a new sort level by current sort level setting.
function copySortLevel() {
    ctxSorting.sortManager.copySortLevel();
    updateSortTable();
};

// move the sort level
function moveSortLevel(offset) {
    ctxSorting.sortManager.moveSortLevel(offset);
    updateSortTable();
};
// get the columns with the column header text for the column selection for sort setting.
function getColumns() {
    var columns = [],
        flex = ctxSorting.flexSheet,
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
};

