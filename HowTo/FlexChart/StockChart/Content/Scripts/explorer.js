var currentInput, addInput, currentAuto, addAuto,
    rangeButtons, selectedBtn, volumeBtn, movingAverage,
    stockInfosCbx, movingAverageTypeMenu, periodInput,
    stChart, seriesLength, stChartSeriesItems, markcontents, pOffset, movingAverageSeriesCount,
    rsChart, rangeSelector, dateRangePanel, ranges, checkboxShowMovingAverage,checkboxShowNews, checkboxShowLineMarker, showLineMarker = false;

function InitialWhenDReady() {
    currentInput = document.getElementById("goToSymbol").getElementsByTagName("input")[0];
    addInput = document.getElementById("addSymbol").getElementsByTagName("input")[0];
    checkboxShowMovingAverage = document.getElementById("checkboxShowMovingAverage");
    currentAuto = wijmo.Control.getControl("#CurrentAuto");
    addAuto = wijmo.Control.getControl("#AddAuto");
    movingAverageTypeMenu = wijmo.Control.getControl("#movingAverageTypeMenu");
    periodInput = wijmo.Control.getControl("#periodInput");
    movingAverage = document.getElementById("movingAverage");
    checkboxShowLineMarker = document.getElementById("checkboxShowLineMarker");
    checkboxShowNews = document.getElementById("checkboxShowNews");
    movingAverageSeriesCount = 0;

    rangeButtons = $("#rangeBtnCon").find("button");
    selectedBtn = $("#rangeBtnCon").find("button.active");
    volumeBtn = $("#volumeBtn");

    stockInfosCbx = document.getElementById("stockInfos").getElementsByTagName("input");

    chartTypeMenu = wijmo.Control.getControl("#ChartTypeMenu");

    stChart = wijmo.Control.getControl("#stockChart");
    stChartSeriesItems = [];
    seriesLength = stChart.series.length;
    for (var i = 0; i < seriesLength; i++) {
        stChartSeriesItems.push(stChart.series[i].collectionView.items);
        if (stChart.series[i] instanceof wijmo.chart.analytics.MovingAverage) {
            movingAverageSeriesCount++;
        }
    }
    updateMovingAverage();
    rsChart = wijmo.Control.getControl("#rangeSelector");
    rangeSelector = c1.getExtender(rsChart, "RangeSelectorExtender");

    dateRangePanel = document.getElementById("dateRangePanel");
    ranges = document.getElementById("storeRange").getElementsByTagName("input");

    if (ranges[0].value === "0" && ranges[1].value) {
        setTimeout(function () {
            rangeSelector.min = getChartStartDate(2).valueOf();
            rangeSelector.max = null;
        }, 100);
    } else {
        setTimeout(function () {
            updateStChartRange(rangeSelector.min, rangeSelector.max);
        }, 100)
    }

    if (seriesLength - movingAverageSeriesCount > 2) {
        chartTypeMenu.hostElement.style["display"] = "none";
    }

    $("#reset").find("input").attr("value", "false");
    currentInput.setAttribute("value", "");
    addInput.setAttribute("value", "");
    stChart.hostElement.addEventListener('mousemove', function (e) {
        setQuotesDetailInfo(e);
    });
    stChart.hostElement.addEventListener('mouseleave', function (e) {
        seriesMarkerShowing(false);
    });

    //Avoid LineMarker affecting the annotation tooltips showing.
    document.getElementsByClassName("wj-chart-linemarker")[2].childNodes[0].style.display = "none";
}

/* Functions for AutoComplete and Buttons  */

function changeBtnDisable(button, toggle) {
    if (button) {
        if (toggle) {
            button.removeAttribute("disabled");
        } else {
            button.setAttribute("disabled", "disabled");
        }
    }
}

function changeGoToBtn(auto) {
    var button = document.getElementById("goToBtn"),
        toggle = auto.selectedValue !== null
        && (auto.selectedItem.symbol + ": " + auto.selectedItem.name) === auto.text;
    changeBtnDisable(button, toggle);
}

function changeAddBtn(auto) {
    var button = document.getElementById("addBtn"),
        toggle = auto.selectedValue !== null
        && (auto.selectedItem.symbol + ": " + auto.selectedItem.name) === auto.text;
    changeBtnDisable(button, toggle);
}

function setCurrentItem() {
    var form = document.forms[0];
    if (form) {
        var value = currentAuto.selectedValue;
        if (value && value !== "null") {
            currentInput.setAttribute("value", value);
            form.submit();
        }
    }
}

function addNewItem() {
    var form = document.forms[0];
    if (form) {
        var value = addAuto.selectedValue;
        if (value && value !== "null") {
            addInput.setAttribute("value", value);
            form.submit();
        }
    }
}

function resetItems() {
    var form = document.forms[0];

    $("#reset").find("input[type='hidden']").attr("value", "true");
    form.submit();
}


/* Operations in StockChart  */

function toggleSeriesDisplay(idx) {
    var seriesIdx = idx * 2;
    if (stChart) {
        stChart.series[seriesIdx].visibility =
            stockInfosCbx[idx - 1].checked && stChart.series[seriesIdx].itemsSource.length > 0 ? 0 : 3;
        if (checkboxShowMovingAverage.checked && stChart.series[seriesIdx].visibility === 0) {
            stChart.series[seriesIdx + 1].visibility = 0;
        } else {
            stChart.series[seriesIdx + 1].visibility = 3;
        }
        chartTypeMenu.selectedIndex = 0;
        for (var i = 0; i < stChart.series.length - 1; i++) {
            if (stChart.series[i].visibility === 0) {
                stChart.series[i].binding = "CloseChg";
                if (i === 0) {
                    continue;
                }
                chartTypeMenu.hostElement.style["display"] = "none";
                return;
            }
        }
        chartTypeMenu.hostElement.style["display"] = "";
    }
}

function toggleVolDisplay() {
    if (stChart) {
        var length = stChart.series.length,
            visibility = stChart.series[length - 1].visibility;
        if (visibility === 0) {
            stChart.series[length - 1].visibility = 3;
            volumeBtn.removeClass("active");
        } else {
            stChart.series[length - 1].visibility = 0;
            volumeBtn.addClass("active");
        }
    }
}

function changeChartType(chartTypeMenu, ui) {
    if (stChart) {
        var length = stChart.series.length, type, chartType = 3;
        if (!chartTypeMenu.selectedValue) {
            return;
        }
        type = chartTypeMenu.selectedValue.Header;

        chartTypeMenu.header = "Chart Type: <b>" + type + "</b>";

        switch (type) {
            case "Line":
                chartType = 3;
                break;
            case "Area":
                chartType = 5;
                break;
            case "Candlestick":
                chartType = 7;
                break;
            case "HLOC":
                chartType = 8;
                break;
        }

        stChart.beginUpdate();

        if (length > 1) {
            if (chartType === 7 || chartType === 8) {
                stChart.series[0].binding = "High,Low,Open,Close";
            } else {
                stChart.series[0].binding = "Close";
            }
            stChart.series[0].chartType = chartType;
        }

        stChart.endUpdate();
    }
}

function chartPeriodChange(selected) {
    if (rangeButtons && rangeButtons.eq(selected).hasClass("active")) {
        return;
    }
    selectedBtn.removeClass("active");
    selectedBtn = rangeButtons.eq(selected);
    selectedBtn.addClass("active");

    rangeSelector.min = getChartStartDate(selected).valueOf();
    rangeSelector.max = null;
}

function updateMovingAverage() {
    if (!checkboxShowMovingAverage)
        return;
    var showMovingAverage = checkboxShowMovingAverage.checked, index, seriesType = "Simple";
    var period = Math.max(periodInput.min, Math.min(periodInput.value, periodInput.max));

    if (movingAverageTypeMenu.selectedItem) {
        movingAverageTypeMenu.header = "Type: <b>" + movingAverageTypeMenu.selectedItem.Header + "</b>";
        seriesType = movingAverageTypeMenu.selectedItem.Header;
    }
    movingAverage.style.display = showMovingAverage ? "inline" : "none";
    for (index = 0; index < stChart.series.length; index++) {
        if (stChart.series[index] instanceof wijmo.chart.analytics.MovingAverage) {
            stChart.series[index].visibility = (stChart.series[index - 1].visibility === 0 && showMovingAverage) ? "Visible" : "Hidden";
            stChart.series[index].period = period;
            stChart.series[index].type = seriesType;
        }
    }
}

function updateLineMarker() {
    var extenders = c1.getExtenders(stChart, wijmo.chart.LineMarker);
    for (var i = 0; i < extenders.length; i++) {
        showLineMarker = checkboxShowLineMarker.checked;
        extenders[i].isVisible = showLineMarker;
    }
    updateSeriesMarkers();
}

function updateNews() {
    var layer = getAnnotationLayer(stChart);
    if (!layer) {
        return;
    }
    for (var i = 0; i < layer.items.length; i++) {
        if (layer.items[i] instanceof wijmo.chart.annotation.Rectangle) {
            layer.items[i].isVisible = checkboxShowNews.checked;
        }
    }
}

function itemClicked(menu) {
    if (menu.selectedValue) {
        exportImage(menu.selectedValue.Header);
    }
}

//set main quote detail information
function setQuotesDetailInfo(e) {
    var series = stChart.series,
        hitTest, detail = '', al = getAnnotationLayer(stChart),
        point, pointIndex, datailEle = wijmo.getElement("#detail");
    if (!series || series.length === 0) {
        return;
    }
    if (e) {
        point = e instanceof MouseEvent ?
        new wijmo.Point(e.pageX, e.pageY) :
        new wijmo.Point(e.changedTouches[0].pageX, e.changedTouches[0].pageY);
    }


    series.forEach(function (ser) {
        if (!ser.itemsSource) {
            return;
        }
        if (e) {
            // each series has different data range
            hitTest = ser.hitTest(new wijmo.Point(point.x, NaN));
            if (hitTest == null || hitTest.x == null || hitTest.y == null) {
                //hide the series annotation
                annoItem = al.getItem(ser.name);
                if (annoItem) {
                    annoItem.isVisible = false;
                }
                return;
            }
            pointIndex = hitTest.pointIndex;
        } else {
            pointIndex = ser.itemsSource.length - 1;
        }

        var itm = ser.itemsSource[pointIndex],
            dateStr;

        if (!itm || ser instanceof wijmo.chart.analytics.MovingAverage ||
            ser.binding === 'Volume') {
            return;
        }

        annoItem = al.getItem(ser.name);
        if (annoItem) {
            if (annoItem.pointIndex !== pointIndex) {
                annoItem.pointIndex = pointIndex;
            }
            if (!annoItem.isVisible) {
                annoItem.isVisible = true;
            }
        }

        dateStr = wijmo.Globalize.format(itm.TimeSlab, 'MMM dd, yyyy');

        if (series.length <= 3) {//include volume, trend series
            if (ser.binding === 'high,low,open,close' && e) {
                detail = dateStr +
                    ' Open: ' + itm.Open +
                    ' High: ' + itm.High +
                    ' Low: ' + itm.Low +
                    ' Close: ' + itm.Close +
                    ' Volume: ' + itm.Volume;
            } else {
                detail = dateStr +
                    ' Price: ' + itm.Close +
                    ' Volume: ' + itm.Volume;
            }
        } else {
            detail += getCurPointQuoteInfo(ser.name, itm.CloseChg, ser.style.stroke);
        }
    })

    //for ios: when dom reload, the event don't fire
    window.setTimeout(function () {
        datailEle.innerHTML = detail;
    }, 0);
}

function getCurPointQuoteInfo(syml, info, symlColor) {
    var color = '#129B14', pSymbol = '';

    if (info < 0) {
        color = '#BA2418';
    } else {
        pSymbol = "+";
    }

    return '<span style="color: ' + symlColor + ';">' + syml + '</span><span style="color:' + color + ';">' +
        pSymbol + (info * 100).toFixed(2) + '%'
        + '</span> ';
}

// helper function to export the FlexChart to an image
function exportImage(extension) {
    if (stChart) {
        var chart = stChart,
            canvas = document.createElement('canvas'),
            ctx = canvas.getContext('2d'),
            svg = chart.hostElement.querySelector('svg'),
            size = svg.getBoundingClientRect(),
            xml;

        // inline <text> styles
        textInliner(chart.hostElement);

        // set canvas height/width
        canvas.height = size.height;
        canvas.width = size.width;

        // handle rectangle fill, otherwise the fill will be transparent
        ctx.fillStyle = window.getComputedStyle(document.querySelector('.stchart')).backgroundColor;
        ctx.fillRect(0, 0, size.width, size.height);

        /* Perform other preprocessing if needed */

        // serialize SVG to XML
        xml = new XMLSerializer().serializeToString(svg);

        // call canvg extension method
        ctx.drawSvg(xml, 0, 0, size.width, size.height);
        canvas.toBlob(function (blob) {
            if (!blob) { return; }
            // use FileSaver.js to save the image
            saveAs(blob, 'chart.' + extension);
        });

        canvas = null;
    }
}

function updateSeriesMarkers() {
    var seriesAnno, al = getAnnotationLayer(stChart);
    series = stChart.series;

    if (!al) {
        return;
    }

    //clear items
    if (al && al.items.length > 1) {
        for (var i = al.items.length - 1; i >= 0; i--) {
            if (al.items[i] instanceof wijmo.chart.annotation.Circle) {
                al.items.removeAt(i);
            }
        }
    }
    if (!showLineMarker) {
        for (var i = 0; i < series.length; i++) {
            if (series[i] instanceof wijmo.chart.analytics.MovingAverage ||
                series[i].binding === 'Volume') {
                continue;
            }
            seriesAnno = new wijmo.chart.annotation.Circle({
                name: series[i].name,
                seriesIndex: i,
                isVisible: false,
                position: wijmo.chart.annotation.AnnotationPosition.Center,
                attachment: wijmo.chart.annotation.AnnotationAttachment.DataIndex,
                radius: 3.5,
                style: { fill: series[i].style.stroke, stroke: series[i].style.stroke }
            });
            al.items.push(seriesAnno);
        }
    }
}

function seriesMarkerShowing(visible) {
    var al = getAnnotationLayer(stChart), annoItems;
    if (!al) {
        return;
    }
    annoItems = al.items;
    annoItems.forEach(function (itm) {
        if (itm instanceof wijmo.chart.annotation.Circle) {
            itm.isVisible = visible;
        }
    });
}

// external CSS is not detected when exporting
// so one must inline some/all of these styles
function textInliner(chartHost) {
    var textEls = [].slice.call(chartHost.querySelectorAll('text')),
        style;

    textEls.forEach(function (current, index, array) {
        style = window.getComputedStyle(current);
        current.style.fontSize = style.getPropertyValue('font-size');
        current.style.fontFamily = style.getPropertyValue('font-family');
        current.style.fill = style.getPropertyValue('fill');
    });
}

function getAnnotationLayer(chart) {
    return c1.getExtender(chart, "AnnotationLayer");
}