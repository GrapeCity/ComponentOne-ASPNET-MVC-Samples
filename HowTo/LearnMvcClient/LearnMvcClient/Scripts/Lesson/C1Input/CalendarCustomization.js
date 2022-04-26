c1.documentReady(function () {
    var theCalendar = wijmo.Control.getControl('#theCalendar');
    theCalendar.valueChanged.addHandler(function (s, e) {
        showCurrentDate();
    });
    theCalendar.formatItem.addHandler(function (s, e) { // apply styles to weekends and holidays
        var weekday = e.data.getDay(),
            holiday = getHoliday(e.data);
        wijmo.toggleClass(e.item, 'date-weekend', weekday == 0 || weekday == 6);
        wijmo.toggleClass(e.item, 'date-holiday', holiday != null);
        e.item.title = holiday;
    });

    // show the currently selected date
    function showCurrentDate() {
        var el = document.getElementById('theCalendarOutput');
        el.textContent = wijmo.Globalize.format(theCalendar.value, 'D');
    }
    showCurrentDate();

    // gets the holiday for a given date
    var holidays = {
        '1/1': 'New Year\'s Day',
        '6/14': 'Flag Day',
        '7/4': 'Independence Day',
        '11/11': 'Veteran\'s Day',
        '12/25': 'Christmas Day',
        '1/3/1': 'Martin Luther King\'s Birthday', // third Monday in January
        '2/3/1': 'Washington\'s Birthday', // third Monday in February
        '5/3/6': 'Armed Forces Day', // third Saturday in May
        '9/1/1': 'Labor Day', // first Monday in September
        '10/2/1': 'Columbus Day', // second Monday in October
        '11/4/4': 'Thanksgiving Day', // fourth Thursday in November
    };
    function getHoliday(date) {
        var day = date.getDate(),
            month = date.getMonth() + 1,
            holiday = holidays[month + '/' + day];
        if (!holiday) {
            var weekDay = date.getDay(),
                weekNum = Math.floor((day - 1) / 7) + 1;
            holiday = holidays[month + '/' + weekNum + '/' + weekDay];
        }
        return holiday;
    }
});