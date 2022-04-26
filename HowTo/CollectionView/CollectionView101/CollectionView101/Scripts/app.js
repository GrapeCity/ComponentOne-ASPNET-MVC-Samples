function getNames() {
    return ['CustomerID', 'CompanyName', 'ContactName', 'City', 'Country', 'Phone'];
};

function getGroupNames() {
    return ['ProductID', 'ProductName', 'SupplierID', 'CategoryID', 'QuantityPerUnit', 'UnitPrice', 'UnitsInStock', 'UnitsOnOrder', 'ReorderLevel'];
};

$(document).ready(function () {
    //Disable Server Reading
    fGDisableServerView = wijmo.Control.getControl('#fGDisableServerView');
    
    //Current Record Management
    crmGrid = wijmo.Control.getControl('#crmGrid');
    cvCRM = crmGrid.itemsSource; //new wijmo.collections.CollectionView(getData(10)),
    // Add the processes for buttons' click
    // move the current to the next one
    document.getElementById('btnCRMMoveNext').addEventListener('click', function () {
        cvCRM.moveCurrentToNext();
    });

    // move the current to the preivous one
    document.getElementById('btnCRMMovePre').addEventListener('click', function () {
        cvCRM.moveCurrentToPrevious();
    });

    // when the current item is the 4th one, forbid changing current.
    document.getElementById('btnCRMStop4').addEventListener('click', function () {
        cvCRM.currentChanging.addHandler(stopCurrentIn4th);
    });

    // restore to be able to change current.
    document.getElementById('btnCRMReset').addEventListener('click', function () {
        cvCRM.currentChanging.removeHandler(stopCurrentIn4th);
    });

    // define the funciton to forbid the current moving.
    function stopCurrentIn4th(sender, e) {
        // when the current is the 4rd item, stop moving.
        if (sender.currentPosition === 3) {
            e.cancel = true;
        }
    };

    //Sorting
    sortingGrid = wijmo.Control.getControl('#sortingGrid');
    cvSorting = sortingGrid.itemsSource;
    sortingFieldNameList = document.getElementById('sortingFieldNameList');
    sortingOrderList = document.getElementById('sortingOrderList');
    //sortingNames = getNames();
    // initialize the list items for field names and orders.
    sortingFieldNameList.innerHTML += '<option value="" selected="selected">Please choose the field you want to sort by...</option>';
    for (var i = 0; i < sortingNames.length; i++) {
        sortingFieldNameList.innerHTML += '<option value="' + sortingNames[i] + '">' + sortingNames[i] + '</option>';
    }

    // track the list change in order to udpate the sortDescriptions property.
    sortingFieldNameList.addEventListener('change', sortGrid);
    sortingOrderList.addEventListener('change', sortGrid);

    //Filtering
    // create collectionview, grid, filter with timeout, textbox for inputting filter.
    filteringGrid = wijmo.Control.getControl('#filteringGrid');
    cvFiltering = filteringGrid.collectionView;//filteringGrid.itemsSource;
    filteringInput = document.getElementById('filteringInput');
    // apply filter when input
    filteringInput.addEventListener('input', filterGrid);

    //Grouping
    // create collectionview, grid, the select element and the names list.
    groupingGrid = wijmo.Control.getControl('#groupingGrid');
    cvGrouping = groupingGrid.itemsSource;
    groupingFieldNameList = document.getElementById('groupingFieldNameList');    
    groupingFieldNameList.addEventListener('change', groupGrid);
    // initialize the list and listen to the list's change.
    groupingFieldNameList.innerHTML += '<option value="" selected="selected">Please choose the field you want to group by...</option>';
    for (var i = 0; i < groupingNames.length; i++) {
        groupingFieldNameList.innerHTML += '<option value="' + groupingNames[i] + '">' + groupingNames[i] + '</option>';
    }

    //Tracking changes
    tcMainGrid = wijmo.Control.getControl('#tcMainGrid');// the flexGrid to edit the data
    tcEditedGrid = wijmo.Control.getControl('#tcEditedGrid'); // the flexGrid to record the edited items
    tcAddedGrid = wijmo.Control.getControl('#tcAddedGrid'); // the flexGrid to record the added items
    tcRemovedGrid = wijmo.Control.getControl('#tcRemovedGrid'); // the flexGrid to record the removed items
    cvTrackingChanges = tcMainGrid.itemsSource;

    tcEditedGrid.itemsSource = cvTrackingChanges.itemsEdited;
    tcAddedGrid.itemsSource = cvTrackingChanges.itemsAdded;
    tcRemovedGrid.itemsSource = cvTrackingChanges.itemsRemoved;

    // track changes of the collectionview
    cvTrackingChanges.trackChanges = true;
});

//Disable Server Read
var fGDisableServerView = null;

//Current Record Management 
// create collectionview, grid
var crmGrid = null
    , cvCRM = null;

//Batch Edit
function batchUpdate() {
var batchEditGrid = wijmo.Control.getControl('#fGBECView'),
    cv = batchEditGrid.collectionView;
cv.commit();
};


//Sorting
// create collectionview, grid, the jQuery elements, the field name list.
var cvSorting = null,
    sortingGrid =null,
    sortingFieldNameList = null,
    sortingOrderList = null,
    sortingNames = getNames();

function sortGrid() {
    var fieldName = sortingFieldNameList.value,
        ascending = sortingOrderList.value,
        sd, sdNew;

    if (!fieldName) {
        return;
    }

    ascending = ascending === 'true';
    sd = cvSorting.sortDescriptions;
    sdNew = new wijmo.collections.SortDescription(fieldName, ascending);

    // remove any old sort descriptors and add the new one
    sd.splice(0, sd.length, sdNew);
};

//Filtering
// create collectionview, grid, filter with timeout, textbox for inputting filter.
var cvFiltering = null,
    filteringGrid = null,
    toFilter,
    filteringInput = null;

// define the filter function for the collection view.
function filterFunction(item) {
    var filter = filteringInput.value.toLowerCase();
    if (!filter) {
        return true;
    }

    return item.Country.toLowerCase().indexOf(filter) > -1;
};

// apply filter (applied on a 500 ms timeOut)
function filterGrid() {
    if (toFilter) {
        clearTimeout(toFilter);
    }

    toFilter = setTimeout(function () {
        toFilter = null;
        if (cvFiltering.filter === filterFunction) {
            cvFiltering.refresh();
        }
        else {
            cvFiltering.filter = filterFunction;
        }
    }, 500);
};

//Grouping
// create collectionview, grid, the select element and the names list.
var cvGrouping = null,
    groupingGrid = null,
    groupingFieldNameList = null,
    groupingNames = getGroupNames();

// update the group settings.
function groupGrid() {
    var gd,
        fieldName = groupingFieldNameList.value;

    gd = cvGrouping.groupDescriptions;

    if (!fieldName) {
        // clear all the group settings.
        gd.splice(0, gd.length);
        return;
    }

    if (findGroup(fieldName) >= 0) {
        return;
    }

    if (fieldName === 'UnitPrice') {
        // when grouping by amount, use ranges instead of specific values
        gd.push(new wijmo.collections.PropertyGroupDescription(fieldName, function (item, propName) {
            var value = item[propName]; // UnitPrice
            if (value > 100) return 'Large Amounts';
            if (value > 50) return 'Medium Amounts';
            if (value > 0) return 'Small Amounts';
            return 'Negative Amounts';
        }));
    }
    else {
        // group by specific property values
        gd.push(new wijmo.collections.PropertyGroupDescription(fieldName));
    }
};

// check whether the group with the specified property name already exists.
function findGroup(propName) {
    var gd = cvGrouping.groupDescriptions;
    for (var i = 0; i < gd.length; i++) {
        if (gd[i].propertyName === propName) {
            return i;
        }
    }
    return -1;
};

//Tracking changes
var tcMainGrid = null,
    tcEditedGrid = null,
    tcAddedGrid = null,
    tcRemovedGrid = null,
    cvTrackingChanges = null;

