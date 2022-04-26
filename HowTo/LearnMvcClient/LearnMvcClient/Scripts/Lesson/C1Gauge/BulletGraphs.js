c1.documentReady(function () {
    // dashboard dada
    var dashboard = [
      { heading: 'Revenue', sub: 'US$, thousands', max: 400, actual: 345, target: 340, bad: 210, good: 300 },
      { heading: 'Profit', sub: '%', max: 50, actual: 37, target: 32, bad: 20, good: 30 },
      { heading: 'Order Size', sub: 'US$, average', max: 600, actual: 320, target: 520, bad: 400, good: 500 },
      { heading: 'New Customers', sub: 'count', max: 1500, actual: 1100, target: 1000, bad: 600, good: 900 },
      { heading: 'Satisfaction', sub: '0 to 5', max: 5, actual: 4, target: 4.5, bad: 2.5, good: 4.5 },
    ];

    // create the dashboard
    var table = document.createElement('table');
    document.getElementById('dashboard').appendChild(table);
    dashboard.forEach(function (item) {
        var tr = document.createElement('tr');
        table.appendChild(tr);

        // heading
        var td = document.createElement('td');
        tr.appendChild(td);
        var fmt = '<div class="heading">{heading}</div><div class="sub">{sub}</div>';
        td.innerHTML = wijmo.format(fmt, item);

        // warning
        td = document.createElement('td');
        tr.appendChild(td);
        if (item.actual < item.target) {
            td.innerHTML = '<span class="glyphicon glyphicon-thumbs-down warning"></span>';
        }

        // bullet graph
        td = document.createElement('td');
        tr.appendChild(td);
        var bg = new wijmo.gauge.BulletGraph(document.createElement('div'), {
            value: item.actual,
            target: item.target,
            max: item.max,
            bad: item.bad,
            good: item.good
        });
        td.appendChild(bg.hostElement);

        // max value
        td = document.createElement('td');
        tr.appendChild(td);
        td.innerHTML = wijmo.format('<div class="max">{max:n0}</span>', item);
    });
});