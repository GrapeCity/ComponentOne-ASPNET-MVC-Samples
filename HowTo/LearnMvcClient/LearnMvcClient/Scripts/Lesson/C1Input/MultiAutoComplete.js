c1.documentReady(function () {
    var theMultiAutoComplete = wijmo.Control.getControl('#theMultiAutoComplete');
    theMultiAutoComplete.selectedItemsChanged.addHandler(function (s, e) {
        var arr = s.selectedItems,
      	  html = '';
        for (var i = 0; i < arr.length; i++) {
            html += wijmo.format('<li>{Country}</li>', arr[i]);
        }
        document.getElementById('selectedItems').innerHTML = html;
    });
});