function refreshData(start, end, action, callback, data) {
    if (!data) {
        data = {};
    }

    if (start != null) {
        var startDate = new Date(start);
        data.startDate = (startDate.getMonth() + 1) + '/' + startDate.getDate() + '/' + startDate.getFullYear();
        data.start = start;
    }

    if (end != null) {
        var endDate = new Date(end);
        data.endDate = (endDate.getMonth() + 1) + '/' + endDate.getDate() + '/' + endDate.getFullYear();
        data.end = end;
    }

    ajaxRequest(action, data, callback);
}

function ajaxRequest(url, data, success, dataType, type) {
    c1.mvc.Utils.ajax({
        url: url,
        success: success,
        data: data,
        dataType: dataType || 'json'
    });
}