c1.documentReady(function () {
    var dashboardLayout = wijmo.Control.getControl('#dashboardLayout'),
        autoGridLayout = c1.getService('autoGridLayout');

    // applies the layout to the DashboardLayout control.
    dashboardLayout.layout = autoGridLayout;
});