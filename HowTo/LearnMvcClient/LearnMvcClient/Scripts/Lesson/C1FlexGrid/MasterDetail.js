c1.documentReady(function () {
    // create some random data
    var countries = 'US,Germany,UK,Japan,Italy,Greece'.split(',');
    var products = 'Phones,Cars,Stereos,Watches,Computers'.split(',');
    var data = [];
    for (var i = 0; i < 50; i++) {
        data.push({
            id: i,
            country: countries[i % countries.length],
            product: products[i % products.length],
            date: wijmo.DateTime.addDays(new Date(), i),
            sales: Math.random() * 10000,
            expenses: Math.random() * 5000,
        });
    }

    // show countries in combo
    var theCombo = wijmo.Control.getControl('#theCombo');
    theCombo.selectedIndexChanged.addHandler(function (s, e) {
        view.refresh();
    });

    // show items for the selected country in the detail grid
    var theGridDetail = wijmo.Control.getControl('#theGridDetail');
    theGridDetail.itemsSource = data;

    //show items for the selected country
    var view = theGridDetail.collectionView;
    view.filter = function (item) {
        return item.country == theCombo.text;
    }

    // using a grid as the master
    var theGridMaster = wijmo.Control.getControl('#theGridMaster');
    theGridMaster.itemsSource = data;
    theGridMaster.selectionChanged.addHandler(function (s, e) {
        updateDetailControls();
    });

    // update detail controls when selection changes
    function updateDetailControls() {
        var item = theGridMaster.collectionView.currentItem;
        var bndCtls = document.querySelectorAll('.bnd-ctl');
        for (var i = 0; i < bndCtls.length; i++) {
            var host = bndCtls[i];
            var prop = host.id.substr(3).toLowerCase();
            var ctl = wijmo.Control.getControl(host);
            if (wijmo.isString(item[prop])) {
                ctl.text = item[prop];
            } else {
                ctl.value = item[prop];
            }
        }
    }

    // set a property on the current item
    function setProperty(prop, val) {
        var v = theGridMaster.collectionView;
        v.editItem(v.currentItem);
        v.currentItem[prop] = val;
        v.commitEdit();
    }

    // define detail controls
    var theCountry = wijmo.Control.getControl('#theCountry');
    theCountry.textChanged.addHandler(function (s, e) {
        setProperty('country', s.text);
    });
    var theProduct = wijmo.Control.getControl('#theProduct');
    theProduct.textChanged.addHandler(function (s, e) {
        setProperty('product', s.text);
    });
    var theDate = wijmo.Control.getControl('#theDate');
    theDate.valueChanged.addHandler(function (s, e) {
        setProperty('date', s.value);
    });
    var theSales = wijmo.Control.getControl('#theSales');
    theSales.valueChanged.addHandler(function (s, e) {
        setProperty('sales', s.value);
    });
    var theExpenses = wijmo.Control.getControl('#theExpenses');
    theExpenses.valueChanged.addHandler(function (s, e) {
        setProperty('expenses', s.value);
    });
});