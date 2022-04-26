c1.documentReady(function () {
    var tree = wijmo.Control.getControl('#theTree');

    // handle checkboxes
    document.getElementById('rtl').addEventListener('click', function (e) {
        var content = document.getElementById('content');
        content.setAttribute('dir', e.target.checked ? 'rtl' : '');
    });
});