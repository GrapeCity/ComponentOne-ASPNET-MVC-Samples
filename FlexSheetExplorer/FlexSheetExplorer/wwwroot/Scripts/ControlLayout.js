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

function initControlNavDropDown() {
    var index = CurrentPageIndex;
    var control = wijmo.Control.getControl('#controlNav');
    control.selectedIndex = index;
}

function gotoDoc(url) {
    window.open(url, '_blank');
}

function fillSummary() {
    var element = document.querySelector('.description');
    if (!element || element.textContent) return;
    var demoDes = document.querySelector('.demo-description>div');
    if (!demoDes) return;
    var demoText = demoDes.textContent || '';
    var findEnd = function (text) {
        var index = demoText.indexOf(text);
        if (index == -1) return false;
        element.textContent = demoText.substring(0, index + 1);
        return true;
    };
    if (findEnd('. ')) return;
    if (findEnd('.\r')) return;
    if (findEnd('.\n')) return;
}

function customChangeRowTotals(olapControl, value) {
    if (olapControl && olapControl.engine) {
        olapControl.engine.showRowTotals = value;
    }
}

function customChangeColumnTotals(olapControl, value) {
    if (olapControl && olapControl.engine) {
        olapControl.engine.showColumnTotals = value;
    }
}

function customChangeShowZeros(olapControl, value) {
    if (olapControl && olapControl.engine) {
        olapControl.engine.showZeros = value;
    }
}

function customChangeChartType(olapControl, value) {
    if (olapControl) {
        olapControl.chartType = value;
    }
}

c1.documentReady(function () {
    fillSummary();
    initTabs();
    initControlNavDropDown();
    intFeaturesNav();
});