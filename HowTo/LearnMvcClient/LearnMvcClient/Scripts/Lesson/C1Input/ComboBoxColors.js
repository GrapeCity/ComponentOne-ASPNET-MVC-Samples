c1.documentReady(function () {
    var theComboBox = wijmo.Control.getControl('#theComboBox');
    theComboBox.textChanged.addHandler(function (s, e) {
        document.getElementById('theColor').style.background = s.text;
    });
    theComboBox.formatItem.addHandler(function (s, e) {
        var template = '<span class="colorthumb" style="background:{data}">&nbsp;&nbsp;&nbsp;</span> {data}';
        e.item.innerHTML = wijmo.format(template, e);
    });
    theComboBox.itemsSource = 'Aqua,Azure,Black,Blue,Brown,Chartreuse,Chocolate,Coral,Crimson,Cyan,Fuchsia,Gold,Gray,Green,Khaki,Lavender,Lime,Navy,Olive,Orange,Orchid,Pink,Plum,Purple,Red,Silver,Tan,Turquoise,Violet,White,Yellow'.split(',');
});