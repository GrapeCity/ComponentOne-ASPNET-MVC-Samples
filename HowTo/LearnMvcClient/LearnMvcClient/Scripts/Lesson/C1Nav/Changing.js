c1.documentReady(function () {
    var theTree = wijmo.Control.getControl('#theTree');
    theTree.itemsSource = getData();
    theTree.displayMemberPath = 'header';
    theTree.childItemsPath = 'items';
    theTree.selectedItem = theTree.itemsSource[0];

    // handle buttons
    document.getElementById('btnTree').addEventListener('click', function () {
        var item = changeItem();
        theTree.loadTree();
        theTree.getNode(item).element.textContent = item.header;
    });
    document.getElementById('btnNode').addEventListener('click', function () {
        var item = changeItem();
        theTree.getNode(item).element.textContent = item.header;
    });

    // change an item on the list and return the changed item
    function changeItem() {
        var index = Date.now() % theTree.itemsSource.length,
                item = theTree.itemsSource[index];
        item.header = wijmo.format('Changed at {now:hh:mm:ss}', {
            now: new Date()
        });
        return item;
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