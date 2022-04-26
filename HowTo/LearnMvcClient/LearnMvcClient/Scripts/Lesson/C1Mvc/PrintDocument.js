c1.documentReady(function () {
    // render using window.print()
    document.getElementById('btnPrintDirect').addEventListener('click', function (e) {
        window.print();
    });

    // render using wijmo.PrintDocument class
    document.getElementById('btnPrintDoc').addEventListener('click', function (e) {

        // create PrintDocument
        var doc = new wijmo.PrintDocument({
            title: 'PrintDocument Test',
            copyCss: true // includes the CSS style sheets defined in the main document.
        });

        // add C1WebMvc CSS explicitly (only the CSS file ends with '.css' is copied).
        var links = document.head.querySelectorAll('LINK');
        for (var i = 0; i < links.length; i++) {
            var link = links[i];
            if (link.href.match(/C1WebMvc\/WebResources\?/i) && link.rel.match(/stylesheet/i)) {
                doc.append('<link href="' + link.href + '" rel="stylesheet">');
            }
        }

        // add some simple text
        doc.append('<h1>Printing Example</h1>');
        doc.append('<p>This document was created using the <b>PrintDocument</b> class.</p>');

        // add existing elements
        doc.append('<h2>Render Existing Elements</h2>');
        doc.append('<p>Check out these gauges:</p>');
        doc.append(document.getElementById('tblGauge'));

        // add a printer-friendly version of a FlexGrid to the document
        var flex = wijmo.Control.getControl('#theGrid');
        doc.append('<h2>Printer-Friendly FlexGrid</h2>');
        doc.append('<p>And here\'s a FlexGrid rendered as a table:</p>');
        var tbl = renderTable(flex);
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