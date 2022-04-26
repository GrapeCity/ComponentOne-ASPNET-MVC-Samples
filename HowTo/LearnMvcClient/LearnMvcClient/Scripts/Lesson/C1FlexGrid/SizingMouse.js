c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');
    var theGridNoHeaders = wijmo.Control.getControl('#theGridNoHeaders');

    theGridNoHeaders.headersVisibility = 'None';
    theGridNoHeaders.allowResizing = wijmo.grid.AllowResizing.ColumnsAllCells;
});