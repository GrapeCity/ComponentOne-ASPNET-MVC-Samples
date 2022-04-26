function changeContent(hitInfo, pt) {
    markcontents = getMarkerContents(new wijmo.Point(pt.x, pt.y));
    return markcontents ? markcontents.content : '';
}

function changeYContent() {
    return markcontents && markcontents.y ? markcontents.y.toString() : '';
}

function changeXContent() {
    return markcontents && markcontents.x ? markcontents.x.toString() : '';
}

//Occured in StockChart "Rendered" event
function updatePOffset(chart) {
    var chartHostEle = chart.hostElement,
        pa = chartHostEle.querySelector('.wj-plot-area');
    pOffset = wijmo.getElementRect(pa);
}

function getMarkerContents(pt) {
    var ht, xContent, yContent,
        newHitPoint = new wijmo.Point(pt.x, NaN), content = '';

    if (!stChart || stChart.series.length < 1) {
        return;
    }
    //calculate the y value
    yContent = getAxixYValue(pt.y);
    ht = stChart.series[0].hitTest(newHitPoint);

    if (ht.x && ht.y !== null) {
        xContent = wijmo.Globalize.formatDate(ht.x, 'MM-dd-yyyy');
    }
    return { content: '', x: xContent, y: yContent };
}


function getAxixYValue(y) {
    var yVal, axisYRange, axisYMax, axisYMin;

    if (pOffset === undefined) {
        return 0;
    }

    axisYMax = stChart.axisY.actualMax;
    axisYMin = stChart.axisY.actualMin;
    axisYRange = axisYMax - axisYMin;

    yVal = ((axisYMax - ((y - pOffset.top) / pOffset.height) * axisYRange) * 100)
        .toFixed(2) + '%';

    return yVal;
}

