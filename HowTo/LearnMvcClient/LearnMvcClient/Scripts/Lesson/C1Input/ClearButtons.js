c1.documentReady(function () {
    var ctls = [];
    ctls.push(wijmo.Control.getControl('#theInputDate'));
    ctls.push(wijmo.Control.getControl('#theInputTime'));
    ctls.push(wijmo.Control.getControl('#theInputDateTime'));
    ctls.push(wijmo.Control.getControl('#theInputNumber'));
    ctls.push(wijmo.Control.getControl('#theComboBox'));

    // add clear button to all the controls
    ctls.forEach(function (ctl) {
        addClearButton(ctl)
    });

    var inputEvent = document.createEvent('HTMLEvents');
    inputEvent.initEvent('input', true, false);

    // add clear button to a C1 MVC input control
    function addClearButton(ctl) {
        var host = ctl.hostElement,
                input = ctl.inputElement;
        host.classList.remove('wj-state-empty');
        host.classList.add('wj-clear-input');
        
        // add wj clear input icon
        var wjClearInputICon = document.createElement('span');
        wjClearInputICon.classList.add("wj-clear-input-icon");
        input.parentNode.insertBefore(wjClearInputICon, input.nextSibling);

        host.addEventListener('click', function (e) {
            if (e.target.classList.contains("wj-clear-input-icon") && !wijmo.closest(e.target, '.wj-state-empty')) {
                //e.preventDefault();
                e.stopImmediatePropagation();
                input.value = '';
                input.dispatchEvent(inputEvent);
                input.focus();
            }
        }, true);
    }
});