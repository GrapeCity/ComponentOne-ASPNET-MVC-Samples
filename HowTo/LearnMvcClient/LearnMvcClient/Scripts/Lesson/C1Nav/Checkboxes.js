c1.documentReady(function () {
    var tree = wijmo.Control.getControl('#theTree');

    tree.showCheckboxes = true;
    tree.checkedItemsChanged.addHandler(function (s, e) {
        var items = s.checkedItems,
            msg = '';
        if (items.length) {
            msg = '<p><b>Checked Items:</b></p><ol>\r\n';
            for (var i = 0; i < items.length; i++) {
                msg += '<li>' + items[i].Header + '</li>\r\n';
            }
            msg += '</ol>';
        }
        document.getElementById('tvChkStatus').innerHTML = msg;
    });

    // handle buttons
    document.getElementById('btnCheckAll').addEventListener('click', function (e) {
        tree.checkAllItems(true);
    });
    document.getElementById('btnUncheckAll').addEventListener('click', function (e) {
        tree.checkAllItems(false);
    });
    document.getElementById('btnSaveState').addEventListener('click', function (e) {
        checkedItems = tree.checkedItems || [];
    });
    document.getElementById('btnRestoreState').addEventListener('click', function (e) {
        tree.checkedItems = checkedItems;
    });
});