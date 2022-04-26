c1.documentReady(function () {
    var theSplitButton = wijmo.Control.getControl('#theSplitButton');

    // item clicked fires when you select an option or click the header
    theSplitButton.isButton = true;
    theSplitButton.itemClicked.addHandler(function (s, e) {
        alert('Running ' + s.selectedValue);
    });

    // update header to show current selection
    theSplitButton.selectedIndexChanged.addHandler(function (s, e) {
        if (s.selectedIndex > -1) {
            s.header = wijmo.format('Run: <b>{header}</b>', s.selectedItem);
        }
    });

    // populate menu after hooking up the selectedIndexChanged event
    theSplitButton.displayMemberPath = 'header';
    theSplitButton.selectedValuePath = 'value';
    theSplitButton.itemsSource = [
      { header: 'Internet Explorer', value: 'IE' },
      { header: 'Chrome', value: 'CHR' },
      { header: 'Firefox', value: 'FFX' },
      { header: 'Safari', value: 'IOS' },
      { header: 'Opera', value: 'OPR' },
    ];

    // initialize value
    theSplitButton.selectedValue = 'FFX';
});