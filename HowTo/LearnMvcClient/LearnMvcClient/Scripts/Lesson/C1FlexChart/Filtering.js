c1.documentReady(function () {
    var theChart = wijmo.Control.getControl('#theChart');
    var theMonth = wijmo.Control.getControl('#theMonth');
    theMonth.valueChanged.addHandler(function(s, e){
        // reset the chart zoom
        applyZoom(theChart, null);

        // apply filter to chart data
        theChart.collectionView.filter = function (item) {
            if (theMonth.value == null) {
                return true; // no filter
            }
            return wijmo.Globalize.format(item.Date, theMonth.format) == theMonth.text;
        }
    });

    document.getElementById('btnResetFilter').addEventListener('click', function () {
        theMonth.value = null;
    });

    // zoom with the mouse wheel
    theChart.hostElement.addEventListener('wheel', function (e) {
        if (e.ctrlKey) {
            var center = theChart.pointToData(e.clientX, e.clientY);
            applyZoom(theChart, e.deltaY > 0 ? 1.1 : .9, center);
            e.preventDefault();
        }
    })

    // zoom logic
    document.getElementById('btnZoomIn').addEventListener('click', function () {
        applyZoom(theChart, .9)
    });
    document.getElementById('btnZoomOut').addEventListener('click', function () {
        applyZoom(theChart, 1.1)
    });
    document.getElementById('btnResetZoom').addEventListener('click', function () {
        applyZoom(theChart, null);
    });

    // apply a zoom factor to the chart (keeping the center constant)
    function applyZoom(chart, factor, center) {
        applyZoomAxis(chart.axisX, factor, center ? center.x : null);
        applyZoomAxis(chart.axisY, factor, center ? center.y : null);
    }
    function applyZoomAxis(axis, factor, center) {
        if (!factor) { // reset
            axis.min = axis.max = null;
        } else {
            var min = (axis.min != null ? axis.min : axis.actualMin).valueOf();
            var max = (axis.max != null ? axis.max : axis.actualMax).valueOf();
            if (center == null) {
                center = (min + max) / 2;
            }
            axis.min = center - (center - min) * factor;
            axis.max = center + (max - center) * factor;
        }
    }
});