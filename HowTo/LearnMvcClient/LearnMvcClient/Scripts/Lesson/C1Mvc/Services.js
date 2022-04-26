var remoteCollectionView;

function movePage(obj) {
    if (!remoteCollectionView) {
        return;
    }

    if (obj.name === 'Next') {
        remoteCollectionView.moveToNextPage();
    }else{
        remoteCollectionView.moveToPreviousPage();
    }
}

c1.documentReady(function () {
    // get the service with the specified id.
    remoteCollectionView = c1.getService('sCV');
});