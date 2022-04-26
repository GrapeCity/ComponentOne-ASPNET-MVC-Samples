c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');
    var filter = c1.getExtender(theGrid, "theGridFilter");

    // filter only by condition (not by value)
    filter.defaultFilterType = wijmo.grid.filter.FilterType.Condition;

    // remove sort buttons
    filter.showSortButtons = false;
});