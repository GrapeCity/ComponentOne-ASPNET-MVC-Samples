//// basic features
var basicReportViewer = null,
    basicFullScreen = null,
    basicSelectMouseMode = null,
    basicZoomFactor = null,
    basicContinuousViewMode = null,
    paramsReportViewer = null,
    parameterValues = [1];

function InitialControls() {
    //// basic features
    basicReportViewer = wijmo.Control.getControl('#basicReportViewer');
    basicFullScreen = document.getElementById('basicFullScreen');
    basicSelectMouseMode = document.getElementById('mouseMode');
    basicZoomFactor = wijmo.Control.getControl('#basicZoomFactor');
    basicContinuousViewMode = document.getElementById('basicContinuousViewMode');
    // fullScreen
    basicFullScreen.checked = basicReportViewer.fullScreen;
    basicFullScreen.addEventListener('change', function () {
        basicReportViewer.fullScreen = this.checked;
    });

    // continousViewMode
    basicContinuousViewMode.checked = basicReportViewer.viewMode == wijmo.viewer.ViewMode.Continuous;
    basicContinuousViewMode.addEventListener('change', function () {
        basicReportViewer.viewMode = this.checked ? wijmo.viewer.ViewMode.Continuous : wijmo.viewer.ViewMode.Single;
    });

    //// parameters features
    paramsReportViewer = wijmo.Control.getControl('#paramsReportViewer');
    paramsReportViewer.queryLoadingData.addHandler(function (sender, e) {
        e.data["parameters.pCategory"] = parameterValues;
    }, paramsReportViewer);
};

//// basic features
function basicZoomFactor_ValueChanged(sender) {
    if (sender.value < sender.min || sender.value > sender.max) {
        return;
    }
    basicReportViewer.zoomFactor = sender.value;
};

//// mouseMode
function mouseMode_SelectedIndexChanged(sender){
    if (!sender.selectedValue)
        return;
    basicReportViewer.mouseMode = parseInt(sender.selectedValue);
};

//// report Names
function namesReportPath_SelectedIndexChanged(sender) {
    if (!sender.selectedValue)
        return;
    var namesReportViewer = wijmo.Control.getControl('#namesReportViewer');
    var reportPath = sender.selectedValue;
    var index = reportPath.lastIndexOf('/');
    var filePath = reportPath.substr(0, index);
    var reportName = reportPath.substr(index + 1);
    namesReportViewer.filePath = filePath;
    namesReportViewer.reportName = reportName;
};

//// parameters
function parameters_CheckedItemsChanged(sender, e) {
    var items = sender.checkedItems;
    parameterValues = [];
    for (var i = 0; i < items.length; i++) {
        parameterValues.push(items[i].Value);
    }
}

function applyParameters() {
    paramsReportViewer.reload();
}