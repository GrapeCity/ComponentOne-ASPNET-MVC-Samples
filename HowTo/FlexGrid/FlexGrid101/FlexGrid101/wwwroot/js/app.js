var smFlexGrid = smMenu = gFlexGrid = null;

$(document).ready(function () {
    //Selection Modes Modules
    smFlexGrid = wijmo.Control.getControl("#smFlexGrid");
});

//Selection Modes Modules
function smMenu_SelectedIndexChanged(sender) {
    if (sender.selectedValue != null && smFlexGrid != null) {
        smFlexGrid.selectionMode = sender.selectedValue;
    }
}

//Group By Modules
function gMenu_SelectedIndexChanged(sender) {
    var grid = wijmo.Control.getControl("#gFlexGrid");
    if (sender.selectedValue && grid) {
        var name = sender.selectedValue;
        var groupDescriptions = grid.collectionView.groupDescriptions;
        grid.beginUpdate();
        groupDescriptions.clear();

        if (name.indexOf("Country") > -1) {
            groupDescriptions.push(new wijmo.collections.PropertyGroupDescription("Country"));
        }

        if (name.indexOf("Product") > -1) {
            groupDescriptions.push(new wijmo.collections.PropertyGroupDescription("Product"));
        }

        if (name.indexOf("Color") > -1) {
            groupDescriptions.push(new wijmo.collections.PropertyGroupDescription("Color"));
        }

        if (name.indexOf("Start") > -1) {
            groupDescriptions.push(new wijmo.collections.PropertyGroupDescription("Start", function (item, prop) {
                var value = item[prop];
                return value.getFullYear() + "/" + value.getMonth();
            }));
        }

        if (name.indexOf("Amount") > -1) {
            groupDescriptions.push(new wijmo.collections.PropertyGroupDescription("Amount", function (item, prop) {
                var value = item[prop];
                if (value <= 500) {
                    return "<500";
                }

                if (value > 500 && value <= 1000) {
                    return "500 to 1000";
                }

                if (value > 1000 && value <= 5000) {
                    return "1000 to 5000";
                }

                return "More than 5000";
            }));
        }
        grid.endUpdate();
    }
}

//Conditional styling Modules
function csFlexGrid_ItemFormatter(panel, r, c, cell) {
    if (wijmo.grid.CellType.Cell == panel.cellType && panel.columns[c].binding == '_Color') {
        var cellData = panel.getCellData(r, c);
        cell.style.color = cellData < 0 ? 'red' : cellData < 500 ? 'black' : 'green';
    }
    if (wijmo.grid.CellType.Cell == panel.cellType && panel.columns[c].binding == 'Amount') {
        var cellData = panel.getCellData(r, c);
        cell.style.color = cellData < 0 ? 'red' : cellData < 500 ? 'black' : 'green';
    }
    if (wijmo.grid.CellType.Cell == panel.cellType && panel.columns[c].binding == 'Discount') {
        var cellData = panel.getCellData(r, c);
        cell.style.color = cellData < .1 ? 'red' : cellData < .2 ? 'black' : 'green';
    }
}
