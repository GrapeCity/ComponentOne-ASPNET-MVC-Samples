c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');
    var theComboBox = wijmo.Control.getControl('#theComboBox');

    // data used in the combo
    var theData = [
  	{ name: 'Zero', value: 0 },
  	{ name: 'Low', value: 10 },
  	{ name: 'Medium', value: 50 },
  	{ name: 'High', value: 100 },
  	{ name: 'Maximum', value: 150 },
    ];

    // show the data
    theGrid.itemsSource = theData;

    theComboBox.itemsSource = theData;
    theComboBox.selectedIndexChanged.addHandler(function (s, e) {
        updateAllText(s);
    });

    updateAllText(theComboBox);

    function updateAllText(combo) {
        updateText(combo, 'displayMemberPath');
        updateText(combo, 'selectedValuePath');
        updateText(combo, 'selectedIndex');
        updateText(combo, 'selectedItem');
        updateText(combo, 'selectedValue');
        updateText(combo, 'text');
    }

    // show current values
    function updateText(combo, prop) {
        var value = combo[prop];
        if (wijmo.isObject(value)) {
            value = JSON.stringify(value);
        }
        document.getElementById(prop).textContent = value;
    }
});