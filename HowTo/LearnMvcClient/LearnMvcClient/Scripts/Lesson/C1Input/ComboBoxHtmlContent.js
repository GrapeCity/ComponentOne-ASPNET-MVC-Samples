c1.documentReady(function () {
    var theCombo = wijmo.Control.getControl('#theCombo');

    // define template for the details
    var template = '<div class="item">' +
      '<h1>{name}</h1>' +
      '<b>{city}</b> ({state})<br/>' +
      '{address}<br/>' +
      'Phone: <b>{phone}</b><br/>' +
      'Fax: <b>{fax}</b><br/>' +
      'Website: <a href="{site}" target="_blank">{site}</a><br/>' +
      '</div>';

    theCombo.formatItem.addHandler(function (s, e) {
        var html = wijmo.format(template, e.data, function (data, name, fmt, val) {
            return wijmo.isString(data[name]) ? wijmo.escapeHtml(data[name]) : val;
        });
        e.item.innerHTML = html;
    });

    theCombo.itemsSource = getData();

    // some data to show in our accordion
    function getData() {
        return [{
            name: 'Electro Mart',
            city: 'Accident',
            state: 'Maryland',
            address: '8785 Windfall St.',
            phone: '(800) 555-1234',
            fax: '(800) 555-5678',
            site: 'http://www.electromartnot.com'
        }, {
            name: 'Sue\'s Depot',
            city: 'Big Arm',
            state: 'Montana',
            address: '77 Winchester Lane',
            phone: '(800) 555-2345',
            fax: '(800) 555-6789',
            site: 'http://www.suesdepotnot.com'
        }, {
            name: 'D&K Digital Locker',
            city: 'Chicken',
            state: 'Alaska',
            address: '787 Lakeview St. ',
            phone: '(800) 555-3456',
            fax: '(800) 555-7890',
            site: 'http://www.digilockernot.com'
        }, {
            name: 'Paul\'s Pub & Bistro',
            city: 'Coupon',
            state: 'Pennsylvania',
            address: '711 Old York Drive ',
            phone: '(800) 555-0987',
            fax: '(800) 555-6543',
            site: 'http://www.paulspubnot.com'
        }, {
            name: 'Amazing Deals Inc',
            city: 'Cut And Shoot',
            state: 'Texas',
            address: '91 Beech St.',
            phone: '(800) 955-2109',
            fax: '(800) 955-8765',
            site: 'http://www.amazingdealsnot.com'
        }];
    }
});