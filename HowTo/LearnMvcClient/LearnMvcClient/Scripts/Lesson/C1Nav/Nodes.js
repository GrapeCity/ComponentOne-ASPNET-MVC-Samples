c1.documentReady(function () {
    var theTree = wijmo.Control.getControl('#theTree');

    // scan visible nodes now and when the user clicks the button
    scanNodes(true);
    document.getElementById('scan').addEventListener('click', function (e) {
        scanNodes(true);
    });

    // scan nodes and show their information
    function scanNodes(visible) {
        var result = '',
                cnt = 0;
        for (var node = theTree.nodes[0]; node; node = node.next(visible)) {
            cnt++;
            result +=
              wijmo.format('{Header}', node.dataItem) +
              wijmo.format(' <span class="node-info">(level: {level}, index: {index}, isCollapsed: {isCollapsed})</span><br/>', node);
        }
        document.getElementById('scanResult').innerHTML = result;
    }
});