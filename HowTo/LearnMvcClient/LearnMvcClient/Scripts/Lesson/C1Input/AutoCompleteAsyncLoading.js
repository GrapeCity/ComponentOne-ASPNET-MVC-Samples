c1.documentReady(function () {
    var theAutoComplete = wijmo.Control.getControl('#theAutoComplete');

    theAutoComplete.itemsSourceFunction = function(query, max, callback) {
        if (!query) {
            callback(null);
            return;
        }

        var url = 'https://services.odata.org/V4/Northwind/Northwind.svc/Products';
        wijmo.httpRequest(url, {
            data: {
                $format: 'json',
                $select: 'ProductID,ProductName',
                $filter: 'indexof(ProductName, \'' + query + '\') gt -1'
            },
            success: function(xhr) {
                var response = JSON.parse(xhr.response);
                var arr = response.d ? response.d.results : response.value;
                callback(response.value);
            }
        });
    };

    theAutoComplete.selectedIndexChanged.addHandler(function (s, e) {
        var product = theAutoComplete.selectedItem;
        document.getElementById('msg').textContent = product
          ? wijmo.format('{ProductName} (ID: {ProductID})', product)
          : 'None';
    });
});