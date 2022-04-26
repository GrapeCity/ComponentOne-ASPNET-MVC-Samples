c1.documentReady(function(){
    var theChart = wijmo.Control.getControl('#theChart');
    var chartType = wijmo.Control.getControl('#chartType');
    var errorAmount = wijmo.Control.getControl('#errorAmount');

    theChart.itemsSource = getData();

    // create an ErrorBar series and add it to the Chart
    var errorBar = new wijmo.chart.analytics.ErrorBar();
    errorBar.errorBarStyle = {
        stroke: 'darkred',
        strokeWidth: 3
    };
    theChart.series.push(errorBar);

    // select chart type
    chartType.textChanged.addHandler(function (s, e) {
        theChart.chartType = s.text;
    });

    // select error mode/amount
    errorAmount.textChanged.addHandler(function (s, e) {
        var value = s.selectedItem.value;
        errorBar.value = value;
        errorBar.binding = value != null ? 'amount' : 'amount,errorPlus,errorMinus';
        errorBar.errorAmount = s.selectedItem.mode;
    });
    errorAmount.itemsSource = [
      { hdr: 'Bound Error Values', value: null, mode: 'Custom' },
      { hdr: '15%', value: .15, mode: 'Percentage' },
      { hdr: '5 units', value: 5, mode: 'FixedValue' },
      { hdr: '1.5 Std Dev', value: 1.5, mode: 'StandardDeviation' },
      { hdr: 'Standard Error', value: 1, mode: 'StandardError' },
      { hdr: 'Plus 5, Minus 10', value: { plus: 5, minus: 10 }, mode: 'Custom' }
    ];

    // randomize the data
    document.getElementById('btnRandomize').addEventListener('click', function () {
        theChart.itemsSource = getData();
    });

    // generate some random data
    function getData() {
        var arr = [],
                date = new Date();
        for (var i = 0; i < 10; i++) {
            arr.push({
                date: date,
                amount: 20 + 80 * Math.random(),
                errorPlus: 10 * Math.random(),
                errorMinus: 10 * Math.random(),
            });
            date = wijmo.DateTime.addMonths(date, 1);
        }
        return arr;
    }
});