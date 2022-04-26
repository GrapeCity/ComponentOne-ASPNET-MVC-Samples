var tpProducts,
    requestQueue = [],
    basePath = '';

window.addEventListener('resize', relayout, false);

c1.documentReady(function () {
    init();
    relayout();
    getData();
});

function init() {
    tpProducts = wijmo.Control.getControl('#tpProducts');
}

function getData() {
    var pane = tpProducts.selectedTab.pane;
    if (pane.initialized) {
        return;
    }

    pane.initialized = true;
    requestQueue.push({
        target: pane,
        type: tpProducts.selectedIndex
    });

    if (requestQueue.length > 1) {
        return;
    }

    requestProducts();
}

function sendRequest(req) {
    var success = function (data) {
        buildGrid(req.target, data);
        if (requestQueue.length > 0) {
            requestProducts();
        }
    };

    ajaxRequest(req.url, {
        productType: req.type
    }, success);
}

// create the product list.
function buildGrid(target, data) {
    var host = window.location.protocol + '//' + window.location.hostname;
    if (window.location.port != 0) {
        host += ':' + window.location.port;
    }

    if (basePath.length > 0) {
        var separator = basePath.indexOf('/')==0 ? '' : '/';
        host += separator + basePath;
    }

    var items = data.items;
    var productContent = '';
    for (var i = 0; i < items.length; i++) {
        var item = items[i];
        productContent += '<div class="product-item">';
        productContent += '<img src="' + host + item.ImagePath + '" />';
        productContent += '<span>' + item.Label + '</span>';
        productContent += '</div>';
    }
    target.innerHTML = productContent;
}

function selectedIndexChanged(sender, e) {
    getData();
}

function relayout() {
    var rectTPProducts = tpProducts.hostElement.getBoundingClientRect(),
        tpHeaders = tpProducts.hostElement.querySelector('div.wj-tabheaders'),
        tpPanes = tpProducts.hostElement.querySelector('div.wj-tabpanes');

    tpPanes.style.height = (rectTPProducts.height - tpHeaders.getBoundingClientRect().height) + 'px';
}