c1.documentReady(function () {
    var tabDetached = wijmo.Control.getControl('#tabDetached');

    // hide the built-in content area
    tabDetached.hostElement.querySelector('.wj-tabpanes').style.display = 'none';

    tabDetached_selectedIndexChanged(tabDetached);
});

function tabDetached_selectedIndexChanged(s, e) {
    var div = document.getElementById('detachedContent');
    div.innerHTML = 'Content for tab <b>' +
        s.selectedTab.header.textContent +
        '</b>...';
}