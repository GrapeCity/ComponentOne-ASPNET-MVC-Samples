c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');

    // animate grid when user clicks the button
    document.getElementById('btnAnimate').addEventListener('click', function () {
        wijmo.animate(function (pct) {

            // calculate transform for given percent (zero to one)
            var xform = '';
            if (pct < 1) {
                if (pct > .5) pct = pct - 1;
                xform = 'rotateY( ' + (pct * 180) + 'deg)';
            }

            // apply the transform to the grid element
            theGrid.hostElement.style.transform = xform;
        }, 2000); // animate for two seconds
    })
});