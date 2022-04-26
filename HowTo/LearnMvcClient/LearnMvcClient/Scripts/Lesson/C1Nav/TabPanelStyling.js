c1.documentReady(function () {
    var theTabPanel = wijmo.Control.getControl('#theTabPanel');
    // change tab position
    var tabPosition = new wijmo.input.ComboBox('#tabPosition', {
        itemsSource: 'Left,Right,Above,Below'.split(','),
        selectedIndexChanged: function (s, e) {
            var host = theTabPanel.hostElement;
            s.itemsSource.forEach(function (pos, index) {
                var clsName = 'tabs-' + pos.toLowerCase();
                wijmo.toggleClass(host, clsName, index == s.selectedIndex);
            });
        },
        selectedIndex: 2, // above is the default
    });

    // toggle animation
    document.getElementById('isAnimated').addEventListener('click', function (e) {
        theTabPanel.isAnimated = e.target.checked;
    });

    // toggle custom headers
    document.getElementById('customHeaders').addEventListener('click', function (e) {
        wijmo.toggleClass(theTabPanel.hostElement, 'custom-headers', e.target.checked);
    });

    //  change tab alignment
    var tabAlign = new wijmo.input.ComboBox('#tabAlign', {
        itemsSource: 'Left,Center,Right'.split(','),
        selectedIndexChanged: function (s, e) {
            var host = theTabPanel.hostElement,
                headers = host.querySelector('.wj-tabheaders');
            headers.style.textAlign = s.text;
        }
    });
});