c1.documentReady(function () {
    var theMenuFile = wijmo.Control.getControl('#theMenuFile');
    initMenu(theMenuFile, 'menuFile');
    theMenuFile.itemClicked.addHandler(menuClick);

    var theMenuEdit = wijmo.Control.getControl('#theMenuEdit');
    initMenu(theMenuEdit, 'menuEdit');
    theMenuEdit.itemClicked.addHandler(menuClick);

    // use the same event handler for both
    function menuClick(s, e) {
        var msg = wijmo.format('You selected option **{selectedIndex}** from menu **{header}**', s);
        alert(msg);
    }

    // init menu from markup
    function initMenu(menu, elementId) {

        // get host element, header, items
        var host = document.getElementById(elementId);
        var header = host.firstChild.textContent.trim();
        var items = host.querySelectorAll('div');
        var menuItems = [];
        for (var i = 0; i < items.length; i++) {
            var item = items[i];
            // the default displayMemberPath is "Header"
            menuItems.push({ Header: item.outerHTML });
        }

        // clear host and instantiate menu
        host.innerHTML = '';
        menu.header= header;
        menu.itemsSource = menuItems;
    }
});