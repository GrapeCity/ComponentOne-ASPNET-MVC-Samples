c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');
    var countries = 'US,Germany,UK,Japan,Sweden,Norway,Denmark'.split(',');

    theGrid.columns[1].dataMap = countries;


    var view = theGrid.collectionView;
    view.getError = function (item, prop) {
        if (prop == 'Sales' && item.Sales < 0) {
            return 'Sales cannot be negative!';
        }
        if (prop == 'Expenses' && item.Expenses < 0) {
            return 'Expenses cannot be negative!';
        }
    }


});