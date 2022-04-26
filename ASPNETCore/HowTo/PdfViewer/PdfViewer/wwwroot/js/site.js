var lastTempPdf;
function uploadPdf(fileInput, host) {
    var files = fileInput.files;
    if (!files || !files.length) return;

    var file = files[0];
    if (!file) return;

    if (file.name.slice(-4).toLowerCase() !== '.pdf') {
        showMessageLabel();
        return;
    } else {
        hideMessageLabel();
    }

    var data = new FormData();
    data.append("file", file);

    var pdfUrl = "UploadFiles/" + file.name;;
    var url = host + "api/storage/" + pdfUrl;

    if (url === lastTempPdf) return;

    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        contentType: false,
        processData: false,
        success: function (data, success, obj) {
            deleteTempPdf(true);
            lastTempPdf = url;
            loadPdf(pdfUrl);
        }
    });
}

function deleteTempPdf(async) {
    if (!lastTempPdf) {
        return;
    }

    $.ajax({
        url: lastTempPdf,
        type: 'DELETE',
        async: async
    });

    lastTempPdf = null;
}

function loadPdf(filePath) {
    var pdfViewer = wijmo.Control.getControl('#pdfViewer');
    pdfViewer.filePath = filePath;
    pdfViewer.isDisabled = false;
}

function showMessageLabel() {
    wijmo.removeClass(document.getElementById('message'), 'hidden');
}

function hideMessageLabel() {
    wijmo.addClass(document.getElementById('message'), 'hidden');
}

window.addEventListener('unload', function () { deleteTempPdf(false) });