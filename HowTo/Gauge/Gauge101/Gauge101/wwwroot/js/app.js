$(document).ready(function () {
    var showRanges = document.getElementById('urShowRanges');
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



    var autoscaleInput = document.getElementById('asAutoScale');
    autoscaleInput.checked = true;
    autoscaleInput.addEventListener('change', function () {

        // determine autoScale by checkbox's checked state
        var radialGauge = wijmo.Control.getControl("#asRadialGauge");
        radialGauge.autoScale = this.checked;
    });

    var readOnlyInput = document.getElementById('evReadOnly');
    readOnlyInput.addEventListener('change', function () {

        // determine readOnly by checkbox's checked state
        var linearGauge = wijmo.Control.getControl("#evLinearGauge");
        var radialGauge = wijmo.Control.getControl("#evRadialGauge");
        linearGauge.isReadOnly = this.checked;
        radialGauge.isReadOnly = this.checked;
    });




});


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