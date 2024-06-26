﻿// show the percentage complete for each task
function ganttItemFormatter(engine, hti, defaultFormatter) {

    // draw the item as usual
    defaultFormatter();

    // show percentage done
    var task = hti.item;
    if (wijmo.isNumber(task.Percent) && task.Percent > 0) {
        var pct = wijmo.clamp(task.Percent, 0, 100) / 100,
            rc = getTaskRect(hti.series.chart, task).inflate(-8, -8);
        engine.fill = pct == 1 ? 'green' : 'gold';//engine.stroke;
        engine.drawRect(rc.left, rc.top, rc.width * pct, rc.height);
    }
}

// show the task dependencies
function ganttChartRendered(s, e) {
    var chart = s,
        tasks = chart.collectionView.items;
    tasks.forEach(function (task) { // for each task
        var parents = getTaskParents(task, tasks); // get the parent tasks
        parents.forEach(function (parent) { // for each parent
            drawConnectingLine(e.engine, chart, task, parent); // draw connector
        });
    });
}

function drawConnectingLine(engine, chart, task, parent) {
    var rc1 = getTaskRect(chart, parent),   // parent rect
        rc2 = getTaskRect(chart, task),     // task rect
        x1 = rc1.left + rc1.width / 2,      // parent x center
        x2 = rc2.left,                      // task left
        y1 = rc1.bottom,                    // parent bottom
        y2 = rc2.top + rc2.height / 2;      // task y center

    // draw connecting line
    var xs = [x1, x1, x2],
        ys = [y1, y2, y2];
    engine.drawLines(xs, ys, 'connector', {
        stroke: 'black'
    });

    // draw arrow at the end
    var sz = 5;
    xs = [x2 - 2 * sz, x2, x2 - 2 * sz];
    ys = [y2 - sz, y2, y2 + sz];
    engine.drawPolygon(xs, ys, 'arrow', {
        fill: 'black'
    })
}

function getTaskRect(chart, task) {
    var x1 = chart.axisX.convert(task.Start),
        x2 = chart.axisX.convert(task.End),
        index = chart.collectionView.items.indexOf(task),
        y1 = chart.axisY.convert(index - .35),
        y2 = chart.axisY.convert(index + .35);
    return new wijmo.Rect(x1, y1, x2 - x1 + 1, y2 - y1 + 1);
}

function getTaskParents(task, tasks) {
    parents = [];
    if (task.Parent) {
        task.Parent.split(',').forEach(function (name) {
            for (var i = 0; i < tasks.length; i++) {
                if (tasks[i].Name == name) {
                    parents.push(tasks[i]);
                    break;
                }
            }
        });
    }
    return parents;
}