// define the instance for the AccessibilityExtender object.
var acx;

function sortedColumn(sender, e) {
    var grid = sender;
    var col = grid.columns[e.col];
    if (acx) {
        acX.alert('column ' + col.header +
        ' sorted in ' + (col.currentSort == '+' ? 'ascending' : 'descending') + ' order');
    }
}

function initHamburgerNav() {
    var hamburgerNavBtn = document.querySelector('.hamburger-nav-btn');
    var hamburgerNavEle = document.querySelector('.narrow-screen.site-nav');
    new c1.sample.MultiLevelMenu(hamburgerNavEle, hamburgerNavBtn);
}

// hook up filter
var toSearch = null;

c1.documentReady(function () {
    initHamburgerNav();

    var theGrid = wijmo.Control.getControl('#theGrid');

    // extend accessibility features
    acX = new wijmo.grid.accessibility.AccessibilityExtender(theGrid);

    document.getElementById('filter').addEventListener('input', function (e) {
        clearTimeout(toSearch);
        toSearch = setTimeout(function () {
            var search = e.target.value,
                rx = new RegExp(search, 'i');
            theGrid.collectionView.filter = function (item) {
                return !search || JSON.stringify(item).match(rx);
            }

            // notify users that a filter was applied
            setTimeout(function () {
                var msg = search
                    ? 'grid filtered on ' + search
                    : 'filter removed';
                msg += ': ' + theGrid.rows.length + ' items displayed';
                if (acx) {
                    acX.alert(msg);
                }
            }, 200)
        }, 900)
    });
});

