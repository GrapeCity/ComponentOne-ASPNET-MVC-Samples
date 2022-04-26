c1.documentReady(function () {
    // html detail provider
    var htmlDetail = wijmo.Control.getControl('#htmlDetail');
    // html detail provider
    var dpHtml = c1.getExtender(htmlDetail, 'htmlDetailRow');
    // use animation when showing details
    dpHtml.isAnimated = true;
    // create detail cells for a given row
    dpHtml.createDetailCell = function (row) {

        // build detail content for the current category
        var cat = row.dataItem;
        var prods = getProducts(cat.CategoryID);
        var html = 'ID: <b>' + cat.CategoryID + '</b><br/>';
        html += 'Name: <b>' + cat.CategoryName + '</b><br/>';
        html += 'Description: <b>' + cat.Description + '</b><br/>';
        html += 'Products: <b>' + prods.length + ' items</b><br/>';
        html += '<ol>';
        prods.forEach(function (product) {
            html += '<li>' + product.ProductName + '</li>';
        });
        html += '</ol>';

        // create and return detail cell
        var cell = document.createElement('div');
        cell.innerHTML = html;
        return cell;
    }

    // grid with grid detail
    var gridDetail = wijmo.Control.getControl('#gridDetail');
    // grid detail provider
    var dpGrid = c1.getExtender(gridDetail, 'gridDetailRow');
    // use animation when showing details
    dpGrid.isAnimated = true;
    // limit height of detail rows
    dpGrid.maxHeight = 150;
    // create detail cells for a given row
    dpGrid.createDetailCell = function (row) {
        var cell = document.createElement('div');
        var detailGrid = new wijmo.grid.FlexGrid(cell, {
            headersVisibility: wijmo.grid.HeadersVisibility.Column,
            autoGenerateColumns: false,
            itemsSource: getProducts(row.dataItem.CategoryID),
            columns: [
                { header: 'ID', binding: 'ProductID' },
              { header: 'Name', binding: 'ProductName' },
              { header: 'Qty/Unit', binding: 'QuantityPerUnit' },
              { header: 'Unit Price', binding: 'UnitPrice' },
              { header: 'Discontinued', binding: 'Discontinued' }
            ]
        });
        return cell;
    }

    var categories = [], products = [];
    var categoriesIsDone = false, productsIsDone = false;

    function validate() {
        if (categoriesIsDone && productsIsDone) {
            htmlDetail.itemsSource = categories;
            gridDetail.itemsSource = categories;
        }
    }

    // get products for a given category
    function getProducts(categoryID) {
        var arr = [];
        products.forEach(function (product) {
            if (product.CategoryID == categoryID) {
                arr.push(product);
            }
        });
        return arr;
    }

    // get data from OData service
    var nwindService = 'https://services.odata.org/Northwind/Northwind.svc/';
    function getOdata(url, fill, finish) {
        wijmo.httpRequest(nwindService + url, {
            success: function (xhr) {
                var data = JSON.parse(xhr.responseText);
                fill(data.value);

                var nextLink = data['odata.nextLink'];
                if (nextLink == null) {
                    finish();
                } else {
                    getOdata(nextLink + '&$format=json', fill, finish);
                }
            }
        });
    }

    var categoriesUrl = 'Categories?$format=json&$inlinecount=allpages&$select=CategoryID,CategoryName,Description';
    getOdata(categoriesUrl,
        function (value) {
            categories = categories.concat(value);
        },
        function () {
            categoriesIsDone = true;
            validate();
        });

    var productsUrl = 'Products?$format=json&$inlinecount=allpages';
    getOdata(productsUrl,
        function (value) {
            products = products.concat(value);
        },
        function () {
            productsIsDone = true;
            validate();
        });
});

