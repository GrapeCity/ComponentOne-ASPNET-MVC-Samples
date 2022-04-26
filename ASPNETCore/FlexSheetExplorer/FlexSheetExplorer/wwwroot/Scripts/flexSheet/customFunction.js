var ctx = {
    flexSheet: null
};

function unknownFunction(sender, e) {
    var result = '';
    if (e.params) {
        for (var i = 0; i < e.params.length; i++) {
            result += e.params[i];
        }
    }
    e.value = result;
};

c1.documentReady(function () {
    var flexSheet = ctx.flexSheet = wijmo.Control.getControl('#customFuncSheet');

    flexSheet.addFunction('customSumProduct', function (range1, range2) {
        var result = 0;

        if (range1.length > 0 && range1.length === range2.length && range1[0].length === range2[0].length) {
            for (var i = 0; i < range1.length; i++) {
                for (var j = 0; j < range1[0].length; j++) {
                    result += range1[i][j] * range2[i][j];
                }
            }
        }

        return result;
    }, 'Custom SumProduct Function', 2, 2);

    for (var ri = 0; ri < flexSheet.rows.length; ri++) {
        for (var ci = 0; ci < 3; ci++) {
            flexSheet.setCellData(ri, ci, ri + ci);
        }
    }

    flexSheet.setCellData(0, 3, '=customSumProduct(A1:A10, B1:B10)');
    flexSheet.setCellData(1, 3, '=customFunc(1, "B", 3)');

});