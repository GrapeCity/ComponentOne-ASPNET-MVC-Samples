// The RegExp object which is used to tell a DateTime text.
var dateJsonRegx = /^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}(\.\d*)?(Z|[\+\-]\d{2}:\d{2}|)$/;
function reponseTextParsing(sender, args) {
    var dateText = args.value;
    // check whether it is a valid DateTime text.
    var matched = dateJsonRegx.exec(dateText);
    if (!matched) {
        return;
    }
    var timeZoneText = matched[2];
    var dateKind = getDateKind(timeZoneText);
    // only customize the parsing for the Date object in Utc or Local format.
    if (dateKind == c1.mvc.DateKind.Unspecified) {
        return;
    }
    if (typeof dateText === 'string' && matched) {
        var index = dateText.indexOf(timeZoneText);
        // remove the time zone text and create a Date object.
        var date = new Date(dateText.substr(0, index));
        // Don't forget to set the dateKind for the Date object parsed.
        // It could be used in OnClientRequestDataStringifying.
        date.dateKind = dateKind;
        args.result = date;
        args.cancel = true;
    }
}

function getDateKind(timeZoneText) {
    if (!timeZoneText) {
        return c1.mvc.DateKind.Unspecified;
    }

    if (timeZoneText.toLowerCase() === 'z') {
        return c1.mvc.DateKind.Utc;
    }

    return c1.mvc.DateKind.Local;
}

function requestDataStringifying(sender, args) {
    if (args.value instanceof Date || args.parent[args.key] instanceof Date) {
        var date = args.value instanceof Date ? args.value : args.parent[args.key];
        // only customize the serialization for the Date object in Utc format.
        if (!date.dateKind || date.dateKind == c1.mvc.DateKind.Unspecified) {
            return;
        }

        args.result = c1.mvc.Utils.formatNumber(date.getFullYear(), 4) + '-' +
                c1.mvc.Utils.formatNumber(date.getMonth() + 1, 2) + '-' +
                c1.mvc.Utils.formatNumber(date.getDate(), 2) + 'T' +
                c1.mvc.Utils.formatNumber(date.getHours(), 2) + ':' +
                c1.mvc.Utils.formatNumber(date.getMinutes(), 2) + ':' +
                c1.mvc.Utils.formatNumber(date.getSeconds(), 2) + '.' +
                c1.mvc.Utils.formatNumber(date.getMilliseconds(), 3)
                + (date.dateKind == c1.mvc.DateKind.Utc ? 'Z' : getLocalTimeZoneText());
        args.cancel = true;
    }
}

function getLocalTimeZoneText() {
    var date = new Date();
    var timeoffset = date.getTimezoneOffset();
    var result = '';
    if (timeoffset > 0) {
        result += '-';
    } else {
        result += '+';
        timeoffset *= -1;
    }
    var hour = Math.floor(timeoffset / 60);
    result += formatNumber(hour, 2);
    result += ":";
    result += formatNumber(timeoffset - hour * 60, 2);
    return result;
}

function formatNumber(n, k) {
    // Format integers to have at least k digits.
    var text = n.toString();
    while (text.length < k) {
        text = '0' + text;
    }
    return text;
}

function initHamburgerNav() {
    var hamburgerNavBtn = document.querySelector('.hamburger-nav-btn');
    var hamburgerNavEle = document.querySelector('.narrow-screen.site-nav');
    new c1.sample.MultiLevelMenu(hamburgerNavEle, hamburgerNavBtn);
}

c1.documentReady(function () {
    initHamburgerNav();
});