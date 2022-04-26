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

    createTabs('navList', 'panelList');
    createTabs('sourceTab', 'sourcePanel');
}

c1.documentReady(function () {
    initTabs();
});

function hasClass(obj, cls) {
    return obj && obj.className.match(new RegExp('(\\s|^)' + cls + '(\\s|$)'));
}

function addClass(obj, cls) {
    if (obj && !this.hasClass(obj, cls)) obj.className += ' ' + cls;
}

function removeClass(obj, cls) {
    if (obj && hasClass(obj, cls)) {
        var reg = new RegExp('(\\s|^)' + cls + '(\\s|$)');
        obj.className = obj.className.replace(reg, ' ');
    }
}

function toggleClass(obj, cls) {
    if (hasClass(obj, cls)) {
        removeClass(obj, cls);
    } else {
        addClass(obj, cls);
    }
}