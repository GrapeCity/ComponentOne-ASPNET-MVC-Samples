c1.documentReady(function () {
    // get the GroupPanel extender created by the control extender builder directly.
    var dgpGrid = wijmo.Control.getControl('#dgpGrid');
    // get extender by id
    var sdGroupPanel = c1.getExtender(dgpGrid, 'dpGroupPanel');
    // get extender by type
    //var sdGroupPanel = c1.getExtenders(dgpGrid, c1.mvc.grid.grouppanel.GroupPanel)[0];

    sdGroupPanel.placeholder = 'This is the GroupPanel generated directly via control extender builder';

    // create the GroupPanel extender in the client.
    var csgpGrid = wijmo.Control.getControl('#csgpGrid');
    var csGroupPanel = new wijmo.grid.grouppanel.GroupPanel('#csGroupPanel', {
        placeholder: 'This is the GroupPanel created via Javascript codes',
        grid: csgpGrid
    });
});