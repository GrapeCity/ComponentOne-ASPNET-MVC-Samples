c1.documentReady(function () {
    var formats = 'n0,n2,c0,c2,p0,p2'.split(',');
    for (var i = 0; i < formats.length; i++) {
        var fmt = formats[i];
        var theNumber = wijmo.Control.getControl('#theInputNumberF' + fmt);
        theNumber.format = fmt;
    }
});