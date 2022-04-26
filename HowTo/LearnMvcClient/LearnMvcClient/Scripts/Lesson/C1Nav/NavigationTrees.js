c1.documentReady(function () {
    var theTree = wijmo.Control.getControl('#theTree');
    theTree.itemClicked.addHandler(function (s, e) {
        var msg = document.getElementById('msg');
        msg.innerHTML = wijmo.format('Navigating to <b>** {Header} **</b>',
          s.selectedItem);
    });
});