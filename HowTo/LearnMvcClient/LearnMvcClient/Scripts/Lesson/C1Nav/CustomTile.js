var popup, cmbChartType;
function formatTile(sender, e) {
    var dashboard = sender, // gets the DashboardLayout control
        tile = e.tile, // gets the formatted tile
        grid = wijmo.Control.getControl('#salesDashboardFGrid');

    if (grid && grid.selectedItems && grid.selectedItems.length) {
        var selectedRowData = grid.selectedItems[0];
        switch (tile.headerText) {
            case 'Country':
                // update the tile content.
                e.tile.hostElement.style.backgroundColor = '#009ccc';
                var htmlContent = '<div style="color:white;">' + countryText + '</div>' +
                    '<div style="font-size:72px; text-align: center; color:white;overflow:hidden; text-overflow:ellipsis">' +
                    selectedRowData.Country +
                    '</div>';
                e.contentElement.innerHTML = htmlContent;
                break;
            case 'KPIs':
                // modify the header title.
                e.headerElement.querySelector('span.title').innerText = kpisHeaderText;

                // update the guages properties.
                var kPIsSalesGauge = wijmo.Control.getControl('#kPIsSalesGauge'),
                    kPIsExpensesGauge = wijmo.Control.getControl('#kPIsExpensesGauge'),
                    kPIsProfitGauge = wijmo.Control.getControl('#kPIsProfitGauge'),
                    kPIsData = selectedRowData.KPIsData,
                    kPIsExpensesGaugeValue = document.getElementById('kPIsExpensesGaugeValue'),
                    kPIsSalesGaugeValue = document.getElementById('kPIsSalesGaugeValue');
                kPIsProfitGaugeValue = document.getElementById('kPIsProfitGaugeValue');
                kPIsSalesGauge.max = kPIsData.Max;
                kPIsSalesGauge.value = kPIsData.Sales;
                kPIsSalesGaugeValue.innerText = share_Text1 + ': ' + wijmo.format('{Sales:c0}', kPIsData);
                kPIsExpensesGauge.max = kPIsData.Max;
                kPIsExpensesGauge.value = kPIsData.Expenses;
                kPIsExpensesGaugeValue.innerText = share_Text2 + ': ' + wijmo.format('{Expenses:c0}', kPIsData);
                kPIsProfitGauge.max = kPIsData.Sales;
                kPIsProfitGauge.value = kPIsData.Profit;
                kPIsProfitGaugeValue.innerText = share_Text3 + ': ' + wijmo.format('{Profit:c0}', kPIsData);

                break;
            case 'Countrywise Sales':
                // modify the header title.
                e.headerElement.querySelector('span.title').innerText = countrywiseHeaderText;

                // customize the toolbar
                var toolbar = e.toolbar; // get the toolbar control.

                // add a 'Settings' item in toolbar for setting the chart options via dom.
                var iconClose = document.createElement('img');
                iconClose.title = settingsText;
                iconClose.alt = settingsText;
                iconClose.style.marginRight = '6px';
                iconClose.style.cursor = 'default';
                iconClose.src = "../Content/images/th.png";
                // insert the item at the first position
                var eleToolbar = toolbar.hostElement;
                eleToolbar.insertBefore(iconClose, eleToolbar.firstChild);
                iconClose.addEventListener('click', function () {
                    // when this item is clicked, show a dialog to specify the chart type.
                    if (!popup) {
                        popup = new wijmo.input.Popup('#popup');
                    }

                    if (!cmbChartType) {
                        cmbChartType = new wijmo.input.ComboBox('#chartType', { itemsSource: ["Column", "Bar", "Scatter", "Line", "LineSymbols", "Area", "Spline", "SplineSymbols", "SplineArea"] });
                    }

                    var countrywiseSalesChart = wijmo.Control.getControl('#countrywiseSalesChart');
                    if (countrywiseSalesChart) {
                        cmbChartType.text = wijmo.chart.ChartType[countrywiseSalesChart.chartType];
                    }

                    popup.show(true, function (e) {
                        if (e.dialogResult == 'wj-hide-ok') {
                            // apply the chart type
                            var chart = wijmo.Control.getControl('#countrywiseSalesChart');
                            chart.chartType = wijmo.chart.ChartType[cmbChartType.text];
                        }
                    });
                });

                // clear all the internal items.
                toolbar.clear();

                // add a custom item in toolbar for exporting the content via toolbar api.
                var strExportIcon = '<img style="vertical-align:middle" src="../Content/images/icon_export.png" alt="' + exportText + '" title="' + exportText + '" />';
                toolbar.insertToolbarItem({
                    icon: strExportIcon,
                    title: exportText,
                    command: function () {
                        var selector = e.tile.content,
                            chart = wijmo.Control.getControl(selector);
                        chart.saveImageToFile(selector.substr(1) + '.png');
                    }
                }, 0);
                break;
            default:
                break;
        }
    }
}

function gridSelectionChanged(sender, e) {
    // refresh the DashboardLayout control after the selectionChanged is fired in the grid.
    var dashboard = wijmo.Control.getControl('#custom');
    dashboard.refresh();
}