c1.documentReady(function () {
    var theChart = wijmo.Control.getControl('#theChart');
    var xPos = wijmo.Control.getControl('#xPosition');
    var yPos = wijmo.Control.getControl('#yPosition');

    // change axis postion and origin
    xPos.textChanged.addHandler(function (s, e) {
        theChart.axisX.position = s.text;
    });
    document.getElementById('xOrigin').addEventListener('click', function (e) {
        theChart.axisX.origin = e.target.checked ? 0 : null;
    });

    yPos.textChanged.addHandler(function (s, e) {
        theChart.axisY.position = s.text;
    });
    document.getElementById('yOrigin').addEventListener('click', function (e) {
        theChart.axisY.origin = e.target.checked ? 0 : null;
    });
});