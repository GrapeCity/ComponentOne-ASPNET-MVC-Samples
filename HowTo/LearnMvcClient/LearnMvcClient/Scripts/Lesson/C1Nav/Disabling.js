c1.documentReady(function () {
    var tree = wijmo.Control.getControl('#theTree');

    document.getElementById('btnDisableNode').addEventListener('click', function (e) {
        var nd = tree.selectedNode;
        if (nd) {
            nd.isDisabled = true;
        }
    });
    document.getElementById('btnEnableAllNodes').addEventListener('click', function (e) {
        for (var nd = tree.getFirstNode() ; nd; nd = nd.next()) {
            nd.isDisabled = false;
        }
    });
});