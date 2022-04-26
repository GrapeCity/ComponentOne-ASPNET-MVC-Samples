c1.documentReady(function () {
    var glyphs = 'asterisk,calendar,check,circle,clock,diamond,down,' +
        'down-left,down-right,file,filter,left,minus,pencil,plus,right,' +
        'square,step-backward,step-forward,up,up-left,up-right';

    var rowTemplate = '<tr>' +
        '<td>{glyph}</td>' +
        '<td style="font-size:125%;"><span class="wj-glyph-{glyph}"></span></td>' +
        '<td><code>&lt;span class="wj-glyph-{glyph}"&gt;&lt;/span&gt;</code></td>' +
      '</tr>';

    var tbody = '';
    glyphs.split(',').forEach(function (item, index) {
        tbody += rowTemplate.replace(/\{glyph\}/g, item);
    });
    document.querySelector('tbody').innerHTML = tbody;
});