c1.documentReady(function () {
    var tabInCode = wijmo.Control.getControl('#tabInCode'),
        url = 'https://services.odata.org/Northwind/Northwind.svc/',
        headers = 'Products,Customers'.split(',');
    tabInCode.tabs.deferUpdate(function () { // update only when done
        headers.forEach(function (header) {

            // create the tab header element
            var elHeader = document.createElement('a');
            elHeader.textContent = header;

            // create the tab pane element
            var elPane = document.createElement('div'),
                elGrid = document.createElement('div'),
                grid = new wijmo.grid.FlexGrid(elGrid, {
                    isReadOnly: true,
                    itemsSource: new wijmo.odata.ODataCollectionView(url, header)
                });
            elPane.appendChild(elGrid);

            // add the new Tab to the TabPanel
            tabInCode.tabs.push(new wijmo.nav.Tab(elHeader, elPane));
        });
    });
});