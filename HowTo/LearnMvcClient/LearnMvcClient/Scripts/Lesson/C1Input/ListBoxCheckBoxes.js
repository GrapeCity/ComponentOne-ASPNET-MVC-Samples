c1.documentReady(function () {
    var theListBox = wijmo.Control.getControl('#theListBox');
    theListBox.checkedMemberPath = "selected";
    theListBox.checkedItemsChanged.addHandler(function (s, e) {
        var arr = s.checkedItems,
      	  html = '';
        for (var i = 0; i < arr.length; i++) {
            html += wijmo.format('<li>{Country}</li>', arr[i]);
        }
        document.getElementById('checkedItems').innerHTML = html;
    });
});