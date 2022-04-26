c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');
    var view = theGrid.collectionView;
    var countries = 'US,Germany,UK,Japan,Italy,Greece'.split(',');

    view.getError = function (item, property) {
        switch (property) {
            case 'Country':
                return countries.indexOf(item.Country) < 0 ? 'Invalid Country' : '';
            case 'Downloads':
            case 'Sales':
            case 'Expenses':
                return item[property] < 0 ? 'Negative values not allowed!' : '';
            case 'Active':
                return item.Active && item.Country.match(/^(US|UK)$/)
                  ? 'Active items not allowed in the US or UK!'
                  : '';
        }
        return null;
    };

    // use getError to provide form validation
    var theItem = {
        Country: theForm.Country.value,
        Downloads: theForm.Downloads.value,
        Sales: theForm.Sales.value
    }
    document.getElementById('theForm').addEventListener('input', function (e) {
        var theProp = e.target.id;
        theItem[theProp] = e.target.value;
        var err = view.getError(theItem, theProp);
        e.target.setCustomValidity(err);
    });
    document.getElementById('theForm').addEventListener('submit', function (e) {
        e.preventDefault();
    });
});