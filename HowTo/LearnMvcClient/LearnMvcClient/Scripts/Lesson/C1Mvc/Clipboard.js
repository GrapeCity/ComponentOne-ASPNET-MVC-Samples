c1.documentReady(function () {
    var hasHeaders = false;
    var theGrid = wijmo.Control.getControl('#theGrid');
    theGrid.copying.addHandler(function (s, e) { // // add headers to clip string
        hasHeaders = false;
        var includeHeaders = document.getElementById('includeHeaders').checked;
        if (includeHeaders) {
            var text = s.getClipString(),
                sel = s.selection,
                hdr = '';
            for (var c = sel.leftCol; c <= sel.rightCol; c++) {
                if (hdr) hdr += '\t';
                hdr += s.columns[c].header;
            }
            text = hdr + '\r\n' + text;

            // put text with headers on the clipboard
            wijmo.Clipboard.copy(text);
            hasHeaders = true;

            // prevent the grid from overwriting our clipboard content
            e.cancel = true;
        }
    });
    theGrid.pasting.addHandler(function (s, e) { // prevent pasting content with headers...
        if (hasHeaders) {
            e.cancel = true;
        }
    });
});