'use strict';
//// save and load views
var app = {
    viewDefs: null
};
var saveLoadViewsPivotPanel = null;

// view and edit the source data
var pivotEngine = null,
    vESourceFGrid = null;

// export the result to excel
var exportExcelPivotGrid = null;

function InitialControls() {
    // view and edit the source data
    pivotEngine = c1.getService('pivotEngine');
    vESourceFGrid = wijmo.Control.getControl('#vESourceFGrid');
    vESourceFGrid.itemsSource = pivotEngine.itemsSource;

    // export the result to excel
    exportExcelPivotGrid = wijmo.Control.getControl('#exportExcelPivotGrid');

    // save and load views

    // pre-defined views
    app.viewDefs = [
        {
            name: "Sales by Product",
            def: "{\"showColumnTotals\":2,\"showRowTotals\":2,\"defaultFilterType\":3,\"fields\":[{\"dataType\":2,\"format\":\"n0\",\"aggregate\":1,\"showAs\":0,\"descending\":false,\"isContentHtml\":false,\"binding\":\"ID\",\"header\":\"ID\"},{\"dataType\":1,\"format\":\"\",\"aggregate\":2,\"showAs\":0,\"descending\":false,\"isContentHtml\":false,\"binding\":\"Product\",\"header\":\"Product\"},{\"dataType\":1,\"format\":\"\",\"aggregate\":2,\"showAs\":0,\"descending\":false,\"isContentHtml\":false,\"binding\":\"Country\",\"header\":\"Country\"},{\"dataType\":4,\"format\":\"d\",\"aggregate\":2,\"showAs\":0,\"descending\":false,\"isContentHtml\":false,\"binding\":\"Date\",\"header\":\"Date\"},{\"dataType\":2,\"format\":\"n0\",\"aggregate\":1,\"showAs\":0,\"descending\":false,\"isContentHtml\":false,\"binding\":\"Sales\",\"header\":\"Sales\"},{\"dataType\":2,\"format\":\"n0\",\"aggregate\":1,\"showAs\":0,\"descending\":false,\"isContentHtml\":false,\"binding\":\"Downloads\",\"header\":\"Downloads\"},{\"dataType\":3,\"format\":\"\",\"aggregate\":2,\"showAs\":0,\"descending\":false,\"isContentHtml\":false,\"binding\":\"Active\",\"header\":\"Active\"},{\"dataType\":2,\"format\":\"n0\",\"aggregate\":1,\"showAs\":0,\"descending\":false,\"isContentHtml\":false,\"binding\":\"Discount\",\"header\":\"Discount\"}],\"rowFields\":{\"items\":[\"Product\"]},\"columnFields\":{\"items\":[]},\"filterFields\":{\"items\":[]},\"valueFields\":{\"items\":[\"Sales\"]}}"
        },
        {
            name: "Sales by Country",
            def: "{\"showZeros\":false,\"showColumnTotals\":2,\"showRowTotals\":2,\"defaultFilterType\":3,\"fields\":[{\"dataType\":2,\"format\":\"n0\",\"aggregate\":1,\"showAs\":0,\"descending\":false,\"isContentHtml\":false,\"binding\":\"ID\",\"header\":\"ID\"},{\"dataType\":1,\"format\":\"\",\"aggregate\":2,\"showAs\":0,\"descending\":false,\"isContentHtml\":false,\"binding\":\"Product\",\"header\":\"Product\"},{\"dataType\":1,\"format\":\"\",\"aggregate\":2,\"showAs\":0,\"descending\":false,\"isContentHtml\":false,\"binding\":\"Country\",\"header\":\"Country\"},{\"dataType\":4,\"format\":\"d\",\"aggregate\":2,\"showAs\":0,\"descending\":false,\"isContentHtml\":false,\"binding\":\"Date\",\"header\":\"Date\"},{\"dataType\":2,\"format\":\"n0\",\"aggregate\":1,\"showAs\":0,\"descending\":false,\"isContentHtml\":false,\"binding\":\"Sales\",\"header\":\"Sales\"},{\"dataType\":2,\"format\":\"n0\",\"aggregate\":1,\"showAs\":0,\"descending\":false,\"isContentHtml\":false,\"binding\":\"Downloads\",\"header\":\"Downloads\"},{\"dataType\":3,\"format\":\"\",\"aggregate\":2,\"showAs\":0,\"descending\":false,\"isContentHtml\":false,\"binding\":\"Active\",\"header\":\"Active\"},{\"dataType\":2,\"format\":\"n0\",\"aggregate\":1,\"showAs\":0,\"descending\":false,\"isContentHtml\":false,\"binding\":\"Discount\",\"header\":\"Discount\"}],\"rowFields\":{\"items\":[\"Country\"]},\"columnFields\":{\"items\":[]},\"filterFields\":{\"items\":[]},\"valueFields\":{\"items\":[\"Sales\"]}}"
        },
        {
            name: "Sales and Downloads by Country",
            def: "{\"showZeros\":false,\"showColumnTotals\":2,\"showRowTotals\":2,\"defaultFilterType\":3,\"fields\":[{\"dataType\":2,\"format\":\"n0\",\"aggregate\":1,\"showAs\":0,\"descending\":false,\"isContentHtml\":false,\"binding\":\"ID\",\"header\":\"ID\"},{\"dataType\":1,\"format\":\"\",\"aggregate\":2,\"showAs\":0,\"descending\":false,\"isContentHtml\":false,\"binding\":\"Product\",\"header\":\"Product\"},{\"dataType\":1,\"format\":\"\",\"aggregate\":2,\"showAs\":0,\"descending\":false,\"isContentHtml\":false,\"binding\":\"Country\",\"header\":\"Country\"},{\"dataType\":4,\"format\":\"d\",\"aggregate\":2,\"showAs\":0,\"descending\":false,\"isContentHtml\":false,\"binding\":\"Date\",\"header\":\"Date\"},{\"dataType\":2,\"format\":\"n0\",\"aggregate\":1,\"showAs\":0,\"descending\":false,\"isContentHtml\":false,\"binding\":\"Sales\",\"header\":\"Sales\"},{\"dataType\":2,\"format\":\"n0\",\"aggregate\":1,\"showAs\":0,\"descending\":false,\"isContentHtml\":false,\"binding\":\"Downloads\",\"header\":\"Downloads\"},{\"dataType\":3,\"format\":\"\",\"aggregate\":2,\"showAs\":0,\"descending\":false,\"isContentHtml\":false,\"binding\":\"Active\",\"header\":\"Active\"},{\"dataType\":2,\"format\":\"n0\",\"aggregate\":1,\"showAs\":0,\"descending\":false,\"isContentHtml\":false,\"binding\":\"Discount\",\"header\":\"Discount\"}],\"rowFields\":{\"items\":[\"Country\"]},\"columnFields\":{\"items\":[]},\"filterFields\":{\"items\":[]},\"valueFields\":{\"items\":[\"Sales\",\"Downloads\"]}}"            
        },
        {
            name: "Sales Trend by Product",
            def: "{\"showZeros\": false, \"showColumnTotals\": 2, \"showRowTotals\": 2, \"defaultFilterType\": 3, \"fields\": [{ \"dataType\": 2, \"format\": \"n0\", \"aggregate\": 1, \"showAs\": 0, \"descending\": false, \"isContentHtml\": false, \"binding\": \"ID\", \"header\": \"ID\" }, { \"dataType\": 1, \"format\": \"\", \"aggregate\": 2, \"showAs\": 0, \"descending\": false, \"isContentHtml\": false, \"binding\": \"Product\", \"header\": \"Product\" }, { \"dataType\": 1, \"format\": \"\", \"aggregate\": 2, \"showAs\": 0, \"descending\": false, \"isContentHtml\": false, \"binding\": \"Country\", \"header\": \"Country\" }, { \"dataType\": 4, \"format\": \"yyyy \\\"Q\\\"q\", \"aggregate\": 2, \"showAs\": 0, \"descending\": false, \"isContentHtml\": false, \"binding\": \"Date\", \"header\": \"Date\" }, { \"dataType\": 2, \"format\": \"p2\", \"aggregate\": 3, \"showAs\": 2, \"descending\": false, \"isContentHtml\": false, \"binding\": \"Sales\", \"header\": \"Sales\" }, { \"dataType\": 2, \"format\": \"n0\", \"aggregate\": 1, \"showAs\": 0, \"descending\": false, \"isContentHtml\": false, \"binding\": \"Downloads\", \"header\": \"Downloads\" }, { \"dataType\": 3, \"format\": \"\", \"aggregate\": 2, \"showAs\": 0, \"descending\": false, \"isContentHtml\": false, \"binding\": \"Active\", \"header\": \"Active\" }, { \"dataType\": 2, \"format\": \"n0\", \"aggregate\": 1, \"showAs\": 0, \"descending\": false, \"isContentHtml\": false, \"binding\": \"Discount\", \"header\": \"Discount\" }, { \"dataType\": 2, \"format\": \"n0\", \"aggregate\": 1, \"showAs\": 0, \"descending\": false, \"isContentHtml\": false, \"binding\": \"Sales\", \"header\": \"Sales2\" }], \"rowFields\": { \"items\": [\"Date\"] }, \"columnFields\": { \"items\": [\"Product\"] }, \"filterFields\": { \"items\": [] }, \"valueFields\": { \"items\": [\"Sales\"]}}"
        }
    ]
    saveLoadViewsPivotPanel = wijmo.Control.getControl('#saveLoadViewsPivotPanel');

    // populate list of pre-defined views
    var viewList = document.getElementById('views');
    for (var i = 0; i < app.viewDefs.length; i++) {
        var li = wijmo.createElement('<li><a href="#theView" index="' + i + '">' +app.viewDefs[i].name + '</a></li>');
        viewList.appendChild(li);
    }

    // apply pre-defined views
    viewList.addEventListener('click', function (e) {
        if (e.target.tagName == 'A') {
            var index = e.target.getAttribute('index');
            saveLoadViewsPivotPanel.viewDefinition = app.viewDefs[index].def;
        }
    });
};

// Configure the PivotPanel Properties
function customChangeRowTotals(olapControl, value) {
    if (olapControl && olapControl.engine) {
        olapControl.engine.showRowTotals = value;
    }
};

function customChangeColumnTotals(olapControl, value) {
    if (olapControl && olapControl.engine) {
        olapControl.engine.showColumnTotals = value;
    }
};

function customChangeShowZeros(olapControl, value) {
    if (olapControl && olapControl.engine) {
        olapControl.engine.showZeros = value;
    }
};

function customChangeTotalsBeforeData(olapControl, value) {
    if (olapControl && olapControl.engine) {
        olapControl.engine.totalsBeforeData = value;
    }
};

// Show the Results in a PivotChart
function cmbChartType_SelectedIndexChanged(sender) {
    var value = sender.selectedValue;
    var control = wijmo.Control.getControl("#rPCPivotChart");
    if (control) {
        control.chartType = value;
    }
};

// Customize the PivotGrid Cells
function cTPGCPivotGrid_ItemFormatter(panel, r, c, cell) {
    if (wijmo.grid.CellType.Cell == panel.cellType && c % 2 == 1) {
        var value = panel.getCellData(r, c),
            color = '#d8b400',
            glyph = 'circle';
        if (value != null) {
            if (value < 0) { // negative variation
                color = '#9f0000';
                glyph = 'down';
            } else if (value > 0.05) { // positive variation
                color = '#4c8f00';
                glyph = 'down';
            }
            cell.style.color = color;
            cell.innerHTML += ' <span style="font-size:120%" class="wj-glyph-' + glyph + '"></span>';
        }
    }
};

// export to excel
function exportToExcel() {
    // create book with current view
    var book = wijmo.grid.xlsx.FlexGridXlsxConverter.save(exportExcelPivotGrid, {
        includeColumnHeaders: true,
        includeRowHeaders: true
    });
    book.sheets[0].name = 'Main View';
    addTitleCell(book.sheets[0], getViewTitle(pivotEngine));

    // add sheet with transposed view
    transposeView(pivotEngine);
    var transposed = wijmo.grid.xlsx.FlexGridXlsxConverter.save(exportExcelPivotGrid, {
        includeColumnHeaders: true,
        includeRowHeaders: true
    });
    transposed.sheets[0].name = 'Transposed View';
    addTitleCell(transposed.sheets[0], getViewTitle(pivotEngine));
    book.sheets.push(transposed.sheets[0]);
    transposeView(pivotEngine);

    // save the book
    book.save('wijmo.olap.xlsx');
};

// save/load/transpose/export views
function transposeView(ng) {
    ng.deferUpdate(function () {

        // save row/col fields
        var rows = [],
            cols = [];
        for (var r = 0; r < ng.rowFields.length; r++) {
            rows.push(ng.rowFields[r].header);
        }
        for (var c = 0; c < ng.columnFields.length; c++) {
            cols.push(ng.columnFields[c].header);
        }

        // clear row/col fields
        ng.rowFields.clear();
        ng.columnFields.clear();

        // restore row/col fields in transposed order
        for (var r = 0; r < rows.length; r++) {
            ng.columnFields.push(rows[r]);
        }
        for (var c = 0; c < cols.length; c++) {
            ng.rowFields.push(cols[c]);
        }
    });
}

// build a title for the current view
function getViewTitle(ng) {
    var title = '';
    for (var i = 0; i < ng.valueFields.length; i++) {
        if (i > 0) title += ', ';
        title += ng.valueFields[i].header;
    }
    title += ' by ';
    if (ng.rowFields.length) {
        for (var i = 0; i < ng.rowFields.length; i++) {
            if (i > 0) title += ', ';
            title += ng.rowFields[i].header;
        }
    }
    if (ng.rowFields.length && ng.columnFields.length) {
        title += ' and by ';
    }
    if (ng.columnFields.length) {
        for (var i = 0; i < ng.columnFields.length; i++) {
            if (i > 0) title += ', ';
            title += ng.columnFields[i].header;
        }
    }
    return title;
}

// adds a title cell into an xlxs sheet
function addTitleCell(sheet, title) {
    // create cell
    var cell = new wijmo.xlsx.WorkbookCell();
    cell.value = title;
    cell.style = new wijmo.xlsx.WorkbookStyle();
    cell.style.font = new wijmo.xlsx.WorkbookFont();
    cell.style.font.bold = true;

    // create row to hold the cell
    var row = new wijmo.xlsx.WorkbookRow();
    row.cells[0] = cell;

    // and add the new row to the sheet
    sheet.rows.splice(0, 0, row);
}

// gets a random integer between zero and max
function randomInt(max) {
    return Math.floor(Math.random() * (max + 1));
}

// save/load view definition

// save view definition
function saveView() {
    if (saveLoadViewsPivotPanel && saveLoadViewsPivotPanel.viewDefinition)
        localStorage.setItem('viewDefinition', saveLoadViewsPivotPanel.viewDefinition);
};

// restore view definition
function loadView() {
    if (localStorage.getItem('viewDefinition'))
        saveLoadViewsPivotPanel.viewDefinition = localStorage.getItem('viewDefinition');
};