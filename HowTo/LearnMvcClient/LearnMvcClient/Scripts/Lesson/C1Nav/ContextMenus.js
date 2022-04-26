c1.documentReady(function () {
    var theMenu = wijmo.Control.getControl('#theMenu');
    theMenu.itemClicked.addHandler(function (s, e) {
        alert('thanks for selecting ' + s.selectedValue.Header);
    });

    var theTree = wijmo.Control.getControl('#theTree');
    theTree.itemsSource = getData();

    // attach context menu to tree
    theTree.hostElement.addEventListener('contextmenu', function (e) {
        e.preventDefault();
        theMenu.show(e);
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