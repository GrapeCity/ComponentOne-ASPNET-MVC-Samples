c1.documentReady(function () {
    var theListBox = wijmo.Control.getControl('#theListBox');

    showDetail(theListBox);
    theListBox.selectedIndexChanged.addHandler(showDetail);

    function showDetail(s, e) {
        var template = '<b>Selection:</b><br>' +
      	'Index: <b>{index}</b><br>' +
      	'Country: <b>{country}</b></br>' +
        'GDP (US$M): <b>{gdp:n0}</b></br>' +
        'Population: <b>{pop:n0}</b></br>';
        var html = wijmo.format(template, {
            index: s.selectedIndex,
            country: s.selectedItem.Country,
            gdp: s.selectedItem.Gdpm,
            pop: s.selectedItem.Popk * 1000
        });
        document.getElementById('selectedItem').innerHTML = html;
    }
});