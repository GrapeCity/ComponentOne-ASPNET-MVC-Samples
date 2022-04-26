c1.documentReady(function () {
    var tree = wijmo.Control.getControl('#theTree');

    tree.formatItem.addHandler(function (s, e) {
        if (e.dataItem.NewItem) {
            var imgUrl = newImageUrl;
            e.element.innerHTML +=
                '<img class="new-icon" src="' + imgUrl + '">';
        }
    });
});