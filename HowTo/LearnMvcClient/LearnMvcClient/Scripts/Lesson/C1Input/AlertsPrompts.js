c1.documentReady(function () {
    var popup = wijmo.Control.getControl('#popup');

    // alert popup
    function alertPopup(options, callback) {
        updateDialog(options);
        popup.show(true, function (sender) {
            if (callback) {
                callback(sender.dialogResult)
            }
        });
    }

    // prompt popup
    function propmtPopup(options, callback) {
        updateDialog(options, true);
        popup.show(true, function (sender) {
            if (callback) {
                var result = sender.dialogResult.indexOf('ok') > -1
                    ? popup.hostElement.querySelector('input').value
                  : null;
                callback(result);
            }
        });
    }

    // update dialog to use as an alert or prompt
    function updateDialog(options, input) {

        // fill out default options
        options.ok = options.ok || 'OK';
        options.cancel = options.cancel || 'Cancel';
        options.clsDialog = options.clsDialog || 'wj-dialog';
        options.clsHeader = options.clsHeader || 'wj-dialog-header';
        options.clsBody = options.clsBody || 'wj-dialog-body';
        options.clsInput = options.clsInput || 'wj-control';
        options.clsFooter = options.clsFooter || 'wj-dialog-footer';
        options.clsOK = options.clsOK || 'wj-btn';
        options.clsCancel = options.clsCancel || 'wj-btn';

        // create dialog
        var template = '<div class="{clsDialog}" style="width:100%;" role="dialog">' +
          '<div class="{clsHeader}">' +
          '<h4>{header}</h4>' +
          '</div>' +
          '<div class="{clsBody}">' +
          '<p>{body}</p>' +
          (input ? '<input class="{clsInput}" value="{defVal}">' : '') +
          '</div>' +
          '<div class="{clsFooter}">' +
          '<button class="{clsOK} wj-hide-ok">{ok}</button>' +
          (options.cancel ? '<button class="{clsCancel} wj-hide">{cancel}</button>' : '') +
          '</div>' +
          '</div>';
        var dialog = wijmo.createElement(wijmo.format(template, options));
        popup.content = dialog;

        // honor 'small' option
       popup.hostElement.style.width = options.small ? '20%' : '40%'
    }

    // show the Alert/Prompt
    document.getElementById('btnShow').addEventListener('click', function () {
        var options = getOptions();
        if (cmbType.text == 'Alert') {
            alertPopup(options, function (result) {
                alert('you entered: ' + result);
            });
        } else {
            propmtPopup(options, function (result) {
                alert('you entered: ' + result);
            });
        }
    });

    var cmbType = wijmo.Control.getControl('#type');
    cmbType.textChanged.addHandler(function (s, e) {
        document.getElementById('btnShow').textContent = 'Show ' + s.text;
    });
    
    var header = wijmo.Control.getControl('#header');
    var body = wijmo.Control.getControl('#body');
    var defVal = wijmo.Control.getControl('#defVal');
    var ok = wijmo.Control.getControl('#ok');
    var cancel = wijmo.Control.getControl('#cancel');
    var clsDialog = wijmo.Control.getControl('#clsDialog');
    var clsHeader = wijmo.Control.getControl('#clsHeader');
    var clsBody = wijmo.Control.getControl('#clsBody');
    var clsInput = wijmo.Control.getControl('#clsInput');
    var clsFooter = wijmo.Control.getControl('#clsFooter');
    var clsOK = wijmo.Control.getControl('#clsOK');
    var clsCancel = wijmo.Control.getControl('#clsCancel');

    function getChecked(id) {
        return document.getElementById(id).checked;
    }

    // load options into an object
    function getOptions() {
        return {
            header: header.text,
            body: body.text,
            defVal: defVal.text,
            small: getChecked('small'),
            ok: ok.text,
            cancel: cancel.text,
            clsDialog: clsDialog.text,
            clsHeader: clsHeader.text,
            clsBody: clsBody.text,
            clsInput: clsInput.text,
            clsFooter: clsFooter.text,
            clsOK: clsOK.text,
            clsCancel: clsCancel.text,
        }
    }
});