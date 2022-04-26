c1.documentReady(function () {
    // workers tree data (heterogeneous collection)
    var workers = [{
        name: 'Jack Smith',
        checks: [{
            name: 'check1',
            earnings: [
              { name: 'hourly', hours: 30.0, rate: 15.0 },
              { name: 'overtime', hours: 10.0, rate: 20.0 },
              { name: 'bonus', hours: 5.0, rate: 30.0 }
            ]
        }, {
            name: 'check2',
            earnings: [
              { name: 'hourly', hours: 20.0, rate: 18.0 },
              { name: 'overtime', hours: 20.0, rate: 24.0 }
            ]
        }]
    }, {
        name: 'Jack Smith',
        checks: [{
            name: 'check1',
            earnings: [
              { name: 'hourly', hours: 30.0, rate: 15.0 },
              { name: 'overtime', hours: 10.0, rate: 20.0 },
              { name: 'bonus', hours: 5.0, rate: 30.0 }
            ]
        }, {
            name: 'check2',
            earnings: [
              { name: 'hourly', hours: 20.0, rate: 18.0 },
              { name: 'overtime', hours: 20.0, rate: 24.0 }
            ]
        }]
    }, {
        name: 'Jane Smith',
        checks: [{
            name: 'check1',
            earnings: [
              { name: 'hourly', hours: 30.0, rate: 15.0 },
              { name: 'overtime', hours: 10.0, rate: 20.0 },
              { name: 'bonus', hours: 5.0, rate: 30.0 }
            ]
        }, {
            name: 'check2',
            earnings: [
              { name: 'hourly', hours: 20.0, rate: 18.0 },
              { name: 'overtime', hours: 20.0, rate: 24.0 }
            ]
        }]
    }];

    // workers tree
    var workersGrid = wijmo.Control.getControl('#workersGrid');
    workersGrid.loadedRows.addHandler(function (s, e) {
        s.rows.forEach(function (row) {
            row.isReadOnly = false;
        })
    });
    workersGrid.beginningEdit.addHandler(function (s, e) {
        var item = s.rows[e.row].dataItem,
            binding = s.columns[e.col].binding;
        if (!(binding in item)) { // property not on this item?
            e.cancel = true; // can't edit!
        }
    });
    workersGrid.childItemsPath = ['checks', 'earnings'];
    workersGrid.itemsSource = workers;
});