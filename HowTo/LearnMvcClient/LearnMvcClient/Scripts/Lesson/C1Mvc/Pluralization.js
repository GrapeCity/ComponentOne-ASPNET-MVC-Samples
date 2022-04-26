c1.documentReady(function () {
    // build format string with pluralization
    var fmtPlural = {
        count: 'count',
        when: {
            0: 'No items selected.',
            1: 'A single item is selected.',
            2: 'A pair is selected.',
            3: 'A trio is selected.',
            4: 'A quartet is selected.',
            'other': '{count:n0} items are selected.'
        }
    }
    fmtPlural = JSON.stringify(fmtPlural);

    // let the user choose the number of items
    var theInputNumber = wijmo.Control.getControl('#theInputNumber');
    updateMessage();
    theInputNumber.valueChanged.addHandler(function (s, e) {
        updateMessage();
    });

    function updateMessage() {
        document.getElementById('msg').textContent = wijmo.format(fmtPlural, {
            count: theInputNumber.value
        });
    }
});