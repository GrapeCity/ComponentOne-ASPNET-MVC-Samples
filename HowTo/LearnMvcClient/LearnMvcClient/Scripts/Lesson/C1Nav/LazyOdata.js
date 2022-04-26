c1.documentReady(function () {
    var theTree = wijmo.Control.getControl('#theTree');
    theTree.displayMemberPath = ['FullName', 'ShipName', 'Summary'];
    theTree.childItemsPath = ['Orders', 'Order_Details'];
    theTree.lazyLoadFunction = lazyLoadODataFunction;

    // lazy-load orders and details
    function lazyLoadODataFunction(node, callback) {
        switch (node.level) {

            // load orders for an employee
            case 0:
                var url = 'Employees(' + node.dataItem.EmployeeID + ')/Orders?$format=json&$inlinecount=allpages&$select=OrderID,ShipName,ShipCountry';
                var orders = [];
                getOdata(url,
                    function (value) {
                        orders = orders.concat(value);
                    },
                    function () {
                        orders.forEach(function (item) {
                            item.Order_Details = [];
                        });
                        callback(orders);
                    });
                break;

                // load extended details for an order
            case 1:
                var url = "Order_Details_Extendeds/?$format=json&$inlinecount=allpages&select=ProductName,ExtendedPrice&$filter=OrderID eq " + node.dataItem.OrderID;
                var orderDetails = [];
                getOdata(url,
                    function (value) {
                        orderDetails = orderDetails.concat(value);
                    },
                    function () {
                        orderDetails.forEach(function (item) {
                            item.Summary = wijmo.format('{ProductName}: {ExtendedPrice:c}', item);
                        });
                        callback(orderDetails);
                    });
                break;

                // default
            default:
                callback(null);
        }
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

    var employees = [];
    getOdata('Employees?$format=json&$inlinecount=allpages&$select=EmployeeID,FirstName,LastName',
        function (value) {
            employees = employees.concat(value);
        },
        function () {
            employees.forEach(function (item) {
                item.FullName = item.FirstName + ' ' + item.LastName;
                item.Orders = [];
            });
            theTree.itemsSource = employees;
        });
});