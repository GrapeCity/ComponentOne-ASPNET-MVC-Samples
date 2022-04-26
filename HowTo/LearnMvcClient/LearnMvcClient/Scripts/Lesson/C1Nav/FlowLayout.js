c1.documentReady(function () {
    var dashboardLayout = wijmo.Control.getControl('#dashboardLayout'),
        flowLayout = c1.getService('flowLayout');

    // applies the layout to the DashboardLayout control.
    dashboardLayout.layout = flowLayout;
});