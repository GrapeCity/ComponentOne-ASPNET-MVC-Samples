c1.documentReady(function () {
    var frmLogin = wijmo.Control.getControl('#frmLogin');
    var frmCreateAccount = wijmo.Control.getControl('#frmCreateAccount');
    var frmEditAccount = wijmo.Control.getControl('#frmEditAccount');

    initDialog(frmLogin);
    initDialog(frmCreateAccount);
    initDialog(frmEditAccount);

    // init dialogs with transition effects
    function initDialog(dlg) {
        var host = dlg.hostElement;
        dlg.fadeIn = false; // disable standard animation effect
        dlg.shown.addHandler(function (s, e) {
            wijmo.toggleClass(host, 'custom-animation-visible', true);
        });
        dlg.hiding.addHandler(function (s, e) {
            wijmo.toggleClass(host, 'custom-animation-visible', false);
        });
        wijmo.toggleClass(host, 'custom-animation', true);
    }

    // show forms
    document.getElementById('btnLogin').addEventListener('click', function () {
        frmLogin.show(true, function (sender) {
            switch (sender.dialogResult) {
                case 'submit':
                    alert('form submitted');
                    break;
                case 'wj-hide-create':
                    btnCreateAccount.click(); // open the Create Account form
                    break;
            }
        });
    });
    document.getElementById('btnCreateAccount').addEventListener('click', function () {
        frmCreateAccount.show(true, function (sender) {
            if (sender.dialogResult == 'submit') {
                alert('form submitted');
            }
        });
    });
    document.getElementById('btnEditAccount').addEventListener('click', function () {
        frmEditAccount.show(true, function (sender) {
            if (sender.dialogResult == 'submit') {
                alert('form submitted');
            }
        });
    });

    // validate the form but don't submit
    document.body.addEventListener('submit', function (e) {
        e.preventDefault(); // don't submit
        if (e.target.checkValidity()) {
            var dlg = wijmo.Control.getControl(e.target);
            dlg.hide('submit'); // close the dialog passing a dialogResult
        }
    });

    // validate password/new password confirmation
    ensureSameValue('password', 'confirm');
    ensureSameValue('newPassword', 'newConfirm');
    function ensureSameValue(f1, f2) {
        var inputs = [document.getElementById(f1), document.getElementById(f2)];
        inputs.forEach(function (input) {
            input.addEventListener('input', function (e) {
                var err = inputs[0].value != inputs[1].value ? 'Passwords don\'t match.' : '';
                inputs[1].setCustomValidity(err);
            });
        })
    }
});