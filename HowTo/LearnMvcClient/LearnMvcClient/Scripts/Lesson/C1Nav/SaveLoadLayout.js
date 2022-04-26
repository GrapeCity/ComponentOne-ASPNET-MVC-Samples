// Saves or restores the layout definition in localStorage
function saveLayout() {
    var dashboard = wijmo.Control.getControl('#saveLoadDashboard');
    localStorage['layout'] = dashboard.saveLayout();
}

// Loads the layout definition in localStorage
function loadLayout() {
    var dashboard = wijmo.Control.getControl('#saveLoadDashboard'),
        layoutDef = localStorage['layout'];
    if (layoutDef) {
        dashboard.loadLayout(layoutDef);
    }
}