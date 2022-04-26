c1.documentReady(function () {
    var tt = new wijmo.Tooltip();
    tt.setTooltip('#setTooltip', '#setTooltipDocs');
    tt.setTooltip('#theSpan', 'This is the <b>first</b> span.');
    tt.setTooltip('#theOtherSpan', 'This is the <b>second</b> span.');
    tt.setTooltip('#btnAddNew', 'Click to add 1,000 items using the <b>addNew</b> method.');
    tt.setTooltip('#btnPush', 'Click to add 1,000 items using the <b>push</b> method.');
    tt.setTooltip('#btnPushDefer', 'Click to add 1,000 items using the <b>push</b> method<br>within a <b>deferUpdate</b> block.');
});