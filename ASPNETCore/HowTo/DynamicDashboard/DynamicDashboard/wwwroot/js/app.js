var settingsObj = null, salesDashboardFGrid = null, costBudgetingChart = null, costBudgetingChartTypeCmb = null, costBudgetingFGrid = null, monthlySalesChart = null, monthlySalesChartTypeCmb = null, monthlySalesChartTrendLineCmb = null, monthlySalesFGrid = null, countrywiseSalesChart = null, countrywiseSalesChartTypeCmb = null, countrywiseSalesFGrid = null, monthlyProfitChart = null, monthlyProfitChartTypeCmb = null, monthlyProfitFGrid = null, kPIsSalesGauge = null, kPIsExpensesGauge = null, kPIsProfitGauge = null, kPIsSalesGaugeValue = null, kPIsExpensesGaugeValue = null, kPIsProfitGaugeValue = null, imgScrollMenuShowHide = null, scrollmenu = null;
var salesDashboardFGridCV = null, monthlyProfitChartCV = null, monthlyProfitFGridCV = null, monthlyProfitChartFilterText = '', monthlySalesChartCV = null, monthlySalesFGridCV = null, monthlySalesChartFilterText = '';
// drag-drop event handlers
var dashboard = null;
var dragSource = null, dropTarget = null;
// function to instantiate bootstrap carousel
function InstantiateCarousel() {
    // Instantiate the Bootstrap carousel
    $('.multi-item-carousel').carousel({
        interval: false
    });
    $('.multi-item-carousel .item').each(function () {
        var next = $(this).next();
        if (!next.length) {
            next = $(this).siblings(':first');
        }
        next.children(':first-child').clone().appendTo($(this));
        if (next.next().length > 0) {
            next.next().children(':first-child').clone().appendTo($(this));
        }
        else {
            $(this).siblings(':first').children(':first-child').clone().appendTo($(this));
        }
    });
}
;
// function to close dashboard widget
function closeWidget(widgetId) {
    var widget = document.getElementById(widgetId);
    $('#' + widgetId + '_li').css('display', 'none');
    addToWidgetList(widgetId);
    refreshAllControls();
}
;
//function to set tabs
function setTabs() {
    $(".css-tabs a[data-toggle]").on("click", function (e) {
        var selector = $(this).data("toggle"); // get corresponding element
        $(this).parent().find('a').each(function () {
            $(this).removeClass();
        });
        $(this).addClass('link-selected');
        $(this).parent().find('.tab-selected').each(function () {
            $(this).removeClass();
            $(this).addClass('tab');
        });
        $('#' + selector).addClass('tab-selected');
        refreshAllControls();
    });
}
;
// function to add dashboard widget
function addWidget(widgetId, controlID) {
    if ($('#' + widgetId + '_li').css('display') != 'block') {
        $("#sortable").append($('#sortable  > #' + widgetId + '_li'));
        $('#' + widgetId + '_li').css('display', 'block');
        removeFromWidgetList(widgetId);
        refreshAllControls();
    }
    else {
        $('#' + widgetId + '_li').effect("bounce", "slow");
    }
}
;
$(document).ready(function () {
});
$(document).click(function (event) {
    var headerMenuClose = true;
    var scrollMenuClose = true;
    var element = event.target;
    if (element.id != 'imgIconMenu' && element.id != 'imgIconMenu')
        headerMenuShowHide('imgIconMenu', 'headerMenuList', 'close');
    if (element.id != 'imgScrollMenuShowHide' && element.id != 'scrollmenu'
        && (element.parentElement != null && element.parentElement.id != 'bs-carousel-prev')
        && (element.parentElement != null && element.parentElement.id != 'bs-carousel-next'))
        scrollMenuShowHide('close');
});
function handleDragStart(e) {
    var target = wijmo.closest(e.target, '.tile');
    if (target && target.parentElement == dashboard) {
        dragSource = target;
        wijmo.addClass(dragSource, 'drag-source');
        var dt = e.dataTransfer;
        dt.effectAllowed = 'move';
        dt.setData('text', dragSource.innerHTML);
    }
}
function handleDragOver(e) {
    if (dragSource) {
        var tile = wijmo.closest(e.target, '.tile');
        if (tile == dragSource) {
            tile = null;
        }
        if (dragSource && tile && tile != dragSource) {
            e.preventDefault();
            e.dataTransfer.dropEffect = 'move';
        }
        if (dropTarget != tile) {
            wijmo.removeClass(dropTarget, 'drag-over');
            dropTarget = tile;
            wijmo.addClass(dropTarget, 'drag-over');
        }
    }
}
function handleDragEnd(e) {
    wijmo.removeClass(dragSource, 'drag-source');
    wijmo.removeClass(dropTarget, 'drag-over');
    dragSource = dropTarget = null;
}
function handleDrop(e) {
    if (dragSource && dropTarget) {
        e.stopPropagation();
        e.stopImmediatePropagation();
        e.preventDefault();
        var srcIndex = getIndex(dragSource), dstIndex = getIndex(dropTarget), refChild = srcIndex > dstIndex ? dropTarget : dropTarget.nextElementSibling;
        dragSource.parentElement.insertBefore(dragSource, refChild);
        // focus and view on the tile that was dragged
        dragSource.focus();
        // invalidate Wijmo controls after layout updates
        wijmo.Control.invalidateAll();
        refreshAllControls();
    }
}
function getIndex(e) {
    var p = e.parentElement;
    for (var i = 0; i < p.children.length; i++) {
        if (p.children[i] == e)
            return i;
    }
    return -1;
}
;
// print document
function printDocument() {
    headerMenuShowHide('imgIconMenu', 'headerMenuList', 'close');
    window.print();
}
;
function scrollMenuShowHide(mode) {
    var imgScrollMenuShowHide = document.getElementById('imgScrollMenuShowHide');
    var scrollmenu = document.getElementById('scrollmenu');
    if (imgScrollMenuShowHide.title == 'Show menu' && mode != 'close') {
        imgScrollMenuShowHide.setAttribute('src', window.location.href + '/images/icon_menuclose.png');
        imgScrollMenuShowHide.setAttribute('title', 'Hide menu');
        scrollmenu.setAttribute('class', 'show');
    }
    else {
        imgScrollMenuShowHide.setAttribute('src', window.location.href + '/images/icon_menuopen.png');
        imgScrollMenuShowHide.setAttribute('title', 'Show menu');
        scrollmenu.setAttribute('class', 'hide');
    }
}
;
function headerMenuShowHide(menuLinkID, menuListID, mode) {
    var menuLink = document.getElementById(menuLinkID);
    var menuList = document.getElementById(menuListID);
    if (mode != 'close' && menuList.className == 'headerMenuListHide') {
        menuList.setAttribute('class', 'headerMenuListShow');
        menuLink.title = 'Close menu';
    }
    else {
        menuList.setAttribute('class', 'headerMenuListHide');
        menuLink.title = 'Open menu';
    }
}
;
function refreshAllControls() {
    if (costBudgetingChart)
        costBudgetingChart.refresh();
    if (costBudgetingFGrid)
        costBudgetingFGrid.refresh();
    if (monthlySalesChart)
        monthlySalesChart.refresh();
    if (monthlySalesFGrid)
        monthlySalesFGrid.refresh();
    if (countrywiseSalesChart)
        countrywiseSalesChart.refresh();
    if (countrywiseSalesFGrid)
        countrywiseSalesFGrid.refresh();
    if (monthlyProfitChart)
        monthlyProfitChart.refresh();
    if (monthlyProfitFGrid)
        monthlyProfitFGrid.refresh();
}
;
function chart_exportToImage(chartID) {
    var temp = '#' + chartID;
    var chartToExport = wijmo.Control.getControl(temp);
    chartToExport.saveImageToFile(chartID + '.png');
}
;
function removeFromWidgetList(widgetID) {
    if (settingsObj && widgetID) {
        var index = settingsObj.widgetsToHide.indexOf(widgetID);
        if (index > -1) {
            settingsObj.widgetsToHide.splice(index, 1);
        }
    }
}
;
function addToWidgetList(widgetID) {
    if (settingsObj && widgetID) {
        var index = settingsObj.widgetsToHide.indexOf(widgetID);
        if (index == -1) {
            settingsObj.widgetsToHide.push(widgetID);
        }
    }
}
;
function getSettings() {
    var retVal;
    var ls = localStorage;
    var settings = ls.getItem('widgetSettings');
    if (settings == 'undefined' || settings == null) {
        settings = {
            costBudgetChartType: costBudgetingChartTypeCmb.selectedValue,
            monthlySalesChartType: monthlySalesChartTypeCmb.selectedValue,
            monthlySalesChartTrendLine: monthlySalesChartTrendLineCmb.selectedValue,
            countrywiseSalesChartType: countrywiseSalesChartTypeCmb.selectedValue,
            monthlyProfitChartType: monthlyProfitChartTypeCmb.selectedValue,
            widgetsToHide: ["costBudgetingChartWidget", "countrywiseSalesChartWidget"]
        };
        retVal = settings;
    }
    else
        retVal = JSON.parse(settings);
    return retVal;
}
;
function applyAllSettings() {
    if (settingsObj) {
        // costBudgetChart
        resetChartType(costBudgetingChart, settingsObj.costBudgetChartType);
        costBudgetingChartTypeCmb.selectedValue = settingsObj.costBudgetChartType;
        // monthlySalesChart
        resetChartType(monthlySalesChart, settingsObj.monthlySalesChartType);
        monthlySalesChartTypeCmb.selectedValue = settingsObj.monthlySalesChartType;
        resetChartTrendLineType(monthlySalesChart, settingsObj.monthlySalesChartTrendLine);
        monthlySalesChartTrendLineCmb.selectedValue = settingsObj.monthlySalesChartTrendLine;
        // countrywiseSalesChart
        resetChartType(countrywiseSalesChart, settingsObj.countrywiseSalesChartType);
        countrywiseSalesChartTypeCmb.selectedValue = settingsObj.countrywiseSalesChartType;
        // monthlyProfitChart
        resetChartType(monthlyProfitChart, settingsObj.monthlyProfitChartType);
        monthlyProfitChartTypeCmb.selectedValue = settingsObj.monthlyProfitChartType;
        var widgetsToHide = settingsObj.widgetsToHide;
        for (var i = 0; i < widgetsToHide.length; i++) {
            closeWidget(widgetsToHide[i]);
        }
    }
}
;
function saveSettings() {
    headerMenuShowHide('imgIconMenu', 'headerMenuList', 'close');
    if (settingsObj) {
        var ls = localStorage;
        ls.setItem('widgetSettings', JSON.stringify(settingsObj));
    }
}
;
function resetChartType(chart, chartType) {
    if (chart && chartType) {
        chart.chartType = chartType;
    }
}
;
function resetChartTrendLineType(chart, trendLineType) {
    if (chart && trendLineType) {
        chart.beginUpdate();
        chart.series[2].fitType = trendLineType;
        chart.endUpdate();
    }
}
;
function costBudgetingFGrid_cellEditEnded(sender) {
    if (sender != null) {
        costBudgetingChart.itemsSource = costBudgetingFGrid.itemsSource;
        costBudgetingChart.refresh();
    }
}
;
function costBudgetingChartTypeCmb_selectedIndexChanged(sender) {
    if (sender && sender.selectedValue && settingsObj && costBudgetingChart) {
        resetChartType(costBudgetingChart, sender.selectedValue);
        settingsObj.costBudgetChartType = sender.selectedValue;
        saveSettings();
    }
}
;
function monthlySalesFGrid_cellEditEnded(sender) {
    if (sender != null) {
        monthlySalesChart.itemsSource = monthlySalesFGrid.itemsSource;
        monthlySalesChart.refresh();
    }
}
;
function monthlySalesChartTypeCmb_selectedIndexChanged(sender) {
    if (sender && sender.selectedValue && settingsObj && monthlySalesChart) {
        resetChartType(monthlySalesChart, sender.selectedValue);
        settingsObj.monthlySalesChartType = sender.selectedValue;
    }
}
;
function monthlySalesChartTrendLineCmb_selectedIndexChanged(sender) {
    if (sender && sender.selectedValue && settingsObj && monthlySalesChart) {
        resetChartTrendLineType(monthlySalesChart, sender.selectedValue);
        settingsObj.monthlySalesChartTrendLine = sender.selectedValue;
    }
}
;
function countrywiseSalesFGrid_cellEditEnded(sender) {
    if (sender != null) {
        countrywiseSalesChart.itemsSource = countrywiseSalesFGrid.itemsSource;
        countrywiseSalesChart.refresh();
    }
}
;
function countrywiseSalesChartTypeCmb_selectedIndexChanged(sender) {
    if (sender && sender.selectedValue && settingsObj && countrywiseSalesChart) {
        resetChartType(countrywiseSalesChart, sender.selectedValue);
        settingsObj.countrywiseSalesChartType = sender.selectedValue;
    }
}
;
function monthlyProfitFGrid_cellEditEnded(sender) {
    if (sender != null) {
        monthlyProfitChart.itemsSource = monthlyProfitFGrid.itemsSource;
        monthlyProfitChart.refresh();
    }
}
;
function monthlyProfitChartTypeCmb_selectedIndexChanged(sender) {
    if (sender && sender.selectedValue && settingsObj && monthlyProfitChart) {
        resetChartType(monthlyProfitChart, sender.selectedValue);
        settingsObj.monthlyProfitChartType = sender.selectedValue;
    }
}
;
function salesDashboardFGrid_selectionChanged(sender) {
    if (sender.selectedItems != null && sender.selectedItems[0].Country != null) {
        // reset KPIs Gauges
        var KPIsData = sender.selectedItems[0].KPIsData;
        kPIsSalesGauge.max = KPIsData.Max;
        kPIsSalesGauge.value = KPIsData.Sales;
        kPIsSalesGaugeValue.innerText = 'Sales: ' + wijmo.format('{Sales:c0}', KPIsData);
        kPIsExpensesGauge.max = KPIsData.Max;
        kPIsExpensesGauge.value = KPIsData.Expenses;
        kPIsExpensesGaugeValue.innerText = 'Expenses: ' + wijmo.format('{Expenses:c0}', KPIsData);
        kPIsProfitGauge.max = KPIsData.Sales;
        kPIsProfitGauge.value = KPIsData.Profit;
        kPIsProfitGaugeValue.innerText = 'Profit: ' + wijmo.format('{Profit:c0}', KPIsData);
        // reset monthlyProfitChart
        monthlyProfitChartFilterText = sender.selectedItems[0].Country;
        monthlyProfitChartCV.refresh();
        monthlyProfitFGridCV.refresh();
        // reset monthlySalesChart
        monthlySalesChartFilterText = sender.selectedItems[0].Country;
        monthlySalesChartCV.refresh();
        monthlySalesFGridCV.refresh();
    }
}
;
function documentReadyLoad() {
    'use strict';
    InstantiateCarousel();
    imgScrollMenuShowHide = document.getElementById('imgScrollMenuShowHide');
    scrollmenu = document.getElementById('scrollmenu');
    salesDashboardFGrid = wijmo.Control.getControl("#salesDashboardFGrid");
    costBudgetingChart = wijmo.Control.getControl("#costBudgetingChart");
    costBudgetingChartTypeCmb = wijmo.Control.getControl('#costBudgetingChartTypeCmb');
    costBudgetingFGrid = wijmo.Control.getControl("#costBudgetingFGrid");
    monthlySalesChart = wijmo.Control.getControl("#monthlySalesChart");
    monthlySalesChartTypeCmb = wijmo.Control.getControl('#monthlySalesChartTypeCmb');
    monthlySalesChartTrendLineCmb = wijmo.Control.getControl('#monthlySalesChartTrendLineCmb');
    monthlySalesFGrid = wijmo.Control.getControl("#monthlySalesFGrid");
    countrywiseSalesChart = wijmo.Control.getControl("#countrywiseSalesChart");
    countrywiseSalesChartTypeCmb = wijmo.Control.getControl('#countrywiseSalesChartTypeCmb');
    countrywiseSalesFGrid = wijmo.Control.getControl("#countrywiseSalesFGrid");
    monthlyProfitChart = wijmo.Control.getControl("#monthlyProfitChart");
    monthlyProfitChartTypeCmb = wijmo.Control.getControl('#monthlyProfitChartTypeCmb');
    monthlyProfitFGrid = wijmo.Control.getControl("#monthlyProfitFGrid");
    kPIsSalesGauge = wijmo.Control.getControl("#kPIsSalesGauge");
    kPIsExpensesGauge = wijmo.Control.getControl("#kPIsExpensesGauge");
    kPIsProfitGauge = wijmo.Control.getControl("#kPIsProfitGauge");
    kPIsSalesGaugeValue = document.getElementById('kPIsSalesGaugeValue');
    kPIsExpensesGaugeValue = document.getElementById('kPIsExpensesGaugeValue');
    kPIsProfitGaugeValue = document.getElementById('kPIsProfitGaugeValue');
    salesDashboardFGridCV = salesDashboardFGrid.collectionView;
    // Monthly Profit Widget
    monthlyProfitChartCV = monthlyProfitChart.collectionView;
    monthlyProfitChartCV.filter = function (item) {
        return !monthlyProfitChartFilterText || item.Country.indexOf(monthlyProfitChartFilterText) > -1;
    };
    monthlyProfitFGridCV = monthlyProfitFGrid.collectionView;
    monthlyProfitFGridCV.filter = function (item) {
        return monthlyProfitChartFilterText == 'All' || !monthlyProfitChartFilterText || item.Country.indexOf(monthlyProfitChartFilterText) > -1;
    };
    monthlyProfitChartFilterText = 'All';
    monthlyProfitChartCV.refresh();
    monthlyProfitFGridCV.refresh();
    // Quarter Sales Widget
    monthlySalesChartCV = monthlySalesChart.collectionView;
    monthlySalesChartCV.filter = function (item) {
        return !monthlySalesChartFilterText || item.Country.indexOf(monthlySalesChartFilterText) > -1;
    };
    monthlySalesFGridCV = monthlySalesFGrid.collectionView;
    monthlySalesFGridCV.filter = function (item) {
        return monthlySalesChartFilterText == 'All' || !monthlySalesChartFilterText || item.Country.indexOf(monthlySalesChartFilterText) > -1;
    };
    monthlySalesChartFilterText = 'All';
    monthlySalesChartCV.refresh();
    monthlySalesFGridCV.refresh();
    salesDashboardFGrid_selectionChanged(salesDashboardFGrid);
    settingsObj = getSettings();
    applyAllSettings();
    setDragDropTouch(wijmo);
    setTabs();
    // enable drag-drop within dashboard element
    dashboard = document.querySelector('.dashboard');
    dashboard.addEventListener('dragstart', handleDragStart);
    dashboard.addEventListener('dragover', handleDragOver);
    dashboard.addEventListener('drop', handleDrop);
    dashboard.addEventListener('dragend', handleDragEnd);
}
;
