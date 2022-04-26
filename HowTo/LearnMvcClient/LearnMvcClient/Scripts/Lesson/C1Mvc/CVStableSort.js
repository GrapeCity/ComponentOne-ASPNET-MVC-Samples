c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');
    var view = theGrid.collectionView;
    view.useStableSort = true;
});