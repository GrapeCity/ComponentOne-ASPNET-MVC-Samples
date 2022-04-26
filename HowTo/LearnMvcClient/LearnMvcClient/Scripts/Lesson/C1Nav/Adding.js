c1.documentReady(function () {
    var theTree = wijmo.Control.getControl('#theTree');
    theTree.itemsSource = getData();
    theTree.displayMemberPath = 'header';
    theTree.childItemsPath = 'items';
    theTree.selectedItem = theTree.itemsSource[0];

    // handle buttons
    document.getElementById('btnBefore').addEventListener('click', function () {
        addNode('before');
    });
    document.getElementById('btnAfter').addEventListener('click', function () {
        addNode('after');
    });
    document.getElementById('btnInto').addEventListener('click', function () {
        addNode('into');
    });

    // add a node
    function addNode(where) {
        var tree = theTree,
          item = tree.selectedItem;
        if (item) {

            // find the array where the new item should be added
            var path = 'items';
            var parent = tree.selectedNode.parentNode;
            var arr = parent ? parent.dataItem[path] : tree.itemsSource;

            // add the new item to the data tree
            var index = 0;
            switch (where) {
                case 'before':
                    index = arr.indexOf(item);
                    break;
                case 'after':
                    index = arr.indexOf(item) + 1;
                    break;
                case 'into':
                    arr = item[path];
                    if (!arr) {
                        arr = item[path] = [];
                    }
                    index = arr.length;
                    break;
            }

            // create the new item
            var newItem = {
                header: document.getElementById('theInput').value
            }
            arr.splice(index, 0, newItem);

            // re-load the tree
            tree.loadTree();

            // select the new item
            tree.selectedItem = newItem;
            tree.focus();
        }
    }

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