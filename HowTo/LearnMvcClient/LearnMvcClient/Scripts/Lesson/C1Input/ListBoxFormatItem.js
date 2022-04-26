c1.documentReady(function () {
    var theListBox = wijmo.Control.getControl('#theListBox');

    // Wijmo glyphs
    var glyphs = 'asterisk,calendar,check,circle,clock,diamond,down,' +
  	'down-left,down-right,file,filter,left,minus,pencil,plus,right,' +
    'square,step-backward,step-forward,up,up-left,up-right';

    theListBox.formatItem.addHandler(function (s, e) {
        e.item.innerHTML = '<div class="wj-glyph">' +
      	'<span class="wj-glyph-' + e.data + '"></span>' +
        '</div>' +
        e.data;
    });

    theListBox.selectedIndexChanged.addHandler(function (s, e) {
        document.getElementById('selectedItem').innerHTML =
       '<span class="wj-glyph-' + s.selectedItem + '"></span>';
    });

    theListBox.itemsSource = glyphs.split(',');
});