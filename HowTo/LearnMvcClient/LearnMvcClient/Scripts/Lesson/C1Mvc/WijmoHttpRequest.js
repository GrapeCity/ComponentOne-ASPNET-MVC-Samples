c1.documentReady(function () {
    // parameters for httpRequest
    var url = 'https://services.odata.org/V4/Northwind/Northwind.svc/Categories';
    var params = {
        '$format': 'json',
        '$select': 'CategoryID,CategoryName,Description'
    };

    // make the request
    wijmo.httpRequest(url, {
        data: params,
        success: function (xhr) {

            // show results in a grid
            var grid = wijmo.Control.getControl('#theGrid');
            grid.itemsSource = JSON.parse(xhr.responseText).value;

            // make last column fill the grid
            grid.columns[grid.columns.length - 1].width = '*';
        }
    });
});