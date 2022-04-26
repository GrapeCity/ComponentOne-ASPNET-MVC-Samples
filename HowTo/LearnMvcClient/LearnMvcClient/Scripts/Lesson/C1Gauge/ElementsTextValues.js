c1.documentReady(function () {
    var theGauge = wijmo.Control.getControl('#theGauge');
    var showText = wijmo.Control.getControl('#showText');
    var format = wijmo.Control.getControl('#format');

    // customize text properties
    showText.textChanged.addHandler(function (s, e) {
        theGauge.showText = s.text;
    });
    format.textChanged.addHandler(function (s, e) {
        theGauge.format = s.text;
    });

    document.getElementById('getText').addEventListener('click', function (e) {
        theGauge.getText = e.target.checked ? getTextCallback : null;
    });

    // getText callback function
    function getTextCallback(gauge, part, value, text) {
        switch (part) {
            case 'value':
                if (value <= 10) return 'Empty!';
                if (value <= 25) return 'Low...';
                if (value <= 95) return 'Good';
                return 'Full';
            case 'min':
                return 'empty';
            case 'max':
                return 'full';
        }
        return text;
    }
});