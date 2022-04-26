c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');
    theGrid.initialize({
        allowResizing: 'None',
        autoGenerateColumns: false,
        columns: [
            { binding: 'Country', header: 'Country', width: '2*', align: 'center', isReadOnly: true },
            { binding: 'Sales', header: 'Sales', format: 'c0', width: '*' },
            { binding: 'Expenses', header: 'Expenses', format: 'c0', width: '*' },
        ]
    });
});