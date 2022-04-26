c1.documentReady(function () {
    // default clipboard behavior
    var theGrid = wijmo.Control.getControl('#theGrid');

    // custom clipboard behavior
    var theCustomGrid = wijmo.Control.getControl('#theCustomGrid');
    theCustomGrid.copying.addHandler(function (s, e) { // // add headers to clip string
        var text = s.getClipString();
        var sel = s.selection;
        var hdr = '';
        for (var c = sel.leftCol; c <= sel.rightCol; c++) {
            if (hdr) hdr += '\t';
            hdr += s.columns[c].header;
        }
        text = hdr + '\r\n' + text;

        // put text with headers on the clipboard
        wijmo.Clipboard.copy(text);

        // prevent the grid from overwriting our clipboard content
        e.cancel = true;
        hasHeaders = true;
    });
    theCustomGrid.pasting.addHandler(function (s, e) {
        e.cancel = true; // prevent pasting data with headers
    });
});