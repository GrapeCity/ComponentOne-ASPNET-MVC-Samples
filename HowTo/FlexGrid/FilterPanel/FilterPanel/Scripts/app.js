function initHamburgerNav() {
    var hamburgerNavBtn = document.querySelector('.hamburger-nav-btn');
    var hamburgerNavEle = document.querySelector('.narrow-screen.site-nav');
    new c1.sample.MultiLevelMenu(hamburgerNavEle, hamburgerNavBtn);
}

c1.documentReady(function () {

    // get the grid
    var grid = wijmo.Control.getControl('#grid');

    // create the filter
    var filter = c1.getExtender(grid, 'gridFilter');

    // create the filter panel
    var filterPanel = new wijmo.grid.filter.FilterPanel('#filterPanel', {
        filter: filter,
        placeholder: 'Active Filters'
    });

    initHamburgerNav();
});