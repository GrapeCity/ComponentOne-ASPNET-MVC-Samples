c1.documentReady(function () {
    var theTree = wijmo.Control.getControl('#theTree');

    // handle checkboxes
    document.getElementById('customCSS').addEventListener('click', function (e) {
        wijmo.toggleClass(theTree.hostElement, 'custom-tree', e.target.checked);
    });
});