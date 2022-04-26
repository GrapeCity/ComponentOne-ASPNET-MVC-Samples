c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');
    theGrid.childItemsPath = "cities";
    theGrid.itemsSource = getData();

    // update filter
    document.getElementById('filter').addEventListener('input', function (e) {
        var filter = e.target.value.toLowerCase();
        applyHierarchicalFilter(theGrid, filter);
    });

    // update row visibility
    function applyHierarchicalFilter(grid, filter) {
        var rows = grid.rows;
        for (var i = 0; i < rows.length; i++) {
            var row = rows[i],
            state = row.dataItem,
            rng = row.getCellRange();

            // handle states (level 0)
            if (row.level == 0) {

                // check if the state name matches the filter
                var stateVisible = state.name.toLowerCase().indexOf(filter) >= 0;
                if (stateVisible) {

                    // it does, so show the state and all its cities
                    for (var j = rng.topRow; j <= rng.bottomRow; j++) {
                        rows[j].visible = true;
                    }

                } else {

                    // it does not, so check the cities
                    for (var j = rng.topRow + 1; j <= rng.bottomRow; j++) {
                        var city = rows[j].dataItem,
                            cityVisible = city.name.toLowerCase().indexOf(filter) >= 0;
                        rows[j].visible = cityVisible;
                        stateVisible |= cityVisible;
                    }

                    // if at least one city is visible, the state is visible
                    rows[i].visible = stateVisible;
                }

                // move on to the next group
                i = rng.bottomRow;
            }
        }
    }

    // some hierarchical data
    function getData() {
        return [
            {
                name: 'Washington', type: 'state', population: 6971, cities: [
                      { name: 'Seattle', type: 'city', population: 652 },
              { name: 'Spokane', type: 'city', population: 210 }]
            },
            {
                name: 'Oregon', type: 'state', population: 3930, cities: [
                { name: 'Portland', type: 'city', population: 609 },
                { name: 'Eugene', type: 'city', population: 159 }]
            },
          {
              name: 'California', type: 'state', population: 38330, cities: [
                      { name: 'Los Angeles', type: 'city', population: 3884 },
              { name: 'San Diego', type: 'city', population: 1356 },
              { name: 'San Francisco', type: 'city', population: 837 }]
          }
        ];
    }
});