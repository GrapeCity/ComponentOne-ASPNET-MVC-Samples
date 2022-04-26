c1.documentReady(function () {
    var popClickBlur = wijmo.Control.getControl('#popClickBlur');
    popClickBlur.showTrigger = 'Click';
    popClickBlur.hideTrigger = 'Blur';

    var popClickClick = wijmo.Control.getControl('#popClickClick');
    popClickClick.showTrigger = 'Click';
    popClickClick.hideTrigger = 'Click';

    var popClickNone = wijmo.Control.getControl('#popClickNone');
    popClickNone.showTrigger = 'Click';
    popClickNone.hideTrigger = 'None';
});