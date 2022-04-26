c1.documentReady(function () {
    var dashboardLayout = wijmo.Control.getControl('#dashboardLayout');

    // toggle allowDrag property
    document.getElementById('allowDrag').addEventListener('click', function (e) {
        dashboardLayout.allowDrag = e.target.checked;
    });

    // toggle allowResize property
    document.getElementById('allowResize').addEventListener('click', function (e) {
        dashboardLayout.allowResize = e.target.checked;
    });

    // toggle allowMaximize property
    document.getElementById('allowMaximize').addEventListener('click', function (e) {
        dashboardLayout.allowMaximize = e.target.checked;
    });

    // toggle allowHide property
    document.getElementById('allowHide').addEventListener('click', function (e) {
        dashboardLayout.allowHide = e.target.checked;
    });

    // toggle allowShowAll property
    document.getElementById('allowShowAll').addEventListener('click', function (e) {
        dashboardLayout.allowShowAll = e.target.checked;
    });
});