var wijmo;
(function (wijmo) {
    var material;
    (function (material) {
        // bootstrap Wijmo's MDL support when the document loads
        document.addEventListener('DOMContentLoaded', function () {
            bootstrap();
        });
        /**
         * Add Wijmo support for MDL's tabs and text input containers.
         */
        function bootstrap() {
            _bootstrapTabs();
            _bootstrapTextFields(true);
        }
        material.bootstrap = bootstrap;
        // invalidate Wijmo controls when the user switches tabs
        // so they will be laid our to fit the containing tab pane
        function _bootstrapTabs() {
            var tabs = document.querySelectorAll('.mdl-tabs__tab-bar');
            for (var i = 0; i < tabs.length; i++) {
                var tab = tabs[i];
                tab.addEventListener('click', function (e) {
                    if (wijmo.contains(tab, e.target)) {
                        wijmo.Control.invalidateAll(tab.parentElement);
                        _bootstrapTextFields(false);
                    }
                });
            }
        }
        // add event handlers to 
        // update the state of mdl-textfield elements containing Wijmo Input controls
        function _bootstrapTextFields(addHandlers) {
            var controls = document.querySelectorAll('.mdl-textfield>.wj-control');
            for (var i = 0; i < controls.length; i++) {
                var ctl = wijmo.Control.getControl(controls[i]);
                if (ctl) {
                    _updateTextFieldState(ctl);
                    if (addHandlers) {
                        if (ctl.placeholder) {
                            ctl.placeholder = '';
                        }
                        //ctl.hostElement.style.width = '100%';
                        ctl.gotFocus.addHandler(_updateTextFieldState);
                        ctl.lostFocus.addHandler(_updateTextFieldState);
                        if (ctl.textChanged) {
                            ctl.textChanged.addHandler(_updateTextFieldState);
                        }
                        if (ctl.valueChanged) {
                            ctl.valueChanged.addHandler(_updateTextFieldState);
                        }
                        if (ctl.isDroppedDownChanged) {
                            ctl.isDroppedDownChanged.addHandler(_updateTextFieldState);
                        }
                    }
                }
            }
        }
        // update the state of mdl-textfield elements containing Wijmo Input controls
        function _updateTextFieldState(s) {
            var container = wijmo.closest(s.hostElement, '.mdl-textfield'), input = s.inputElement;
            if (container && input) {
                wijmo.toggleClass(input, 'md-input', true);
                wijmo.toggleClass(container, 'is-dirty', input.value.length > 0);
                wijmo.toggleClass(container, 'is-focused', s.containsFocus());
                var wjValidationError = s.hostElement.wj_validation_error;
                if (wjValidationError && typeof(wjValidationError)=='function') {
                    var errorMsg = wjValidationError();
                    if (typeof (errorMsg) == 'boolean') {
                        errorMsg = errorMsg ? 'error' : '';
                    }
                    if (input.setCustomValidity) {
                        input.setCustomValidity(errorMsg);
                    }
                }
                wijmo.toggleClass(container, 'is-invalid', !input.validity.valid);
            }
        }
    })(material = wijmo.material || (wijmo.material = {}));
})(wijmo || (wijmo = {}));
//# sourceMappingURL=wijmo.material.js.map