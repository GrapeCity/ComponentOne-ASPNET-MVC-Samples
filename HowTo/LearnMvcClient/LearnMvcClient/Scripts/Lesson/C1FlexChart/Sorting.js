c1.documentReady(function () {
    var theChart = wijmo.Control.getControl('#theChart');

    // connect sort buttons
    sortOnClick('btnNone', null);
    sortOnClick('btnCountry', 'Country');
    sortOnClick('btnSales', 'Sales');
    sortOnClick('btnExpenses', 'Expenses');
    sortOnClick('btnDownloads', 'Downloads');

    function sortOnClick(id, prop) {
        document.getElementById(id).addEventListener('click', function () {
            var sd = theChart.collectionView.sortDescriptions;
            sd.clear();
            sd.push(new wijmo.collections.SortDescription(prop, true))
        })
    }
});