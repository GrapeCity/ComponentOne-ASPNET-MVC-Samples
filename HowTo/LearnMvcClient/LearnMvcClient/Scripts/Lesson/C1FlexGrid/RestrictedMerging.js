c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');

    // apply the custom merge manager
    theGrid.mergeManager = new wijmo.grid.RestrictedMergeManager(theGrid);
});

// RestrictedMergeManager class (transpiled from TypeScript)

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
        var RestrictedMergeManager = (function (_super) {
            __extends(RestrictedMergeManager, _super);
            function RestrictedMergeManager() {
                _super.apply(this, arguments);
            }
            // get a "restricted" merge range
            // this range will not span rows that contain multiple values in the
            // previous column.
            RestrictedMergeManager.prototype.getMergedRange = function (p, r, c, clip) {
                if (clip === void 0) { clip = true; }
                var rng = null;
                // start with single cell
                rng = new grid.CellRange(r, c);
                var pcol = c > 0 ? c - 1 : c;
                // get reference values to use for merging
                var val = p.getCellData(r, c, false);
                var pval = p.getCellData(r, pcol, false);
                // expand up
                while (rng.row > 0 &&
                    p.getCellData(rng.row - 1, c, false) == val &&
                    p.getCellData(rng.row - 1, pcol, false) == pval) {
                    rng.row--;
                }
                // expand down
                while (rng.row2 < p.rows.length - 1 &&
                    p.getCellData(rng.row2 + 1, c, false) == val &&
                    p.getCellData(rng.row2 + 1, pcol, false) == pval) {
                    rng.row2++;
                }
                // don't bother with single-cell ranges
                if (rng.isSingleCell) {
                    rng = null;
                }
                // done
                return rng;
            };
            return RestrictedMergeManager;
        }(grid.MergeManager));
        grid.RestrictedMergeManager = RestrictedMergeManager;
    })(grid = wijmo.grid || (wijmo.grid = {}));
})(wijmo || (wijmo = {}));
//# sourceMappingURL=RestrictedMergeManager.js.map