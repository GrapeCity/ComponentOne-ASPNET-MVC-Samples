var theGrid;
var customCells = true;
var autoUpdate = true;
var interval = 100; // update interval in ms: 1000, 500, 100, 10, 1
var batchSize = 5; // items to update: 100, 50, 10, 5, 1

// cellElements object keeps track of grid's cell elements
var clearCells = false;
var cellElements = {};

// generate sparklines as SVG
function getSparklines(data) {
    var svg = '',
        min = Math.min.apply(Math, data),
        max = Math.max.apply(Math, data),
        x1 = 0,
        y1 = scaleY(data[0], min, max);
    for (var i = 1; i < data.length; i++) {
        var x2 = Math.round((i) / (data.length - 1) * 100);
        var y2 = scaleY(data[i], min, max);
        svg += '<line x1=' + x1 + '% y1=' + y1 + '% x2=' + x2 + '% y2=' + y2 + '% />';
        x1 = x2;
        y1 = y2;
    }
    return '<svg><g>' + svg + '</g></svg>';
}
function scaleY(value, min, max) {
    return max > min ? 100 - Math.round((value - min) / (max - min) * 100) : 0;
}

// custom cell painting
function formatCell(cell, item, col, flare) {
    var history;
    switch (col.binding) {
        case 'Bid':
            history = 'BidHistory';
            break;
        case 'Ask':
            history = 'AskHistory';
            break;
        case 'LastSale':
            history = 'SaleHistory';
            break;
        default:
            break;
    }

    if (!history || !customCells) {
        cell.innerHTML = '';
        cell.textContent = wijmo.Globalize.format(item[col.binding], col.format);
        return;
    }

    var cellContainer = cell.firstElementChild;

    // value
    var eleValue = cellContainer.querySelector('div.value');
    eleValue.textContent = wijmo.Globalize.format(item[col.binding], col.format);

    // % change
    var hist = item[history];
    var chg = hist.length > 1 ? hist[hist.length - 1] / hist[hist.length - 2] - 1 : 0;
    var eleCHG = cellContainer.querySelector('div.chg');
    eleCHG.textContent = wijmo.Globalize.format(chg * 100, 'n1') + '%';

    // up/down glyph
    var glyph = chg > +0.001 ? 'up' : chg < -0.001 ? 'down' : 'circle';
    var span = cellContainer.querySelector('div.glyph span');
    span.setAttribute('class', 'wj-glyph-' + glyph);

    // sparklines
    var eleSpark = cellContainer.querySelector('div.spark');
    eleSpark.innerHTML = getSparklines(hist);

    // change direction
    var dir = glyph == 'circle' ? 'none' : glyph;
    var containerClass = 'ticker chg-' + dir;

    // flare direction
    var flareDir = flare ? dir : 'none';
    containerClass = containerClass + " flare-" + flareDir;
    cellContainer.setAttribute('class', containerClass);
}
function formatDynamicCell(templateCellId, rowIndex, columnIndex) {
    var col = theGrid.columns[columnIndex];
    var row = theGrid.rows[rowIndex].dataItem;
    var cell = document.getElementById(templateCellId).parentElement;
    formatCell(cell, row, col, false);
}

// get a random number within a given interval
function randBetween(min, max) {
    return Math.floor(Math.random() * (max - min + 1) + min);
}

// add a value to a history array
function addHistory(array, data) {
    array.push(data);
    if (array.length > 10) { // limit history length
        array.splice(0, 1);
    }
}

// update grid cells when items change
function updateGrid(changedItems) {
    for (var symbol in changedItems) {
        var itemCells = cellElements[symbol];
        if (itemCells) {
            var item = itemCells.item;
            theGrid.columns.forEach(function (col) {
                var cell = itemCells[col.binding];
                if (cell) {
                    formatCell(cell, item, col, true);
                }
            })
        }
    }
}

// Custom the row header and collect the cell elements.
function formatItem(sender, args) {
    var grid = sender;
    // show symbols in row headers
    if (args.panel == grid.rowHeaders && args.col == 0) {
        args.cell.textContent = grid.rows[args.row].dataItem.Symbol;
    }

    if (args.panel == grid.cells) {
        var col = grid.columns[args.col],
            item = grid.rows[args.row].dataItem;

        // clear cell elements
        if (clearCells) {
            clearCells = false;
            cellElements = {};
        }

        // store cell element
        if (!cellElements[item.Symbol]) {
            cellElements[item.Symbol] = { item: item };
        }

        cellElements[item.Symbol][col.binding] = args.cell;
    }
}

// simulate updates/notifications
function updateTrades() {
    var now = new Date();
    var changedItems = {};
    var data = theGrid.collectionView.sourceCollection;
    for (var i = 0; i < batchSize; i++) {

        // select an item
        var index = randBetween(0, data.length - 1);
        var item = data[index];

        // update current data
        item.Bid = item.Bid * (1 + (Math.random() * .11 - .05));
        item.Ask = item.Ask * (1 + (Math.random() * .11 - .05));
        item.BidSize = randBetween(10, 1000);
        item.AskSize = randBetween(10, 1000);
        var sale = (item.Ask + item.Bid) / 2;
        item.LastSale = sale;
        item.LastSize = Math.floor((item.AskSize + item.BidSize) / 2);
        item.QuoteTime = now;
        item.TradeTime = new Date(Date.now() + randBetween(0, 60000));

        // update history data
        addHistory(item.AskHistory, item.Ask);
        addHistory(item.BidHistory, item.Bid);
        addHistory(item.SaleHistory, item.LastSale);

        // keep track of changed items
        changedItems[item.Symbol] = true;
    }

    // update the grid
    if (autoUpdate) {
        updateGrid(changedItems);
    }

    // and schedule the next batch
    setTimeout(updateTrades, interval);
}

// listen the selectedIndexChanged event for batchSize
function batchSize_SelectedIndexChanged(sender, args) {
    batchSize = sender.selectedValue;
}

// listen the selectedIndexChanged event for interval
function updateInterval_SelectedIndexChanged(sender, args) {
    interval = sender.selectedValue;
}

function updatingView() {
    clearCells = true;
}

function initHamburgerNav() {
    var hamburgerNavBtn = document.querySelector('.hamburger-nav-btn');
    var hamburgerNavEle = document.querySelector('.narrow-screen.site-nav');
    new c1.sample.MultiLevelMenu(hamburgerNavEle, hamburgerNavBtn);
}

c1.documentReady(function () {
    // get the initialized value from ui.
    var updateInterval = wijmo.Control.getControl('#updateInterval');
    var batchSize = wijmo.Control.getControl('#batchSize');
    interval = updateInterval.selectedValue;
    batchSize = batchSize.selectedValue;

    document.getElementById('customCells').addEventListener('click', function (e) {
        customCells = e.target.checked;
        theGrid.refresh();
    });
    document.getElementById('autoUpdate').addEventListener('click', function (e) {
        autoUpdate = e.target.checked;
        theGrid.refresh();
    });

    theGrid = wijmo.Control.getControl('#theGrid');
    theGrid.rowHeaders.columns[0].width = 80;
    updateTrades();

    initHamburgerNav();
});