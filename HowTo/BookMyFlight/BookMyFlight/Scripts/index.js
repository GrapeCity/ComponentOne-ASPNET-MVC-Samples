var LBRecentSearches = TripType = TripTypeSingle = TripTypeRound = ACFromDest = ACToDest = IDDepartDate = IDReturnDate = INAdult = INChild = INInfant = CmbClass = DivFlightDir = ImgFlightDir = DivRetDateMsg = null;
var ClientIP = null;

$(document).ready(function () {
    LBRecentSearches = wijmo.Control.getControl("#LBRecentSearches");
    ImgFlightDir = document.getElementById('ImgFlightDir');
    TripType = 'ROUND';
    DivRetDateMsg = document.getElementById('DivRetDateMsg');
    DivFlightDir = document.getElementById('FlightDir');
    TripTypeSingle = document.getElementById('TripTypeSingle');
    TripTypeRound = document.getElementById('TripTypeRound');
    ACFromDest = wijmo.Control.getControl("#ACFromDest");
    ACToDest = wijmo.Control.getControl("#ACToDest");
    IDDepartDate = wijmo.Control.getControl("#IDDepartDate");
    IDReturnDate = wijmo.Control.getControl("#IDReturnDate");
    INAdult = wijmo.Control.getControl("#INAdult");
    INChild = wijmo.Control.getControl("#INChild");
    INInfant = wijmo.Control.getControl("#INInfant");
    CmbClass = wijmo.Control.getControl("#CmbClass");

    //function to handle change event of TripType Radio Buttons
    $('#TripTypeSingle,#TripTypeRound').change(function () {
        if (TripTypeSingle.checked) {
            $('#DivReturnDate').hide();
            $('#DivRetDateMsg').show();
            document.getElementById("ImgFlightDir").src = HostPath + "images/SINGLE.PNG";
            TripType = 'SINGLE';
        }
        else {
            $('#DivReturnDate').show();
            $('#DivRetDateMsg').hide();
            document.getElementById("ImgFlightDir").src = HostPath + "images/ROUND.PNG";
            TripType = 'ROUND';
        }
    });

});

//selectedIndexChanged event handler
function ACFromDest_SelectedIndexChanged(sender) {
    if (ACFromDest.selectedIndex >= 0 && ACFromDest.selectedValue != null && ACToDest.selectedIndex >= 0 && ACToDest.selectedValue != null && ACFromDest.selectedValue == ACToDest.selectedValue) {
        $('#LblMsg').attr('style', 'color:red;font-weight:bold;');
        $('#LblMsg').html('From and Destination place can\'t be same.');
        ACFromDest.selectedIndex = -1;
    }
}


//selectedIndexChanged event handler
function ACToDest_SelectedIndexChanged(sender) {
    if (ACFromDest.selectedIndex >= 0 && ACFromDest.selectedValue != null && ACToDest.selectedIndex >= 0 && ACToDest.selectedValue != null && ACFromDest.selectedValue == ACToDest.selectedValue) {
        $('#LblMsg').attr('style', 'color:red;font-weight:bold;');
        $('#LblMsg').html('From and Destination place can\'t be same.');
        ACToDest.selectedIndex = -1;
    }
}

//function to handle valuechanged event of C1Inputdate for Departure Date
function IDDepartDate_valueChanged(sender) {
    var DepartDate = IDDepartDate.value;
    IDReturnDate.min = DepartDate;
    if (IDReturnDate.value < DepartDate)
        IDReturnDate.value = DepartDate;
}

//function to handle valuechanged event of C1Inputdate for Return Date
function IDReturnDate_valueChanged(sender) {
    
}

//function to search flights for selected criteria
function CallSearch() {
    $('#LblMsg').html('');
    var RetValue = false;
    if (TripTypeSingle.checked==false && TripTypeRound.checked ==false) {
        $('#LblMsg').attr('style', 'color:red;font-weight:bold;');
        $('#LblMsg').html('Select your Trip Type: One Way/Round Trip.');
        return RetValue;
    }
    if (ACFromDest.selectedIndex<0) {
        $('#LblMsg').attr('style', 'color:red;font-weight:bold;');
        $('#LblMsg').html('Select Leaving From Airport.');
        $('#ACFromDest').focus();
        return RetValue;
    }
    if (ACToDest.selectedIndex < 0) {
        $('#LblMsg').attr('style', 'color:red;font-weight:bold;');
        $('#LblMsg').html('Select Going To Airport.');
        $('#ACToDest').focus();
        return RetValue;
    }
    var DepartDate = new Date(IDDepartDate.value);
    //DepartDate = new Date(DepartDate.getFullYear(), DepartDate.getMonth(), 1);//29401
    var ReturnDate = new Date(IDReturnDate.value);
    //ReturnDate = new Date(ReturnDate.getFullYear(), ReturnDate.getMonth(), 1);
    if(ReturnDate<DepartDate){//if (IDReturnDate.value<IDDepartDate.value) {
        $('#LblMsg').attr('style', 'color:red;font-weight:bold;');
        $('#LblMsg').html('Return date can\'t be smaller than Departure Date. Select a valid Return date.');
        $('#IDReturnDate').focus();
        return RetValue;
    }
    if (CmbClass.selectedIndex<0) {
        $('#LblMsg').attr('style', 'color:red;font-weight:bold;');
        $('#LblMsg').html('Select a Class.');
        $('#CmbClass').focus();
        return RetValue;
    }
    RetValue = true;
    //var DepartDate = new Date(IDDepartDate.value);
    var DepartDateStr = (DepartDate.getMonth() + 1) + '/' + DepartDate.getDate() + '/' + DepartDate.getFullYear();
    //var ReturnDate = new Date(IDReturnDate.value);
    var ReturnDateStr = (ReturnDate.getMonth() + 1) + '/' + ReturnDate.getDate() + '/' + ReturnDate.getFullYear();
    window.location.href = HostPath + 'Home/RefineSearch?TripType=' + TripType + '&FromDest=' + ACFromDest.selectedItem.Dest + '&FromCity=' + ACFromDest.selectedItem.City + '&FromAirport=' + ACFromDest.selectedItem.AirportInfo + '&ToDest=' + ACToDest.selectedItem.Dest + '&ToCity=' + ACToDest.selectedItem.City + '&ToAirport=' + ACToDest.selectedItem.AirportInfo + '&DepartDateStr=' + DepartDateStr + '&ReturnDateStr=' + ReturnDateStr + '&Adult=' + (INAdult.selectedIndex + 1) + '&Child=' + INChild.selectedIndex + '&Infant=' + INInfant.selectedIndex + '&Class=' + CmbClass.selectedValue;
    return RetValue;
}

//function to handle selection changed event of C1ListBox for recent searches
function LBRecentSearches_SelectedIndexChanged(sender) {
    if (sender == null)
        return;
    if (sender.selectedIndex == -1)
        return;
    var SelItem = sender.selectedItem;
    var DepartDate = new Date(SelItem.DepartDate);
    var DepartDateStr = (DepartDate.getMonth() + 1) + '/' + DepartDate.getDate() + '/' + DepartDate.getFullYear();
    var ReturnDate = new Date(SelItem.ReturnDate);
    var ReturnDateStr = (ReturnDate.getMonth() + 1) + '/' + ReturnDate.getDate() + '/' + ReturnDate.getFullYear();
    window.location.href = HostPath + 'Home/RefineSearch?TripType=' + SelItem.TripType + '&FromDest=' + SelItem.FromDest + '&ToDest=' + SelItem.ToDest + '&DepartDateStr=' + DepartDateStr + '&ReturnDateStr=' + ReturnDateStr + '&Adult=' + SelItem.Adult + '&Child=' + SelItem.Child + '&Infant=' + SelItem.Infant + '&Class=' + 'ECONOMY';
};