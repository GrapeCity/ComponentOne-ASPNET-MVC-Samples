c1.documentReady(function () {
    // create a group to show the grand totals
    var grandTotalsGroup = new wijmo.collections.PropertyGroupDescription('Grand Total',
        function (item, propName) {
            return '';
        }
    );
    var countryGroup = new wijmo.collections.PropertyGroupDescription('Country');

    var theGrid = wijmo.Control.getControl('#theGrid');
    theGrid.collectionView.groupDescriptions.push(grandTotalsGroup);
    theGrid.collectionView.groupDescriptions.push(countryGroup);

    // start collapsed
    theGrid.collapseGroupsToLevel(1);

    // custom cell calculation
    theGrid.formatItem.addHandler(function (s, e) {

        // cells and column footer panels only
        if (e.panel == s.cells) {

            // get row, column, and data item (or group description)
            var r = s.rows[e.row];
            var c = s.columns[e.col];
            var item = s.rows[e.row].dataItem;
            var group = r instanceof wijmo.grid.GroupRow ? item : null;

            // assume value is not negative
            var negative = false;

            // calculate profit
            if (c.binding == 'Profit') {
                var profit = group
                  ? group.getAggregate('Sum', 'Sales') - group.getAggregate('Sum', 'Expenses')
                  : item.Sales - item.Expenses;
                e.cell.textContent = wijmo.Globalize.format(profit, c.format);
                negative = profit < 0;
            }

            // update 'negative' class on cell
            wijmo.toggleClass(e.cell, 'negative', negative);
        }

    });
});