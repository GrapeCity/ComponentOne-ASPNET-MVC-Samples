c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl("#theGrid");
    var view = theGrid.collectionView;

    // change the grouping
    document.addEventListener('change', function (e) {

        // remove the old groups
        view.groupDescriptions.clear();

        // add the new groups
        var props = e.target.value.split(',');
        for (var i = 0; i < props.length; i++) {
            var prop = props[i];

            // group sales by value ranges
            var gd;
            if (prop == 'Sales') {
                gd = new wijmo.collections.PropertyGroupDescription(prop, function (item) {
                    if (item.Sales > 80000) return 'High';
                    if (item.Sales > 40000) return 'Medium';
                    return 'Low';
                });
            } else { // group others by value
                gd = new wijmo.collections.PropertyGroupDescription(prop);
            }
            view.groupDescriptions.push(gd);
        }
    });

    // dump grouped data to console (no grid)
    document.getElementById('btnDump').addEventListener('click', function () {
        if (!view.groups) {
            console.log('*** no groups');
        } else {
            console.log('*** ' + view.groups.length + ' groups:');
            for (var i = 0; i < view.groups.length; i++) {
                dumpGroup(view.groups[i], '');
            }
        }
    });

    function dumpGroup(group, level) {

        // show information for this group
        var propName = group.groupDescription['propertyName'];
        var groupName = group.name;
        var groupInfo = propName + ' > ' + groupName; // group summary
        groupInfo += ' (' + group.items.length + ' items)'; // item count
        groupInfo += ' total sales: ' + wijmo.Globalize.format(group.getAggregate('Sum', 'sales'), 'c2');
        console.log(level + groupInfo);

        // show subgroups
        if (group.groups) {
            for (var i = 0; i < group.groups.length; i++) {
                dumpGroup(group.groups[i], level + '  ');
            }
        }
    }
});