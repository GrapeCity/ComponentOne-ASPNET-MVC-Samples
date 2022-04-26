c1.documentReady(function () {
    // parse dates
    document.getElementById('dtBtn').addEventListener('click', function () {
        var text = document.getElementById('dtIn').value;
        var fmt = document.getElementById('dtFmt').value;
        var value = wijmo.Globalize.parseDate(text, fmt);
        var output = document.getElementById('dtOutput');
        if (wijmo.isDate(value)) {
            output.textContent = 'Parsed OK: ' + wijmo.Globalize.format(value, fmt);
        } else {
            output.textContent = '** Could not parse date... **';
        }
    });

    // parse numbers
    document.getElementById('numBtn').addEventListener('click', function () {
        var text = document.getElementById('numIn').value;
        var fmt = document.getElementById('numFmt').value;
        var value = wijmo.Globalize.parseFloat(text, fmt);
        var output = document.getElementById('numOutput');
        if (wijmo.isNumber(value)) {
            output.textContent = 'Parsed OK: ' + wijmo.Globalize.format(value, fmt);
        } else {
            output.textContent = '** Could not parse number... **';
        }
    });
});