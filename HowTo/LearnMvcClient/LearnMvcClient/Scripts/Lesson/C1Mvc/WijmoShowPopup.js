c1.documentReady(function () {
    // generate some random data
    var data = [],
      countries = ['US', 'Germany', 'UK', 'Japan', 'Italy', 'Greece'],
      products = ['Widget', 'Sprocket', 'Gadget', 'Doohickey'],
      colors = ['Black', 'White', 'Red', 'Green', 'Blue'],
      dt = new Date();
    for (var i = 0; i < 100; i++) {
        var date = new Date(dt.getFullYear(), i % 12, 25, i % 24, i % 60, i % 60),
          countryId = Math.floor(Math.random() * countries.length),
          productId = Math.floor(Math.random() * products.length),
          colorId = Math.floor(Math.random() * colors.length);
        var item = {
            id: i,
            start: date,
            end: date,
            country: countries[countryId],
            product: products[productId],
            color: colors[colorId],
            countryId: countryId,
            productId: productId,
            colorId: colorId,
            amount1: Math.random() * 10000 - 5000,
            amount2: Math.random() * 10000 - 5000,
            amount3: Math.random() * 10000 - 5000,
            amount4: Math.random() * 10000 - 5000,
            amount5: Math.random() * 10000 - 5000,
            discount: Math.random() / 4,
            active: i % 4 == 0
        };
        data.push(item);
    }

    var theGrid = wijmo.Control.getControl('#theGrid');
    theGrid.itemsSource = data;
    theGrid.formatItem.addHandler(function (s, e) {
        if (e.panel == s.topLeftCells) {
            e.cell.innerHTML = '<span class="column-picker-icon glyphicon glyphicon-cog"></span>';
        }
    });

    var theColumnPicker = wijmo.Control.getControl('#theColumnPicker');
    theColumnPicker.initialize({
        itemsSource: theGrid.columns,
        checkedMemberPath: 'visible',
        displayMemberPath: 'header',
        lostFocus: function () {
            wijmo.hidePopup(theColumnPicker.hostElement);
        }
    });

    // show the column picker when the user clicks the top-left cell
    var ref = theGrid.hostElement.querySelector('.wj-topleft');
    ref.addEventListener('mousedown', function (e) {
        if (wijmo.hasClass(e.target, 'column-picker-icon')) {
            wijmo.showPopup(theColumnPicker.hostElement, ref, false, true, false);
            theColumnPicker.focus();
            e.preventDefault();
        }
    });

    // save/restore layout buttons
    document.getElementById('btnSave').addEventListener('click', function () {
        localStorage.setItem('myLayout', theGrid.columnLayout);
    });
    document.getElementById('btnRestore').addEventListener('click', function () {
        var layout = localStorage.getItem('myLayout');
        if (layout) {
            theGrid.columnLayout = layout;
        }
    });
});