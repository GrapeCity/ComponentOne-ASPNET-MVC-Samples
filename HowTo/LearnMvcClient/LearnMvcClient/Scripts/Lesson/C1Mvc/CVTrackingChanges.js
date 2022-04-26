c1.documentReady(function () {
    // grid for editing
    var theGrid = wijmo.Control.getControl('#theGrid');
    var view = theGrid.collectionView;
    view.trackChanges = true;

    // grids to show changes
    var edited = wijmo.Control.getControl('#edited');
    edited.itemsSource = view.itemsEdited;
    var added = wijmo.Control.getControl('#added');
    added.itemsSource = view.itemsAdded;
    var removed = wijmo.Control.getControl('#removed');
    removed.itemsSource = view.itemsRemoved;
});