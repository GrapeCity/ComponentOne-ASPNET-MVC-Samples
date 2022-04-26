function countriesMenu_selectedIndexChanged() {
    var reportViewer = wijmo.Control.getControl("#reportViewer");
    if (reportViewer) {
        reportViewer.reload();
    }
}

function reportViewer_queryLoadingData(s, e) {
    var countriesMenu = wijmo.Control.getControl("#countriesMenu");
    e.data.country = countriesMenu.selectedValue;
}