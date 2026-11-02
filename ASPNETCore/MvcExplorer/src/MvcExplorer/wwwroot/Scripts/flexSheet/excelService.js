var ctx = {
    serverUrl: '',
    flexSheet: null,
    format: 'Xlsx'
};

c1.documentReady(function () {
    ctx.flexSheet = wijmo.Control.getControl('#excelServiceSheet');
});

function exportFormatChanged(arg) {
    ctx.format = arg;
    wijmo.Control.getControl("#exportFormat").header = "Export Format: <b>" + arg.toUpperCase() + "</b>";
}

function exportFlexSheet() {
    var exporter = new wijmo.grid.ExcelExporter(),
        control = ctx.flexSheet;
    exporter.requestExport(control, ctx.serverUrl + "api/export/excel", {
        fileName: document.getElementById("exportName").value,
        type: ctx.format
    });
}

function importFlexSheet() {
    var file = document.getElementById("fileinput").files[0],
        control = ctx.flexSheet,
        exporter;
    if (!file) {
        return;
    }
    exporter = new wijmo.grid.ExcelImporter();
    exporter.requestImport(control, ctx.serverUrl + "api/import/excel", file, false);
}