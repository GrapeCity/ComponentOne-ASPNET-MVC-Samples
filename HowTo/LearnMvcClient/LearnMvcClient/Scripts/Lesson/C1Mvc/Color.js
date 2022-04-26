c1.documentReady(function () {
    var clrStart = wijmo.Control.getControl('#clrStart');
    var clrEnd = wijmo.Control.getControl('#clrEnd');
    clrStart.valueChanged.addHandler(interpolate);
    clrEnd.valueChanged.addHandler(interpolate);

    interpolate();

    // interpolate colors
    var animInt;
    function interpolate() {

        // remove old gradient
        var tr = document.getElementById('theColorRow');
        while (tr.hasChildNodes()) {
            tr.removeChild(tr.lastChild);
        }

        // calculate new gradient
        var c1 = new wijmo.Color(clrStart.value),
            c2 = new wijmo.Color(clrEnd.value),
        cnt = 80;
        for (var i = 0; i < cnt; i++) {
            var td = document.createElement('td');
            td.innerHTML = '&nbsp;'
            td.style.background = wijmo.Color.interpolate(c1, c2, i / cnt);
            tr.appendChild(td);
        }

        // animate the color
        var theColor = document.getElementById('theColor');
        clearInterval(animInt);
        animInt = wijmo.animate(function (pct) {
            theColor.style.background = wijmo.Color.interpolate(c1, c2, pct);
        }, 2000);

    }
});