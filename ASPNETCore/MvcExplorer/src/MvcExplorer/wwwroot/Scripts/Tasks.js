var tpTasks, gridTasks, gpTasks,
    arrSources = {}, arrGColumnNames = {};

c1.documentReady(function () {
    init();
    getData();
});

function init() {
    gridTasks = wijmo.Control.getControl('#gridTasks');
    tpTasks = wijmo.Control.getControl('#tpTasks');
    gpTasks = wijmo.Control.getControl('.wj-grouppanel');
    var tabCount = tpTasks.tabs.length;
    for (var i = 0; i < tabCount; i++) {
        arrGColumnNames[i] = ['AssignedTo'];
    }

    // for IE
    var eleRoot = tpTasks.hostElement.querySelector('div[wj-part="root"]');
    tpTasks.hostElement.style.height = eleRoot.getBoundingClientRect().height + 'px';
}

function reset() {
    var tabCount = tpTasks.tabs.length;
    for (var i = 0; i < tabCount; i++) {
        if (arrSources[i]) {
            arrGColumnNames[i] = [];
            if (arrSources[i].groupDescriptions) {
                arrSources[i].groupDescriptions.forEach(function (gd) {
                    arrGColumnNames[i].push(gd.propertyName);
                });
            }
        }
    }
    arrSources = {};
}

function selectedIndexChanged(sender, e) {
    getData();
}

function updateData(start, end, url) {
    var currentType = tpTasks.selectedIndex;
    if (arrSources && arrSources[currentType]) {
        var cv = arrSources[currentType],
            gcns = [];
        if (cv.groupDescriptions) {
            cv.groupDescriptions.forEach(function (gd) {
                gcns.push(gd.propertyName);
            });
            cv.groupDescriptions.clear();
        }
        initFlexGrid(cv, gcns);
        return;
    }

    // send a request for the data.
    refreshData(start, end, url, function (data) {
        initFlexGrid(data[0].items, arrGColumnNames[currentType]);
        arrSources[currentType] = gridTasks.collectionView;
    }, {
        taskType: currentType
    });
}

function initFlexGrid(source, gcns) {
    gridTasks.itemsSource = source;
    gpTasks.refresh();
    if (gcns) {
        gcns.forEach(function (gcn) {
            dogrouping(gridTasks, gpTasks, gcn);
        });
    }
    gpTasks.invalidate();
}
// do grouping in grouppanel.
function dogrouping(grid, group, cn) {
    var column,
        colCount = grid.columns.length;
    for (var i = 0; i < colCount; i++) {
        if (grid.columns[i].binding == cn) {
            column = grid.columns[i];
            break;
        }
    }
    group._gds.deferUpdate(function () {
        var pgd = new wijmo.collections.PropertyGroupDescription(column.binding);
        group._gds.push(pgd);
    });
    column.visible = false;
    group._hiddenCols.push(column);
}

// Custom cell formatter to paint the progress bar.
function completeFormatter(panel, r, c, cell) {
    if (panel.columns[c].binding === 'Complete') {
        cell.style.textAlign = '';
        if (panel.cellType === wijmo.grid.CellType.Cell) {
            cell.innerHTML = '';
            var value = panel.getCellData(r, c);
            var content = buildProgressBar(value, cell.clientWidth - 3*2);
            content += '<span class="progress-value">';
            content += (value * 100) + '%';
            content += '</span>';
            cell.innerHTML = content;
        }
    }
}
// Draw the progress bar.
function buildProgressBar(value, fullWidth) {
    var content = '<div class="progress-bar" style="';
    content += 'width:' + (value * fullWidth) + 'px;"';
    content += '></div>';
    return content;
}