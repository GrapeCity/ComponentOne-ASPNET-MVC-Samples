onload = function () {
    var start = new Date();
    for (var i = 0; i < 12; i++) {
        customizeMonth(document.getElementById("month" + i), wijmo.DateTime.addMonths(start, -i));
    }

    // customize a month
    function customizeMonth(month, date) {
        // add a custom header element
        var fmt = wijmo.format('<div class="month-header">' +
            '<div class="month-title">{header}</div>' +
            '<div class="month-status">{uptime}% uptime</div>' +
            '</div>', {
                header: wijmo.Globalize.format(date, 'MMMM yyyy'),
                uptime: getUptime()
            });
        var newHeader = wijmo.createElement(fmt);
        var hdr = month.querySelector('.wj-calendar-header');
        hdr.parentElement.insertBefore(newHeader, hdr);

        // show only first letter of week day
        var cells = month.querySelectorAll('table tr.wj-header td');
        for (var i = 0; i < 7; i++) {
            cells[i].textContent = cells[i].textContent.substr(0, 1);
        }
    }

    function getUptime(date) {
        var tm = [100, 99.75, 99.998, 99.98, 99.996, 99.93];
        return tm[Math.floor(Math.random() * tm.length)];
    }
}

// format the calendar cells to show events
function formatDayCell(s, e) {

    // format cell content
    var html = wijmo.format('<div>{date}</div>', {
        date: e.data.getDate()
    });
    var event = getEvent(e.data);
    html += event
        ? '<img src="' + window.Resources.Calendar.Images[event.type] + '"/>'
        : '<img/>';
    e.item.innerHTML = html;

    // add tooltip to cell
    var tip = wijmo.format('<div class="event-tip event-type-{eventType}">' +
            '<div>{date:MMM d, yyyy}</div>' +
            '<div class="event">{eventMessage}</div>' +
        '</div>', {
            date: e.data,
            eventMessage: event ? event.msg : window.Resources.Calendar.NoEvent,
            eventType: event ? event.type.toLocaleLowerCase() : 'none'
        });
    if (!window.calendarTooltip) window.calendarTooltip = new wijmo.Tooltip();
    calendarTooltip.setTooltip(e.item, tip);
}

// generate some events between now and a year ago
function getEvents() {
    var arr = [],
        types = 'Maintenance,Incident,Notice,Outage'.split(','),
        messages = window.Resources.Calendar.Events.split(',');
    for (var i = 0; i < 100; i++) {
        var dt = wijmo.DateTime.addDays(new Date(), -Math.round(Math.random() * 365));
        arr.push({
            id: i,
            date: dt,
            type: types[Math.floor(Math.random() * types.length)],
            msg: messages[Math.floor(Math.random() * messages.length)]
        });
    }
    return arr;
}

function getEvent(date) {
    if (!window.calendarEvents) window.calendarEvents = getEvents();
    for (var i = 0; i < calendarEvents.length; i++) {
        if (wijmo.DateTime.sameDate(calendarEvents[i].date, date)) {
            return calendarEvents[i];
        }
    }
    return null;
}
