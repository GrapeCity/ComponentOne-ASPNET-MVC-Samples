c1.documentReady(function () {
    var theTree = wijmo.Control.getControl('#theTree');
    theTree.displayMemberPath = "Header";
    theTree.childItemsPath = "Items";
    theTree.imageMemberPath = "Image";
});