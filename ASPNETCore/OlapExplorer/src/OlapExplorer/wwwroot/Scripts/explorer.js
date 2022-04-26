function hasClass(obj, cls) {
    return obj && obj.className.match(new RegExp('(\\s|^)' + cls + '(\\s|$)'));
}

function addClass(obj, cls) {
    if (obj && !this.hasClass(obj, cls)) obj.className += " " + cls;
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

function toggleTabPanelVisibility(tabs, panels, i) {
    var activeID = 0, j;
    for (j = 0; j < tabs.length; j++) {
        if (tabs[j].parentElement.className.indexOf("active") >= 0) {
            activeID = j;
            break;
        }
    }
    if (activeID != i) {
        removeClass(tabs[activeID].parentElement, "active");
        removeClass(panels[activeID], "active");
        addClass(tabs[i].parentElement, "active");
        addClass(panels[i], "active");
    }
    //event.preventDefault();
}

function addEventListenerForTab(tabs, panels, i) {
    tabs[i].addEventListener("click", function (event) {
        toggleTabPanelVisibility(tabs, panels, i);

        if (tabs[i].id !== "docs") {
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

    tabs = tabCon.getElementsByTagName("a");
    panel = document.getElementById(panelId);
    panels = new Array();

    for (i = 0; i < panel.children.length; i++) {
        className = panel.children[i].className;
        if (className && className.indexOf("tab-pane") >= 0) {
            panels.push(panel.children[i]);
        }
    }
    addClass(tabs[0].parentElement, "active");
    addClass(panels[0], "active");
    for (i = 0; i < tabs.length; i++) {
        addEventListenerForTab(tabs, panels, i);
    }
}

function createCollapseStruct(titleEle, toggleClassName) {
    titleEle.addEventListener("click", function (event) {
        var list = event.currentTarget.nextElementSibling;
        if (list) {
            toggleClass(list, toggleClassName);
            event.preventDefault();
        }
    });
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

document.onreadystatechange = function () {
    if (document.readyState !== "complete") {
        return;
    }

    var //li = document.createElement("li"),
        //docs = document.getElementById("docs"),
        //ul = document.getElementById("navList"),
        titles = document.getElementsByClassName("collapse-title"),
        sampleNavBtn = document.getElementById("sampleNavBtn"),
        controlNavBtn = document.getElementById("controlNavBtn");

    createTabs("navList", "panelList");
    createTabs("sourceTab", "sourcePanel");

    //add documentation tab to the navTabs.
    //if (docs) {
    //    docs.style.display = "";
    //    li.appendChild(docs);
    //    ul.appendChild(li);
    //}

    SyntaxHighlighter.all();
    SyntaxHighlighter.defaults['toolbar'] = false;
    SyntaxHighlighter.defaults['quick-code'] = false;

    for (var i = 0; i < titles.length; i++) {

        if (titles[i] === sampleNavBtn) {
            createCollapseStruct(sampleNavBtn, "mob-hide-0");
        } else if (titles[i] === controlNavBtn) {
            createCollapseStruct(controlNavBtn, "hide");
        } else {
            createCollapseStruct(titles[i], "mob-hide-1");
        }
    }

    document.addEventListener("click", function (event) {
        var list = controlNavBtn ? controlNavBtn.nextElementSibling : null;
        if (list && !list.contains(event.target) &&
            event.target !== controlNavBtn) {
            if (!hasClass(list, "hide")) {
                addClass(list, "hide");
            }
        }
    });
}


