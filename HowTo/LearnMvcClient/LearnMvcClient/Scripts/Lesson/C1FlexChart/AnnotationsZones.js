c1.documentReady(function () {
    var theChart = wijmo.Control.getControl('#theChart');

    // add annotation layer
    var annotations = new wijmo.chart.annotation.AnnotationLayer(theChart, [
        {
            type: 'Rectangle',
            attachment: 'DataCoordinate',
            point: { x: new Date(2017, 0, 1), y: 855 },
            position: 1, // Center Top Bottom Left Right
            width: 20000, height: 20000,
            style: { fill: 'red', opacity: .15 }
        },
        {
            type: 'Rectangle',
            attachment: 'DataCoordinate',
            point: { x: new Date(2017, 0, 1), y: 815 },
            position: 2, // Center Top Bottom Left Right
            width: 20000, height: 20000,
            style: { fill: 'green', opacity: .15 }
        },
    ]);
});