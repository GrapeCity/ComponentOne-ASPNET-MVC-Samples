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

    // combo and selectedItem
    showDetail();
    theCombo.selectedIndexChanged.addHandler(function (s, e) {
        showDetail();
    });

    function showDetail() {
        var html = wijmo.format(detailTemplate, theCombo.selectedItem,
            function (data, name, fmt, val) {
                if (wijmo.isString(data[name])) {
                    val = wijmo.escapeHtml(data[name]);
                }
                return val;
            });
        document.getElementById('detail').innerHTML = html;
    }
});