var anChart = null
    , checkboxIsEditable = null
    , axisScrollBar = null
    , al = null;

function initialControls() {
    createEditableAnnotations(wijmo);
    anChart = wijmo.Control.getControl("#anChart");
    checkboxIsEditable = document.getElementById('checkboxIsEditable');        
    setAxisScrollbars();
    setEditableAnnotationLayer();
};

function setEditableAnnotationLayer() {
    anChart.rendered.addHandler(function () {
        if (!al) {
            al = new wijmo.chart.annotation.EditableAnnotationLayer(anChart);
            al.isEditable = checkboxIsEditable.checked == null ? false : checkboxIsEditable.checked;//$scope.isEditable;
            //add customize button
            var triangle, triangleBtn = new wijmo.chart.annotation.Button(function (engine) {
                var triangleIcon = new wijmo.chart.annotation.Polygon({
                    tooltip: 'Custom Button - Triangle </br>Select to add Triangle Annotation.',
                    points: [{
                        x: 10, y: 5
                    }, {
                        x: 5, y: 15
                    }, {
                        x: 15, y: 15
                    }],
                    style: {
                        fill: 'yellow'
                    }
                });
                triangleIcon.render(engine);
                return triangleIcon._element;
            }, function (point, isDataCoordinate) {
                var x = isDataCoordinate ? point.dx : point.x,
                    y = isDataCoordinate ? point.dy : point.y;
                triangle = new wijmo.chart.annotation.Polygon({
                    attachment: isDataCoordinate ? wijmo.chart.annotation.AnnotationAttachment.DataCoordinate : wijmo.chart.annotation.AnnotationAttachment.Absolute,
                    tooltip: 'Customize Annotation - Triangle',
                    points: [{
                        x: x, y: y
                    }, {
                        x: x, y: y
                    }, {
                        x: x, y: y
                    }],
                    style: {
                        fill: 'yellow'
                    }
                });
                al.items.push(triangle);
            }, function (originPoint, currentPoint, isDataCoordinate) {
                var ox = isDataCoordinate ? originPoint.dx : originPoint.x,
                    oy = isDataCoordinate ? originPoint.dy : originPoint.y,
                    cx = isDataCoordinate ? currentPoint.dx : currentPoint.x,
                    cy = isDataCoordinate ? currentPoint.dy : currentPoint.y,
                    offsetX = Math.abs(cx - ox),
                    offsetY = cy - oy,
                    isXDate = wijmo.isDate(ox),
                    isYDate = wijmo.isDate(oy),
                    oaDate = wijmo.chart.FlexChart._fromOADate;

                triangle.points.clear();
                triangle.points.push({ x: ox, y: isYDate ? oaDate(oy - ofssetY) : oy - offsetY });
                triangle.points.push({ x: isXDate ? oaDate(ox - offsetX) : ox - offsetX, y: isYDate ? oaDate(oy - 0 + ofssetY) : oy + offsetY });
                triangle.points.push({ x: isXDate ? oaDate(ox - 0 + offsetX) : ox + offsetX, y: isYDate ? oaDate(oy - 0 + ofssetY) : oy + offsetY });
            });
            al.buttons.push(triangleBtn);

            //add pre-defined annotations.
            var watermarker = new wijmo.chart.annotation.Text({
                position: wijmo.chart.annotation.AnnotationPosition.Left | wijmo.chart.annotation.AnnotationPosition.Top,
                attachment: wijmo.chart.annotation.AnnotationAttachment.Relative,
                text: 'watermarker',
                tooltip: 'Text Watermarker',
                point: { x: 1, y: 1 },
                style: { 'fill': '#cccccc', 'font-size': '30px', opacity: 0.2 }
            });
            al.items.push(watermarker);

            var imgSrc = window.location.pathname + '/Content/c1logo_watermark.png';
            var imgmarker = new wijmo.chart.annotation.Image({
                position: wijmo.chart.annotation.AnnotationPosition.Right | wijmo.chart.annotation.AnnotationPosition.Top,
                attachment: wijmo.chart.annotation.AnnotationAttachment.Relative,
                tooltip: 'Image Watermarker',
                point: { x: 0, y: 1 },
                width: 75,
                height: 75,
                href: imgSrc.replace('//','/'),
                style: { opacity: 1 }

            });
            al.items.push(imgmarker);

            var centerHLine = new wijmo.chart.annotation.Line({
                attachment: wijmo.chart.annotation.AnnotationAttachment.DataCoordinate,
                position: wijmo.chart.annotation.AnnotationPosition.Center,
                content: 'Vertical Center Line',
                start: { x: 50, y: 0 },
                end: { x: 50, y: 1000 },
                style: {
                    'stroke-width': '2px',
                    stroke: 'green'
                }
            });
            al.items.push(centerHLine);

            var centerVLine = new wijmo.chart.annotation.Line({
                attachment: wijmo.chart.annotation.AnnotationAttachment.Relative,
                position: wijmo.chart.annotation.AnnotationPosition.Center,
                content: 'Horizontal Center Line',
                start: { x: 0, y: 0.5 },
                end: { x: 1, y: 0.5 },
                style: {
                    'stroke-width': '2px',
                    stroke: 'red'
                }
            });
            al.items.push(centerVLine);

            var topRange = new wijmo.chart.annotation.Polygon({
                attachment: wijmo.chart.annotation.AnnotationAttachment.Relative,
                points: [{ x: 0, y: 0 }, { x: 0, y: 0.2 }, { x: 1, y: 0.2 }, { x: 1, y: 0 }],
                content: 'Top Range',
                style: { fill: '#FEF0DB', opacity: 0.5, stroke: '#FEF0DB' }
            });
            al.items.push(topRange);

            //flag
            var pointIndex = 10;
            ['black', 'blue', 'green', 'red', 'yellow'].forEach(function (v) {
                var flagSrc = window.location.pathname + '/Content/flag-' + v + '-icon.png';
                var flag = new wijmo.chart.annotation.Image({
                    attachment: wijmo.chart.annotation.AnnotationAttachment.DataIndex,
                    seriesIndex: 0,
                    pointIndex: pointIndex,
                    width: 24,
                    height: 24,
                    href: flagSrc.replace('//', '/'),

                });
                al.items.push(flag);
                pointIndex += 10;
            });

            var rect = new wijmo.chart.annotation.Rectangle({
                attachment: wijmo.chart.annotation.AnnotationAttachment.DataCoordinate,
                point: { x: 10, y: 650 },
                width: 120,
                height: 150,
                style: {
                    'stroke-dasharray': '4 4',
                    'stroke': 'red',
                    'fill-opacity':'0'
                }
            });
            al.items.push(rect);

            var circle = new wijmo.chart.annotation.Circle({
                seriesIndex: 0,
                attachment: wijmo.chart.annotation.AnnotationAttachment.DataCoordinate,
                radius: 80,
                point: { x: 70, y: 350 },
                style: {
                    stroke: 'blue',
                    fill: 'blue',
                    opacity: 0.3
                }
            });
            al.items.push(circle);
        }
    });
};

function setAxisScrollbars() {
    if (!anChart) {
        return;
    }
    // create Scrollbar
    if (!axisScrollBar) {
        axisScrollBar = new wijmo.chart.interaction.AxisScrollbar(anChart.axes[0]);

        window.setTimeout(function () {
            axisScrollBar.maxPos = 0.5;
        }, 20);
    }    
};

function checkboxIsEditable_OnChanged() {
    if (checkboxIsEditable != null && checkboxIsEditable.checked != null) {
        if (al == null || checkboxIsEditable == null || checkboxIsEditable.checked == null) {
            return;
        }
        al.isEditable = checkboxIsEditable.checked;
        setEditableAnnotationLayer();
    }
};

