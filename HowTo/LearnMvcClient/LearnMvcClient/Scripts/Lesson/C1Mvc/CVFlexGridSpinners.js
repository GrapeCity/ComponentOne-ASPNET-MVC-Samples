c1.documentReady(function () {
    //---------------------------------------------------------------------
    // grid to use with GIF spinner
    var theGridGif = wijmo.Control.getControl('#theGridGif');
    var theSpinnerGif = document.getElementById('theSpinnerGif');
    // load data into the grid
    document.getElementById('btn-load-gif').addEventListener('click', function () {

        // prepare to load data
        theGridGif.isDisabled = true;
        theGridGif.itemsSource = null;
        theGridGif.hostElement.insertBefore(theSpinnerGif, theGridGif.hostElement.firstChild);

        // load data with a delay
        getData(function (data) {
            theGridGif.itemsSource = data;
            theGridGif.isDisabled = false;
            theSpinnerGif.parentElement.removeChild(theSpinnerGif);
        }, 3000);
    })

    //---------------------------------------------------------------------
    //grid to use with Gauge spinner
    var theGridGauge = wijmo.Control.getControl('#theGridGauge');
    // load data into the grid
    document.getElementById('btn-load-gauge').addEventListener('click', function () {

        // prepare to load data
        theGridGauge.itemsSource = null;
        setSpinner(theGridGauge, true);

        // load data
        getData(function (data) {
            theGridGauge.itemsSource = data;
            setSpinner(theGridGauge, false);
        }, 3000);
    });

    // create a spinner Gauge
    var theSpinnerInterval;
    var theSpinnerGauge = wijmo.Control.getControl('#theSpinnerGauge');

    // add or remove a spinner to/from the grid
    function setSpinner(grid, on) {

        // stop spinner
        if (theSpinnerInterval) {
            clearInterval(theSpinnerInterval);
            theSpinnerInterval = null;
        }

        // add/remove spinner
        var spinner = theSpinnerGauge.hostElement;
        if (on) {
            grid.isDisabled = true;
            grid.hostElement.insertBefore(spinner, grid.hostElement.firstChild);
            theSpinnerGauge.invalidate();
            theSpinnerGauge.value = 0;
            theSpinnerInterval = setInterval(function () {
                theSpinnerGauge.value = (theSpinnerGauge.value + 1) % 100;
            }, 50);
        } else {
            grid.isDisabled = false;
            spinner.parentElement.removeChild(spinner);
        }
    }

    //---------------------------------------------------------------------
    // get some data asynchronously
    function getData(callback, delay) {
        var countries = 'US,UK,China,Germany,India'.split(','),
          data = [];
        for (var i = 0; i < 1000; i++) {
            data.push({
                id: i,
                country: countries[i % countries.length],
                sales: Math.random() * 1000,
                downloads: Math.random() * 10000,
            });
        }
        setTimeout(function () {
            callback(data)
        }, delay);
    }
});