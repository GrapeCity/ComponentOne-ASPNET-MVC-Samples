c1.documentReady(function () {
    // create some random data
    var countries = 'US,Germany,UK,Japan,Italy,Greece,China,Spain,Sweden,Norway,Denmark,Finland'.split(','),
      data = [];
    for (var i = 0; i < countries.length; i++) {
        data.push({
            id: i,
            country: countries[i],
            active: i % 5 != 0,
            downloads: Math.round(Math.random() * 200000),
            sales: Math.random() * 100000,
            expenses: Math.random() * 50000
        });
    }

    // bind two grids to the same array
    // each shows an independent view of the same data
    var gridArray1 = wijmo.Control.getControl('#gridArray1');
    var gridArray2 = wijmo.Control.getControl('#gridArray2');
    gridArray1.itemsSource = data;
    gridArray2.itemsSource = data;

    // bind two grids to the same CollectionView
    // both share the same view (sorting, filtering, selection, etc)
    var view = new wijmo.collections.CollectionView(data);
    var gridCv1 = wijmo.Control.getControl('#gridCv1');
    var gridCv2 = wijmo.Control.getControl('#gridCv2');
    gridCv1.itemsSource = view;
    gridCv2.itemsSource = view;

    // bind a ListBox to the same view
    var listBox = wijmo.Control.getControl('#listBox');
    listBox.itemsSource = view;
    listBox.displayMemberPath='country';

    // add filters to all grids on the page
    var grids = document.querySelectorAll('.wj-flexgrid');
    for (var i = 0; i < grids.length; i++) {
        var grid = wijmo.Control.getControl(grids[i]);
        var filter = new wijmo.grid.filter.FlexGridFilter(grid);
    }
});