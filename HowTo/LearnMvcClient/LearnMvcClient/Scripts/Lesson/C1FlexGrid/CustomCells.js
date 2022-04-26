c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');

    // insert extra column header row
    var ch = theGrid.columnHeaders,
          hr = new wijmo.grid.Row();
    ch.rows.insert(0, hr);

    // fill out headings in extra header row
    for (var i = 0; i < theGrid.columns.length; i++) {
        var hdr = ch.getCellData(1, i);
        if (hdr == 'Diff') hdr = ch.getCellData(1, i - 1)
        ch.setCellData(0, i, hdr);
    }

    // allow merging across and down extra header row
    theGrid.allowMerging = 'ColumnHeaders';
    hr.allowMerging = true;
    theGrid.columns[0].allowMerging = true;
    theGrid.columns[1].allowMerging = true;

    // custom rendering for headers and "Diff" columns
    theGrid.formatItem.addHandler(function (s, e) {

        // center-align column headers
        if (e.panel == s.columnHeaders) {
            e.cell.innerHTML = '<div class="v-center">' +
      	e.cell.innerHTML + '</div>';
        }

        // custom rendering for "Diff" columns
        if (e.panel == s.cells) {
            var col = s.columns[e.col];
            if (e.row > 0 && (col.binding == 'SalesDiff' || col.binding == 'ExpensesDiff')) {
                var vnow = s.getCellData(e.row, e.col - 1),
                    vprev = s.getCellData(e.row - 1, e.col - 1),
                    diff = (vnow / vprev) - 1;

                // format the cell
                var html = '<div class="diff-{cls}">' +
                '<span style="font-size:75%">{val}</span> ' +
                '<span style="font-size:120%" class="wj-glyph-{glyph}"></span>';
                html = html.replace('{val}', wijmo.Globalize.format(diff, col.format));
                if (diff < 0.01) {
                    html = html.replace('{cls}', 'down').replace('{glyph}', 'down');
                } else if (diff > 0.01) {
                    html = html.replace('{cls}', 'up').replace('{glyph}', 'up');
                } else {
                    html = html.replace('{cls}', 'none').replace('{glyph}', 'circle');
                }
                e.cell.innerHTML = html;
            }
        }
    });
});