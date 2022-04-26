c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');

    // scroll into view
    document.getElementById('scrollIntoView').addEventListener('click', function () {
        theGrid.scrollIntoView(100, -1);
    });

    // set top cell
    document.getElementById('scrollToTop').addEventListener('click', function () {
        if (true) {

            // get cell rect, adjust scrollPosition.y to bring it to the top
            var rc = theGrid.cells.getCellBoundingRect(100, 0, true);
            theGrid.scrollPosition = new wijmo.Point(theGrid.scrollPosition.x, -rc.top);

        } else {

            // simpler but less efficient (requires a timeOut)
            theGrid.scrollIntoView(theGrid.rows.length - 1, -1);
            setTimeout(function () {
                theGrid.scrollIntoView(100, -1);
            })
        }
    });
});