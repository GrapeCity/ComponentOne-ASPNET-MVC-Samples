c1.documentReady(function () {
    var theCombo = wijmo.Control.getControl('#theCombo');

    // define a template for showing the current item's details
    var detailTemplate = '<div>' +
      '<div>Country: <b>{Country}</b></div>' +
      '<div>GDP (M$/yr): <b>{Gdpm:c0}</b></div>' +
      '<div>Population (thousands): <b>{Popk:n0}</b></div>' +
      '<div>GRP/Capita: <b>{Gdpcap:c0}</b></div>' +
      '<div>Rank: <b>{Id}</b></div>' +
      '</div>';

    // update detail when current item changes
    var view = theCombo.collectionView;
    view.currentChanged.addHandler(function (s, e) {
        var html = wijmo.format(detailTemplate, s.currentItem,
            function (data, name, fmt, val) {
                if (wijmo.isString(data[name])) {
                    val = wijmo.escapeHtml(data[name]);
                }
                return val;
            }
        );
        document.getElementById('detail').innerHTML = html;
    });

    // sort and filter the collectionView
    view.sortDescriptions.push(new wijmo.collections.SortDescription('Country', true));
    view.filter = function (item) {
        return item.Popk > 20000; // 20 million people or more
    }
    view.moveCurrentToFirst();
});