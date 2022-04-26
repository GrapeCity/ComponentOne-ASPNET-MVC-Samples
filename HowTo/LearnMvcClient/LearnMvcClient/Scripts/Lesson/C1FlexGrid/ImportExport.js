c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');

    // render using wijmo.PrintDocument class
    document.getElementById('btnPrintDoc').addEventListener('click', function (e) {

        // create PrintDocument
        var doc = new wijmo.PrintDocument({
            title: 'PrintDocument Test',
            copyCss: true
        });

        // add some simple text
        doc.append('<h1>Printing Example</h1>');
        doc.append('<p>This document was created using the <b>PrintDocument</b> class.</p>');

        // add a printer-friendly version of a FlexGrid to the document
        doc.append('<p>Here\'s a FlexGrid rendered as a table:</p>');
        var tbl = renderTable(theGrid);
        doc.append(tbl);

        // print the document
        doc.print();
    });

    // custom grid rendering function: 
    // renders grid as a table
    function renderTable(flex) {

        // start table
        var tbl = '<table>';

        // headers
        if (flex.headersVisibility & wijmo.grid.HeadersVisibility.Column) {
            tbl += '<thead>';
            for (var r = 0; r < flex.columnHeaders.rows.length; r++) {
                tbl += renderRow(flex.columnHeaders, r);
            }
            tbl += '</thead>';
        }

        // body
        tbl += '<tbody>';
        for (var r = 0; r < flex.rows.length; r++) {
            tbl += renderRow(flex.cells, r);
        }
        tbl += '</tbody>';

        // done
        tbl += '</table>';
        return tbl;
    }
    function renderRow(panel, r) {
        var tr = '',
        row = panel.rows[r];
        if (row.renderSize > 0) {
            tr += '<tr>';
            for (var c = 0; c < panel.columns.length; c++) {
                var col = panel.columns[c];
                if (col.renderSize > 0) {

                    // get cell style, content
                    var style = 'width:' + col.renderSize + 'px;' +
                        'text-align:' + col.getAlignment() + ';' +
                        'padding-right: 6px';
                    var content = panel.getCellData(r, c, true);
                    if (!row.isContentHtml && !col.isContentHtml) {
                        content = wijmo.escapeHtml(content);
                    }

                    // add cell to row
                    if (panel.cellType == wijmo.grid.CellType.ColumnHeader) {
                        tr += '<th style="' + style + '">' + content + '</th>';
                    } else {

                        // show boolean values as checkboxes
                        var raw = panel.getCellData(r, c, false);
                        if (raw === true) {
                            content = '&#9745;';
                        } else if (raw === false) {
                            content = '&#9744;';
                        }

                        tr += '<td style="' + style + '">' + content + '</td>';
                    }
                }
            }
            tr += '</tr>';
        }
        return tr;
    }
});