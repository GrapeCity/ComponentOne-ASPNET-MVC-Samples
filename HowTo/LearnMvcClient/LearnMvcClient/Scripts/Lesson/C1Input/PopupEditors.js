c1.documentReady(function () {

    // create some random data
    var countries = 'US,Germany,UK,Japan,Sweden,Norway,Denmark'.split(',');
    var data = [];
    for (var i = 0; i < 100; i++) {
        data.push({
            id: i,
            country: countries[i % countries.length],
            sales: Math.random() * 10000,
            expenses: Math.random() * 5000,
            overdue: (i + 1) % 4 == 0
        });
    }

    // the grid with custom editing behavior
    var theGrid = wijmo.Control.getControl('#theGrid');
    theGrid.itemsSource = data;

    // add 'edit button' to row header cells
    theGrid.formatItem.addHandler(function (s, e) {
        if (e.panel == s.rowHeaders && e.col == 0) {
            e.cell.innerHTML = '<span class="wj-glyph-pencil"></span>';
        }
    });

    // handle button clicks
    theGrid.addEventListener(theGrid.hostElement, 'click', function (e) {
        var ht = theGrid.hitTest(e);
        if (ht.panel == theGrid.rowHeaders) {

            // prepare form
            var item = theGrid.rows[ht.row].dataItem;
            formControls.itemId.textContent = item.id;
            formControls.country.text = item.country;
            formControls.sales.value = item.sales;
            formControls.expenses.value = item.expenses;

            // show the form
            formControls.popup.show(true, function (e) {
                if (e.dialogResult == 'wj-hide-ok') {

                    // commit changes if the user pressed the OK button
                    theGrid.collectionView.editItem(item);
                    item.country = formControls.country.text;
                    item.sales = formControls.sales.value;
                    item.expenses = formControls.expenses.value;
                    theGrid.collectionView.commitEdit();
                }

                // return focus to the grid
                theGrid.focus();
            });
        }
    });

    // attach controls to input form
    var formControls = {
        popup: wijmo.Control.getControl('#popup'),
        itemId: document.getElementById('item-id'),
        country: wijmo.Control.getControl('#country'),
        sales: wijmo.Control.getControl('#sales'),
        expenses: wijmo.Control.getControl('#expenses'),
    };
});