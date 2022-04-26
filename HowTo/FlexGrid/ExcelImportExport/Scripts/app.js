//Controls Declaration
var gFlexGrid = null,
    IncludeHeadersExport = null,
    IncludeHeadersImport = null;

//Controls Initialization
function InitialControls() {
    gFlexGrid = wijmo.Control.getControl("#gFlexGrid");
    IncludeHeadersImport = document.getElementById('IncludeHeadersImport');
    IncludeHeadersExport = document.getElementById('IncludeHeadersExport');
}

// export
function exportExcel() {
    if (gFlexGrid) {
        wijmo.grid.xlsx.FlexGridXlsxConverter.save(gFlexGrid, { includeColumnHeaders: IncludeHeadersExport.checked }, 'FlexGrid.xlsx');
    }
};

// import
function importExcel() {
    if (gFlexGrid) {
        if ($('#importFile')[0].files[0]) {
            wijmo.grid.xlsx.FlexGridXlsxConverter.load(gFlexGrid, $('#importFile')[0].files[0], { includeColumnHeaders: IncludeHeadersImport.checked });
        }
        else {
            alert('Select an Excel file to Import.');
        }
    }
};
