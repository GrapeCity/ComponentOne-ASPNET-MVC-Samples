c1.documentReady(function () {
    var formats = 'd|D|M|d/M/yy|MMMM dd, yyyy'.split('|');
    for (var i = 0; i < formats.length; i++) {
        var theDate = wijmo.Control.getControl('#theInputDateF' + i);
        theDate.format = formats[i];
    }
});