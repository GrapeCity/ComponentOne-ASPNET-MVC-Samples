c1.documentReady(function () {
    var theComboBox = wijmo.Control.getControl('#theComboBox');
    var theDate = wijmo.Control.getControl('#theDate');

    // customize the DropDowns
    document.getElementById('isAnimated').addEventListener('click', function (e) {
        theComboBox.isAnimated = e.target.checked;
        theDate.isAnimated = e.target.checked;
    });
    document.getElementById('isDroppedDown').addEventListener('click', function (e) {
        theComboBox.isDroppedDown = e.target.checked;
        //theDate.isDroppedDown = e.target.checked;
    });
    document.getElementById('dropDownCssClass').addEventListener('click', function (e) {
        theComboBox.dropDownCssClass = e.target.checked ? 'custom-dd' : '';
        theDate.dropDownCssClass = e.target.checked ? 'custom-dd' : '';
    });
});