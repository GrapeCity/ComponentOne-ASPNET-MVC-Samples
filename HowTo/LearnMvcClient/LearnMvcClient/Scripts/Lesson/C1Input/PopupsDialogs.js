c1.documentReady(function () {
    // show dropDown when the user clicks the button
    // note that no event handler is needed here: setting the
    // Popup's owner is enough
    var dropDown = wijmo.Control.getControl('#dropDown');
    dropDown.owner = document.getElementById('btnDropDown');

    // show dialog when the user clicks the button
    // here there's no owner element, so we do need an event handler
    var dialog = wijmo.Control.getControl('#dialog');
    document.getElementById('btnDialog').addEventListener('click', function (e) {
        // show the dialog (modal window)
        dialog.show(true, function (sender) {

            // handle result when user closes the dialog
            if (dialog.dialogResult == 'wj-hide-ok') {
                alert('The dialog was submitted.');
            } else {
                alert('The dialog was canceled.');
            }
        });
    });
});