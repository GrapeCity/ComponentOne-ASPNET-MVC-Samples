var TravellersList = null,
    LblMsg = null,
    LblDlgMsg = null,
    LBTravelers = null,
    SelTravelerID = null,
    CmbTitle = null,
    TBFirstName = null,
    TBMiddleName = null,
    TBLastName = null,
    IDDOB = null,
    TBEmail = null,
    TBCountryCode = null,
    TBPhone = null,
    BtnSaveUpdate = null,
    DlgAddEditTravelLabel = null;


$(document).ready(function () {
    SetPageTitle('MY TRAVELLERS');
    LblMsg = document.getElementById('LblMsg');
    LblDlgMsg = document.getElementById('LblDlgMsg');
    LBTravelers = wijmo.Control.getControl("#LBTravelers");
    SelTravelerID = document.getElementById('SelTravelerID');
    CmbTitle = wijmo.Control.getControl("#CmbTitle");
    TBFirstName = document.getElementById('TBFirstName');
    TBMiddleName = document.getElementById('TBMiddleName');
    TBLastName = document.getElementById('TBLastName');
    IDDOB = wijmo.Control.getControl("#IDDOB");
    TBEmail = document.getElementById('TBEmail');
    TBCountryCode = document.getElementById('TBCountryCode');
    TBPhone = document.getElementById('TBPhone');
    BtnSaveUpdate = document.getElementById('BtnSaveUpdate');
    DlgAddEditTravelLabel = document.getElementById('BtnSaveUpdate');
    FillTravelers(null, null, null, null);
});

//function to add/update traveler
function AddUpdateTraveler(TravelerId, Title, FName, MName, LName, DOBStr, Email,ISD, Phone) {    
    LblDlgMsg.innerHTML = null;
    LblDlgMsg.className = 'SuccessMsgClass';
    ShowSpinner();
    $.ajax({
        url: HostPath + "MyTravelers/AddUpdateTraveler",
        type: "POST",
        dataType: "json",
        data: { TravelerId: TravelerId, Title: Title, FName: FName, MName: MName, LName: LName, DOBStr: DOBStr, Email: Email,ISD:ISD, Phone: Phone },
        success: function (data) {
            if (data.ResultType == "Success" && data.ErrMsg == "") {
                if (data.TravelersList != null && data.TravelersList.length > 0) {
                    TravellersList = data.TravelersList;
                    for (var i = 0; i < TravellersList.length; i++) {
                        if (TravellersList[i].hasOwnProperty('DOB')) {
                            if (TravellersList[i].DOB != null)
                                TravellersList[i].DOB = new Date(parseInt(TravellersList[i].DOB.substr(6)));
                        }
                    }
                }
                LBTravelers.itemsSource = TravellersList;
                if (TravelerId == 0)
                    LblDlgMsg.innerHTML = 'New Traveler has been added successfully.';
                else
                    LblDlgMsg.innerHTML = 'Traveler has been updated successfully.';
            }
            else {
                LblDlgMsg.innerHTML = 'Error: ' + data.ErrMsg;
                LblDlgMsg.className = 'FailMsgClass';
            }
            HideSpinner();
        },
        error: function (data) {
            HideSpinner();
        }
    });
};

//function to delete selected Traveler
function DeleteTraveler(TravelerId) {
    LblDlgMsg.innerHTML = null;
    LblDlgMsg.className = 'NormalMsgClass';
    ShowSpinner();
    $.ajax({
        url: HostPath + "MyTravelers/DeleteTraveler",
        type: "POST",
        dataType: "json",
        data: { TravelerId: TravelerId},
        success: function (data) {
            if (data.ResultType == "Success" && data.ErrMsg == "") {
                TravellersList = data.TravelersList;
                if (data.TravelersList != null && data.TravelersList.length > 0) {
                    for (var i = 0; i < TravellersList.length; i++) {
                        if (TravellersList[i].hasOwnProperty('DOB')) {
                            if (TravellersList[i].DOB != null)
                                TravellersList[i].DOB = new Date(parseInt(TravellersList[i].DOB.substr(6)));
                        }
                    }
                }
                LBTravelers.itemsSource = TravellersList;
                LblDlgMsg.className = 'SuccessMsgClass';
                LblDlgMsg.innerHTML = 'Traveler has been deleted successfully.';
            }
            else {
                LblDlgMsg.innerHTML = 'Error: ' + data.ErrMsg;
                LblDlgMsg.className = 'FailMsgClass';
            }
            HideSpinner();
        },
        error: function (data) {
            HideSpinner();
        }
    });
};

//function to bind travelers list with C1ListBox-LBTravelers
function FillTravelers(TravelerId, UserId, Name, Active) {
    TravellersList = [];
    ShowSpinner();
    $.ajax({
        url: HostPath + "MyTravelers/GetTravelers",
        type: "POST",
        dataType: "json",
        data: { TravelerId: TravelerId, UserId: UserId, Name: Name, Active: Active },
        success: function (data) {
            if (data.ResultType == "Success" && data.ErrMsg == "") {
                if (data.TravelersList != null && data.TravelersList.length > 0) {
                    TravellersList = data.TravelersList;
                    for (var i = 0; i < TravellersList.length; i++) {
                        if (TravellersList[i].hasOwnProperty('DOB')) {
                            if (TravellersList[i].DOB != null)
                                TravellersList[i].DOB = new Date(parseInt(TravellersList[i].DOB.substr(6)));
                        }
                    }
                }
                else {
                    LblMsg.className = 'NormalMsgClass';
                    LblMsg.innerHTML = 'No Traveler found.';
                }
                LBTravelers.itemsSource = TravellersList;
            }
            HideSpinner();
        },
        error: function (data) {
            HideSpinner();
        }
    });
};

//function to set dialog's control to blank
function SetBlank() {
    SelTravelerID.value='0';
    CmbTitle.selectedIndex = -1;
    TBFirstName.value = null;
    TBMiddleName.value = null;
    TBLastName.value = null;
    TBEmail.value = null;
    TBCountryCode.value = '+';
    TBPhone.value = null;
};

//function to set dialog's control for adding a new traveler
function CallAddTraveler() {
    LblMsg.innerHTML = LblDlgMsg.innerHTML = null;
    BtnSaveUpdate.innerHTML = DlgAddEditTravelLabel.innerHTML = 'Add Traveler';    
    SetBlank();
};

//function to add/update a traveler
function SaveUpdate() {
    LblDlgMsg.innerHTML = null;
    LblDlgMsg.className = 'FailMsgClass';
    if (CmbTitle.selectedIndex < 0) {
        LblDlgMsg.innerHTML = 'Select Title.';
        return;
    }
    if (TBFirstName.value==null || TBFirstName.value=='') {
        LblDlgMsg.innerHTML = 'Enter First Name.';
        return;
    }
    if (TBLastName.value == null || TBLastName.value == '') {
        LblDlgMsg.innerHTML = 'Enter Last Name.';
        return;
    }
    if (IDDOB.value==null || IDDOB.value=='') {
        LblDlgMsg.innerHTML = 'Invalid Date of Birth.';
        return;
    }
    if (TBEmail.value == null || TBEmail.value == '') {
        LblDlgMsg.innerHTML = 'Enter a valid Email Id.';
        return;
    }
    if (TBCountryCode.value == null || TBCountryCode.value == '') {
        LblDlgMsg.innerHTML = 'Enter valid ISD code.';
        return;
    }
    if (TBPhone.value == null || TBPhone.value == '') {
        LblDlgMsg.innerHTML = 'Enter valid Contact No.';
        return;
    }
    var DOB=new Date(IDDOB.value);
    var DOBStr=(DOB.getMonth()+1)+'/'+DOB.getDate()+'/'+DOB.getFullYear();
    AddUpdateTraveler(SelTravelerID.value,CmbTitle.selectedValue,TBFirstName.value,TBMiddleName.value,TBLastName.value,DOBStr,TBEmail.value,TBCountryCode.value,TBPhone.value)
    
};

///function to set dialog to edit a selected traveler
function CallEditTraveler(TravelerId) {
    BtnSaveUpdate.innerHTML = DlgAddEditTravelLabel.innerHTML = 'Update Traveler';
    SetBlank();
    LblMsg.innerHTML = LblDlgMsg.innerHTML = null;
    LblMsg.className = 'FailMsgClass';    
    if (TravelerId != null && TravelerId > 0 && TravellersList!=null && TravellersList.length>0) {
        var ItemIndex = TravellersList.map(function (x) { return x.Id; }).indexOf(TravelerId);
        var SelTraveler = TravellersList[ItemIndex];
        if(SelTraveler!=null)
        {
            SelTravelerID.value = SelTraveler.Id;
            CmbTitle.selectedValue = SelTraveler.Title;
            TBFirstName.value = SelTraveler.First_Name;
            TBMiddleName.value = SelTraveler.Middle_Name;
            TBLastName.value = SelTraveler.Last_Name;
            IDDOB.value = new Date(SelTraveler.DOB);
            TBEmail.value = SelTraveler.Email_ID;
            TBCountryCode.value = SelTraveler.ISD;
            TBPhone.value = SelTraveler.Contact_No;
        }
        else
            LblMsg.innerHTML = 'No traveller is selected to edit. Please! select one and try again.';
    }
    else
        LblMsg.innerHTML = 'No traveller is selected to edit. Please! select one and try again.';
};

//function to process deletion of selected traveler
function CallDeleteTraveler(TravelerId) {
    DeleteTraveler(TravelerId);
};