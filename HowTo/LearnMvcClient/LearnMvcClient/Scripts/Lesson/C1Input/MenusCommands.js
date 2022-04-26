c1.documentReady(function () {
    var currentTax = wijmo.Control.getControl('#currentTax');
    var changeTax = wijmo.Control.getControl('#changeTax');

    // set command object for the tax menu
    changeTax.command = {
        // execute the command
        executeCommand: function (arg) {
            arg = wijmo.changeType(arg, wijmo.DataType.Number);
            if (wijmo.isNumber(arg)) {
                currentTax.value += arg;
            }
        },

        // check if a command can be executed
        canExecuteCommand: function (arg) {
            arg = wijmo.changeType(arg, wijmo.DataType.Number);
            if (wijmo.isNumber(arg)) {
                var val = currentTax.value + arg;
                return val >= 0 && val <= 1;
            }
            return false;
        }
    };
});