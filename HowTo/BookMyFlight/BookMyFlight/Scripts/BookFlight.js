var LblMsg = null,
    AdultList = null,
    ChildList = null,
    InfantList = null,
    PnlTravelers = null,
    PnlMakePayment = null,
    PnlReport = null,
    Visa = null,
    MasterCard = null,
    AmericanExpress = null,
    CardNumber = null,
    Expiration = null,
    Code = null;

var SelAdultList = [],
    SelChildList = [],
    SelInfantList = [];

var SelAdultList_New = [],
    SelChildList_New = [],
    SelInfantList_New = [];

$(document).ready(function () {
    SetPageTitle('BOOK FLIGHT-TRAVELLERS DETAIL');
    LblMsg = document.getElementById('LblMsg');
    AdultList = wijmo.Control.getControl("#AdultList");
    ChildList = wijmo.Control.getControl("#ChildList");
    InfantList = wijmo.Control.getControl("#InfantList");
    PnlTravelers = document.getElementById('PnlTravelers');
    PnlMakePayment = document.getElementById('PnlMakePayment');
    PnlReport = document.getElementById('PnlReport');
    Visa = document.getElementById('Visa');
    MasterCard = document.getElementById('MasterCard');
    AmericanExpress = document.getElementById('AmericanExpress');
    CardNumber = document.getElementById('CardNumber');
    Expiration = document.getElementById('Expiration');
    Code = document.getElementById('Code');

    PnlMakePayment.style.display = 'none';
    PnlReport.style.display = 'none';
    PnlTravelers.style.display = 'block';
    CallBookFlight_Load();
});

function ErrorMessage(message) {
    LblMsg.innerHTML = message ? message : '';
    LblMsg.className = message ? 'FailMsgClass' : 'HiddenMsgClass';
}


//function to set card no. at selection of card types
function CardTypeChanged() {
    if (Visa.checked)
        CardNumber.value = '4693-7558-6581-2485';
    else if (MasterCard.checked)
        CardNumber.value = '5419-8499-9999-4582';
    else if (AmericanExpress.checked)
        CardNumber.value = '3759-876543-21001';
};

//function to set page contents at page loading
function CallBookFlight_Load() {
    ShowSpinner();
    $.ajax({
        url: HostPath + "Home/BookFlight_Load",
        type: "GET",
        dataType: "json",
        data: {},
        success: function (success) {
            if (success.ResultType == "Success" && success.ErrMsg == "") {
                SelAdultList = success.SelAdultList;
                SelChildList = success.SelChildList;
                SelInfantList = success.SelInfantList;
            }
            else {
                ErrorMessage('Error: ' + success.ErrMsg);                
            }
            HideSpinner();
        },
        error: function (error) {
            HideSpinner();
        }
    });
};

//function to handle selection changed event of C1ComboAdultFirstName
function AdultSelIndexChanged(sender) {
    if (sender != null && AdultList != null && AdultList.selectedIndex >= 0) {
        var SelIndex = sender.selectedIndex;
        var SelItem = sender.selectedItem;

        if (SelIndex >= 0) {
            var Title = wijmo.Control.getControl('.CmbAdultTitle');
            if (Title) Title.selectedValue = SelItem.Title;
        }

        if (SelAdultList.length > 0) {
            var MName = document.getElementById('AdultMName_' + SelAdultList[AdultList.selectedIndex].Id);
            var LName = document.getElementById('AdultLName_' + SelAdultList[AdultList.selectedIndex].Id);
            if (SelIndex >= 0) {
                if (MName) MName.value = SelItem.Middle_Name;
                if (LName) LName.value = SelItem.Last_Name;
            } else {
                if (MName) MName.value = null;
                if (LName) LName.value = null;
            }
        }
    }
};

//function to handle selection changed event of C1ComboChildFirstName
function ChildSelIndexChanged(sender) {
    if (sender != null && ChildList != null && ChildList.selectedIndex >= 0) {
        var SelIndex = sender.selectedIndex;
        var SelItem = sender.selectedItem;

        if (SelIndex >= 0) {
            var Title = wijmo.Control.getControl('.CmbChildTitle');
            if (Title) Title.selectedValue = SelItem.Title;
        }

        if (SelChildList.length > 0) {
            var MName = document.getElementById('ChildMName_' + SelChildList[ChildList.selectedIndex].Id);
            var LName = document.getElementById('ChildLName_' + SelChildList[ChildList.selectedIndex].Id);
            if (SelIndex >= 0) {
                if (MName) MName.value = SelItem.Middle_Name;
                if (LName) LName.value = SelItem.Last_Name;
            } else {
                if (MName) MName.value = null;
                if (LName) LName.value = null;
            }
        }
    }
};

//function to handle selection changed event of C1ComboInfantFirstName
function InfantSelIndexChanged(sender) {
    if (sender != null && InfantList != null && InfantList.selectedIndex >= 0) {
        var SelIndex = sender.selectedIndex;
        var SelItem = sender.selectedItem;

        if (SelIndex >= 0) {
            var Title = wijmo.Control.getControl('.CmbInfantTitle');
            if (Title) Title.selectedValue = SelItem.Title;

            var DOB = wijmo.Control.getControl('.IDInfantDOB');
            if (DOB) DOB.value = SelItem.DOB;
        }

        if (SelChildList.length > 0) {
            var MName = document.getElementById('InfantMName_' + SelInfantList[InfantList.selectedIndex].Id);
            var LName = document.getElementById('InfantLName_' + SelInfantList[InfantList.selectedIndex].Id);
            if (SelIndex >= 0) {
                if (MName) MName.value = SelItem.Middle_Name;
                if (LName) LName.value = SelItem.Last_Name;
            } else {
                if (MName) MName.value = null;
                if (LName) LName.value = null;
            }
        }
    }
};

//function to proceed for payment
function CallProceedPayment() {    
    ErrorMessage(null);
    if (CheckTravellersDetail()) {
        ShowSpinner();
        SetPageTitle('BOOK FLIGHT-MAKE PAYMENT');
        PnlReport.style.display = 'none';
        PnlTravelers.style.display = 'none';
        PnlMakePayment.style.display = 'block';
        HideSpinner();
    }
    else {
        ErrorMessage('Please! Enter details of traveller(s).');
    }
};

//function to check whether entered payment info is valid or not
function CheckPaymentInfo() {
    return true;
};

//function to complete flight booking
function CallBook() {
    ErrorMessage(null);
    if (CheckPaymentInfo()) {
        ShowSpinner();
        $.ajax({
            url: HostPath + "Home/SaveFlight",
            type: "POST",
            "content-type": "applciation/json",
            data: { SelAdultList: SelAdultList, SelChildList: SelChildList, SelInfantList: SelInfantList },
            success: function (result) {
                if (result.ResultType == "Success" && result.ErrMsg == "") {
                    if (result.BookID > 0) {
                        SetPageTitle('BOOK FLIGHT-BOOKED SUCCESSFULLY');
                        PnlTravelers.style.display = 'none';
                        PnlMakePayment.style.display = 'none';
                        PnlReport.style.display = 'block';
                        window.open(HostPath + 'MyBookings/OpenInvoice?BookID=' + result.BookID);
                    }
                    else
                    ErrorMessage('Booking is not possible at this time due to some technical issues. Please! try after some time.');
                }
                else {
                    ErrorMessage('Error: ' + result.ErrMsg);                    
                }
                HideSpinner();
            },
            error: function (error) {
                HideSpinner();
            }
        });
    }
    else {
        ErrorMessage('Please! Enter details of traveller(s).');
    }
};

//function to check whether entered travelers details are valid or not
function CheckTravellersDetail() {
    var CDate = new Date();
    //Adult travelers
    SelAdultList_New = [];
    if (SelAdultList != null && SelAdultList.length > 0)
        for (var at = 0; at < SelAdultList.length; at++) {
            var Title = wijmo.Control.getControl('.CmbAdultTitle');
            var FName = wijmo.Control.getControl('.ACAdultFName');
            var MName = document.getElementById('AdultMName_' + SelAdultList[at].Id);
            var LName = document.getElementById('AdultLName_' + SelAdultList[at].Id);
            if (Title.selectedIndex < 0 || FName == null || FName.text == null || FName.text.length <= 0) {
                Title.focus();
                return false;
            }
            SelAdultList[at].TravelerId = FName.selectedIndex >= 0 ? FName.selectedItem.Id : 0;
            SelAdultList[at].Title = Title.selectedValue;
            SelAdultList[at].First_Name = FName.text;
            SelAdultList[at].Middle_Name = MName.value;
            SelAdultList[at].Last_Name = LName.value;
        }
    //Child travelers
    if (SelChildList != null && SelChildList.length > 0)
        for (var ct = 0; ct < SelChildList.length; ct++) {
            var Title = wijmo.Control.getControl('.CmbChildTitle');
            var FName = wijmo.Control.getControl('.ACChildFName');
            var MName = document.getElementById('ChildMName_' + SelChildList[ct].Id);
            var LName = document.getElementById('ChildLName_' + SelChildList[ct].Id);
            if (Title.selectedIndex < 0 || FName == null || FName.text == null || FName.text.length <= 0) {
                Title.focus();
                return false;
            }
            SelChildList[ct].TravelerId = FName.selectedIndex >= 0 ? FName.selectedItem.Id : 0;
            SelChildList[ct].Title = Title.selectedValue;
            SelChildList[ct].First_Name = FName.text;
            SelChildList[ct].Middle_Name = MName.value;
            SelChildList[ct].Last_Name = LName.value;
        }
    //Infant travelers
    if (SelInfantList != null && SelInfantList.length > 0)
        for (var it = 0; it < SelInfantList.length; it++) {
            var Title = wijmo.Control.getControl('.CmbInfantTitle');
            var FName = wijmo.Control.getControl('.ACInfantFName');
            var MName = document.getElementById('InfantMName_' + SelInfantList[it].Id);
            var LName = document.getElementById('InfantLName_' + SelInfantList[it].Id);
            var DOB = wijmo.Control.getControl('.IDInfantDOB');
            if (Title.selectedIndex < 0 || FName == null || FName.text == null || FName.text.length <= 0 || DOB == null || DOB.value == null || DOB.value == '') {
                Title.focus();
                return false;
            }
            SelInfantList[it].TravelerId = FName.selectedIndex >= 0 ? FName.selectedItem.Id : 0;
            SelInfantList[it].Title = Title.selectedValue;
            SelInfantList[it].First_Name = FName.text;
            SelInfantList[it].Middle_Name = MName.value;
            SelInfantList[it].Last_Name = LName.value;
            var DOBDt = new Date(DOB.value);
            SelInfantList[it].DOB = DOBDt.getMonth() + '/' + DOBDt.getDate() + '/' + DOBDt.getFullYear();
        }
    return true;
};
