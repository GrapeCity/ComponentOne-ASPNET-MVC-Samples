c1.documentReady(function () {

    // create a list of items with header and detail properties
    var treeData = [];
    var data = getData();
    for (var i = 0; i < data.length; i++) {
        treeData.push({
            header: data[i].name,
            detail: [data[i]]
        });
    }

    // define template for the details
    var itemTemplate = '<div class="item">' +
      '<b>{city}</b> ({state})<br/>' +
      '{address}<br/>' +
      'Phone: <b>{phone}</b><br/>' +
      'Fax: <b>{fax}</b><br/>' +
      'Website: <a href="{site}">{site}</a><br/>' +
      '</div>';

    var theTree = wijmo.Control.getControl('#theTree');
    theTree.displayMemberPath = 'header';
    theTree.childItemsPath = 'detail';
    theTree.itemsSource = treeData;
    theTree.formatItem.addHandler(function (s, e) {
        switch (e.level) {

            // level 0: wrap header in an H1 tag
            case 0:
                e.element.innerHTML = '<h1>' + e.dataItem.header + '<h1>';
                break;

                // level 1: use template to create details
            case 1:
                var html = wijmo.format(itemTemplate, e.dataItem, function (data, name, fmt, val) {
                    if (wijmo.isString(data[name])) {
                        val = wijmo.escapeHtml(data[name]);
                    }
                    return val;
                });
                e.element.innerHTML = html;
                break;
        }
    });

    // expand selected items, show selection
    theTree.selectedItemChanged.addHandler(function (s, e) {
        var node = theTree.selectedNode;
        if (node && node.parentNode) {
            node = theTree.selectedNode = node.parentNode;
        }
        node.isCollapsed = false;
        document.getElementById('selected').textContent = node.dataItem.header;
    });
    theTree.selectedItem = theTree.itemsSource[0];

    // handle up-arrow key to skip details
    theTree.hostElement.addEventListener('keydown', function (e) {
        var node = null;
        switch (e.keyCode) {
            case wijmo.Key.Up:
                node = theTree.selectedNode.previousSibling(true);
                break;
            case wijmo.Key.Down:
                node = theTree.selectedNode.nextSibling(true);
                break;
        }
        if (node) {
            theTree.selectedNode = node;
            e.preventDefault();
        }
    });

    // some data to show in our accordion
    function getData() {
        return [{
            name: 'Electro Mart',
            city: 'Accident',
            state: 'Maryland',
            address: '8785 Windfall St.',
            phone: '(800) 555-1234',
            fax: '(800) 555-5678',
            site: 'http://www.electromartnot.com'
        }, {
            name: 'Sue\'s Depot',
            city: 'Big Arm',
            state: 'Montana',
            address: '77 Winchester Lane',
            phone: '(800) 555-2345',
            fax: '(800) 555-6789',
            site: 'http://www.suesdepotnot.com'
        }, {
            name: 'D&K Digital Locker',
            city: 'Chicken',
            state: 'Alaska',
            address: '787 Lakeview St. ',
            phone: '(800) 555-3456',
            fax: '(800) 555-7890',
            site: 'http://www.digilockernot.com'
        }, {
            name: 'Paul\'s Pub & Bistro',
            city: 'Coupon',
            state: 'Pennsylvania',
            address: '711 Old York Drive ',
            phone: '(800) 555-0987',
            fax: '(800) 555-6543',
            site: 'http://www.paulspubnot.com'
        }, {
            name: 'Amazing Deals Inc',
            city: 'Cut And Shoot',
            state: 'Texas',
            address: '91 Beech St.',
            phone: '(800) 955-2109',
            fax: '(800) 955-8765',
            site: 'http://www.amazingdealsnot.com'
        }];
    }
});