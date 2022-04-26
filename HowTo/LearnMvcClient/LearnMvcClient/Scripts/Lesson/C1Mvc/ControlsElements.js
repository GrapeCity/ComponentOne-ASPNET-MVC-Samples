c1.documentReady(function () {
    var grid = wijmo.Control.getControl('#theGrid');

    // toggle grid's font
    document.getElementById('toggleFont').addEventListener('click', function () {

        // get control being hosted by element #theGrid
        var ctl = wijmo.Control.getControl('#theGrid');

        // get host element from 'grid' control
        var host = ctl.hostElement;

        // toggle font weight
        var style = host.style;
        style.fontWeight = style.fontWeight ? '' : 'bold';
    });

    // resize the grid the WRONG way
    document.getElementById('resizeBad').addEventListener('click', function () {
        var style = grid.hostElement.style;
        style.height = style.height ? '' : '100px';
    });

    // resize the grid the RIGHT way
    document.getElementById('resizeGood').addEventListener('click', function () {
        var style = grid.hostElement.style;
        style.height = style.height ? '' : '100px';
        grid.invalidate(); // update control layout after resizing the element

        // or (if you have many controls)
        //wijmo.Control.invalidateAll();
    });
});