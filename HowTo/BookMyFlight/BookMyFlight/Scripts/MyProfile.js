var LblMsg=null,
    CmbTitle=null,
    FName=null,
    LName=null,
    Email=null,
    ISD=null,
    Phone=null,
    Address=null,
    CmbCountry = null,
    State=null,
    City=null,
    PIN=null,
    CBoxOffers=null,
    SelCountry = null;

$(document).ready(function () {    
    LblMsg = document.getElementById('LblMsg');
    CmbTitle=wijmo.Control.getControl('#CmbTitle');
    FName=document.getElementById('FName');
    LName=document.getElementById('LName');
    Email=document.getElementById('Email');
    ISD=document.getElementById('ISD');
    Phone=document.getElementById('Phone');
    Address=document.getElementById('Address');
    CmbCountry = wijmo.Control.getControl("#CmbCountry");
    State=document.getElementById('State');
    City=document.getElementById('City');
    PIN=document.getElementById('PIN');    
    SelCountry = document.getElementById('SelCountry');
    CBoxOffers=document.getElementById('CBoxOffers');
    FillCountryList(CmbCountry, SelCountry.value);
});


//function to update user profile
function CallUpdateProfile() {
    LblMsg.innerHTML = null;
    LblMsg.className = 'FailMsgClass';
    if (CmbTitle.selectedValue == null || CmbTitle.selectedValue == '') {
        LblMsg.innerHTML = 'Invalid Title. Select a valid Title.';
        CmbTitle.focus();
        return;
    }
    if (FName.value == null || FName.value == '') {
        LblMsg.innerHTML = 'Enter your First Name.';
        FName.focus();
        return;
    }
    if (LName.value == null || LName.value == '') {
        LblMsg.innerHTML = 'Enter your Last Name.';
        CmbTitle.focus();
        return;
    }
    if (Email.value == null || Email.value == '') {
        LblMsg.innerHTML = 'Enter your Email Id.';
        Email.focus();
        return;
    }
    if (ISD.value == null || ISD.value == '') {
        LblMsg.innerHTML = 'Enter ISD/Country Code.';
        isd.focus();
        return;
    }
    if (Phone.value == null || Phone.value == '') {
        LblMsg.innerHTML = 'Enter valid Phone/Mobile No.';
        Phone.focus();
        return;
    }
    if (Address.value == null || Address.value == '') {
        LblMsg.innerHTML = 'Enter your contact address.';
        Address.focus();
        return;
    }
    if (CmbCountry.selectedValue == null || CmbCountry.selectedValue == '') {
        LblMsg.innerHTML = 'Select your country.';
        CmbCountry.focus();
        return;
    }
    if (State.value == null || State.value == '') {
        LblMsg.innerHTML = 'Enter your state.';
        State.focus();
        return;
    }
    if (City.value == null || City.value == '') {
        LblMsg.innerHTML = 'Enter your city.';
        City.focus();
        return;
    }
    if (PIN.value == null || PIN.value == '') {
        LblMsg.innerHTML = 'Enter your city.';
        PIN.focus();
        return;
    }
    $.ajax({
        url: HostPath + "MyProfile/UpdateUserProfile",
        type: "POST",
        dataType: "json",
        data: { Title: CmbTitle.selectedValue, FName: FName.value, LName: LName.value, Email: Email.value, ISD: ISD.value, Phone: Phone.value, Address: Address.value, Country: CmbCountry.selectedValue, State: State.value, City: City.value, PIN: PIN.value },
        success: function (data) {
            if (data.ResultType == "Success" && data.ErrMsg == "") {
                LblMsg.className = 'SuccessMsgClass';
                LblMsg.innerHTML = 'Your profile has been updated successfully.';
            }
            else {
                LblMsg.innerHTML = 'Error: ' + data.ErrMsg;
                LblMsg.className = 'FailMsgClass';
            }
        }
    });
};