c1.documentReady(function () {
    // template for "Template" cells
    var theTemplate = '<button class="btn btn-default" name="{country}">' +
      '<img src="http://flagpedia.net/data/flags/mini/{abb}.png"/> ' +
      'Click Me!' +
    '</button>';

    var theGrid = wijmo.Control.getControl('#theGrid');
    theGrid.itemsSource = getData();

    theGrid.formatItem.addHandler(function (s, e) {
        if (e.panel == s.cells && s.columns[e.col].binding == 'template') {
            var item = s.rows[e.row].dataItem,
                    html = wijmo.format(theTemplate, item);
            e.cell.innerHTML = html;
        }
    });

    // make rows taller to accommodate buttons
    theGrid.rows.defaultSize = 45;

    // handle button clicks
    theGrid.hostElement.addEventListener('click', function (e) {
        var target = wijmo.closest(e.target, 'button');
        if (target instanceof HTMLButtonElement && target.name) {
            alert('Thanks for clicking "' + target.name + '"');
        }
    });

    // create some random data
    function getData() {
        var countries = 'US,Germany,UK,Japan,Italy,Greece'.split(','),
            abb = 'us,de,gb,jp,it,gr'.split(','),
            data = [];
        for (var i = 0; i < countries.length; i++) {
            data.push({
                country: countries[i],
                abb: abb[i],
                sales: Math.random() * 10000,
                expenses: Math.random() * 5000
            });
        }
        return data;
    }
});