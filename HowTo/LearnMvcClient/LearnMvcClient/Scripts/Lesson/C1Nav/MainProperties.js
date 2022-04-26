c1.documentReady(function () {
    var theTree = wijmo.Control.getControl('#theTree');
    theTree.itemsSource = getData();
    theTree.selectedItem = theTree.itemsSource[0];

    // create checkboxes for the main properties
    var props = 'allowDragging,autoCollapse,expandOnClick,isAnimated,isReadOnly,showCheckboxes'.split(','),
          host = document.getElementById('properties'),
        tpl = '<label><input id="{prop}" type="checkbox"> {prop}</label>';
    props.forEach(function (prop) {
        var el = wijmo.createElement(tpl.replace(/\{prop\}/g, prop), host);
        el.querySelector('input').checked = theTree[prop];
    });
    host.addEventListener('click', function (e) {
        if (e.target instanceof HTMLInputElement) {
            theTree[e.target.id] = e.target.checked;
        }
    })

    // handle buttons
    document.getElementById('btnCollapse').addEventListener('click', function () {
        theTree.collapseToLevel(0);
    });
    document.getElementById('btnExpand').addEventListener('click', function () {
        theTree.collapseToLevel(100000);
    });

    // create some data
    function getData() {
        return [
            {
                header: 'Parent 1', items: [
                  { header: 'Child 1.1' },
          { header: 'Child 1.2' },
          { header: 'Child 1.3' }]
            },
      {
          header: 'Parent 2', items: [
          { header: 'Child 2.1' },
          { header: 'Child 2.2' }]
      },
      {
          header: 'Parent 3', items: [
          { header: 'Child 3.1' }]
      }
        ];
    }
});