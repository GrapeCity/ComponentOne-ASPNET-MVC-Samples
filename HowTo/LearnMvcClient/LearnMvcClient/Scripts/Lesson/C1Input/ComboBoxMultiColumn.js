c1.documentReady(function () {
    var theCombo = wijmo.Control.getControl('#theCombo');
    var theComboCSS = wijmo.Control.getControl('#theComboCSS');
    var theComboTable = wijmo.Control.getControl('#theComboTable');

    // multi-column table-style
    var template = '<table><tr>' +
      '<td>{Country}</td>' +
      '<td class="number" title="GDP (million US$/year)">{Gdpm:c0}</td>' +
      '<td class="number" title="Population (thousands)">{Popk:n0}</td>' +
      '<td class="number" title="GDP/cap (US$/person/year)">{Gdpcap:c0}</td>' +
      '</tr></table>';
    theComboTable.formatItem.addHandler(function (s, e) {
        var html = wijmo.format(template, e.data);
        e.item.innerHTML = html;
    });
});