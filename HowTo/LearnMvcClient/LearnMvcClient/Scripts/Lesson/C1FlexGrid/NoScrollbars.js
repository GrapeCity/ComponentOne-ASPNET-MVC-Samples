c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');

    // support scrolling with the wheel
    theGrid.hostElement.addEventListener('wheel', function (e) {
        root = theGrid.hostElement.querySelector('[wj-part="root"]');
        if (root) {
            root.scrollTop += 32 * (e.deltaY < 0 ? -1 : +1);
            e.preventDefault();
        }
    });
});