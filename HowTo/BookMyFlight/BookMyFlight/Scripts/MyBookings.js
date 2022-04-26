var ListTypeUpcoming = null,
    ListTypeCompleted = null,
    ListTypeCancelled = null,
    LblSelectedRange = null,
    FCBookings = null,
    LBBookings = null,
    CmbFromMonth = null,
    CmbFromYear = null,
    CmbToMonth = null,
    CmbToYear = null,
    LblMsg = null,
    LblRangeDlgMsg = null,
    BookingsList = null,
    ReturnNullList = [];

$(document).ready(function () {
    SetPageTitle('MY BOOKINGS');
    ListTypeUpcoming = document.getElementById('ListTypeUpcoming');
    ListTypeCompleted = document.getElementById('ListTypeCompleted');
    ListTypeCancelled = document.getElementById('ListTypeCancelled');
    LblSelectedRange = document.getElementById('LblSelectedRange');
    FCBookings = wijmo.Control.getControl("#FCBookings");
    LBBookings = wijmo.Control.getControl("#LBBookings");
    CmbFromMonth = wijmo.Control.getControl("#CmbFromMonth");
    CmbFromYear = wijmo.Control.getControl("#CmbFromYear");
    CmbToMonth = wijmo.Control.getControl("#CmbToMonth");
    CmbToYear = wijmo.Control.getControl("#CmbToYear");
    LblMsg = document.getElementById('LblMsg');
    LblRangeDlgMsg = document.getElementById('LblRangeDlgMsg');
    
    $('#ListTypeUpcoming,#ListTypeCompleted,#ListTypeCancelled').change(function () {
        ShowMyBookings();
    });

    LblSelectedRange.innerHTML = "(" + CmbFromMonth.selectedValue + " " + CmbFromYear.selectedValue + "-" + CmbToMonth.selectedValue + " " + CmbToYear.selectedValue + ")";
    ShowMyBookings();
});

$('#LBBookings .wj-listbox-item').click(function () {
    event.stopPropagation();
});

//function to handle itemschanged event of C1ListBox-LBBookings
function LBBookings_ItemsChanged(sender) {
};

//function to handle hover
//$('#DivViewByLink').hover(
//        function () {
//            $(this).prop;
//        },
//    function () {
//        $(this).find('.onlyitem').hide();
//    });

//function to show bookings
function ShowMyBookings() {
    LblMsg.innerHTML = '';
    $('#PnlBookingsFlexChart').hide();
    $('#PnlBookingsFlexGrid').hide();    
    if (ListTypeUpcoming.checked) {
        $('#PnlDateLink').hide();
        ShowUpcomingBookings();
    }
    else if (ListTypeCompleted.checked) {
        $('#PnlDateLink').show();
        ShowCompletedBookings();
    }
    else {
        $('#PnlDateLink').show();
        ShowCancelledBookings();
    }
};

//function to show upcoming bookings
function ShowUpcomingBookings() {
    var TodayDate = new Date();
    var Travel_FDateStr = (TodayDate.getMonth() + 1) + '/' + TodayDate.getDate() + '/' + TodayDate.getFullYear();
    GetBookingsList(null, null, null, Travel_FDateStr, null, 0, null, null, true);
};

//function to show completed bookings
function ShowCompletedBookings() {
    var TodayDate = new Date();
    var Travel_FDateStr = (CmbFromMonth.selectedIndex + 1) + '/01/' + CmbFromYear.selectedValue;
    var Travel_TDateStr = (CmbToMonth.selectedIndex + 1) + '/01/' + CmbToYear.selectedValue;
    GetBookingsList(null, null, null, Travel_FDateStr, Travel_TDateStr, 0, null, null, true);
};

//function to show cancelled bookings
function ShowCancelledBookings() {
    GetBookingsList(null, null, null, null, null, 0, null, null, false);
};

//ItemFormatter of FlexGrid
function FGBookings_ItemFormatter(panel, r, c, cell) {
    if (wijmo.grid.CellType.Cell == panel.cellType && (panel.columns[c].binding == 'Depart_Airlines_Img_URL' || panel.columns[c].binding == 'Return_Airlines_Img_URL')) {
        var cellData = panel.getCellData(r, c);
        if (cellData != null && cellData != '') {
            var ImgURL = '/' + cellData;
            var AirlinesName = panel.getCellData(r, c + 1);
            var CellHTML = '<div><img src="' + ImgURL + '" alt="' + AirlinesName + '" title="' + AirlinesName + '"/>';
            CellHTML += '<br />' + AirlinesName + '</div>';
            cell.innerHTML = CellHTML;
        }
        else
            cell.innerHTML = 'N/A';
    }
    if (wijmo.grid.CellType.Cell == panel.cellType && panel.columns[c].name == 'Print') {
        var cellData = panel.getCellData(r, c);
        var BookID = panel.getCellData(r, 0);
        var printfunc = "PrintInvoice(" + BookID + ")";
        cell.innerHTML = '<button class="btn btn-default btn-sm" onclick="' + printfunc + '"><span class="align-center glyphicon glyphicon-print" style="color:#D14836"></span></button>';
    }
};

//function to bind Bookings data with C1FlexGrid
function SetBookingsGrid(CompareDate, SelMonth, SelYear) {
    ReturnNullList = [];
    var FGBookingData = [];
    if (BookingsList != null && BookingsList.items.length > 0) {
        for (var i = 0; i < BookingsList.items.length; i++) {            
            if (CompareDate == 'Book') {
                if (BookingsList.items[i].Book_Date.getMonth() == SelMonth && BookingsList.items[i].Book_Date.getFullYear() == SelYear) {
                    FGBookingData.push(BookingsList.items[i]);
                    if (BookingsList.items[i].Return_DateTime == null)
                        ReturnNullList[ReturnNullList.length] = BookingsList.items[i].Id;
                }
            }
            else if (CompareDate == 'Travel') {
                if (BookingsList.items[i].Departure_DateTime.getMonth() == SelMonth && BookingsList.items[i].Departure_DateTime.getFullYear() == SelYear) {
                    FGBookingData.push(BookingsList.items[i]);
                    if (BookingsList.items[i].Return_DateTime == null)
                        ReturnNullList[ReturnNullList.length] = BookingsList.items[i].Id;
                }
            }
            else if (CompareDate == 'Both') {
                if ((BookingsList.items[i].Book_Date.getMonth() == SelMonth && BookingsList.items[i].Book_Date.getFullYear() == SelYear)
                    || (BookingsList.items[i].Departure_DateTime.getMonth() == SelMonth && BookingsList.items[i].Departure_DateTime.getFullYear() == SelYear)) {
                    FGBookingData.push(BookingsList.items[i]);
                    if (BookingsList.items[i].Return_DateTime == null)
                        ReturnNullList[ReturnNullList.length] = BookingsList.items[i].Id;
                }
            }
            else if (CompareDate == 'Cancel') {
                FGBookingData = BookingsList;
                if (BookingsList.items[i].Return_DateTime == null)
                    ReturnNullList[ReturnNullList.length] = BookingsList.items[i].Id;
            }
        }
    }
    if (CompareDate != 'Cancel')
        FGBookingData = new wijmo.collections.CollectionView(FGBookingData);
    LBBookings.itemsSource = FGBookingData.items;
    LBBookings.selectedIndex = -1;
    if (FGBookingData && FGBookingData.items.length > 0) {
        $('#PnlBookingsFlexGrid').show();
        HideReturnNullData();
    }
    else
        $('#PnlBookingsFlexGrid').hide();
};

//function to hide return flight panel in case of One-Way trip
function HideReturnNullData() {
    if(ReturnNullList!=null && ReturnNullList.length>0)
    {
        for (var i = 0; i < ReturnNullList.length; i++) {
            document.getElementById('PnlReturn_' + ReturnNullList[i]).style.display = 'none';
        }
    }
};

//function to handle selection changed of C1FlexGrid-FCBookings
function FCBookings_SelectionChanged(sender) {
    if (sender.selection) {
        var currentSelectedItem = sender.selection.collectionView.currentItem;
        SetBookingsGrid('Travel', currentSelectedItem.Month.getMonth(), currentSelectedItem.Month.getFullYear());
    }
};

//function to bind bookings data with C1FlexChart-FCBookings
function SetBookingsChart(BookingsData) {
    var FromDate = new Date(), ToDate = new Date();
    if (ListTypeUpcoming.checked) {
        FromDate.setMonth(FromDate.getMonth(), 1);
        ToDate = new Date(FromDate);
        ToDate.setMonth(ToDate.getMonth() + 5);
    }
    else {
        FromDate = new Date(CmbFromYear.selectedValue, CmbFromMonth.selectedIndex, 1);
        ToDate = new Date(CmbToYear.selectedValue, CmbToMonth.selectedIndex, 1);
    }
    var BookingsChartData = [];
    var i = 0;
    var TempDate = new Date(FromDate);
    while (TempDate <= ToDate) {
        i++;
        var MonthYearDate = new Date(FromDate);
        MonthYearDate.setMonth(MonthYearDate.getMonth() + i - 1);
        var BookingAmt = 0,
            TravelAmt = 0;
        for (var index = 0; index < BookingsData.length; index++) {
            if (BookingsData[index].Book_Date.getMonth() == MonthYearDate.getMonth() && BookingsData[index].Book_Date.getFullYear() == MonthYearDate.getFullYear())
                BookingAmt += BookingsData[index].Total_Fare;

            if (BookingsData[index].Departure_DateTime.getMonth() == MonthYearDate.getMonth() && BookingsData[index].Departure_DateTime.getFullYear() == MonthYearDate.getFullYear())
                TravelAmt += BookingsData[index].Total_Fare;
        }
        BookingsChartData.push({ Month: MonthYearDate, MonthStr: (MonthYearDate.getMonth() + 1 + '-' + MonthYearDate.getFullYear()), BookingAmt: BookingAmt, TravelAmt: TravelAmt });
        TempDate.setMonth(TempDate.getMonth() + 1);
    }
    if (BookingsChartData != null && BookingsChartData.length > 0) {
        var ChartData = new wijmo.collections.CollectionView(BookingsChartData);
        //ChartData.moveCurrentToFirst​();
        FCBookings.itemsSource = ChartData;//new wijmo.collections.CollectionView(BookingsChartData);//BookingsChartData;
        //ChartData.moveCurrentToFirst​();
        //FCBookings.selection = ChartData.items[0];//FCBookings.itemsSource.collectionView.items[0];//BookingsChartData[0];
        //setTimeout(function () {
        //    FCBookings_SelectionChanged(FCBookings);
        //}, 2000);
        
        setTimeout(function () {            
            var currentSelectedItem = ChartData.items[0];//sender.selection.collectionView.currentItem;
            SetBookingsGrid('Travel', currentSelectedItem.Month.getMonth(), currentSelectedItem.Month.getFullYear());
        }, 2000);
        
        $('#PnlBookingsFlexChart').show();
    }
};

//function to get bookings data from server side
function GetBookingsList(BookID, Book_FDateStr, Book_TDateStr, Travel_FDateStr, Travel_TDateStr, User_ID, From_DestID, To_DestID, Active) {
    BookingsList = null;
    ShowSpinner();
    $.ajax({
        url: HostPath + "MyBookings/GetBookings",
        type: "POST",
        dataType: "json",
        data: { BookID: BookID, Book_FDateStr: Book_FDateStr, Book_TDateStr: Book_TDateStr, Travel_FDateStr: Travel_FDateStr, Travel_TDateStr: Travel_TDateStr, User_ID: User_ID, From_DestID: From_DestID, To_DestID: To_DestID, Active: Active },
        success: function (data) {
            if (data.ResultType == "Success" && data.ErrMsg == "") {
                if (data.BookingsList != null && data.BookingsList.length > 0) {
                    BookingsList = data.BookingsList;
                    for (var i = 0; i < BookingsList.length; i++) {
                        if (BookingsList[i].hasOwnProperty('Book_Date')) {
                            if (BookingsList[i].Book_Date != null)
                                BookingsList[i].Book_Date = new Date(parseInt(BookingsList[i].Book_Date.substr(6)));
                        }
                        if (BookingsList[i].hasOwnProperty('Departure_DateTime')) {
                            if (BookingsList[i].Departure_DateTime != null)
                                BookingsList[i].Departure_DateTime = new Date(parseInt(BookingsList[i].Departure_DateTime.substr(6)));
                        }
                        if (BookingsList[i].hasOwnProperty('Departure_ArrivalDateTime')) {
                            if (BookingsList[i].Departure_ArrivalDateTime != null)
                                BookingsList[i].Departure_ArrivalDateTime = new Date(parseInt(BookingsList[i].Departure_ArrivalDateTime.substr(6)));
                        }
                        if (BookingsList[i].hasOwnProperty('Return_DateTime')) {
                            if (BookingsList[i].Return_DateTime != null)
                                BookingsList[i].Return_DateTime = (new Date(parseInt(BookingsList[i].Return_DateTime.substr(6))));
                        }
                        if (BookingsList[i].hasOwnProperty('Return_ArrivalDateTime')) {
                            if (BookingsList[i].Return_ArrivalDateTime != null)
                                BookingsList[i].Return_ArrivalDateTime = new Date(parseInt(BookingsList[i].Return_ArrivalDateTime.substr(6)));
                        }
                    }

                    if (ListTypeCancelled.checked) {
                        BookingsList = new wijmo.collections.CollectionView(BookingsList);
                        SetBookingsGrid('Cancel', null, null);
                    }
                    else {
                        SetBookingsChart(BookingsList);
                        BookingsList = new wijmo.collections.CollectionView(BookingsList);
                    }
                }
                else {
                    $('#LblMsg').addClass('NormalMsgClass');
                    LblMsg.innerHTML = 'No bookings found in selected date range.';
                }
            }
            HideSpinner();
        },
        error: function (data) {
            HideSpinner();
        }
    });
};

//function to apply changes of selected date range
function ApplyDateRange() {
    LblRangeDlgMsg.innerHTML = '';
    var ThisMonth = new Date();
    ThisMonth.setMonth(ThisMonth.getMonth(), 1);
    var FromDate = new Date(CmbFromYear.selectedValue, CmbFromMonth.selectedIndex);
    var ToDate = new Date(CmbToYear.selectedValue, CmbToMonth.selectedIndex);
    if (ToDate < FromDate) {
        $('#LblRangeDlgMsg').addClass('FailMsgClass');
        LblRangeDlgMsg.innerHTML = 'Please! select a proper date range.';
    }
    else {
        LblSelectedRange.innerHTML = "(" + CmbFromMonth.selectedValue + " " + CmbFromYear.selectedValue + "-" + CmbToMonth.selectedValue + " " + CmbToYear.selectedValue + ")";
        var t = document.getElementsByClassName("close");
        t[0].click();
        ShowMyBookings();
    }
};

