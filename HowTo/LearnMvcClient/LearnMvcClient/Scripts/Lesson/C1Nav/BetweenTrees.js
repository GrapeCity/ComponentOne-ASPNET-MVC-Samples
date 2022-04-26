c1.documentReady(function () {
    var firstTree = wijmo.Control.getControl('#firstTree');
    firstTree.itemsSource = [
        {
            header: 'root 1', items: [
            { header: 'Item 1.1' },
            { header: 'Item 1.2' },
            { header: 'Item 1.3' }]
        }
    ];
    firstTree.displayMemberPath = 'header';
    firstTree.childItemsPath = "items";
    firstTree.allowDragging = true;
    firstTree.dragOver.addHandler(dragOverBetweenTrees);

    var secondTree = wijmo.Control.getControl('#secondTree');
    secondTree.itemsSource = [
        {
            header: 'root 2', items: [
            { header: 'Item 2.1' },
            { header: 'Item 2.2' },
            { header: 'Item 2.3' }]
        }
    ];
    secondTree.displayMemberPath = 'header';
    secondTree.childItemsPath = "items";
    secondTree.allowDragging = true;
    secondTree.dragOver.addHandler(dragOverBetweenTrees);

    // handle drag-drop within or between trees
    function dragOverBetweenTrees(s, e) {
        var t1 = e.dragSource.treeView;
        var t2 = e.dropTarget.treeView;

        // prevent dragging within trees
        if (t1 == t2 && !document.getElementById('dragWithin').checked) {
            e.cancel = true;
        }

        // allow dragging between trees
        if (t1 != t2 && document.getElementById('dragBetween').checked) {
            e.cancel = false;
        }
    }
});