c1.documentReady(function () {
    var dashboardLayout = wijmo.Control.getControl('#dashboardLayout'),
        manualGridLayout = c1.getService('manualGridLayout');

    // applies the layout to the DashboardLayout control.
    dashboardLayout.layout = manualGridLayout;
});