c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');

    // create a group to show the grand totals
    var grandTotalsGroup = new wijmo.collections.PropertyGroupDescription('Grand Totals',
        function (item, propName) {
            return '';
        }
    );

    theGrid.getColumn('Sales').aggregate = 'Sum';
    theGrid.getColumn('Expenses').aggregate = 'Sum';


    theGrid.collectionView.groupDescriptions.push(grandTotalsGroup);

    theGrid.frozenRows = 1;
});