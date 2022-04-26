// function used for displaying XYV data in tooltips
function tooltip(ht) {
    var date, content;

    if (!ht || !ht.item) {
        return '';
    }

    date = ht.item.X ? ht.item.X : null;

    if (wijmo.isDate(date)) {
        date = wijmo.Globalize.formatDate(date, 'MM/dd/yy');
    }

    content =
            '<b>' + ht.name + '</b><br/>' +
            'Date: ' + date + '<br/>' +
            'Y: ' + wijmo.Globalize.format(ht.y, 'n2');
    if (ht.item.Volume) {
        content +=
        '<br/>' +
        'Volume: ' + wijmo.Globalize.format(ht.item.Volume, 'n0');
    }

    return content;
}

// function used for displaying HLOCV data in tooltips
function financialTooltip(ht) {
    var date, content;

    if (!ht || !ht.item) {
        return '';
    }

    date = ht.item.X ? ht.item.X : null;

    if (wijmo.isDate(date)) {
        date = wijmo.Globalize.formatDate(date, 'MM/dd/yy');
    }

    content =
           '<b>' + ht.name + '</b><br/>' +
           'Date: ' + date + '<br/>' +
           'Open: ' + wijmo.Globalize.format(ht.item.Open, 'n2') + '<br/>' +
           'High: ' + wijmo.Globalize.format(ht.item.High, 'n2') + '<br/>' +
           'Low: ' + wijmo.Globalize.format(ht.item.Low, 'n2') + '<br/>' +
           'Close: ' + wijmo.Globalize.format(ht.item.Close, 'n2') + '<br/>' +
           'Volume: ' + wijmo.Globalize.format(ht.item.Volume, 'n0');

    return content;
}