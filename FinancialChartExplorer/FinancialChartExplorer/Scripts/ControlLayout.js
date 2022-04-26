function toggleTabPanelVisibility(tabs, panels, i) {
    var activeID = 0, j;
    for (j = 0; j < tabs.length; j++) {
        if (tabs[j].parentElement.className.indexOf('active') >= 0) {
            activeID = j;
            break;
        }
    }
    if (activeID != i) {
        removeClass(tabs[activeID].parentElement, 'active');
        removeClass(panels[activeID], 'active');
        addClass(tabs[i].parentElement, 'active');
        addClass(panels[i], 'active');
    }
}

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

function createCollapseStruct(titleEle, toggleClassName) {
    titleEle.addEventListener('click', function (event) {
        var list = event.currentTarget.nextElementSibling;
        if (list) {
            toggleClass(list, toggleClassName);
            event.preventDefault();
        }
    });
}

function initTabs() {
    SyntaxHighlighter.all();
    SyntaxHighlighter.defaults['toolbar'] = false;
    SyntaxHighlighter.defaults['quick-code'] = false;
    var titles = document.getElementsByClassName('collapse-title'),
        sampleNavBtn = document.getElementById('sampleNavBtn');

    createTabs('navList', 'panelList');
    createTabs('sourceTab', 'sourcePanel');
}

function intFeaturesNav() {
    var featuresNavBtn = document.querySelector('.features-bar');
    var featuresNav = featuresNavBtn.querySelector('ul');
    new c1.sample.MultiLevelMenu(featuresNav, featuresNavBtn);
}

function gotoDoc(url) {
    window.open(url, '_blank');
}

// the functions for pagegroups.
var allGroups = [];
function removeAllListSelection() {
    if (allGroups.length > 1) {
        for (var i = 0; i < allGroups.length; i++) {
            setListBoxSelectionIndex(allGroups[i], -1);
        }
    }
}

function setListBoxSelectionIndex(controlId, index) {
    var control = wijmo.Control.getControl('#' + controlId);
    if (control) {
        control.selectedIndex = index;
    }
}

function bindMouseDown(id) {
    if (allGroups.length > 1) {
        var ele = document.getElementById(id);
        ele.addEventListener('mousedown', function (event) {
            if (event.button == 0) {
                removeAllListSelection();
            }
        });
    }
}

function pageNav_LoadingItems(sender) {
    sender.selectedIndex = -1;
}

c1.documentReady(function () {
    initTabs();
    intFeaturesNav();
});