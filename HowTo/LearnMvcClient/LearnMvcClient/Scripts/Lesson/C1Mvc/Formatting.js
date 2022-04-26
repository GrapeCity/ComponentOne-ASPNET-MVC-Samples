c1.documentReady(function () {
    // select culture
    var theCulture = wijmo.Control.getControl('#theCulture');

    // value and precision
    var theValue = wijmo.Control.getControl('#theValue');
    theValue.valueChanged.addHandler(updateTables);
    var thePrecision = wijmo.Control.getControl('#thePrecision');
    thePrecision.valueChanged.addHandler(updateTables);

    // update the tables
    updateTables();
    function updateTables() {
  
        // update dates
        var theDate = new Date();
        var rows = document.body.querySelectorAll('#tblDates tbody tr');
        for (var i = 0; i < rows.length; i++) {
            var cells = rows[i].children,
            fmt = cells[0].textContent;
            cells[2].textContent = wijmo.Globalize.format(theDate, fmt);
        }

        // update numbers
        var rows = document.body.querySelectorAll('#tblNumbers tbody tr');
        for (var i = 0; i < rows.length; i++) {
            var cells = rows[i].children,
            fmt = cells[0].textContent.replace('*', thePrecision.value.toString());
            cells[2].textContent = wijmo.Globalize.format(theValue.value, fmt);
        }
    }
});