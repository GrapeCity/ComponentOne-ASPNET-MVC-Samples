c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');

    // create the group panel
    var theGroupPanel = new wijmo.grid.grouppanel.GroupPanel('#theGroupPanel', {
        placeholder: 'Drag columns here to create groups',
        grid: theGrid
    });
});