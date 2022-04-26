function InitialControls() {
    var ListBox = wijmo.Control.getControl("#lbListBox");
    selectedIndexChanged(ListBox);

    if (wijmo.culture.Globalize.name == 'ja') {
        var itMask = wijmo.Control.getControl('#itMask');
        itMask.mask = '00:00 ＺＺ';
        itMask.refresh();
    }
}

function inputTime_ValueChanged(sender) {
    var inputDateTime = wijmo.Control.getControl("#iditInputDateTime");
    inputDateTime.value = wijmo.DateTime.fromDateTime(inputDateTime.value, sender.value);
}

function inputDate_ValueChanged(sender) {
    var inputDateTime = wijmo.Control.getControl("#iditInputDateTime");
    inputDateTime.value = wijmo.DateTime.fromDateTime(sender.value, inputDateTime.value);
}

function inputDateTime_ValueChanged(sender) {
    var inputDate = wijmo.Control.getControl("#iditInputDate");
    var inputTime = wijmo.Control.getControl("#iditInputTime");
    inputDate.value = wijmo.DateTime.fromDateTime(sender.value, inputDate.value);
    inputTime.value = wijmo.DateTime.fromDateTime(inputTime.value, sender.value);
    document.getElementById('iditSelectedValue').innerHTML = wijmo.Globalize.format(sender.value, 'MMM dd, yyyy h:mm:ss tt');
}

//selectedIndexChanged event handler
function selectedIndexChanged(sender) {
    //set selectedIndex and selectedValue text
    if (document.getElementById("lbListBox") && document.getElementById("lbSelIdx") && document.getElementById("lbSelVal")) {//if (document.readyState === "complete") {
        document.getElementById('lbSelIdx').innerHTML = sender.selectedIndex;
        document.getElementById('lbSelVal').innerHTML = sender.selectedValue;
    }
}

// InputMask valueChanged event handler
function MaskvalueChanged(sender) {
    var customMaskTrial = wijmo.Control.getControl("#imCustomTrial");
    customMaskTrial.mask = sender.value;
    customMaskTrial.hostElement.title = 'Mask: ' + (sender.value || 'N/A');
}

function itemClicked(sender) {
    alert('You\'ve selected option ' + sender.selectedIndex + ' from the ' + sender.header + ' menu!');
}

function execute(arg) {
    var inputNumber = wijmo.Control.getControl("#mInputNumber");

    // convert argument to Number
    arg = wijmo.changeType(arg, wijmo.DataType.Number);

    // check if the conversion was successful
    if (wijmo.isNumber(arg)) {

        // update the value
        inputNumber.value += arg;
    }
}

function canExecute(arg) {
    var inputNumber = wijmo.Control.getControl("#mInputNumber");

    // convert argument to Number
    arg = wijmo.changeType(arg, wijmo.DataType.Number);

    // check if the conversion was successful
    if (wijmo.isNumber(arg)) {
        var val = inputNumber.value + arg;

        // check if the value is valid
        return val >= 0 && val <= 1;
    }

    return false;
}