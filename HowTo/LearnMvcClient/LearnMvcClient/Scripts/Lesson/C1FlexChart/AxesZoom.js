c1.documentReady(function () {
    var theChart = wijmo.Control.getControl('#theChart');

    // zoom logic
    var zoom = {
        chart: theChart,
        ptStart: null,
        ptEnd: null,
        box: null
    };
    var host = zoom.chart.hostElement;
    host.addEventListener('mousedown', function (e) {
        zoom.ptStart = e;
    });
    host.addEventListener('mousemove', function (e) {
        if (zoom.ptStart) {
            zoom.ptEnd = e;
            updateZoomBox();
        }
    });
    host.addEventListener('mouseup', function (e) {
        applyZoom();
        zoom.ptStart = zoom.ptEnd = null;
        updateZoomBox();
    });
    function updateZoomBox() {
        if (!zoom.box) {
            zoom.box = wijmo.createElement('<div class="zoom-box"></div>', document.body);
        }
        if (!zoom.ptStart) {
            wijmo.setCss(zoom.box, {
                display: 'none'
            });
        } else {
            var x = Math.min(zoom.ptStart.pageX, zoom.ptEnd.pageX),
                y = Math.min(zoom.ptStart.pageY, zoom.ptEnd.pageY),
              w = Math.max(zoom.ptStart.pageX, zoom.ptEnd.pageX) - x,
              h = Math.max(zoom.ptStart.pageY, zoom.ptEnd.pageY) - y;
            wijmo.setCss(zoom.box, {
                display: 'block',
                left: x,
                top: y,
                width: w,
                height: h
            });
        }
    }
    function applyZoom() {
        var xmin = null,
                ymin = null,
            xmax = null,
            ymax = null;

        // calculate custom zoom
        if (zoom.ptStart && zoom.ptEnd) {

            // get mouse position in control coordinates
            var rc = zoom.chart.hostElement.getBoundingClientRect();
            xmin = Math.min(zoom.ptStart.pageX, zoom.ptEnd.pageX) - rc.left,
          ymin = Math.min(zoom.ptStart.pageY, zoom.ptEnd.pageY) - rc.top,
          xmax = Math.max(zoom.ptStart.pageX, zoom.ptEnd.pageX) - rc.left,
          ymax = Math.max(zoom.ptStart.pageY, zoom.ptEnd.pageY) - rc.top;

            // convert to chart coordinates
            var pMin = zoom.chart.pointToData(xmin, ymin),
      		pMax = zoom.chart.pointToData(xmax, ymax);
            xmin = Math.min(pMin.x, pMax.x);
            ymin = Math.min(pMin.y, pMax.y);
            xmax = Math.max(pMin.x, pMax.x);
            ymax = Math.max(pMin.y, pMax.y);
        }

        // apply new ranges to the chart axes
        zoom.chart.deferUpdate(function () {
            zoom.chart.axisX.min = xmin;
            zoom.chart.axisY.min = ymin;
            zoom.chart.axisX.max = xmax;
            zoom.chart.axisY.max = ymax;
        });
    }

    // reset chart zoom
    document.getElementById('btnReset').addEventListener('click', function () {
        zoom.ptStart = zoom.ptEnd = null;
        updateZoomBox();
        applyZoom();
    });
});