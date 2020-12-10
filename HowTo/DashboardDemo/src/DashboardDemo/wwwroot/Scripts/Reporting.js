var tpReports;

window.addEventListener('resize', relayout, false);

c1.documentReady(function () {
    init();
    relayout();
});

function init() {
    tpReports = wijmo.Control.getControl('#tpReports');
}

function selectedIndexChanged(sender, e) {
    var tabPanel = sender,
        selectedIndex = tabPanel.selectedIndex,
        tab = tabPanel.selectedTab,
        eleFlexView,
        flexView;

    if (selectedIndex == 0) {
        return;
    }

    eleFlexView = tab.pane.querySelector('.wj-viewer');
    flexView = wijmo.Control.getControl(eleFlexView);
    if (selectedIndex == 1) {
        flexView.filePath = '/Content/Reports/CurrentOpportunitiesData.flxr';
        flexView.reportName = 'OpportunityItem Report';
    } else if (selectedIndex == 2) {
        flexView.filePath = '/Content/Reports/ProfitAndSales.flxr';
        flexView.reportName = 'ProfitAndSale Report';
    }
    flexView.refresh();
}

function relayout() {
    var rectTPProducts = tpReports.hostElement.getBoundingClientRect(),
        tpHeaders = tpReports.hostElement.querySelector('div.wj-tabheaders'),
        tpPanes = tpReports.hostElement.querySelector('div.wj-tabpanes');

    tpPanes.style.height = (rectTPProducts.height - tpHeaders.getBoundingClientRect().height) + 'px';
    var paneHeight = tpPanes.clientHeight;
    var viewers = tpPanes.querySelectorAll('.wj-viewer');
    for (var i = 0; i < viewers.length; i++) {
        viewers[i].style.height = paneHeight + 'px';
    }
}