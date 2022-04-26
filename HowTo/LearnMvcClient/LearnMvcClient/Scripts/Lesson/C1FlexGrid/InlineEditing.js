c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');
    // make rows taller to accommodate edit buttons and input controls
    theGrid.rows.defaultSize = 40;

    // custom formatter to paint buttons and editors
    theGrid.formatItem.addHandler(function(s, e) {
        if (e.panel == s.cells) {
            var col = s.columns[e.col],
                  item = s.rows[e.row].dataItem;

            if (item == currentEditItem) {

                // create editors and buttons for the item being edited
                switch (col.binding) {
                    case 'Buttons':
                        e.cell.innerHTML = document.getElementById('tplBtnEditMode').innerHTML;
                        e.cell.dataItem = item;
                        break;
                    case 'Country':
                    case 'Sales':
                    case 'Expenses':
                        e.cell.innerHTML = '<input class="form-control" ' +
                          'id="' + col.binding + '" ' +
                          'value="' + s.getCellData(e.row, e.col, true) + '"/>';
                        break;
                }
            } else {

                // create buttons for items not being edited
                switch (col.binding) {
                    case 'Buttons':
                        e.cell.innerHTML = document.getElementById('tplBtnViewMode').innerHTML;
                        e.cell.dataItem = item;
                        break;
                }
            }
        }
    });

    // handle button clicks
    theGrid.addEventListener(theGrid.hostElement, 'click', function (e) {
        var target = e.target;
        if (e.target.tagName != 'BUTTON') {
            target = e.target.parentNode;
        }

        if (target != null && target.tagName == 'BUTTON') {

            // get button's data item
            var item = wijmo.closest(e.target, '.wj-cell').dataItem;

            // handle buttons
            switch (target.id) {

                // start editing this item
                case 'btnEdit':
                    editItem(item);
                    break;

                    // remove this item from the collection
                case 'btnDelete':
                    theGrid.collectionView.remove(item);
                    break;

                    // commit edits
                case 'btnOK':
                    commitEdit();
                    break;

                    // cancel edits
                case 'btnCancel':
                    cancelEdit();
                    break;
            }
        }
    });

    // exit edit mode when scrolling the grid or losing focus
    theGrid.scrollPositionChanged.addHandler(cancelEdit);
    theGrid.lostFocus.addHandler(cancelEdit);
    theGrid.resizingColumn.addHandler(cancelEdit);
    theGrid.draggingColumn.addHandler(cancelEdit);
    theGrid.sortingColumn.addHandler(cancelEdit);

    // editing commands
    var currentEditItem = null;

    function editItem(item) {
        cancelEdit();
        currentEditItem = item;
        theGrid.invalidate();
    }

    function commitEdit() {
        if (currentEditItem) {
            theGrid.columns.forEach(function (col) {
                var input = theGrid.hostElement.querySelector('#' + col.binding);
                if (input) {
                    var value = wijmo.changeType(input.value, col.dataType, col.format);
                    if (wijmo.getType(value) == col.dataType) {
                        currentEditItem[col.binding] = value;
                    }
                }
            });
        }
        currentEditItem = null;
        theGrid.invalidate();
    }

    function cancelEdit() {
        if (currentEditItem) {
            currentEditItem = null;
            theGrid.invalidate();
        }
    }
});