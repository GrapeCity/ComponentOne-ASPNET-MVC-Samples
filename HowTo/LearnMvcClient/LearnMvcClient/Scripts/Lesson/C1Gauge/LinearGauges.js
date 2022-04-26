c1.documentReady(function () {
    var theGauge = wijmo.Control.getControl('#theGauge');
    var value = wijmo.Control.getControl('#value');
    var direction = wijmo.Control.getControl('#direction ');

    theGauge.valueChanged.addHandler(function (s, e) {
        value.value = s.value;
    });

    setDirection('Right');

    value.min = theGauge.min;
    value.max = theGauge.max;
    value.step = theGauge.step;
    value.value = theGauge.value;
    value.valueChanged.addHandler(function (s, e) {
        theGauge.value = s.value;
    });

    direction.textChanged.addHandler(function (s, e) {
        setDirection(s.text);
    });

    function setDirection(dir) {
        theGauge.direction = dir;
        var g = theGauge.hostElement.style;
        switch (dir) {
            case 'Left':
            case 'Right':
                g.height = '2em';
                g.width = '100%';
                break;
            case 'Up':
            case 'Down':
                g.height = '100%';
                g.width = '2em';
                break;
        }
    }
});