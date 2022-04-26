var HostPath = "";
$.support.cors = true;

var PortfolioFGrid = null,
    PortfolioCV = null,
    PortfolioFChart = null,
    AddAutoComp = null,
    addingName = null,
    ChartPeriod = 0,
    Needed;

function InitialControls() {
    PortfolioFGrid = wijmo.Control.getControl('#PortfolioFGrid');
    PortfolioCV = PortfolioFGrid.collectionView;
    AddAutoComp = wijmo.Control.getControl("#AddAutoComp");
    PortfolioFChart = wijmo.Control.getControl('#PortfolioFChart');
    $('#BtnAddToPortfolio').attr('disabled', 'disabled');
    UpdateChartDuration(1);

    PortfolioCV.collectionChanged.addHandler(function () {
        updateChart(PortfolioFChart, PortfolioCV);
    });
}

function PortfolioFGrid_CellEditEnded(grid, e) {
    var col = grid.columns[e.col];
    // for the chart column, when the checkbox editor ends the editing, commit the modification to the server.
    if (col.binding === 'Chart' && PortfolioCV) {
        PortfolioCV.commitEdit();
    }
};

//Function to update chart according to FlexGrid
function updateChart(chart, portfolio) {
    // don't update chart until all done
    chart.beginUpdate();

    // remove current series
    var series = [];
    while (chart.series.length > 0) {
        series.push(chart.series[0]);
        chart.series.splice(0, 1);
    }

    // add new series
    var startDate = getChartStartDate();
    chart.bindingX = 'TimeSlab';
    chart.binding = 'PriceGrowth';
    var items = portfolio.items;
    for (var i = 0; i < items.length; i++) {
        var item = items[i];
        if (item.PriceHistory && item.PriceHistory.length && item.Chart) {
            var series = new wijmo.chart.Series();
            var seriesSource = [];
            for (var count = 0; count < item.PriceHistory.length; count++) {
                if (item.PriceHistory[count].TimeSlab.getTime() < startDate.getTime()) {
                    continue;
                }
                seriesSource.push({ TimeSlab: item.PriceHistory[count].TimeSlab, TimeSlabStr: item.PriceHistory[count].TimeSlabStr, Price: item.PriceHistory[count].Price, PriceGrowth: item.PriceHistory[count].PriceGrowth });
            }
            series.itemsSource = seriesSource;
            series.name = item.Symbol;
            series.style = { stroke: item.Color };
            chart.series.push(series);
        }
    }
    // ready to update chart
    chart.endUpdate();
};

// gets the chart start date based on the current date and chart period
function getChartStartDate(){
    var dt = new Date();
    switch (ChartPeriod) {
        case 5 /* All */:
            return new Date(2005, 1, 1);
        case 0 /* YTD */:            
            return new Date(dt.getFullYear(), 0, 1);
        case 1 /* m6 */:
            dt.setMonth(dt.getMonth() - 6);
            return dt;
        case 2 /* m12 */:
            dt.setFullYear(dt.getFullYear() - 1);
            return dt;
        case 3 /* m24 */:
            dt.setFullYear(dt.getFullYear() - 2);
            return dt;
        case 4 /* m36 */:
            dt.setFullYear(dt.getFullYear() - 3);
            return dt;
    }
    // unknown period, use the last 12 months
    dt.setFullYear(dt.getFullYear() - 1);
    return dt;
};

//Function to delete portfolio from FlexGrid & FlexChart
function deleteRow(symbol, rowIndex) {
    if (!PortfolioCV) {
        return;
    }
    PortfolioCV.removeAt(rowIndex);
};

//Function to add selected portfolio to View (Grid & Chart)
function AddToPortfolio() {
    if (IsValidToAddInPortfolio(AddAutoComp.selectedValue)) {
        // saving the stock name being added.
        addingName = AddAutoComp.selectedValue;
        // clear the selection.
        AddAutoComp.selectedIndex = -1;
        PortfolioCV.refresh();
    }
};

//To update FlexGrid
function collectStockNames(s, e) {
    if (!addingName) {
        return;
    }
    if (e.extraRequestData == null) {
        e.extraRequestData = {};
    }
    e.extraRequestData.stockNames = addingName;
    //set it to null to avoid adding the stock every time.
    addingName = null;
};

//ItemFormatter of FlexGrid
function PortfolioFGrid_ItemFormatter(panel, r, c, cell) {
    if (wijmo.grid.CellType.Cell == panel.cellType && panel.columns[c].binding == 'Color') {
        var cellData = panel.getCellData(r, c);
        cell.innerHTML = '<span style="background-color:' + cellData + ';">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;&nbsp;' + panel.getCellData(r, c + 1);
    }
    if (wijmo.grid.CellType.Cell == panel.cellType && panel.columns[c].binding == 'Symbol') {
        var cellData = panel.getCellData(r, c);
        var link = 'https://finance.yahoo.com/q?s=' + cellData;
        cell.innerHTML = '<a href="' + link + '">' + cellData + '</a>';
    }
    if (wijmo.grid.CellType.Cell == panel.cellType && panel.columns[c].binding == 'Change') {
        var cellData = panel.getCellData(r, c);
        cell.style.color = cellData < 0 ? 'red' : 'green';
    }
    if (wijmo.grid.CellType.Cell == panel.cellType && panel.columns[c].binding == 'Gain') {
        var cellData = panel.getCellData(r, c);
        cell.style.color = cellData < 0 ? 'red' : 'green';
    }
    if (wijmo.grid.CellType.Cell == panel.cellType && panel.columns[c].name == 'Remove') {
        var cellData = panel.getCellData(r, c);
        var symbol = panel.getCellData(r, 2);
        var delfunc = "deleteRow('" + symbol + "'," + r + ")";
        cell.innerHTML = '<a href="#" onclick="' + delfunc + '"><span class="align-center glyphicon glyphicon-remove" style="color:#D14836"></span></a>';
    }
};

//Function to handle selected index changed of AutoComplete
function AddAutoComp_SelectedIndexChanged(sender) {
    var ctrl = sender, btn = $('#BtnAddToPortfolio');

    btn.attr('disabled', 'disabled');
    if (ctrl.selectedIndex >= 0) {
        if(IsValidToAddInPortfolio(ctrl.selectedValue))
            btn.removeAttr('disabled');
    }
};

//To check whether selected portfolio is valid to add or not
function IsValidToAddInPortfolio(symbol) {
    if (symbol && PortfolioCV && PortfolioCV.items) {
        for (var i = 0; i < PortfolioCV.items.length; i++) {
            var TempSymbol = PortfolioCV.items[i].Symbol;
            if (TempSymbol == symbol)
                return false;
        }
        return true;
    }
    return false;
};
    //To update view of chart
function UpdateChartDuration(CPeriod) {
    ChartPeriod = CPeriod;
    updateChart(PortfolioFChart, PortfolioCV);
};

