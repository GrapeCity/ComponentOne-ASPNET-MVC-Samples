c1.documentReady(function () {
    var theTree = wijmo.Control.getControl('#theTree');
    theTree.itemsSource = getData();
    theTree.displayMemberPath = 'header';
    theTree.childItemsPath = 'items';

    // update button state
    theTree.selectedItemChanged.addHandler(function (s, e) {
        var btn = document.getElementById('btnRemove');
        wijmo.setAttribute(btn, 'disabled', theTree.selectedItem ? null : 'disabled');
    });
    theTree.selectedItem = theTree.itemsSource[0];

    // handle buttons
    document.getElementById('btnRemove').addEventListener('click', function () {
        if (theTree.selectedItem) {

            // find the array where the new item should be added
            var parent = theTree.selectedNode.parentNode;
            var arr = parent
              ? parent.dataItem[theTree.childItemsPath]
              : theTree.itemsSource;

            // remove the item from the parent collection
            var index = arr.indexOf(theTree.selectedItem);
            arr.splice(index, 1);

            // refresh the tree
            theTree.loadTree();
        }
    });

    // create some data
    function getData() {
        return [
            {
                header: 'Parent 1', items: [
                { header: 'Child 1.1' },
                { header: 'Child 1.2' },
                { header: 'Child 1.3' }]
            },
          {
              header: 'Parent 2', items: [
              { header: 'Child 2.1' },
              { header: 'Child 2.2' }]
          },
          {
              header: 'Parent 3', items: [
              { header: 'Child 3.1' }]
          }
        ];
    }
});