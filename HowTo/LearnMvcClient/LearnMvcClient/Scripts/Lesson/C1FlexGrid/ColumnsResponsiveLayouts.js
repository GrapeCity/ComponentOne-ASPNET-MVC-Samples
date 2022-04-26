c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');
    theGrid.formatItem.addHandler(function (s, e) { // show company summary on narrow layout
        if (e.panel == s.cells && e.col == 0) {
            var html = wijmo.format('<div class="item-header">{CompanyName}</div>' +
                '<div class="item-detail">{ContactName}, {ContactTitle}</div>' +
      '<div class="item-detail">{Address}, {City}, {Country}</div>',
      s.rows[e.row].dataItem);
            e.cell.innerHTML = html;
        }
    });

    // store default row height
    var defaultRowHeight = theGrid.rows.defaultSize;

    // make it responsive
    theGrid.addEventListener(window, 'resize', updateGridLayout);
    function updateGridLayout() {

        // show/hide columns
        var narrow = theGrid.hostElement.clientWidth < 600;
        theGrid.columns.forEach(function (col) {
            col.visible = col.index == 0 ? narrow : !narrow;
        });

        // make rows taller on phone layout
        theGrid.rows.defaultSize = defaultRowHeight * (narrow ? 3 : 1);

        // hide column headers on narrow layouts
        theGrid.headersVisibility = narrow ? 'None' : 'Column';
    }

    // get some data
    wijmo.httpRequest('https://services.odata.org/Northwind/Northwind.svc/Customers?$format=json', {
        success: function (xhr) {
            var response = JSON.parse(xhr.responseText);
            theGrid.itemsSource = response.value;
            updateGridLayout();
        }
    });
});