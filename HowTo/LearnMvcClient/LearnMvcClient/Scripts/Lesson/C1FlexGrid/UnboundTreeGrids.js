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

    // unbound workers tree
    var uwt = wijmo.Control.getControl('#workersGrid');
    uwt.beginningEdit.addHandler(function (s, e) {
        var value = e.panel.getCellData(e.row, e.col);
        if (value == null) {
            e.cancel = true; // can't edit!
        }
    });

    // add rows
    for (var w = 0; w < workers.length; w++) {

        // add worker
        var worker = workers[w];
        var row = new wijmo.grid.GroupRow(worker);
        row.isReadOnly = false;
        row.level = 0;
        uwt.rows.push(row);
        uwt.setCellData(row.index, 0, worker.name);
        for (var c = 0; c < worker.checks.length; c++) {

            // add check
            var check = worker.checks[c];
            row = new wijmo.grid.GroupRow(check);
            row.isReadOnly = false;
            row.level = 1;
            uwt.rows.push(row);
            uwt.setCellData(row.index, 0, check.name);
            for (var e = 0; e < check.earnings.length; e++) {

                // add earning
                var earning = check.earnings[e];
                row = new wijmo.grid.GroupRow(earning);
                row.isReadOnly = false;
                row.level = 2;
                uwt.rows.push(row);
                uwt.setCellData(row.index, 0, earning.name);
                uwt.setCellData(row.index, 1, earning.hours);
                uwt.setCellData(row.index, 2, earning.rate);

            }
        }
    }
});