c1.documentReady(function () {
    var dashboardLayout = wijmo.Control.getControl('#dashboardLayout'),
        splitLayout = c1.getService('splitLayout');

    // applies the layout to the DashboardLayout control.
    dashboardLayout.layout = splitLayout;
});