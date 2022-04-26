c1.documentReady(function () {
    var menu = wijmo.Control.getControl('#ctxMenu');
    menu.itemClicked.addHandler(function (s, e) {
        alert('Executing **' + menu.selectedValue + '** for element **' + menu.owner.id + '**');
    });

    // use it as a context menu for one or more elements
    var els = document.querySelectorAll('.has-ctx-menu');
    for (var i = 0; i < els.length; i++) {
        els[i].addEventListener('contextmenu', function (e) {
            menu.owner = wijmo.closest(e.target, '.has-ctx-menu');
            if (menu.owner) {
                e.preventDefault();
                menu.show(e);
            }
        }, true);
    }
});