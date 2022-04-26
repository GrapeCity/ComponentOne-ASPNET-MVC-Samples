$.support.cors = true;
var ProcessSpinner = null,
    DivPageTitle = null,
    LblUserName = null,
    BtnLoginLogout = null;
var CountryList = [];

$(document).ready(function () {
    DivPageTitle = document.getElementById('PageTitle');
    LblUserName = document.getElementById('LblUserName');
    ProcessSpinner = document.getElementById('ProcessSpinner');
    BtnLoginLogout = document.getElementById('BtnLoginLogout');
    GetUserInfo();
});

//function to show spinner while in process
function ShowSpinner() {
    ProcessSpinner.style.display = 'block';
};

//function to hide spinner while process completed
function HideSpinner() {
    ProcessSpinner.style.display = 'none';
};

//function to get Login User Info
function GetUserInfo() {
    var UserInfo = { UserID: localStorage.getItem("UserID"), UserName: localStorage.getItem("UserName") };
    if (UserInfo.UserName != null && UserInfo.UserName != '') {
        LblUserName.innerHTML = UserInfo.UserName;
        BtnLoginLogout.innerHTML = 'Sign Out';
    }
    else {
        LblUserName.innerHTML = 'Guest';
        BtnLoginLogout.innerHTML = 'Sign In';
    }
    return UserInfo;
};

//function to get country list using api services
function GetCountryList() {    
    $.ajax({
        url: HostPath + "countryInfoJSON.json",
        type: "GET",
        dataType: "json",
        data: {},
        success: function (data) {
            CountryList = data.geonames;
        },
        error: function (data) {
        }
    });

};

//function to fill country list in C1 Combo using api services
function FillCountryList(CmbCtrlId,SelValue) {
    $.ajax({
        url: HostPath + "countryInfoJSON.json",
        type: "GET",
        dataType: "json",
        data: {},
        success: function (data) {
            CmbCtrlId.itemsSource = data.geonames;
            CmbCtrlId.selectedValue = SelValue;
        },
        error: function (data) {
        }
    });

};

//function to get query string value of current page
function GetQueryStringValue(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results == null ? null : decodeURIComponent(results[1].replace(/\+/g, " "));
}

//function to set title of current page
function SetPageTitle(Title) {
    DivPageTitle.innerHTML = Title;
};


//function to check and set login/logout status
function CallLoginLogout() {
    var user = GetUserInfo();
    if (user.UserName == null || user.UserName == '')
        window.location.href = HostPath + 'Home/Login';
    else
    {
        var ls = localStorage;
        ls.removeItem("UserID");
        ls.removeItem("UserName");
        GetUserInfo();
        window.location.href = HostPath + 'Home/Index';
    }
};

//To stop autoclose functionality of bootstrap menu
$(document).on('click', '.noclose .dropdown-menu', function (e) {
    e.stopPropagation()
});


