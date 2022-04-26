c1.documentReady(function () {
    var theChart = wijmo.Control.getControl('#theChart');
    var fitType = wijmo.Control.getControl('#fitType');
    var order = wijmo.Control.getControl('#order');

    theChart.itemsSource = getData();

    // show the equation on the chart
    var equation = document.getElementById('equation');

    // create a TrendLine and add it to the Chart series collection
    var trendLine = new wijmo.chart.analytics.TrendLine();
    trendLine.binding = 'y';
    trendLine.style = { stroke: 'darkred', strokeWidth: 3 };
    trendLine.visibility = 'Hidden';
    theChart.series.push(trendLine);

    // select trendline order
    order.valueChanged.addHandler(function (s, e) {
        if (s.value > 0) {
            trendLine.order = s.value;
            showEquation();
        }
    });

    // select fit type
    fitType.textChanged.addHandler(function (s, e) {
        trendLine.name = s.text;
        if (s.text) { // show trendline
            trendLine.fitType = s.text;
            trendLine.visibility = 'Visible';
        } else { // hide trendline
            trendLine.visibility = 'Hidden';
        }
        switch (s.text) { // enable/disable order input
            case 'Polynomial':
            case 'Fourier':
                order.isDisabled = false;
                break;
            default:
                order.isDisabled = true;
                break;
        }
        showEquation();
    });

    // show updated equation on a timeOut since the TrendLine update is async
    function showEquation() {
        equation.innerHTML = '';
        setTimeout(function () {
            equation.innerHTML = trendLine.getEquation();
        }, 100);
    }

    // randomize the data
    document.getElementById('btnRandomize').addEventListener('click', function () {
        theChart.itemsSource = getData();
        showEquation();
    });

    // data sources
    function getData() {
        var arr = [],
                cnt = 50,
            a = Math.random(),
            b = Math.random(),
                x = 1;
        for (var i = 1; i < cnt; i++) {
            arr.push({
                x: i,
                y: a + i * b + i * (Math.random() - .5)
            });
        }
        return arr;
    }
});