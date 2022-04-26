var LblMsg = null,
    username = null,
    password = null,
    BtnLogin = null;

$(document).ready(function () {
    SetPageTitle('LOGIN TO YOUR ACCOUNT');
    LblMsg = document.getElementById('LblMsg');
    username = document.getElementById('username');
    password = document.getElementById('password');
    BtnLogin = document.getElementById('BtnLogin');
});

function ErrorMessage(message) {
    LblMsg.innerHTML = message ? message : '';
    LblMsg.className = message ? 'FailMsgClass' : 'HiddenMsgClass';
}

//function to Try for login
function CallLogin() {
    ErrorMessage(null);
    if (username.value == null || username.value == '') {
        ErrorMessage('Enter valid Email/User Name.');
        return;
    }
    if (password.value == null || password.value == '') {
        ErrorMessage('Enter valid password.');
        return;
    }
    ShowSpinner();
    $.ajax({
        url: HostPath + "Home/TryLogin",
        type: "GET",
        dataType: "json",
        data: { Email: username.value, Password: password.value },
        success: function (data) {
            if (data.ResultType == "Success" && data.ErrMsg == "") {
                LblUserName.innerHTML = data.UserName;
                localStorage.setItem("UserID", data.UserID);
                localStorage.setItem("UserName", data.UserName);
                var CallP=GetQueryStringValue('CallP');
                if (CallP == null)
                    window.location.href = HostPath + 'Home/Index';
                else {
                    if (CallP.indexOf("Home/BookFlight") != -1)
                        CallP += "&Ret=" + GetQueryStringValue('Ret');
                    window.location.href = HostPath + CallP;
                }
            }
            else {
                ErrorMessage(data.ErrMsg);
                HideSpinner();
            }
        },
        error: function (data) {
            HideSpinner();
        }
    });
};
