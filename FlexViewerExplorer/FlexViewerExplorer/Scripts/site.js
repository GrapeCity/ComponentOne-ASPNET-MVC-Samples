function hasClass(obj, cls) {
    return obj && obj.className.match(new RegExp('(\\s|^)' + cls + '(\\s|$)'));
}

function addClass(obj, cls) {
    if (!this.hasClass(obj, cls)) obj.className += ' ' + cls;
}

function removeClass(obj, cls) {
    if (hasClass(obj, cls)) {
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
        event.preventDefault();
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

var c1WebApiServiceUrl = siteRootUrl + "api/report/";
var arServiceUrl = 'https://ardemos.grapecity.com/AR12-ReportsGallery/ActiveReports.ReportService.asmx';

function loadFlexReport(filePath, reportName) {
    loadReport(c1WebApiServiceUrl, filePath, reportName);
}

function loadArReport(reportPath) {
    loadReport(arServiceUrl, reportPath, '')
}

function loadSsrsReport(reportPath) {
    loadReport(c1WebApiServiceUrl, reportPath);
}

function loadReport(serviceUrl, filePath, reportName) {
    var reportViewer = wijmo.Control.getControl('#reportViewer');
    if (!reportViewer) {
        return;
    }

    var hidden = hasClass(document.getElementById('reportViewerContainer'), 'hidden');
    if (hidden) {
        removeClass(document.getElementById('reportViewerContainer'), 'hidden');
        addClass(document.getElementById('pdfViewerContainer'), 'hidden');
        reportViewer.refresh();
    }

    reportViewer.serviceUrl = serviceUrl;
    reportViewer.filePath = filePath;
    reportViewer.reportName = reportName;
}

function loadPdf(filePath) {
    var pdfViewer = wijmo.Control.getControl('#pdfViewer');
    if (!pdfViewer) {
        return;
    }

    var hidden = hasClass(document.getElementById('pdfViewerContainer'), 'hidden');
    if (hidden) {
        removeClass(document.getElementById('pdfViewerContainer'), 'hidden');
        addClass(document.getElementById('reportViewerContainer'), 'hidden');
        pdfViewer.refresh();
    }

    pdfViewer.filePath = filePath;
}

function updateTitle(viewerName, reportName) {
    var span = document.querySelector('div.home-title > span');
    span.innerHTML = viewerName +　' - ' + reportName;
}

function flexGrid_ItemFormatter(sender, args) {
    var panel = args.panel,
        r = args.row,
        count = sender.updateCount,
        maxCount = sender.maxUpdateCount;

    if (wijmo.grid.CellType.Cell == panel.cellType) {
        var gr = wijmo.tryCast(sender.rows[r], wijmo.grid.GroupRow);
        if (!count && count != 0) {
            count = 0;
            sender.updateCount = 0;
        }
        if (!maxCount && maxCount != 0) {
            maxCount = 1;
            sender.maxUpdateCount = 1;
        }

        if (gr && count <= maxCount && gr.level == maxCount - count) {
            gr.isCollapsed = true;
        }
    }
}

function flexGrid_UpdatedView(sender, args) {
    if (!sender.updateCount) {
        sender.updateCount = 0;
    }
    if (!sender.maxUpdateCount) {
        sender.maxUpdateCount = 1;
    }
    var count = sender.updateCount + 1;
    sender.updateCount = count;
    if (count > sender.maxUpdateCount) {
        sender.formatItem.removeHandler(flexGrid_ItemFormatter);
        sender.updatedView.removeHandler(flexGrid_UpdatedView, sender);
    }
}

function createCollapseStruct(titleEle, toggleClassName) {
    titleEle.addEventListener('click', function (event) {
        var list = event.currentTarget.nextElementSibling;
        if (list) {
            if (hasClass(list, toggleClassName)) {
                var grid = wijmo.Control.getControl('#sNavList');
                grid.invalidate();
            }
            toggleClass(list, toggleClassName);
            event.preventDefault();
        }
    });
}

function isMobile() {
    return navigator.userAgent.match(/Android/i)
        || navigator.userAgent.match(/webOS/i)
        || navigator.userAgent.match(/BlackBerry/i)
        || navigator.userAgent.match(/Windows Phone/i)
        || navigator.userAgent.match(/iPhone/i)
        || navigator.userAgent.match(/iPod/i);
}

function countdown(countdownText, counter) {
    if (counter > 0) {
        countdownText.innerText = counter + 's';
        counter--;
        setTimeout(function () {
            countdown(countdownText, counter);
        }, 1000);
    } else {
        showFullScreenPopup(false);
    }
}

function showFullScreenPopup(show) {
    var popup = wijmo.Control.getControl('#popupFullScreen');
    if (show) {
        popup.show();
        countdown(document.getElementById('countdownText'), 20);
    } else {
        popup.hide();
    }
}

function fullScreenViewer() {
    var reportViewer = wijmo.Control.getControl('#reportViewer'),
        pdfViewer = wijmo.Control.getControl('#pdfViewer');

    showFullScreenPopup(false);
    reportViewer.fullScreen = true;
    pdfViewer.fullScreen = true;
}

c1.documentReady(function () {
    var titles = document.querySelectorAll('.collapse-title'),
        sampleNavBtn = document.querySelector('#sampleNavBtn'),
        li = document.createElement('li'),
        docs = document.getElementById('docs'),
        ul = document.getElementById('navList');

    createTabs('navList', 'panelList');
    createTabs('sourceTab', 'sourcePanel');

    if (docs) {
        docs.style.display = '';
        li.appendChild(docs);
        ul.appendChild(li);
    }

    SyntaxHighlighter.all();
    SyntaxHighlighter.defaults['toolbar'] = false;
    SyntaxHighlighter.defaults['quick-code'] = false;

    for (var i = 0; i < titles.length; i++) {

        if (titles[i] === sampleNavBtn) {
            createCollapseStruct(sampleNavBtn, 'mob-hide-0');
        } else {
            createCollapseStruct(titles[i], 'mob-hide-1');
        }
    }

    if (isMobile()) {
        showFullScreenPopup(true);
    }
});
