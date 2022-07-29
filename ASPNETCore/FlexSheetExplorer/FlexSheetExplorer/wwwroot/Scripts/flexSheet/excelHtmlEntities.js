c1.documentReady(function () {
   
    //export HtmlEntities
    var HtmlEntitiesflexSheet = wijmo.Control.getControl('#excelHtmlEntitiesSheet');
    var menu = wijmo.Control.getControl('#HtmlEntityConversion');
    generateHtmlSheet(HtmlEntitiesflexSheet);
    var items = [];
    for (var i = 0; i <= 3; i++) {
        items.push({ name: wijmo.grid.xlsx.HtmlEntityConversion[i], val: i })
    }
    menu.itemsSource = items;
    menu.displayMemberPath = "name";
    menu.selectedValuePath = "val";
});

//HtmlEntitiesConversion
function generateHtmlSheet(flexsheet) {
    var companies = ['&quot;Apple&quot;', '&lt;Google&gt;', 'M&amp;M', 'H&amp;M', '<s>Sony</s>', 'Volkswagen&#x2F;Audi'];
    flexsheet.setCellData(0, 0, 'ID')
    flexsheet.setCellData(0, 1, 'Company')
    flexsheet.setCellData(0, 2, 'Sales')
    for (var i = 0; i < companies.length; i++) {
        flexsheet.setCellData(i + 1, 0, i)
        flexsheet.setCellData(i + 1, 1, companies[i])
        flexsheet.setCellData(i + 1, 2, Math.random() * 100000)
    }
    flexsheet.columns.getColumn(1).isContentHtml = true;
    flexsheet.applyCellsStyle({
        fontWeight: 'bold', backgroundColor: '#96abb4', color: 'white'
    }, [new wijmo.grid.CellRange(0, 0, 0, 2)])
}

function exportHtmlEntities() {
    var flexSheet = wijmo.Control.getControl('#excelHtmlEntitiesSheet'), combo = wijmo.Control.getControl('#HtmlEntityConversion'),
        fileName = wijmo.getElement('#fileName2');
    if (flexSheet) {
        if (fileName.value == "") {
            fileName.value = 'FlexSheet.xlsx';
        }
        flexSheet.save(fileName.value, { convertHtmlEntities: combo.selectedItem.val });
    }
}