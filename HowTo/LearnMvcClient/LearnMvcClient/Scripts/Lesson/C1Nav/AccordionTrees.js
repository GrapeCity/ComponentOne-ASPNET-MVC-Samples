c1.documentReady(function () {
    var tree = wijmo.Control.getControl('#theTree');

    tree.itemsSource = getData();
    tree.displayMemberPath = 'header';
    tree.childItemsPath = 'items';
    tree.isContentHtml = true;

    // toggle accordion style
    document.getElementById('customCss').addEventListener('click', function (e) {
        wijmo.toggleClass(tree.hostElement, 'accordion', e.target.checked);
    });

    // handle clicks on accordion items
    tree.hostElement.addEventListener('click', function (e) {
        if (e.target.tagName == 'A') {
            var msg = document.getElementById('msg');
            msg.innerHTML = wijmo.format('Navigating to <b>** {href} **</b>',
              e.target);
            e.preventDefault();
        }
    });

    // get the tree data
    function getData() {
        return [
            {
                header: 'Angular', items: [
                    { header: '<a href="ng/intro">Introduction</a>' },
                    { header: '<a href="ng/samples">Samples</a>' },
                    { header: '<a href="ng/perf">Performance</a>' }]
            },
            {
                header: 'React', items: [
                    { header: '<a href="rc/intro">Introduction</a>' },
                    { header: '<a href="rc/samples">Samples</a>' },
                    { header: '<a href="rc/perf">Performance</a>' }]
            },
            {
                header: 'Vue', items: [
                    { header: '<a href="vue/intro">Introduction</a>' },
                    { header: '<a href="vue/samples">Samples</a>' },
                    { header: '<a href="vue/perf">Performance</a>' }]
            },
            {
                header: 'Knockout', items: [
                  { header: '<a href="ko/intro">Introduction</a>' },
                  { header: '<a href="ko/samples">Samples</a>' },
                  { header: '<a href="ko/perf">Performance</a>' }]
            }
        ];
    }
});