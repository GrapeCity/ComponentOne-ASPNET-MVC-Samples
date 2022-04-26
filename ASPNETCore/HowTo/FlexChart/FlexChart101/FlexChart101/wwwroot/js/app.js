var chartSelectionMode = typeMenu = selectionModeMenu = seriesContainer = detailContainer = null;

$(document).ready(function () {

    //Legend & Title Module
    var ltchart = wijmo.Control.getControl("#chartLegendAndTitles");
    var ltHeader = document.getElementById('headerInput');
    var ltFooter = document.getElementById('footerInput');
    var ltXTitle = document.getElementById('xTitleInput');
    var ltYTitle = document.getElementById('yTitleInput');

    ltHeader.value = 'Sample Chart';
    ltHeader.addEventListener('input', function () {
        ltchart.header = this.value;
    });

    ltFooter.value = 'Copyright (c) ComponentOne';
    ltFooter.addEventListener('input', function () {
        ltchart.footer = this.value;
    });

    ltXTitle.value = 'Country';
    ltXTitle.addEventListener('input', function () {
        ltchart.axisX.title = this.value;
    });

    ltYTitle.value = 'Amount';
    ltYTitle.addEventListener('input', function () {
        ltchart.axisY.title = this.value;
    });


    //Selection Modes Module  
    chartSelectionMode = wijmo.Control.getControl('#chartSelectionMode'),
        typeMenu = wijmo.Control.getControl('#chartTypeMenu'),
        selectionModeMenu = wijmo.Control.getControl('#selectionModeMenu'),
        seriesContainer = document.getElementById('seriesContainer'),
        detailContainer = document.getElementById('detailContainer');

    var chartLegendToggle = wijmo.Control.getControl('#chartLegendToggle'),
        cbSales = document.getElementById('cbSales'),
        cbExpenses = document.getElementById('cbExpenses'),
        cbDownloads = document.getElementById('cbDownloads');

    //Toggle Series Module  

    // loop through custom check boxes
    ['cbSales', 'cbExpenses', 'cbDownloads'].forEach(function (item, index) {
        // update checkbox and toggle FlexChart's series visibility when clicked
        var el = document.getElementById(item);
        el.checked = chartLegendToggle.series[index].visibility === wijmo.chart.SeriesVisibility.Visible;
        el.addEventListener('click', function () {
            if (this.checked) {
                chartLegendToggle.series[index].visibility = wijmo.chart.SeriesVisibility.Visible;
            }
            else {
                chartLegendToggle.series[index].visibility = wijmo.chart.SeriesVisibility.Legend;
            }
        });
    });
});

//Chart Types Module
function typeMenu_SelectedIndexChanged(sender) {
    if (sender.selectedValue) {
        var chartTypes = wijmo.Control.getControl("#chartTypes");
        chartTypes.chartType = sender.selectedValue;
    }
}

function stackingMenu_SelectedIndexChanged(sender) {
    if (sender.selectedValue) {
        var chartTypes = wijmo.Control.getControl("#chartTypes");
        chartTypes.stacking = parseInt(sender.selectedIndex);//sender.selectedValue;
    }
}

function rotatedMenu_SelectedIndexChanged(sender) {
    if (sender.selectedValue) {
        var chartTypes = wijmo.Control.getControl("#chartTypes");
        chartTypes.rotated = sender.selectedValue == 'True' ? true : false;
    }
}

//Legend and Title Module
function positionMenu_SelectedIndexChanged(sender) {
    if (sender.selectedValue) {
        var chart = wijmo.Control.getControl("#chartLegendAndTitles");
        chart.legend.position = parseInt(sender.selectedIndex);
    }
}


//Selection Modes Module
function selectionModeMenu_SelectedIndexChanged(sender) {
    if (sender.selectedValue) {
        //var chart = wijmo.Control.getControl("#chartSelectionMode");
        chartSelectionMode.selectionMode = parseInt(sender.selectedIndex);

        // toggle the series panel's visiblity
        if (sender.selectedIndex === 0 || !chartSelectionMode.selection) {
            if (seriesContainer)
                seriesContainer.style.display = 'none';
        }
        else {
            if (seriesContainer)
                seriesContainer.style.display = 'block';
        }

        // toggle the series panel's visiblity
        if (sender.selectedIndex !== 2 || !chartSelectionMode.selection || !chartSelectionMode.selection.collectionView.currentItem) {
            if (detailContainer)
                detailContainer.style.display = 'none';
        }
        else {
            // update the details
            setSeriesDetail(chartSelectionMode.selection.collectionView.currentItem);
        }

    }
}

function chartTypeMenu_SelectedIndexChanged(sender) {
    if (sender.selectedValue) {
        chartSelectionMode.chartType = sender.selectedValue;
    }
}

// update details when the FlexChart's selection changes
function chartSelectionMode_SelectionChanged(sender) {
    var currentSelection = sender.selection,
        currentSelectItem;
    if (currentSelection) {
        var seriesContainer = document.getElementById('seriesContainer');
        selectionModeMenu = wijmo.Control.getControl('#selectionModeMenu');
        seriesContainer.style.display = 'block'; // show container

        document.getElementById('seriesName').innerHTML = currentSelection.name;
        currentSelectItem = currentSelection.collectionView.currentItem;

        if (currentSelectItem && selectionModeMenu && selectionModeMenu.selectedValue === 'Point') {
            setSeriesDetail(currentSelectItem); // update details
        }
    }
}

// helper method to show details of the FlexChart's current selection
function setSeriesDetail(currentSelectItem) {
    detailContainer.style.display = 'block';
    document.getElementById('seriesCountry').innerHTML = currentSelectItem.Country;
    document.getElementById('seriesSales').innerHTML = wijmo.Globalize.format(currentSelectItem.Sales, 'c2');
    document.getElementById('seriesExpenses').innerHTML = wijmo.Globalize.format(currentSelectItem.Expenses, 'c2');
    document.getElementById('seriesDownloads').innerHTML = wijmo.Globalize.format(currentSelectItem.Downloads, 'n0');
};

//Toggle Series Module  
function chartLegendToggle_SeriesVisibilityChanged(sender) {
    // loop through chart series
    sender.series.forEach(function (series) {
        var seriesName = series.name,
        checked = series.visibility === wijmo.chart.SeriesVisibility.Visible;

        // update custom checkbox panel
        document.getElementById('cb' + seriesName).checked = checked;
    });
}

