c1.documentReady(function () {
    var theChart = wijmo.Control.getControl('#theChart');

    // add annotation layer
    var annotations = new wijmo.chart.annotation.AnnotationLayer(theChart, [
        {
            type: 'Rectangle',
            attachment: 'DataIndex',
            pointIndex: 33,
            position: 'Top',
            tooltip: 'Something <b>negative</b><br/>happened today...',
            content: 'N',
            width: 20, height: 20,
            style: { fill: '#01DFD7', stroke: '#000000' }
        },
        {
            type: 'Rectangle',
            attachment: 'DataIndex',
            pointIndex: 27,
            position: 'Top',
            tooltip: 'Something <b>positive</b><br/>happened today...',
            content: 'P',
            width: 20, height: 20,
            style: { fill: '#01DFD7', stroke: '#000000' }
        },
        {
            type: 'Image',
            href: 'https://maps.google.com/mapfiles/marker_green.png',
            width: 15, height: 30,
            attachment: 'DataCoordinate',
            point: { x: new Date(2017, 1, 7), y: 800 },
            tooltip: 'Time to buy!',
        },
        {
            type: 'Image',
            href: 'https://maps.google.com/mapfiles/marker_yellow.png',
            width: 15, height: 30,
            attachment: 'DataCoordinate',
            point: { x: new Date(2017, 1, 24), y: 800 },
            tooltip: 'Turbulence ahead...',
        },
        {
            type: 'Image',
            href: 'https://maps.google.com/mapfiles/marker_green.png',
            width: 15, height: 30,
            attachment: 'DataCoordinate',
            point: { x: new Date(2017, 2, 5), y: 800 },
            tooltip: 'All is fine again...',
        },
    ]);
});