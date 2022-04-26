c1.documentReady(function () {
    // create an unbound grid with 5 rows and 7 columns
    var theGrid = wijmo.Control.getControl('#theGrid');
    while (theGrid.columns.length < 7) {
        theGrid.columns.push(new wijmo.grid.Column());
    }
    while (theGrid.rows.length < 5) {
        theGrid.rows.push(new wijmo.grid.Row());
    }

    // configure the grid
    theGrid.mergeManager = new wijmo.grid.CustomMergeManager(theGrid);
    theGrid.formatItem.addHandler(centerCell);
    theGrid.rowHeaders.columns[0].width = 80;
    theGrid.rows.defaultSize = 40;
    theGrid.alternatingRowStep = 0;
    theGrid.isReadOnly = true;

    // populate the grid
    setData(theGrid.columnHeaders, 0, ',Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday'.split(','));
    setData(theGrid.cells, 0, '12:00,Walker,Morning Show,Morning Show,Sport,Weather,N/A,N/A'.split(','));
    setData(theGrid.cells, 1, '13:00,Today Show,Today Show,Sesame Street,Football,Market Watch,N/A,N/A'.split(','));
    setData(theGrid.cells, 2, '14:00,Today Show,Today Show,Kid Zone,Football,Soap Opera,N/A,N/A'.split(','));
    setData(theGrid.cells, 3, '15:00,News,News,News,News,News,N/A,N/A'.split(','));
    setData(theGrid.cells, 4, '16:00,News,News,News,News,News,N/A,N/A'.split(','));
});

function setData(p, r, cells) {
    if (p.cellType == wijmo.grid.CellType.Cell) {
        p.grid.rowHeaders.setCellData(r, 0, cells[0]);
    }
    for (var i = 1; i < cells.length; i++) {
        p.setCellData(r, i - 1, cells[i]);
    }
}

function centerCell(s, e) {
    if (e.cell.children.length == 0) {
        e.cell.innerHTML = '<div>' + e.cell.innerHTML + '</div>';
        wijmo.setCss(e.cell, {
            display: 'table',
            tableLayout: 'fixed'
        });
        wijmo.setCss(e.cell.children[0], {
            display: 'table-cell',
            textAlign: 'center',
            verticalAlign: 'middle'
        });
    }
}

// CustomMergeManager class (transpiled from TypeScript)
var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var wijmo;
(function (wijmo) {
    var grid;
    (function (grid) {
        'use strict';
        /**
         * Class that extends the standard MergeManager to support merged ranges that
         * span both rows and columns.
         *
         * This class uses the same content-based approach used by the built-in merge
         * manager, but it could use any other logic instead (for example, a fixed list
         * of pre-defined merged ranges).
         */
        var CustomMergeManager = (function (_super) {
            __extends(CustomMergeManager, _super);
            function CustomMergeManager() {
                _super.apply(this, arguments);
            }
            CustomMergeManager.prototype.getMergedRange = function (panel, r, c, clip) {
                if (clip === void 0) { clip = true; }
                // create basic cell range
                var rg = new grid.CellRange(r, c);
                // expand left/right
                for (var i = rg.col; i < panel.columns.length - 1; i++) {
                    if (panel.getCellData(rg.row, i, true) != panel.getCellData(rg.row, i + 1, true))
                        break;
                    rg.col2 = i + 1;
                }
                for (var i = rg.col; i > 0; i--) {
                    if (panel.getCellData(rg.row, i, true) != panel.getCellData(rg.row, i - 1, true))
                        break;
                    rg.col = i - 1;
                }
                // expand up/down
                for (var i = rg.row; i < panel.rows.length - 1; i++) {
                    if (panel.getCellData(i, rg.col, true) != panel.getCellData(i + 1, rg.col, true))
                        break;
                    rg.row2 = i + 1;
                }
                for (var i = rg.row; i > 0; i--) {
                    if (panel.getCellData(i, rg.col, true) != panel.getCellData(i - 1, rg.col, true))
                        break;
                    rg.row = i - 1;
                }
                // done
                return rg;
            };
            return CustomMergeManager;
        }(grid.MergeManager));
        grid.CustomMergeManager = CustomMergeManager;
    })(grid = wijmo.grid || (wijmo.grid = {}));
})(wijmo || (wijmo = {}));
//# sourceMappingURL=CustomMergeManager.js.map