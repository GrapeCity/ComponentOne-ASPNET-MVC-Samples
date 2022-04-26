function addEventListenerForTab(tabs, panels, i) {
    tabs[i].addEventListener('click', function (event) {
        toggleTabPanelVisibility(tabs, panels, i);

        if (tabs[i].id !== 'docs') {
            event.preventDefault();
        }
    });
}

function createTabs(tabId, panelId) {
    var tabCon = document.getElementById(tabId),
        tabs, panel, panels, i, className;

    if (tabCon == null) {
        return;
    }

    tabs = tabCon.getElementsByTagName('a');
    panel = document.getElementById(panelId);
    panels = new Array();

    for (i = 0; i < panel.children.length; i++) {
        className = panel.children[i].className;
        if (className && className.indexOf('tab-pane') >= 0) {
            panels.push(panel.children[i]);
        }
    }
    addClass(tabs[0].parentElement, 'active');
    addClass(panels[0], 'active');
    for (i = 0; i < tabs.length; i++) {
        addEventListenerForTab(tabs, panels, i);
    }
}

function gotoDoc(url) {
    if (url) {
        window.open(url, '_blank');
    }
}