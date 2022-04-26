var DEFAULT_UNIT = 1000000;//1M
var BAD_UNIT = 0.0000005;
var GOOD_UNIT = 0.0000008;
var DEFAULT_THICKNESS = 0.5;
var COMPLETE_LEVEL = 0.8;
var AXIS_UNIT = 20;

var top3SellingProducts,
    top7SaleCustomers,
    categorySalesVsGoal,
    regionSalesVsGoal,

    dashboard,
    wsSplitLayout,

    tooltip,
    eleSalesVSGoalGauges,
    eleRegionalSalesVsGoalGauges;

c1.documentReady(function () {
    init();
    window.addEventListener('resize', relayout, false);
    relayout();
});

function init() {
    c1.nav.LayoutItem._MIN_SIZE = 0;
    categorySalesVsGoal = c1.getService('categorySalesVsGoal');
    regionSalesVsGoal = c1.getService('regionSalesVsGoal');
    top3SellingProducts = c1.getService('top3SellingProducts');
    top7SaleCustomers = c1.getService('top7SaleCustomers');

    dashboard = wijmo.Control.getControl('#dashboard');
    wsSplitLayout = c1.getService('wsSplitLayout');

    eleSalesVSGoalGauges = document.body.querySelector('.salesVSGoal .gauges');
    eleRegionalSalesVsGoalGauges = document.body.querySelector('.regionalSalesVsGoal .gauges');

    tooltip = new wijmo.Tooltip();
    tooltip.showAtMouse = true;

    //draw guages
    createGauges(eleSalesVSGoalGauges, categorySalesVsGoal.items);
    createGauges(eleRegionalSalesVsGoalGauges, regionSalesVsGoal.items);
}

function adjustWideGridLayout() {
    if (isLayoutChanged()) {
        return;
    }

    var eleDashboard = dashboard.hostElement,
        eleParent = eleDashboard.parentElement;
    eleParent.style.padding = '0px';

    var dashboardRect = eleDashboard.getBoundingClientRect(),
       width = Math.floor(dashboardRect.width),
       height = Math.floor(dashboardRect.height);

    var maxIndex = Math.floor(height / 42);
    var minDelta = -1;
    var rowCount = -1;
    for (var i = 1; i <= maxIndex; i++) {
        var tCellSize = Math.floor(height / (6 * i));
        var delta = height % (6 * i);
        var tColCount = Math.floor(width / (10 * tCellSize));
        delta += width % (10 * tCellSize);
        if (minDelta == -1) {
            minDelta = delta;
        } else if (minDelta > delta) {
            minDelta = delta;
            rowCount = i;
        }
    }

    if (rowCount == -1) {
        return;
    }

    var cellSize = Math.floor(height / (6 * rowCount));
    var tbSpace = height % (6 * rowCount);
    var colCount = Math.floor(width / (10 * cellSize));
    var lrSpace = width % (10 * cellSize);

    if (lrSpace) {
        var leftPadding = Math.floor(lrSpace / 2);
        eleParent.style.paddingLeft = leftPadding + 'px';
        eleParent.style.paddingRight = (lrSpace - leftPadding) + 'px';
    }

    if (tbSpace) {
        var topPadding = Math.floor(tbSpace / 2);
        eleParent.style.paddingTop = topPadding + 'px';
        eleParent.style.paddingBottom = (tbSpace - topPadding) + 'px';
    }

    wsSplitLayout.cellSize = cellSize - 6;
    wsSplitLayout.maxRowsOrColumns = 10 * colCount;
    var group = wsSplitLayout.items[0];
    var count = group.children.length;

    if (count != 6) {
        return;
    }

    group.children[0].rowSpan = 3 * rowCount;
    group.children[0].columnSpan = 6 * colCount;

    group.children[1].rowSpan = 2 * rowCount;
    group.children[1].columnSpan = 4 * colCount;

    group.children[2].rowSpan = 2 * rowCount;
    group.children[2].columnSpan = 4 * colCount;

    group.children[3].rowSpan = 3 * rowCount;
    group.children[3].columnSpan = 3 * colCount;

    group.children[4].rowSpan = 3 * rowCount;
    group.children[4].columnSpan = 3 * colCount;

    group.children[5].rowSpan = 2 * rowCount;
    group.children[5].columnSpan = 4 * colCount;
}

function getScrollBarSize() {
    var div = document.createElement('div');
    div.style.overflowY = 'auto';
    div.style.width = '100px';
    div.style.height = '100px';
    div.innerHTML = '<div style="width:200px;height:200px"></div>'
    document.body.appendChild(div);
    var scrollbarSize = 100 - div.clientWidth;
    document.body.removeChild(div);
    return scrollbarSize;
}

function adjustNarrowGridLayout() {
    var eleDashboard = dashboard.hostElement,
        eleParent = eleDashboard.parentElement;
    eleParent.style.padding = '0px';

    var dashboardRect = eleDashboard.getBoundingClientRect(),
        width = dashboardRect.width,
        height = dashboardRect.height;

    if (height < 300 * 6) {
        width -= getScrollBarSize();
    }

    var colCount = Math.floor(width / 15);
    var rowCount = 300 / 15;
    var lrSpace = width % 15;



    var group = wsSplitLayout.items[0];
    var count = group.children.length;

    if (count == 0) {
        return;
    }

    if (lrSpace) {
        var leftPadding = Math.floor(lrSpace / 2);
        eleParent.style.paddingLeft = leftPadding + 'px';
        eleParent.style.paddingRight = (lrSpace - leftPadding) + 'px';
    }

    if (count >= 1) {
        group.children[0].rowSpan = rowCount;
        group.children[0].columnSpan = colCount;
    }

    if (count >= 2) {
        group.children[1].rowSpan = rowCount;
        group.children[1].columnSpan = colCount;
    }

    if (count >= 3) {
        group.children[2].rowSpan = rowCount;
        group.children[2].columnSpan = colCount;
    }

    if (count >= 4) {
        group.children[3].rowSpan = rowCount;
        group.children[3].columnSpan = colCount;
    }

    if (count >= 5) {
        group.children[4].rowSpan = rowCount;
        group.children[4].columnSpan = colCount;
    }

    if (count >= 6) {
        group.children[5].rowSpan = rowCount;
        group.children[5].columnSpan = colCount;
    }
    wsSplitLayout.cellSize = 9;
    wsSplitLayout.maxRowsOrColumns = colCount;
}



function relayout() {
    var eleIds = ['.budgetVSProfitChart', '.chartSaleTopProd', '.chartSalesVsProfit'];
    for (var i = 0; i < eleIds.length; i++) {
        var domEle = document.body.querySelector(eleIds[i]);
        if (domEle) {
            domEle.removeAttribute('style');
        }
    }

    if (!dashboard) {
        return;
    }

    var layout;
    if (document.body.clientWidth < 1000) {
        adjustNarrowGridLayout();
    } else {
        adjustWideGridLayout();
    }

    layout = wsSplitLayout;

    if (dashboard.layout === layout) {
        dashboard.refresh();
    } else {
        dashboard.layout = layout;
    }
}

// updates the data sources
function updateData(start, end, url) {
    refreshData(start, end, url, function (data) {
        top3SellingProducts.sourceCollection = data[0].items;
        top7SaleCustomers.sourceCollection = data[1].items;
        categorySalesVsGoal.sourceCollection = data[2].items;
        regionSalesVsGoal.sourceCollection = data[3].items;
    });
}

function createGauges(eleTarget, items) {
    // prepare the data for gauges.
    var data = [];
    var max = Number.MIN_VALUE;
    for (var i = 0; i < items.length; i++) {
        var item = items[i];

        var goal = item.Goal / DEFAULT_UNIT;
        var sale = item.Sales / DEFAULT_UNIT;
        var badGoal = item.Goal * BAD_UNIT;
        var goodGoal = item.Goal * GOOD_UNIT;
        max = Math.max(max, sale);

        data.push({
            'goal': goal,
            'sale': sale,
            'badGoal': badGoal,
            'goodGoal': goodGoal,
            'CompletePrecent': item.CompletePrecent,
            'Name': item.Name
        });
    }
    max = Math.floor(max / AXIS_UNIT) * AXIS_UNIT + AXIS_UNIT;

    // remove the tooltip
    var guageContainers = eleTarget.querySelectorAll('.gauge-container');
    for (var j = 0; j < guageContainers.length; j++) {
        tooltip.setTooltip(guageContainers[j], null);
    }

    // create or refresh the gauges.
    var gaugeElements = eleTarget.querySelectorAll('.wj-gauge');
    var count = gaugeElements.length;
    if (!count) {
        // create the gauges.
        for (var i = 0; i < data.length; i++) {
            var item = data[i];
            this.createGauge(eleTarget, item, max);
        }
    } else {
        var j;
        for (j = 0; j < count ; j++) {
            var gauge = wijmo.Control.getControl(gaugeElements[j]);
            var container = wijmo.closest(gauge.hostElement, '.gauge-container');
            if (j < data.length) {
                var item = data[j];
                if (gauge != null) {
                    var oldValue = wijmo.Control._ANIM_DEF_DURATION;
                    wijmo.Control._ANIM_DEF_DURATION = 2000;
                    gauge.initialize({
                        value: item.sale,
                        bad: item.badGoal,
                        good: item.goodGoal,
                        target: item.goal,
                    });
                    wijmo.Control._ANIM_DEF_DURATION = oldValue;
                }
                if (item.CompletePrecent < COMPLETE_LEVEL) {
                    gauge.pointer.color = '#F44336';
                }
                tooltip.setTooltip(container, wijmo.Globalize.format(item.sale, 'C0') + ' (M)');
                var title = container.querySelector('.gauge-title');
                if (title) {
                    title.innerHTML = item.Name;
                }
            } else {
                gauge.dispose();
                wijmo.removeChild(container);
            }
        }

        for (; j < data.length; j++) {
            this.createGauge(eleTarget, data[j], max);
        }
    }

    wijmo.setAttribute(eleTarget, 'max', max);
    createGaugeAxes(eleTarget);
}

function createGauge(eleTarget, item, max) {
    var container = document.createElement('div');
    wijmo.addClass(container, 'gauge-container');
    tooltip.setTooltip(container, wijmo.Globalize.format(item.sale, 'C0') + ' (M)');
    eleTarget.appendChild(container);

    // Create the bullet-graph
    var bullet = document.createElement('div');
    wijmo.addClass(bullet, 'bullet-graph');
    container.appendChild(bullet);
    var bulletGraph = new wijmo.gauge.BulletGraph(bullet, {
        min: 0,
        max: max,
        value: item.sale,
        bad: item.badGoal,
        good: item.goodGoal,
        target: item.goal,
        thickness: 0.7,
        refreshing: gaugeRefreshing,
        isAnimated: true,
        hasShadow: true
    });
    bulletGraph.pointer.thickness = DEFAULT_THICKNESS;
    bulletGraph.face.thickness = 0;
    if (item.CompletePrecent < COMPLETE_LEVEL) {
        bulletGraph.pointer.color = '#F44336';
    }

    var title = document.createElement('div');
    wijmo.addClass(title, 'gauge-title');
    title.innerHTML = item.Name;
    container.appendChild(title);
    return container;
}

function createGaugeAxes(eleTarget) {
    if (eleTarget.children.length == 0) {
        return;
    }
    var count = parseFloat(eleTarget.getAttribute('max')) / AXIS_UNIT;
    var axisLength = eleTarget.clientWidth - 100;// 100 padding for title;
    var startLocation = 100;
    var step = axisLength / count;
    var axisContainer = eleTarget.querySelector('.gauge-axis-container');
    if (!axisContainer) {
        axisContainer = document.createElement('div');
        wijmo.addClass(axisContainer, 'gauge-axis-container');
    } else {
        axisContainer.innerHTML = '';
    }
    eleTarget.appendChild(axisContainer);

    var lastGaugeEle = axisContainer.previousElementSibling;
    if (!lastGaugeEle) {
        return;
    }

    var bottom = lastGaugeEle.offsetHeight;
    var axisContent = '';
    for (var i = 0; i <= count; i++) {
        axisContent += '<div class="gauge-axis-marker" style="';
        axisContent += 'left:' + (startLocation + i * step) + 'px;'
        axisContent += 'bottom:' + (bottom - 5) + 'px;"';
        axisContent += '></div>';

        axisContent += '<span class="gauge-axis-title" style="';
        axisContent += 'left:' + (startLocation + i * step - 5) + 'px;'
        axisContent += 'bottom:' + (bottom - 25) + 'px;"';
        axisContent += '>' + i * AXIS_UNIT + '</span>';
    }
    axisContainer.innerHTML = axisContent;
}

function gaugeRefreshing(sender, e) {
    var eleGauge = sender.hostElement,
        eleGaugeContainer = wijmo.closest(eleGauge, '.gauge-container'),
        eleTileGauge = wijmo.closest(eleGaugeContainer, '.gaugesChart'),
        eleTile = wijmo.closest(eleTileGauge, '.wj-tile-content'),
        eleTitle = eleGaugeContainer.querySelector('.gauge-title');

    eleGauge.style.width = (eleGaugeContainer.parentNode.offsetWidth - 100) + 'px';
    var gaugesTotalHeight = eleTile.offsetHeight;
    eleTileGauge.style.height = gaugesTotalHeight + 'px';

    var gauges = eleTileGauge.querySelectorAll('.gauge-container');
    var gaugeHeight = gaugesTotalHeight / (gauges.length + 1);
    eleGaugeContainer.style.height = gaugeHeight + 'px';
    eleGauge.style.height = gaugeHeight + 'px';
    eleTitle.style.top = gaugeHeight / 4 + 'px';
    createGaugeAxes(eleGaugeContainer.parentNode);
}


function tooltipTemplate(ht) {
    return '<b>' + ht.name + '</b><br/>' + wijmo.Globalize.format(ht.x, 'MMM') + ' ' + wijmo.Globalize.format(ht.y, 'C0');
}

function categorySalesVsGoalChanged(sender, e) {
    createGauges(eleSalesVSGoalGauges, sender.items);
}

function regionSalesVsGoalChanged(sender, e) {
    createGauges(eleRegionalSalesVsGoalGauges, sender.items);
}

function tileContentRefreshing(sender, e) {
    var eleCtl = sender.hostElement,
        tileEle = eleCtl.parentElement.parentElement,
        height = tileEle.clientHeight - 15;
    if (sender instanceof wijmo.chart.FlexChartBase) {
        height -= 12;
    } else if (sender instanceof wijmo.grid.FlexGrid) {
        height -= 5;
    }
    eleCtl.style.height = height + 'px';
}

function chartSaleTopProdRefreshing(sender, e) {
    tileContentRefreshing(sender, e);
    var width = sender.hostElement.clientWidth;
    var angle = null;
    if (width < 250) {
        angle = 90;
    } else if (width < 580) {
        angle = 45;
    }
    sender.axisX.labelAngle = angle;
}