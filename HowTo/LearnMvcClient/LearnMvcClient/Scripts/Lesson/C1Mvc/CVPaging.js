c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');

    // a CollectionView with 5 items per page
    var view = theGrid.collectionView;
    view.pageSize = 5;
    view.pageChanged.addHandler(updateCurrentPage);

    // update pager status
    view.onPageChanged();
    function updateCurrentPage() {
        var curr = wijmo.format('Page {index:n0} of {cnt:n0}', {
            index: view.pageIndex + 1,
            cnt: view.pageCount
        });
        document.getElementById('spanCurrent').textContent = curr;
    }

    // implement pager
    document.getElementById('pager').addEventListener('click', function (e) {
        var btn = wijmo.closest(e.target, 'button');
        var id = btn ? btn.id : '';
        switch (id) {
            case 'btnFirst':
                view.moveToFirstPage();
                break;
            case 'btnPrev':
                view.moveToPreviousPage();
                break;
            case 'btnNext':
                view.moveToNextPage();
                break;
            case 'btnLast':
                view.moveToLastPage();
                break;
        }
    });

    // use server-based paging
    var theGridServer = wijmo.Control.getControl('#theGridServer');
    var viewServer = theGridServer.collectionView;
    viewServer.pageChanged.addHandler(updateCurrentPageServer);

    // update pager status
    viewServer.onPageChanged();
    function updateCurrentPageServer() {
        var curr = wijmo.format('Page {index:n0} of {cnt:n0}', {
            index: viewServer.pageIndex + 1,
            cnt: viewServer.pageCount
        });
        document.getElementById('spanCurrentServer').textContent = curr;
    }

    // implement pager
    document.getElementById('pagerServer').addEventListener('click', function (e) {
        var btn = wijmo.closest(e.target, 'button');
        var id = btn ? btn.id : '';
        switch (id) {
            case 'btnFirstServer':
                viewServer.moveToFirstPage();
                break;
            case 'btnPrevServer':
                viewServer.moveToPreviousPage();
                break;
            case 'btnNextServer':
                viewServer.moveToNextPage();
                break;
            case 'btnLastServer':
                viewServer.moveToLastPage();
                break;
        }
    });

});