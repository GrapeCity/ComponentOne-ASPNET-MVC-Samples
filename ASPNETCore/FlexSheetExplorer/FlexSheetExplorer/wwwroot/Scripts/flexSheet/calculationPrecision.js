c1.documentReady(function () {
    var flexSheet = wijmo.Control.getControl('#formulaSheet');
    generateFormulasSheet(flexSheet);
});
function SetCellContent(sender, args) {
    var flexSheet = wijmo.Control.getControl('#formulaSheet');
    let selection = args.range;
    if (selection.isValid) {
        let cellContent = document.querySelector('#cellContent');
        cellContent.textContent = flexSheet.getCellData(selection.row, selection.col, true);
    }
};

function setCalculationPrecision() {
    var precision = parseInt(document.getElementById("precisionVal").value);
    if (isNaN(precision)) {
        alert("Type a number for Calculation Precision");
        return false;
    }
    $.ajax({
        //async: false,
        method: 'POST',
        url: '/FlexSheet/SetCalculationPrecision',
        data: JSON.stringify(precision),
        dataType: 'html',
        contentType: 'application/json; ',
        success: function (result) {
            $('#formulaSheet').parent().html(jQuery(result).find('#formulaSheet').parent().html());
            var flexSheet = wijmo.Control.getControl('#formulaSheet');
            generateFormulasSheet(flexSheet);
        },
        error: function (xhr) {
            alert(xhr.responseText);
        }
    });
};

function generateFormulasSheet(flexSheet) {
    flexSheet.setCellData(0, 1, "1. Play around with calculationPrecision and numbers, compare them to see how results change");
    flexSheet.setCellData(1, 1, "First number");
    flexSheet.setCellData(2, 1, "=6*0.23");
    flexSheet.setCellData(1, 2, "Second Number");
    flexSheet.setCellData(2, 2, "1.38");
    flexSheet.setCellData(2, 3, "Result:");
    flexSheet.setCellData(2, 4, "=(B3<=C3)");

    flexSheet.applyCellsStyle({
        fontWeight: 'bold'
    }, [new wijmo.grid.CellRange(0, 1, 0, 1)]);

    flexSheet.applyCellsStyle({
        textAlign: 'right'
    }, [new wijmo.grid.CellRange(2, 1, 2, 1), new wijmo.grid.CellRange(2, 3, 2, 3)]);

    flexSheet.mergeRange(new wijmo.grid.CellRange(0, 1, 0, 7));
    flexSheet.mergeRange(new wijmo.grid.CellRange(1, 2, 1, 3));
}


