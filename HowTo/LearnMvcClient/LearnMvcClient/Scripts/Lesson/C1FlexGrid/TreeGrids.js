c1.documentReady(function () {
    // family tree data (homogeneous collection)
    var family = [
    {
        name: 'Albert', children: [
          { name: 'Anton' },
          { name: 'Annette' },
        ]
    },
    {
        name: 'Benjamin', children: [
          {
              name: 'Bridget', children: [
                { name: 'Billy' },
            { name: 'Bernard' },
              ]
          },
          { name: 'Bella' },
          { name: 'Bob' },
        ]
    },
    {
        name: 'Charlie', children: [
          { name: 'Chris' },
          { name: 'Connie' },
          { name: 'Carrie' },
        ]
    },
    {
        name: 'Douglas', children: [
          { name: 'Dinah' },
          { name: 'Donald' }
        ]
    },
    ];

    // family tree
    var familyGrid = wijmo.Control.getControl('#familyGrid');
    familyGrid.headersVisibility = 'None';
    familyGrid.childItemsPath = 'children';
    familyGrid.itemsSource = family;
    familyGrid.columns[0].width = '*';

    // toggle family tree
    document.getElementById('asTree').addEventListener('click', function (e) {
        familyGrid.childItemsPath = e.target.checked ? 'children' : '';
    })

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
    workersGrid.headersVisibility = 'Column';
    workersGrid.childItemsPath = ['checks', 'earnings'];
    workersGrid.itemsSource = workers;
});