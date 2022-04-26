c1.documentReady(function () {
    var theChart = wijmo.Control.getControl('#theChart');

    // toggle custom CSS, grid visibility
    onCheck('customGridlines', function (checked) {
        wijmo.toggleClass(theChart.hostElement, 'custom-gridlines', checked)
    });
    onCheck('x-major', function (checked) {
        theChart.axisX.majorGrid = checked;
    });
    onCheck('x-minor', function (checked) {
        theChart.axisX.minorGrid = checked;
    });
    onCheck('y-major', function (checked) {
        theChart.axisY.majorGrid = checked;
    });
    onCheck('y-minor', function (checked) {
        theChart.axisY.minorGrid = checked;
    });
    onCheck('customUnits', function (checked) {
        if (checked) {
            theChart.axisX.majorUnit = 7;
            theChart.axisX.minorUnit = 1;
            theChart.axisY.majorUnit = 20;
            theChart.axisY.minorUnit = 5;
        } else {
            theChart.axisX.majorUnit = null;
            theChart.axisX.minorUnit = null;
            theChart.axisY.majorUnit = null;
            theChart.axisY.minorUnit = null;
        }
    });
    function onCheck(id, fn) {
        document.getElementById(id).addEventListener('click', function (e) {
            fn(e.target.checked);
        });
    }
});