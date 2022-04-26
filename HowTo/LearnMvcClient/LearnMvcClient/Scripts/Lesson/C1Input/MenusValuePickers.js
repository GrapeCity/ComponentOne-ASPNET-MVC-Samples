c1.documentReady(function () {
    var valuePicker = wijmo.Control.getControl('#valuePicker');

    // update header to show current selection
    valuePicker.selectedIndexChanged.addHandler(function (s, e) {
        if (s.selectedIndex > -1) {
            s.header = wijmo.format('Tax: <b>{header}</b>', s.selectedItem);
        }
    });

    // populate menu after hooking up the selectedIndexChanged event
    valuePicker.itemsSource = [
      { header: 'Exempt', value: 0 },
      { header: '1%', value: 0.01 },
      { header: '5%', value: 0.05 },
      { header: '8.5%', value: 0.085 },
      { header: '10%', value: 0.10 },
      { header: '20%', value: 0.20 }
    ];

    // initialize value
    valuePicker.selectedValue = .085;
});