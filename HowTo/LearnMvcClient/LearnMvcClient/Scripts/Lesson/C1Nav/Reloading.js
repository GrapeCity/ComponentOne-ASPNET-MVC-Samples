c1.documentReady(function () {
    var theTree = wijmo.Control.getControl('#theTree');

    theTree.itemsSource = getData();

    // load elements with a simulated http delay
    theTree.lazyLoadFunction = function (node, callback) {
        setTimeout(function () {
            var result = getLazyData();
            callback(result);
        }, 1000);
    };

    // when collapsing a node with 'reload' set to true, 
    // clear its contents to reload later
    theTree.isCollapsedChanging.addHandler(function (s, e) {
        var node = e.node,
            tree = node.treeView;
        if (!node.isCollapsed && node.dataItem.reload == true) {

            // remove previous lazy-loaded items from data source
            node.dataItem.items = [];

            // re-bind the tree to remove the old nodes
            tree.loadTree();
        }
    });

    // starting data
    function getData() {
        return [
            { header: 'Electronics <i>(reload when opening)</i>', items: [], reload: true },
            { header: 'Toys <i>(load once)</i>', items: [] },
            { header: 'Home <i>(load once)</i>', items: [] },
        ];
    }

    // lazy data (re-loaded when opening nodes)
    function getLazyData() {
        var creationTime = wijmo.Globalize.format(new Date(), 'hh:mm:ss');
        return [
            { header: 'Empty Node at: ' + creationTime },
            {
                header: 'Node with child nodes at: ' + creationTime, items: [
                    { header: 'hello' },
                    { header: 'world' }]
            },
            { header: 'Lazy node <i>(reload when opening)</i>', items: [], reload: true },
        ]
    }
});