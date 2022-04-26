c1.documentReady(function () {
    var tree = wijmo.Control.getControl('#theTree');
    tree.allowDragging = true;

    // use the dragStart event to honor the allowDraggingParentNodes setting
    // by setting the 'cancel' event parameter to true
    tree.dragStart.addHandler(function(s, e) {
        if (e && e.node && e.node.hasChildren) {
            if (!document.getElementById('allowDraggingParentNodes').checked) {
                e.cancel = true; // prevent dragging parent nodes
            } else {
                e.node.isCollapsed = true; // collapse parent nodes when dragging
            }
        }
    });

    // use the dragOver event to honor the allowDroppingIntoEmpty setting
    // by changing the 'position' event parameter to 'Before'
    tree.dragOver.addHandler(function (s, e) {
        if (!document.getElementById('allowDroppingIntoEmpty').checked &&
    !e.dropTarget.hasChildren &&
    e.position == wijmo.nav.DropPosition.Into) {
            e.position = wijmo.nav.DropPosition.Before;
        }
    });
  
    // handle options
    document.getElementById('allowDragging').addEventListener('click', function (e) {
        tree.allowDragging = e.target.checked;
    });

});