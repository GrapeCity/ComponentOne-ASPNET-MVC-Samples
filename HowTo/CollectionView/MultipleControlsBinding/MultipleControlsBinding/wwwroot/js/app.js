//Chart Types Module
function typeMenu_SelectedIndexChanged(sender) {
    if (sender.selectedValue) {
        var chartTypes = wijmo.Control.getControl("#flexChart");
        chartTypes.chartType = sender.selectedValue;
    }
}

function rotatedMenu_SelectedIndexChanged(sender) {
    if (sender.selectedValue) {
        var chartTypes = wijmo.Control.getControl("#flexChart");
        chartTypes.rotated = sender.selectedValue == 'True' ? true : false;
    }
}