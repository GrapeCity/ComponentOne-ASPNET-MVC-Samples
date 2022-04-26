function changeSortBtnState() {
    moveup.disabled = ctx.sortManager.sortDescriptions.currentPosition <= 0;
    movedown.disabled = ctx.sortManager.sortDescriptions.currentPosition >= ctx.sortManager.sortDescriptions.itemCount - 1;
}

function updateSortTable() {
    var i, j, html = '', tr, sortDescriptions = ctx.sortManager.sortDescriptions,
        items = sortDescriptions.items;

    if (!ctx.tbody) {
        return;
    }

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
    changeSortBtnState();
}

function moveCurrentTo(index) {
    var items = ctx.sortManager.sortDescriptions.items, i = 0;
    ctx.sortManager.sortDescriptions.moveCurrentTo(items[index]);
    for (; i < ctx.tbody.children.length; i++) {
        ctx.tbody.children[i].className = index == i ? 'success' : '';
    }
    changeSortBtnState();
}

function columnIndexChanged(ele, index) {
    ctx.sortManager.sortDescriptions.items[index].columnIndex = +ele.value;
}

function ascendingChanged(ele, index) {
    ctx.sortManager.sortDescriptions.items[index].ascending = ele.value == "0";
}

// commit the sorts
function commitSort() {
    ctx.sortManager.commitSort();
    updateSortTable();
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