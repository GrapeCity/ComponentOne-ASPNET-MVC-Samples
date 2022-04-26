c1.documentReady(function () {
    var radial = wijmo.Control.getControl('#radial');

    // add some tooltips
    var tt = new wijmo.Tooltip(),
          host = radial.hostElement;
    tt.setTooltip(host.querySelector('.wj-face'), 'Face');
    tt.setTooltip(host.querySelector('.wj-pointer'), 'Pointer');
    tt.setTooltip(host.querySelector('.wj-thumb'), 'Thumb');
    tt.setTooltip(host.querySelector('.wj-ranges'), 'Ranges');
    tt.setTooltip(host.querySelector('.wj-ticks'), 'Tickmarks');
    tt.setTooltip(host.querySelector('.wj-value'), 'Text: Value');
    tt.setTooltip(host.querySelector('.wj-min'), 'Text: Min');
    tt.setTooltip(host.querySelector('.wj-max'), 'Text: Max');
});