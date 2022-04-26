c1.documentReady(function () {
    var cmbLevels = wijmo.Control.getControl('#cmbLevels');
    var cmbNodesPerLevel = wijmo.Control.getControl('#cmbNodesPerLevel');
    var theTree = wijmo.Control.getControl('#theTree');

    theTree.itemsSource = getData();
    theTree.displayMemberPath = 'header';
    theTree.childItemsPath = 'items';

    // re-bind tree
    document.getElementById('bind').addEventListener('click', function (e) {
        var start = Date.now();
        theTree.itemsSource = getData();
        theTree.loadTree(); // force immediate refresh
        var msg = wijmo.format('Bound to <b>{cnt:no}</b> nodes in <b>{ms:n0}</b> ms.', {
            cnt: theTree.totalItemCount,
            ms: Date.now() - start
        });
        document.getElementById('bindingMsg').innerHTML = msg;
    });
    // get the data
    function getData() {
        var cnt = cmbNodesPerLevel.selectedValue,
                levels = cmbLevels.selectedValue;
        nodes = [];
        for (var i = 0; i < cnt; i++) {
            nodes.push(getNode(0, i, cnt, levels))
        }
        return nodes;
    }
    function getNode(level, id, cnt, levels) {

        // create node
        var node = {
            header: 'Node ' + (level + 1) + '.' + (id + 1),
        };

        // create child nodes
        if (level < levels - 1) {
            node.items = [];
            for (var i = 0; i < cnt; i++) {
                node.items.push(getNode(level + 1, i, cnt, levels))
            }
        }

        // done
        return node;
    }
});