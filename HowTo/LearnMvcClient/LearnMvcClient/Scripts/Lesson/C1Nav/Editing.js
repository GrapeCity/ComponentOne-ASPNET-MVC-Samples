c1.documentReady(function () {
    var tree = wijmo.Control.getControl('#theTree');

    tree.isReadOnly = false;
    tree.nodeEditStarting.addHandler(function (s, e) {
        if (e.node.hasChildren) {
            e.cancel = true;
        }
    });
});