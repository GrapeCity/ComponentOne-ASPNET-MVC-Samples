$(document).ready(function () { 
    InitialControls();
});

function InitialControls() {

    //Basic Features Module
    var basicReversed = document.getElementById('basicReversed');
    basicReversed.addEventListener('change', function () {

        // determine reversed by checkbox's checked state
        var chart = wijmo.Control.getControl("#basicChart");
        chart.reversed = this.checked;
    });

    //Legend & Title Module
    var ltchart = wijmo.Control.getControl("#ltChart");
    var ltHeader = document.getElementById('ltHeader');
    var ltFooter = document.getElementById('ltFooter');

    ltHeader.value = ltchart.header = 'Products By Country';
    ltFooter.value = ltchart.footer = '2014, MESCIUS inc.';

    ltHeader.addEventListener('input', function () {
        ltchart.header = this.value;
    });

    ltFooter.addEventListener('input', function () {
        ltchart.footer = this.value;
    });


    //Selection Module
    var selAnimated = document.getElementById('selAnimated');
    selAnimated.checked = true;
    selAnimated.addEventListener('change', function () {

        // determine Is Animated by checkbox's checked state
        var chart = wijmo.Control.getControl("#selChart");
        chart.isAnimated = this.checked;
    });
}

//Basic Features Module
function basicInnerRadius_valueChanged(sender) {
    var chart = wijmo.Control.getControl("#basicChart");
    chart.innerRadius = sender.value;
};

function basicOffset_valueChanged(sender) {
    var chart = wijmo.Control.getControl("#basicChart");
    chart.offset = sender.value;
};

function basicStartAngle_valueChanged(sender) {
    var chart = wijmo.Control.getControl("#basicChart");
    chart.startAngle = sender.value;
};

function basicPalette_SelectedIndexChanged(sender) {
    if (!sender.selectedValue)
        return;
    var chart = wijmo.Control.getControl("#basicChart");
    chart.palette = wijmo.chart.Palettes[sender.selectedValue];
};


//Legend & Title Module
function ltLegPos_SelectedIndexChanged(sender) {
    if (!sender.selectedValue)
        return;
    var chart = wijmo.Control.getControl("#ltChart");
    chart.legend.position = sender.selectedValue;
};



//Selection Module
function selItemOffset_valueChanged(sender) {
    var chart = wijmo.Control.getControl("#selChart");
    chart.selectedItemOffset = sender.value;
};

function selItemPos_SelectedIndexChanged(sender) {
    if (!sender.selectedValue)
        return;
    var chart = wijmo.Control.getControl("#selChart");
    chart.selectedItemPosition = sender.selectedValue;
};