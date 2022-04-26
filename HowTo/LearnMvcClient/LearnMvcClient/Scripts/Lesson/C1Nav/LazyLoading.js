c1.documentReady(function () {
    var tree = wijmo.Control.getControl('#theTree');
    tree.itemsSource = getData();
    tree.displayMemberPath = 'header';
    tree.childItemsPath = 'items';
    tree.lazyLoadFunction = lazyLoadFunction;

    // start with three lazy-loaded nodes
    function getData() {
        return [
            { header: 'Lazy Node 1', items: [] },
            { header: 'Lazy Node 2', items: [] },
            { header: 'Lazy Node 3', items: [] }
        ];
    }

    // function used to lazy-load node content
    function lazyLoadFunction(node, callback) {
        setTimeout(function () { // simulate http delay
            var result = [ // simulate result
                { header: 'Another lazy node...', items: [] },
                { header: 'A non-lazy node without children' },
                {
                    header: 'A non-lazy node with child nodes', items: [
                        { header: 'hello' },
                        { header: 'world' }]
                }];
            callback(result); // return result to control
        }, 2500); // 2.5sec http delay
    }
});