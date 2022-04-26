// Write your JavaScript code.
var grid, loadingImg, spinner, currentItem, isDocumentLoaded = true,
    breadCrumbs = [], isSelected, isTreemapClicked = false,
    series = new wijmo.chart.research.AggregateSeries(),
    selSeries, expandedRows = [1], barColors = [],
    basePath,
    treemap, flexChart, rngSel, combobox, rangeChart, originalPieBinding, originalPieBindingName,
    gridCV, originalPieCV, pieCV, chartCV, cityCV, airportsMonthlyDataCV, airportGroupedDataCV;

const monthNames = ["January", "February", "March", "April", "May", "June",
    "July", "August", "September", "October", "November", "December"];

const minMaxDiff = 3456000000;

var opts = {
    lines: 13, // The number of lines to draw
    length: 40, // The length of each line
    width: 17, // The line thickness
    radius: 45, // The radius of the inner circle
    scale: 1, // Scales overall size of the spinner
    corners: 1, // Corner roundness (0..1)
    color: '#000', // CSS color or array of colors
    fadeColor: 'transparent', // CSS color or array of colors
    speed: 1, // Rounds per second
    rotate: 0, // The rotation offset
    animation: 'spinner-line-fade-quick', // The CSS animation name for the lines
    direction: 1, // 1: clockwise, -1: counterclockwise
    zIndex: 2e9, // The z-index (defaults to 2000000000)
    className: 'spinner', // The CSS class to assign to the spinner
    top: '50%', // Top position relative to parent
    left: '50%', // Left position relative to parent
    shadow: '0 0 2px transparent', // Box-shadow for the lines
    position: 'absolute' // Element positioning
};

c1.documentReady(function () {
    
    airportsMonthlyDataCV = new wijmo.collections.CollectionView(airportData);
    airportGroupedDataCV = new wijmo.collections.CollectionView(airportGroupedData);

    flexChart = wijmo.Control.getControl("#airportsFlexChart");
    treemap = wijmo.Control.getControl("#airportsTreeMap");
    regionsChart = wijmo.Control.getControl("#regionsFlexChart");    
    rangeChart = wijmo.Control.getControl("#rangeSelectChart");    
    
    for (var i = 0; i < airportsMonthlyDataCV.items.length; i++) {                
        airportsMonthlyDataCV.items[i].airportCode = airportsMonthlyDataCV.items[i].airportCode.trim();
    }

    for (var j = 0; j < airportGroupedDataCV.items.length; j++) {
        airportGroupedDataCV.items[j].recordedDate = new Date(airportGroupedDataCV.items[j].recordedDate);
    }

    airportsMonthlyDataCV.filter = monthlyDataFilter;
    initFlexChart();

    grid.updatedLayout.addHandler(function (s, e) {        
        if (spinner)
            spinner.stop();
    });

    grid.columnHeaders.rows.defaultSize = 30;
    grid.rows.defaultSize = 36;    
});

function initTabs() {
    var tabCon = document.getElementById('tabList');
    var tabs = tabCon.getElementsByTagName('a');

    for (i = 0; i < tabs.length; i++) {
        addEventListenerForTab(tabs, i);
    }
}

function addEventListenerForTab(tabs, i) {
    tabs[i].addEventListener('click', function (event) {
        if (tabs[i].id === 'gridTab') {
            
            loadingImg.style.display = "block";
        }
        else {
            loadingImg.style.display = "none";
            spinner.stop();
        }
    });
}

function axisXitemFormatter(engine, label) {
    engine.textFill = 'transparent';
}

function initFlexChart() {
    
    var series2 = new wijmo.chart.research.AggregateSeries();

    flexChart.beginUpdate();
    rangeChart.beginUpdate();
    
    var gd = airportsMonthlyDataCV.groupDescriptions;
    gd.clear();
    
    var ungroupedCV = new wijmo.collections.CollectionView(airportsMonthlyDataCV.items);

    var groupDesc2 = new wijmo.collections.PropertyGroupDescription("region");
    gd.push(groupDesc2);

    var groupedCV = airportsMonthlyDataCV.groups;
    var groupDesc, newGroupedCV;

    if (groupedCV && groupedCV.length > 0) {
        
        for (var g = 0; g < groupedCV.length; g++) {
            series = new wijmo.chart.research.AggregateSeries();

            newGroupedCV = new wijmo.collections.CollectionView(groupedCV[g].items);

            var sortDesc = new wijmo.collections.SortDescription("recordedDate");
            newGroupedCV.sortDescriptions.push(sortDesc);
            
            series.itemsSource = newGroupedCV;
            series.binding = "delay";
            series.name = groupedCV[g].name;
            series.bindingX = "recordedDate";
            series.groupAggregate = wijmo.Aggregate['Avg'];
            series.autoGroupIntervals = ["MM", "YYYY"];
            series.autoInterval = false;
            series.groupInterval = "MM";
            flexChart.series.push(series);
        }       
        var sortDesc2 = new wijmo.collections.SortDescription("recordedDate");
        ungroupedCV.sortDescriptions.push(sortDesc2);
        
        series2.itemsSource = ungroupedCV;
        series2.binding = "delay";
        series2.name = "Series";
        series2.bindingX = "recordedDate";
        series2.groupAggregate = wijmo.Aggregate['Avg'];
        series2.autoGroupIntervals = ["MM", "YYYY"];
        series2.autoInterval = false;
        series2.groupInterval = "MM";

        rangeChart.series.push(series2);

        flexChart.tooltip.content = function (ht) {
            if (!ht) {
                return '';
            } else if (ht.x && ht.y) {                
                return '<b>Region:</b> ' + ht.name + '<br>' +
                    '<b>Average Delay:</b> ' + ht.y.toFixed(2);
            }
        };        

        flexChart.endUpdate();
        rangeChart.endUpdate();
    }
}

function initRangeChart(ungroupedCV) {

    var groups, groupItems = [], rangeCV,
        sereiesName = "groupedSeries";

    rangeChart.tooltip.content = '';

    var gd1 = ungroupedCV.groupDescriptions;
    gd1.clear();

    var groupDesc3 = new wijmo.collections.PropertyGroupDescription("recordedDate");
    gd1.push(groupDesc3);

    groups = ungroupedCV.groups;

    if (groups && groups.length > 0) {
        //for year to string: sort the group item(year)
        groups.sort(function (a, b) {
                
            if (new Date(a.name) > new Date(b.name)) {
                return 1;
            } else {
                return -1;
            }
        });

        groups.forEach(function (group, idx) {
            var groupname = group.groupDescription.propertyName,
                groupitem = {};            
            groupitem[groupname] = new Date(group.name);
            groupitem["delay"] = group.items[0].delay;
            groupItems.push(groupitem);
        });

        selSeries = new wijmo.chart.Series();

        selSeries.chartType = wijmo.chart.ChartType.Area;
        selSeries.binding = "delay";       
        rangeChart.itemsSource = groupItems;
        rangeChart.bindingX = "recordedDate";
        rangeChart.series.push(selSeries);        
        rangeChart.invalidate();
    }
}

function rangeChanged(args) {
    
    var rs = c1.getExtender(wijmo.Control.getControl("#rangeSelectChart"), "RangeSelector"),
        yRange;
    if (rs) {
        // update main chart's x range       
        flexChart.axisX.min = rs.min;
        flexChart.axisX.max = rs.max;
        flexChart.invalidate();
    }
}

function loadedRows(s, e) {
    grid = wijmo.Control.getControl("#airportsGrid");
    console.log(window.location);   
    gridCV = grid.collectionView;    
        
    pieCV = new wijmo.collections.CollectionView(gridCV.items);    
    groupRowCount = 0;
    for (var r = 0; r < grid.rows.length; r++) {
        if (grid.rows[r].hasChildren) {
            groupRowCount++;
        }
        if (groupRowCount === 2) {
            expandedRows.push(r);
            break;
        }
    }
    CollapseGroups();
}

function sortingColumn(s, e) {
    var groupRowCount = 0;
    expandedRows = [];
    
    for (var r = 0; r < grid.rows.length; r++) {
        if (grid.rows[r].hasChildren) {
            if (!(grid.rows[r].isCollapsed)) {
                expandedRows.push(r);                
            }
            groupRowCount++;
        }
    }

    e.cancel = true;
    var sortCol,
        direction = true;

    if (gridCV.sortDescriptions.length > 1) {
        sortCol = gridCV.sortDescriptions[1].property;
        direction = gridCV.sortDescriptions[1].ascending;
        gridCV.sortDescriptions.pop();
    }

    if (sortCol === grid.columns[e.col].binding)
        direction = !direction;
    else
        direction = true;

    gridCV.sortDescriptions.push(new wijmo.collections.SortDescription(grid.columns[e.col].binding, direction));
    grid.invalidate();
    CollapseGroups();
}

function CollapseGroups() {    
    for (var r = 0; r < grid.rows.length; r++) {
        if (grid.rows[r].hasChildren) {
            grid.rows[r].isCollapsed = expandedRows.indexOf(r) != -1 ? false : true;            
        }
    }
}

function itemFormatter(panel, r, c, cell) {
    var toolTip = new wijmo.Tooltip();

    if (panel.cellType === wijmo.grid.CellType.ColumnHeader) {        
        cell.onmousemove = function () {
            var cellBounds = panel.getCellBoundingRect(r, c);
            var cellText = cell.innerText;

            toolTip.showAtMouse = true;
            toolTip.gap = 5;
            if (cell.clientWidth != cell.scrollWidth)
                toolTip.show(cell, cellText, cellBounds);
        };

        cell.onmouseout = function () {
            toolTip.hide();
        };
    }
    else if (panel.cellType === wijmo.grid.CellType.Cell) {
        if (panel.columns[c].binding === "UserRating") {
            cell.style.textAlign = "";
            cell.innerHTML = buildRating(panel.getCellData(r, c));
        }
        else if (panel.columns[c].binding === "Delayed") {
            var cellData = panel.getCellData(r, c);
            cell.style.color = cellData > 0.2 ? 'red' : 'green';
        }
        else if (panel.columns[c].binding === "AverageDelay") {
            cellData = panel.getCellData(r, c);
            var src = "";

            if (cellData > 40) {
                src = "Triangle_Down.png";
            }
            else {
                src = "Triangle_Up.png";
            }                          
            var host = window.location.href;
            var vv = basePath;
            cell.innerHTML = "<img src='" + host + "//images//" + src + "' alt='KPI' style='height:10px;width:10px' /> " + cell.innerHTML;
        }
    }
}

function flexChartItemFormatter(engine, hitTestInfo, defaultRenderer) {

    var ht = hitTestInfo;
    
    engine.element.style.cursor = "default";

    if (ht.chartElement === wijmo.chart.ChartElement.SeriesSymbol) {
        engine.element.style.cursor = "pointer";
        var bar;
        
        if (barColors.length > 0) {
            bar = barColors.filter(function (s) {
                return s.series === ht.series.name;
            });
        }
        
        if (bar === undefined || bar.length === 0) {
            barColors.push({
                series: ht.series.name,
                fill: engine.fill
            });
        }
    }
    defaultRenderer();
}

function regionsChartItemFormatter(engine, hitTestInfo, defaultRenderer) {
    var ht = hitTestInfo,
        binding = "OnTimeAvg";

    if (ht.series.binding === "avgDelay" && ht.chartElement === wijmo.chart.ChartElement.SeriesSymbol && barColors.length > 0) {
        engine.element.style.cursor = "pointer";
        engine.fill = currentItem.fill;
        engine.stroke = currentItem.stroke;
    }
    else if (ht.chartElement === wijmo.chart.ChartElement.SeriesSymbol && barColors.length > 0) {
        
        engine.element.style.cursor = "pointer";
        var item = barColors.filter(function (s) {
            return s.series === ht.item.Region;
        });
        engine.fill = item[0].fill;
        engine.stroke = item[0].fill;
    }
    else {
        engine.element.style.cursor = "default";
    }
    defaultRenderer();
}

function flexGridSelectionChanged(s, args) {
    
    if (!isTreemapClicked) {
        if (grid.rows[args.row].hasChildren) {
            
            var region = grid.rows[args.row].dataItem.name;
            setTimeout(function () {
                NavigateTreemap(region);
            }, 200);
        }
        else {
            var city = grid.rows[args.row].dataItem.AirportCity;
            setTimeout(function () {
                NavigateTreemap(city);
            }, 200);
        }
    }
    isTreemapClicked = false;
    //}
}

function flexChartSelectionChanged(args) {
    
    if (args.selection) {
        var series = args.selection.name;
        NavigateTreemap(series);
    }
}

function regionsChartSelectionChanged(args) {
    var ht = regionsChart.hitTest(event.pageX, event.pageY);
    
    if (currentItem !== null) {
        if (currentItem.depth === 1) {
            var label = ht.item.AirportCity;
            for (var r = 0; r < grid.rows.length; r++) {
                if (!(grid.rows[r].hasChildren) && grid.rows[r].dataItem.AirportCity === label && !isSelected) {
                    
                    grid.select(r, 0);
                    break;
                }
            }
            NavigateTreemap(label);
        }
    }
    else {
        if (ht.item) {
            var label = ht.item.Region;
            for (var r = 0; r < grid.rows.length; r++) {
                if (grid.rows[r].hasChildren && grid.rows[r].dataItem.name === label && !isSelected) {
                    
                    grid.select(r, 0);
                    expandedRows = [r];
                    CollapseGroups();                    
                    break;
                }
            }
            NavigateTreemap(label);
        }
    }
}

function NavigateTreemap(label) {
    if (treemap) {//ignore case treemap isn't render
        for (var i = 0; i < treemap._areas.length; i++) {
            if (treemap._areas[i].item.label === label) {
                treemap._currentItem = treemap._areas[i].item;
                treemap.invalidate();
                break;
            }
        }
    }
    isTreemapClicked = false;
}

//combobox selectionchanged
function onSelectedIndexChanged(args) {
    
    var year = args.selectedItem;
    if (airportsMonthlyDataCV) {       
        airportsMonthlyDataCV.refresh();
        pieCV.refresh();        
        flexChart.invalidate();
        //debugger;
        //treemap.invalidate();
        rangeChart.invalidate();
    }
}

function buildRating(rating) {
    var markup = "", j, starType;
    for (j = 0; j < 5; j++) {
        starType = j < rating ? "star star-highlighted" : "star star-dimmed";
        markup += "<span class='" + starType + "'>\u2605</span>";
    }
    return markup;
}

function flexChartRendered(args) {
    setTimeout(function () {
        regionsChart.invalidate();
    }, 20);    
}

function regionsChartRendered(args) {
    //debugger;
    createBreadCrumbs();
    regionsChart = wijmo.Control.getControl("#regionsFlexChart");
    regionsChart.selection = null;

    if (!originalPieCV) {
        originalPieCV = regionsChart.collectionView;
        originalPieBinding = regionsChart.series[0].binding;
        originalPieBindingName = regionsChart.bindingX;
    }
}

function onTreemapRendered() {

        if (!treemap) {
            treemap = wijmo.Control.getControl('#airportsTreeMap');
        }

        if (treemap) {
            if (currentItem === treemap._currentItem) {
                return;
            }

            if (isDocumentLoaded) {
                isDocumentLoaded = false;
                isTreemapClicked = false;
            }
            else
                isTreemapClicked = true;

            //debugger;
            currentItem = treemap._currentItem;
            UpdateCharts();
        }
}

function rollUp(num) {
    //rollup treemap *num times.
    for (var i = 0; i < num; i++) {
        
        treemap._rollUp();
    }
}

function createBreadCrumbs() {
    breadCrumbs = [];
    resetBreadCrumbs(currentItem);
    breadCrumbs.reverse();
    appendBreadCrumbs();
}

function appendBreadCrumbs() {
    var ol = document.querySelector("#breadCrumbs");
    if (ol)
        ol.innerHTML = '';

    var len = breadCrumbs.length;
    
    breadCrumbs.forEach(function (label, idx) {        
        ol.appendChild(apendBreadCrumb(label, idx != len - 1, len - idx - 1));
    });
}

function apendBreadCrumb(label, isAnchor, param) {
    var li = document.createElement('li');
    if (isAnchor) {
        li.className = 'breakcrumb-item';
        var a = document.createElement('a');
        a.href = 'javascript:void(0)';
        a.innerHTML = label;
        a.addEventListener('click', function (evt) {
            rollUp(param);
        });
        li.appendChild(a);
    } else {
        li.className = 'breakcrumb-item active';
        li.innerHTML = label;
    }
    return li;
}

function resetBreadCrumbs(item) {
    
    if (item) {
        breadCrumbs.push(item.label);
        resetBreadCrumbs(item.parent);
    } else {
        breadCrumbs.push('Regions');
    }
}

function UpdateCharts() {
    if (currentItem != null) {

        if (currentItem.depth === 1) {            
               
            pieCV.filter = filterFunction;
            pieCV.refresh();
            regionsChart.beginUpdate();
            regionsChart.series[0].binding = 'OnTime';
            regionsChart.bindingX = 'AirportCity';
            regionsChart.itemsSource = pieCV;
            regionsChart.header = regionsChartHeader1 + " : " + currentItem.label;
            regionsChart.axisY.title = regionsChartHeader1;
            regionsChart.axisY.format = "p0";           
            regionsChart.tooltip.content = "<b>{AirportCity}</b><br>On-Time:{value:P}";
            regionsChart.endUpdate();

            for (var s = 0; s < flexChart.series.length; s++) {
                if (flexChart.series[s].name === currentItem.label) {
                    
                    flexChart.selection = flexChart.series[s];
                    flexChart.invalidate();
                    break;
                }
            }
            
            for (var r = 0; r < grid.rows.length; r++) {
                if (grid.rows[r].hasChildren && grid.rows[r].dataItem.name === currentItem.label && !isSelected) {                    
                    grid.select(r, 0);
                    expandedRows = [r];
                    CollapseGroups();
                    isTreemapClicked = false;                                        
                    break;
                }
            }            
        }
        else if (currentItem.depth === 2) {
            var filteredCV = new wijmo.collections.CollectionView(airportsMonthlyDataCV.items);
            filteredCV.filter = airportMonthlyFilter;

            var gd = filteredCV.groupDescriptions;
            gd.clear();
            gd.push(new wijmo.collections.PropertyGroupDescription("recordedMonth"));
            
            var cityArray = [];
            var groups = filteredCV.groups;

            for (var g = 0; g < groups.length; g++) {
                cityArray.push({
                    recDate: new Date(groups[g].items[0].recordedDate),
                    month: groups[g].name + "," + new Date(groups[g].items[0].recordedDate).getFullYear().toString().substr(-2),
                    avgDelay: groups[g].getAggregate("Avg", "delay")
                });
            }

            cityCV = new wijmo.collections.CollectionView(cityArray);
            var sd = cityCV.sortDescriptions;
            sd.clear();
            sd.push(new wijmo.collections.SortDescription("recDate", true));
            regionsChart.beginUpdate();
            regionsChart.series[0].binding = 'avgDelay';
            regionsChart.bindingX = 'month';
            regionsChart.itemsSource = cityCV;
            var h = regionsChartHeader2 + " : " + currentItem.label;
            
            regionsChart.header = h;

            if (h.length > 31 && h.length < 35) {
                setTimeout(function () {
                    regionsChart.hostElement.getElementsByClassName("wj-header")['0'].childNodes[0].style.fontSize = "12pt";
                    regionsChart.hostElement.getElementsByClassName("wj-header")['0'].childNodes[0].attributes['x'].value = "0";
                }, 100);
            }
            else if (h.length >= 35) {
                setTimeout(function () {
                    regionsChart.hostElement.getElementsByClassName("wj-header")['0'].childNodes[0].style.fontSize = "11pt";
                    regionsChart.hostElement.getElementsByClassName("wj-header")['0'].childNodes[0].attributes['x'].value = "0";
                }, 100);
            }

            regionsChart.axisY.title = regionsChartHeader2;
            regionsChart.axisY.format = "d";
            //regionsChart.axisX.title = "Time Period";
            //regionsChart.dataLabel.content = '{value:N2}';
            regionsChart.tooltip.content = "<b>{month}</b><br>Average Delay(mins):{value:N2}";            
            regionsChart.endUpdate();
            regionsChart.invalidate();

            
            for (var r = 0; r < grid.rows.length; r++) {
                if (!(grid.rows[r].hasChildren) && grid.rows[r].dataItem.AirportCity === currentItem.label) {                    
                    grid.select(r, 0);
                    isTreemapClicked = false;                   
                    break;
                }
            }
        }
    }
    else {
        if (originalPieCV) {
            flexChart.selection = null;
            regionsChart.beginUpdate();
            regionsChart.series[0].binding = originalPieBinding;
            regionsChart.bindingX = originalPieBindingName;
            regionsChart.itemsSource = originalPieCV;
            regionsChart.header = regionsChartHeader1 + "(" + regionsChartHeader3 + ")";
            regionsChart.axisY.title = regionsChartHeader2;
            regionsChart.axisY.format = "p0";
            //regionsChart.axisX.title = "Regions";
            //regionsChart.axisX.labels = true;
            //regionsChart.dataLabel.content = '{value:P}';
            regionsChart.tooltip.content = "<b>{Region}</b><br>On-Time:{value:P}";
            //regionsChart.legend.position = 'Right';
            regionsChart.endUpdate();

            regionsChart.invalidate();

            if (pieCV) {
                pieCV.filter = null;
                pieCV.refresh();
            }            
        }
    }
}

function airportMonthlyFilter(item) {
    var filter = item;
    
    if (!filter) {
        return true;
    }
    var items = gridCV.items;
    var obj = items.filter(function (obj) { return obj.AirportCity === currentItem.label });

    return item.airportCode.trim() === obj[0].AirportCode.trim();
}

function filterFunction(item) {
    var filter = item;
    if (!filter) {
        return true;
    }
    return item.Region.indexOf(currentItem.label) > -1;
}

function monthlyDataFilter(item) {
    
    var filter = item;
    if (!filter) {
        return true;
    }
    return new Date(item.recordedDate).getFullYear() === 2017;
}

function invalidateWijmoControls(e) {
    if (!e) e = document.body;
    var ctl = wijmo.Control.getControl(e);
    if (ctl) {
        ctl.invalidate();
    }
    if (e.children) {
        for (var i = 0; i < e.children.length; i++) {
            invalidateWijmoControls(e.children[i]);
        }
    }
};

function hasClass(obj, cls) {
    return obj && obj.className.match(new RegExp('(\\s|^)' + cls + '(\\s|$)'));
}

function addClass(obj, cls) {
    if (obj && !this.hasClass(obj, cls)) obj.className += ' ' + cls;
}

function removeClass(obj, cls) {
    if (obj && hasClass(obj, cls)) {
        var reg = new RegExp('(\\s|^)' + cls + '(\\s|$)');
        obj.className = obj.className.replace(reg, ' ');
    }
}

function toggleClass(obj, cls) {
    if (hasClass(obj, cls)) {
        removeClass(obj, cls);
    } else {
        addClass(obj, cls);
    }
}