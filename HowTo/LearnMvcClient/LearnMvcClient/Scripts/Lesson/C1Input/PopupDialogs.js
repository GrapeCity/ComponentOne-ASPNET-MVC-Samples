c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');
    var theDialog = wijmo.Control.getControl('#theDialog');

    theGrid.itemsSource = getData();

    // use ctrl+Delete to delete the current row
    theGrid.hostElement.addEventListener('keydown', function (e) {
        var view = theGrid.collectionView;

        // looking for ctrl+Delete
        if (e.ctrlKey && e.keyCode == wijmo.Key.Delete && view.currentItem) {

            // prevent the grid from getting the key
            e.preventDefault();

            // confirm row deletion
            theDialog.show(true, function (sender) {

                // delete the row
                if (sender.dialogResult == 'wj-hide-ok') {
                    view.remove(view.currentItem);
                }

                // return focus to the grid
                theGrid.focus();
            });
        }
    }, true); // grab the event before the grid

    // generate some random data
    function getData() {
        var countries = 'US,Canada,Mexico,Germany,UK,France,Italy,Greece,Holland,Japan,Korea,China'.split(','),
        data = [];
        for (var i = 0; i < 1000; i++) {
            data.push({
                id: i,
                country: countries[i % countries.length],
                sales: Math.random() * 10000,
                expenses: Math.random() * 5000,
                active: i % 3 == 0
            });
        }
        return data;
    }
});