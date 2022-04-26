//Controls Declaration
var FlexChart = typeMenu = exportToMenu = null;

//Controls Initialization
$(document).ready(function () {
    typeMenu = wijmo.Control.getControl('#typeMenu');
});

//change Chart Types
function typeMenu_ItemClicked(sender) {
    sender.header = "Chart Type: <b>" + sender.selectedItem.Header + "</b>";
    FlexChart.chartType = sender.selectedItem.Header;
}

//export Chart
function exportToMenu_ItemClicked(sender) {
    if (FlexChart != null) {
        FlexChart.saveImageToFile('chart.' + sender.selectedItem.CommandParameter || 'png');
    }
}