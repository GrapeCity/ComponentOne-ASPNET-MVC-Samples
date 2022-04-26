c1.documentReady(function () {
    var red = wijmo.Control.getControl('#red');
    var green = wijmo.Control.getControl('#green');
    var blue = wijmo.Control.getControl('#blue');

    // color being edited
    var theColor = new wijmo.Color('grey');
    var thePanel = document.getElementById('thePanel');
    updateColor();
    function updateColor() {
        thePanel.style.background = theColor;
    }

    red.value = theColor.r;
    red.valueChanged.addHandler(function (s, e) {
        theColor.r = s.value;
        updateColor();
    });
    initGauge(red, 'red');

    green.value = theColor.g;
    green.valueChanged.addHandler(function (s, e) {
        theColor.g = s.value;
        updateColor();
    });
    initGauge(green, 'green');

    blue.value = theColor.b;
    blue.valueChanged.addHandler(function (s, e) {
        theColor.b = s.value;
        updateColor();
    });
    initGauge(blue, 'blue');

    function initGauge(g, color) {
        g.isReadOnly = false,
  	g.thumbSize = 10,
  	g.hasShadow = false,
  	g.min = 0;
        g.max = 255;
        g.step = 5,
        g.showTicks = true;
        g.tickSpacing = 25;
        g.face.thickness = .2;
        g.pointer.thickness = .3;
        g.pointer.color = color;
        g.hostElement.style.color = color;
    }
});