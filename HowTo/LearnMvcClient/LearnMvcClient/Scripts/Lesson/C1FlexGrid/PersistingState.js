c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');
    var theFilter = c1.getExtender(theGrid, "theGridFilter");

    // save/restore grid state
    document.getElementById('btnSave').addEventListener('click', function () {
        var sorts = theGrid.collectionView.sortDescriptions.map(function (sort) {
            return { property: sort.property, ascending: sort.ascending };
        });

        var state = {
            columns: theGrid.columnLayout,
            filterDefinition: theFilter.filterDefinition,
            sortDescriptions: sorts
        }
        localStorage['gridState'] = JSON.stringify(state);
    });
    document.getElementById('btnRestore').addEventListener('click', function () {
        var json = localStorage['gridState'];
        if (json) {
            var state = JSON.parse(json);

            // restore column layout (order/width)
            theGrid.columnLayout = state.columns;

            // restore filter definitions
            theFilter.filterDefinition = state.filterDefinition;

            // restore sort state
            theGrid.collectionView.sortDescriptions.clear();
            for (var i = 0; i < state.sortDescriptions.length; i++) {
                var sortDesc = state.sortDescriptions[i];
                theGrid.collectionView.sortDescriptions.push(
                    new wijmo.collections.SortDescription(sortDesc.property, sortDesc.ascending)
                );
            }
        }
    });
});