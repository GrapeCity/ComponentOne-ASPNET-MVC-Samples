var dashboard,
    wsSplitLayout,
    nsSplitLayout,

    chartOpportunities,
    chartRegionSales,

    pivotEngine;

window.addEventListener('resize', relayout, false);

c1.documentReady(function () {
    init();
    relayout();
});

function init() {
    dashboard = wijmo.Control.getControl('#dashboard');
    wsSplitLayout = c1.getService('wsSplitLayout');
    nsSplitLayout = c1.getService('nsSplitLayout');

    chartOpportunities = wijmo.Control.getControl('#chartOpportunities');
    chartRegionSales = wijmo.Control.getControl('#chartRegionSale');

    pivotEngine = c1.getService('indexEngine');
}

function relayout() {
    var eleIds = ['pivotGrid', 'chartRegionSale', 'chartOpportunities'];
    for (var i = 0; i < eleIds.length; i++) {
        var domEle = document.getElementById(eleIds[i]);
        if (domEle) {
            domEle.removeAttribute('style');
        }
    }

    if (!dashboard) {
        return;
    }

    var layout = document.body.clientWidth < 1000 ? nsSplitLayout : wsSplitLayout;

    if (dashboard.layout === layout) {
        dashboard.refresh();
    } else {
        dashboard.layout = layout;
    }
}

function updateData(start, end, url) {
    refreshData(start, end, url, function (data) {
        chartOpportunities.itemsSource = data[1].items;
        chartRegionSales.itemsSource = data[0].items;
    });
}

//function minSale(list) {
//    var min = Number.MAX_VALUE;
//    list.forEach(function (v) {
//        if (min > v.Sales) {
//            min = v.Sales;
//        }
//    })
//    return min;
//}

function tileContentRefreshing(sender, e) {
    var eleCtl = sender.hostElement,
        eleTile = eleCtl.parentElement.parentElement,
        height = eleTile.getBoundingClientRect().height - 15;

    if (sender instanceof wijmo.chart.FlexChartBase) {
        height -= 12;
    } else if (sender instanceof wijmo.grid.FlexGrid) {
        height -= 5;
    }

    eleCtl.style.height = height + 'px';
}

// Update the template for PivotPanel so that the cards and the fields are horizontally align.
wijmo.olap.PivotPanel.controlTemplate =
    '<div class="flex horizontal auto" style="width:100%;height:100%">' +
        // fields
        '<div style="width:40%">' +
            '<label wj-part="g-flds"></label>' +
            '<div wj-part="d-fields"></div>' +
        '</div>' +

        // drag/drop area
        '<div style="width:60%" class="flex vertical">' +

            '<label wj-part="g-drag"></label>' +
            '<table class="viewdefinition">' +
                '<tr>' +
                    '<td width="50%">' +
                    '<label><span class="wj-glyph wj-glyph-filter"></span> <span wj-part="g-flt"></span></label>' +
                    '<div wj-part="d-filters"></div>' +
                    '</td>' +
                    '<td width= "50%" style= "border-left-style:solid">' +
                    '<label><span class="wj-glyph">&#10996;</span> <span wj-part="g-cols"></span></label>' +
                    '<div wj-part="d-cols"></div>' +
                    '</td>' +
                '</tr>' +
                '<tr>' +
                    '<td width="50%">' +
                    '<label><span class="wj-glyph">&#8801;</span> <span wj-part="g-rows"></span></label>' +
                    '<div wj-part="d-rows"></div>' +
                    '</td>' +
                    '<td width= "50%" style= "border-left-style:solid">' +
                    '<label><span class="wj-glyph">&#931;</span> <span wj-part="g-vals"></span></label>' +
                    '<div wj-part="d-vals"></div>' +
                    '</td>' +
                '</tr>' +
            '</table>' +
        '</div>' +

        '<div>' +
            // progress indicator
            '<div wj-part="d-prog" class="wj-state-selected" style="width:0px;height:3px"></div>' +

            // update panel
            '<div style="display:table">' +
                '<label style="display:table-cell;vertical-align:middle">' +
                    '<input wj-part="chk-defer" type="checkbox"/> <span wj-part="g-defer"></span>' +
                '</label>' +
                '<button wj-part="btn-update" class="wj-btn wj-state-disabled" style="float:right;margin:6px" disabled></button>' +
            '</div>' +
        '</div>' +
    '</div>';

var pivotPanelShown = false;
var original_PivotEngine_FieldPropertyChanged = wijmo.olap.PivotEngine.prototype._fieldPropertyChanged;
wijmo.olap.PivotEngine.prototype._fieldPropertyChanged = function (field, e) {
    if (pivotPanelShown) {
        return;
    }

    original_PivotEngine_FieldPropertyChanged.call(this, field, e);
}

function popupShown(sender, e) {
    if (!pivotEngine) {
        return;
    }
    // suspend the changes of the view definition to apply them 
    // after clicking the "Apply" button
    localStorage['indexEngine'] = pivotEngine.viewDefinition;
    pivotEngine.beginUpdate();
    pivotPanelShown = true;
}

function apply() {
    if (!pivotEngine) {
        return;
    }

    pivotEngine.endUpdate();
    pivotPanelShown = false;
}

function cancel() {
    if (!pivotEngine) {
        return;
    }

    pivotEngine.viewDefinition = localStorage['indexEngine'];
    pivotEngine.endUpdate();
    pivotPanelShown = false;
}

function pivotGridLoadedRows(sender, e) {
    var grid = sender,
        columnCount = grid.columns.length;

    if (columnCount) {
        var column = grid.columns[columnCount - 1];
        column.width = '*';
    }
}