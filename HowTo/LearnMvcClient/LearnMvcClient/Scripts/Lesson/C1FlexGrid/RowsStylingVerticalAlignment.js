c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');

    // make rows taller to show the vertical alignment
    theGrid.rows.defaultSize = 45;
    theGrid.columnHeaders.rows.defaultSize = 65;
    theGrid.allowResizing = 'Both';
    theGrid.deferResizing = true;
  
    // use formatItem event to apply transform
    theGrid.formatItem.addHandler(function (s, e) {
        if (!e.cell.querySelector('input')) {
            e.cell.innerHTML = '<div class="v-center">' +
            e.cell.innerHTML +
          '</div>';
        } else if (e.cell.querySelector('label')) {
            e.cell.children[0].classList.add("v-center-label");
        }
    });
});