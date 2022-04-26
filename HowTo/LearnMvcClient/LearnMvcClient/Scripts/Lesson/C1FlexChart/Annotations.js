c1.documentReady(function () {
    var theChart = wijmo.Control.getControl('#theChart');

    // add annotation layer
    var annotations = new wijmo.chart.annotation.AnnotationLayer(theChart, [
      { // attach an Ellipse to data point 15
          type: 'Ellipse',
          content: 'Ellipse',
          tooltip: 'This is an <b>Ellipse</b><br/>annotation',
          attachment: 'DataIndex',
          seriesIndex: 0,
          pointIndex: 15,
          width: 100, height: 40,
          position: 'Top', // Center/Top/Bottom/Left/Right
          style: { fill: 'green', stroke: 'darkgreen', strokeWidth: 2, opacity: .25 }
      },
      { // show a trendline between Feb 10 and Mar 3
          type: 'Line',
          tooltip: 'This is a <b>Line</b><br/>annotation',
          position: 'Center',
          attachment: 'DataCoordinate',
          start: { x: new Date(2017, 1, 10), y: 810 },
          end: { x: new Date(2017, 2, 3), y: 840 },
          style: { stroke: 'darkgreen', strokeWidth: 4, opacity: .5 }
      }
    ]);
});