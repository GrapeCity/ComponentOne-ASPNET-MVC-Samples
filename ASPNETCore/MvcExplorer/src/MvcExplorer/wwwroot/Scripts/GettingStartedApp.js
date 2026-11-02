var smFlexGrid = smMenu = gFlexGrid = null;
var chartSelectionMode = typeMenu = selectionModeMenu = seriesContainer = detailContainer = null;

$(document).ready(function () {
    //Selection Modes Modules
    smFlexGrid = wijmo.Control.getControl("#smFlexGrid");

    //Gauge101
    var showRanges = document.getElementById('urShowRanges');
    if (showRanges) {
        showRanges.checked = true;
        showRanges.addEventListener('change', function () {

            // determine showRanges by checkbox's checked state
            var showRanges = this.checked;

            var linearGauge = wijmo.Control.getControl("#urLinearGauge");
            var bulletGraph = wijmo.Control.getControl("#urBulletGraph");
            var radialGauge = wijmo.Control.getControl("#urRadialGauge");

            linearGauge.showRanges = showRanges;
            bulletGraph.showRanges = showRanges;
            radialGauge.showRanges = showRanges;

            linearGauge.pointer.thickness = showRanges ? 0.5 : 1;
            bulletGraph.pointer.thickness = showRanges ? 0.5 : 1;
            radialGauge.pointer.thickness = showRanges ? 0.5 : 1;
        });
    }

    var autoscaleInput = document.getElementById('asAutoScale');
    if (autoscaleInput) {
        autoscaleInput.checked = true;
        autoscaleInput.addEventListener('change', function () {

            // determine autoScale by checkbox's checked state
            var radialGauge = wijmo.Control.getControl("#asRadialGauge");
            radialGauge.autoScale = this.checked;
        });
    }
 

    var readOnlyInput = document.getElementById('evReadOnly');
    if (readOnlyInput) {
        readOnlyInput.addEventListener('change', function () {

            // determine readOnly by checkbox's checked state
            var linearGauge = wijmo.Control.getControl("#evLinearGauge");
            var radialGauge = wijmo.Control.getControl("#evRadialGauge");
            linearGauge.isReadOnly = this.checked;
            radialGauge.isReadOnly = this.checked;
        });
    }


    //FlexChart101
    //Legend & Title Module
    var ltchart = wijmo.Control.getControl("#chartLegendAndTitles");
    var ltHeader = document.getElementById('headerInput');
    var ltFooter = document.getElementById('footerInput');
    var ltXTitle = document.getElementById('xTitleInput');
    var ltYTitle = document.getElementById('yTitleInput');

    if (ltHeader) {
        ltHeader.value = 'Sample Chart';
        ltHeader.addEventListener('input', function () {
            ltchart.header = this.value;
        });
    }

    if (ltFooter) {
        ltFooter.value = 'Copyright (c) ComponentOne';
        ltFooter.addEventListener('input', function () {
            ltchart.footer = this.value;
        });
    }

    if (ltXTitle) {
        ltXTitle.value = 'Country';
        ltXTitle.addEventListener('input', function () {
            ltchart.axisX.title = this.value;
        });
    }

    if (ltYTitle) {
        ltYTitle.value = 'Amount';
        ltYTitle.addEventListener('input', function () {
            ltchart.axisY.title = this.value;
        });
    }
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
        if (el) {
            el.checked = chartLegendToggle.series[index].visibility === wijmo.chart.SeriesVisibility.Visible;
            el.addEventListener('click', function () {
                if (this.checked) {
                    chartLegendToggle.series[index].visibility = wijmo.chart.SeriesVisibility.Visible;
                }
                else {
                    chartLegendToggle.series[index].visibility = wijmo.chart.SeriesVisibility.Legend;
                }
            });
        }
  
    });

    //FlexPie101
    InitialControls();

    //Input101
    InitialControlsForInput();
});

//Selection Modes Modules
function smMenu_SelectedIndexChanged(sender) {
    if (sender.selectedValue != null && smFlexGrid != null) {
        smFlexGrid.selectionMode = sender.selectedValue;
    }
}

//Group By Modules
function gMenu_SelectedIndexChanged(sender) {
    var grid = wijmo.Control.getControl("#gFlexGrid");
    if (sender.selectedValue && grid) {
        var name = sender.selectedValue;
        var groupDescriptions = grid.collectionView.groupDescriptions;
        grid.beginUpdate();
        groupDescriptions.clear();

        if (name.indexOf("Country") > -1) {
            groupDescriptions.push(new wijmo.collections.PropertyGroupDescription("Country"));
        }

        if (name.indexOf("Product") > -1) {
            groupDescriptions.push(new wijmo.collections.PropertyGroupDescription("Product"));
        }

        if (name.indexOf("Color") > -1) {
            groupDescriptions.push(new wijmo.collections.PropertyGroupDescription("Color"));
        }

        if (name.indexOf("Start") > -1) {
            groupDescriptions.push(new wijmo.collections.PropertyGroupDescription("Start", function (item, prop) {
                var value = item[prop];
                return value.getFullYear() + "/" + value.getMonth();
            }));
        }

        if (name.indexOf("Amount") > -1) {
            groupDescriptions.push(new wijmo.collections.PropertyGroupDescription("Amount", function (item, prop) {
                var value = item[prop];
                if (value <= 500) {
                    return "<500";
                }

                if (value > 500 && value <= 1000) {
                    return "500 to 1000";
                }

                if (value > 1000 && value <= 5000) {
                    return "1000 to 5000";
                }

                return "More than 5000";
            }));
        }
        grid.endUpdate();
    }
}

//Conditional styling Modules
function csFlexGrid_ItemFormatter(panel, r, c, cell) {
    if (wijmo.grid.CellType.Cell == panel.cellType && panel.columns[c].binding == '_Color') {
        var cellData = panel.getCellData(r, c);
        cell.style.color = cellData < 0 ? 'red' : cellData < 500 ? 'black' : 'green';
    }
    if (wijmo.grid.CellType.Cell == panel.cellType && panel.columns[c].binding == 'Amount') {
        var cellData = panel.getCellData(r, c);
        cell.style.color = cellData < 0 ? 'red' : cellData < 500 ? 'black' : 'green';
    }
    if (wijmo.grid.CellType.Cell == panel.cellType && panel.columns[c].binding == 'Discount') {
        var cellData = panel.getCellData(r, c);
        cell.style.color = cellData < .1 ? 'red' : cellData < .2 ? 'black' : 'green';
    }
}


/*Gauge101*/

// InputNumber valueChanged event
function gsValue_valueChanged(sender) {
    if (sender.value < sender.min || sender.value > sender.max) {
        return;
    }
    var linearGauge = wijmo.Control.getControl("#gsLinearGauge");
    var radialGauge = wijmo.Control.getControl("#gsRadialGauge");
    linearGauge.value = sender.value;
    radialGauge.value = sender.value;
};

function dvValue_valueChanged(sender) {
    if (sender.value < sender.min || sender.value > sender.max) {
        return;
    }
    var linearGauge = wijmo.Control.getControl("#dvLinearGauge");
    var radialGauge = wijmo.Control.getControl("#dvRadialGauge");
    linearGauge.value = sender.value;
    radialGauge.value = sender.value;
};

function urValue_valueChanged(sender) {
    if (sender.value < sender.min || sender.value > sender.max) {
        return;
    }
    var linearGauge = wijmo.Control.getControl("#urLinearGauge");
    var bulletGraph = wijmo.Control.getControl("#urBulletGraph");
    var radialGauge = wijmo.Control.getControl("#urRadialGauge");
    linearGauge.value = sender.value;
    bulletGraph.value = sender.value;
    radialGauge.value = sender.value;
};

function dvShowTextMenu_execute(arg) {
    var linearGauge = wijmo.Control.getControl("#dvLinearGauge"),
        radialGauge = wijmo.Control.getControl("#dvRadialGauge"),
        showTextMenu = wijmo.Control.getControl("#dvShowTextMenu")
    showTextMenu.header = wijmo.gauge.ShowText[arg];
    linearGauge.showText = arg;
    radialGauge.showText = arg;
};

function asValue_valueChanged(sender) {
    if (sender.value < sender.min || sender.value > sender.max) {
        return;
    }
    var radialGauge = wijmo.Control.getControl("#asRadialGauge");
    radialGauge.value = sender.value;
};


function asStartAngle_valueChanged(sender) {
    if (sender.value < sender.min || sender.value > sender.max) {
        return;
    }
    var radialGauge = wijmo.Control.getControl("#asRadialGauge");
    radialGauge.startAngle = sender.value;
};

function asSweepAngle_valueChanged(sender) {
    if (sender.value < sender.min || sender.value > sender.max) {
        return;
    }
    var radialGauge = wijmo.Control.getControl("#asRadialGauge");
    radialGauge.sweepAngle = sender.value;
};

function dValue_valueChanged(sender) {
    if (sender.value < sender.min || sender.value > sender.max) {
        return;
    }
    var linearGauge = wijmo.Control.getControl("#dLinearGauge");
    var bulletGraph = wijmo.Control.getControl("#dBulletGraph");
    linearGauge.value = sender.value;
    bulletGraph.value = sender.value;
};

function dDirection_SelectedIndexChanged(sender) {
    // make sure there is a selectedValue
    if (!sender.selectedValue)
        return;

    var linearGauge = wijmo.Control.getControl("#dLinearGauge");
    var bulletGraph = wijmo.Control.getControl("#dBulletGraph");
    var direction = sender.selectedValue,
        dirCols = Array.prototype.slice.call(document.querySelectorAll('.direction-col'));

    // split or stack columns
    dirCols.forEach(function (el) {
        if (direction == 'Up' || direction == 'Down') {
            el.className += ' col-md-6';
        }
        else {
            el.className = el.className.replace('col-md-6', '');
        }
    });

    // set Gauge.direction
    linearGauge.direction = direction;
    bulletGraph.direction = direction;

    // update gauge's CSS Class
    if (direction == 'Up' || direction == 'Down') {
        linearGauge.hostElement.className += ' vertical-gauge';
        bulletGraph.hostElement.className += ' vertical-gauge';
    }
    else {
        linearGauge.hostElement.className = linearGauge.hostElement.className.replace('vertical-gauge', '');
        bulletGraph.hostElement.className = bulletGraph.hostElement.className.replace('vertical-gauge', '');
    }
};


function dDirection_execute(arg) {
    var linearGauge = wijmo.Control.getControl("#dLinearGauge"),
        bulletGraph = wijmo.Control.getControl("#dBulletGraph"),
        directionMenu = wijmo.Control.getControl("#dDirection"),
        direction = arg;

    directionMenu.header = wijmo.gauge.GaugeDirection[direction];
    dirCols = Array.prototype.slice.call(document.querySelectorAll('.direction-col'));
    // split or stack columns
    dirCols.forEach(function (el) {
        if (direction == 2 || direction == 3) {//if (direction == 'Up' || direction == 'Down') {
            el.className += ' col-md-6';
        }
        else {
            el.className = el.className.replace('col-md-6', '');
        }
    });

    // set Gauge.direction
    linearGauge.direction = direction;
    bulletGraph.direction = direction;

    // update gauge's CSS Class
    if (direction == 2 || direction == 3) {//if (direction == 'Up' || direction == 'Down') {
        linearGauge.hostElement.className = linearGauge.hostElement.className.replace('wj-lineargauge', 'vertical-gauge');
        bulletGraph.hostElement.className = bulletGraph.hostElement.className.replace('wj-lineargauge', 'vertical-gauge');
    }
    else {
        linearGauge.hostElement.className = linearGauge.hostElement.className.replace('vertical-gauge', 'wj-lineargauge');
        bulletGraph.hostElement.className = bulletGraph.hostElement.className.replace('vertical-gauge', 'wj-lineargauge');
    }
};

function sValue_valueChanged(sender) {
    if (sender.value < sender.min || sender.value > sender.max) {
        return;
    }
    var linearGauge = wijmo.Control.getControl("#sLinearGauge");
    var radialGauge = wijmo.Control.getControl("#sRadialGauge");
    linearGauge.value = sender.value;
    radialGauge.value = sender.value;
};

function evValue_valueChanged(sender) {
    if (sender.value < sender.min || sender.value > sender.max) {
        return;
    }
    var linearGauge = wijmo.Control.getControl("#evLinearGauge");
    var radialGauge = wijmo.Control.getControl("#evRadialGauge");
    linearGauge.value = sender.value;
    radialGauge.value = sender.value;
};


/*FlexChart101*/
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


//FlexPie101
function InitialControls() {

    //Basic Features Module
    var basicReversed = document.getElementById('basicReversed');
    if (basicReversed) {
        basicReversed.addEventListener('change', function () {

            // determine reversed by checkbox's checked state
            var chart = wijmo.Control.getControl("#basicChart");
            chart.reversed = this.checked;
        });
    }

    //Legend & Title Module
    var ltchart = wijmo.Control.getControl("#ltChart");
    var ltHeader = document.getElementById('ltHeader');
    var ltFooter = document.getElementById('ltFooter');

    if (ltHeader) {
        ltHeader.value = ltchart.header = 'Products By Country';
        ltHeader.addEventListener('input', function () {
            ltchart.header = this.value;
        });
    }

    if (ltFooter) {
        ltFooter.value = ltchart.footer = '2014, MESCIUS inc.';
        ltFooter.addEventListener('input', function () {
            ltchart.footer = this.value;
        });
    }

    //Selection Module
    var selAnimated = document.getElementById('selAnimated');
    if (selAnimated) {
        selAnimated.checked = true;
        selAnimated.addEventListener('change', function () {

            // determine Is Animated by checkbox's checked state
            var chart = wijmo.Control.getControl("#selChart");
            chart.isAnimated = this.checked;
        });
    }

}

//Basic Features Module
function basicInnerRadius_valueChanged(sender) {
    var chart = wijmo.Control.getControl("#basicChart");
    chart.innerRadius = sender.value;
};

function basicOffset_valueChanged(sender) {
    var chart = wijmo.Control.getControl("#basicChart");
    chart.offset = sender.value;
};

function basicStartAngle_valueChanged(sender) {
    var chart = wijmo.Control.getControl("#basicChart");
    chart.startAngle = sender.value;
};

function basicPalette_SelectedIndexChanged(sender) {
    if (!sender.selectedValue)
        return;
    var chart = wijmo.Control.getControl("#basicChart");
    chart.palette = wijmo.chart.Palettes[sender.selectedValue];
};


//Legend & Title Module
function ltLegPos_SelectedIndexChanged(sender) {
    if (!sender.selectedValue)
        return;
    var chart = wijmo.Control.getControl("#ltChart");
    chart.legend.position = sender.selectedValue;
};


//Selection Module
function selItemOffset_valueChanged(sender) {
    var chart = wijmo.Control.getControl("#selChart");
    chart.selectedItemOffset = sender.value;
};

function selItemPos_SelectedIndexChanged(sender) {
    if (!sender.selectedValue)
        return;
    var chart = wijmo.Control.getControl("#selChart");
    chart.selectedItemPosition = sender.selectedValue;
};



//Input101
function InitialControlsForInput() {
    var ListBox = wijmo.Control.getControl("#lbListBox");
    if (ListBox) {
        selectedIndexChanged(ListBox);

        if (wijmo.culture.Globalize.name == 'ja') {
            var itMask = wijmo.Control.getControl('#itMask');
            itMask.mask = '00:00 ＺＺ';
            itMask.refresh();
        }
    }
}

function inputTime_ValueChanged(sender) {
    var inputDateTime = wijmo.Control.getControl("#iditInputDateTime");
    inputDateTime.value = wijmo.DateTime.fromDateTime(inputDateTime.value, sender.value);
}

function inputDate_ValueChanged(sender) {
    var inputDateTime = wijmo.Control.getControl("#iditInputDateTime");
    inputDateTime.value = wijmo.DateTime.fromDateTime(sender.value, inputDateTime.value);
}

function inputDateTime_ValueChanged(sender) {
    var inputDate = wijmo.Control.getControl("#iditInputDate");
    var inputTime = wijmo.Control.getControl("#iditInputTime");
    inputDate.value = wijmo.DateTime.fromDateTime(sender.value, inputDate.value);
    inputTime.value = wijmo.DateTime.fromDateTime(inputTime.value, sender.value);
    document.getElementById('iditSelectedValue').innerHTML = wijmo.Globalize.format(sender.value, 'MMM dd, yyyy h:mm:ss tt');
}

//selectedIndexChanged event handler
function selectedIndexChanged(sender) {
    //set selectedIndex and selectedValue text
    if (wijmo.Control.getControl("#lbListBox") && document.getElementById("lbSelIdx") && document.getElementById("lbSelVal")) {//if (document.readyState === "complete") {
        document.getElementById('lbSelIdx').innerHTML = sender.selectedIndex;
        document.getElementById('lbSelVal').innerHTML = sender.selectedValue;
    }
}

// InputMask valueChanged event handler
function MaskvalueChanged(sender) {
    var customMaskTrial = wijmo.Control.getControl("#imCustomTrial");
    customMaskTrial.mask = sender.value;
    customMaskTrial.hostElement.title = 'Mask: ' + (sender.value || 'N/A');
}

function itemClicked(sender) {
    alert('You\'ve selected option ' + sender.selectedIndex + ' from the ' + sender.header + ' menu!');
}

function execute(arg) {
    var inputNumber = wijmo.Control.getControl("#mInputNumber");

    // convert argument to Number
    arg = wijmo.changeType(arg, wijmo.DataType.Number);

    // check if the conversion was successful
    if (wijmo.isNumber(arg)) {

        // update the value
        inputNumber.value += arg;
    }
}

function canExecute(arg) {
    var inputNumber = wijmo.Control.getControl("#mInputNumber");

    // convert argument to Number
    arg = wijmo.changeType(arg, wijmo.DataType.Number);

    // check if the conversion was successful
    if (wijmo.isNumber(arg)) {
        var val = inputNumber.value + arg;

        // check if the value is valid
        return val >= 0 && val <= 1;
    }

    return false;
}