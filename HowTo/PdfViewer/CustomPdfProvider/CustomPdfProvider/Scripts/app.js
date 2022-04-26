function loadPdf() {
    var pdfViewer = wijmo.Control.getControl('#PdfViewer');
    if (pdfViewer) {
        pdfViewer.deferUpdate(function () {
            pdfViewer.filePath = pdfFilesCombo.value;
        });
    }
}

function initHamburgerNav() {
    var hamburgerNavBtn = document.querySelector('.hamburger-nav-btn');
    var hamburgerNavEle = document.querySelector('.narrow-screen.site-nav');
    new c1.sample.MultiLevelMenu(hamburgerNavEle, hamburgerNavBtn);
}

var pdfFilesCombo;
c1.documentReady(function () {
    pdfFilesCombo = document.querySelector('#pdfFiles');

    pdfFilesCombo.onchange = function () {
        loadPdf();
    }

    initHamburgerNav();
});