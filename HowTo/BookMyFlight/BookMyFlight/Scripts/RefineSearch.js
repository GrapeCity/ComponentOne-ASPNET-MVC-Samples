var TripTypeSingle = null,
    TripTypeRound = null,
     ACFromDest = null,
     ACToDest = null,
     IDDepartDate = null,
     IDDepartDateFF = null,
     IDReturnDate = null,
     IDReturnDateFF = null,
     INAdult = null,
     INChild = null,
     INInfant = null,
     CmbClass = null,
     FGDepartList = null,
     FGReturnList = null,
     LGDepartTime = null,
     LGReturnTime = null,
     LGFlightDuration = null,
     LGPriceRange = null,
     LBAirlines = null,
     LblFromDest = null,
     LblFromCity = null,
     LblToDest = null,
     LblToCity = null,
     FCDepartFare = null,
     FCReturnFare = null,
     LBDepartList = null,
     LBReturnList = null;

var PnlFromDepartTimesFilter = null,
PnlToDepartTimesFilter = null,
LblDepartFromInFilter = null,
LblDepartToInFilter = null,
LblSelMinDepartInFilter = null,
LblSelMaxDepartInFilter = null,
LblSelMinReturnInFilter = null,
LblSelMaxReturnInFilter = null,
PnlFlightDurationFilter = null,
LblMinDurationInFilter = null,
LblMaxDurationInFilter = null,
PnlAirlinesFilter = null,
PnlPriceFilter = null,
LblSelMaxPriceInFilter = null,
LblMinPriceInFilter = null,
LblMaxPriceInFilter = null,
LblDepartDestFF = null,
LblReturnDestFF = null,
PnlDepartGrid = null,
PnlReturnGrid = null,
LblPageMsg = null,
LblModifyPnlMsg = null,
LblFFPnlMsg = null,
LnkFareFinder = null,
CheapFromRecID = null,
CheapToRecID = null,
ImgCheapFromFlight = null,
CheapFromDest1 = null,
CheapToDest1 = null,
CheapFromDepartTime = null,
CheapFromArrivalTime = null,
CheapFromFlightNo = null,
CheapFromDuration = null,
CheapFromStopInfo = null,
PnlCheapFromFlight = null,
ImgCheapToFlight = null,
CheapFromDest2 = null,
CheapToDest2 = null,
CheapToDepartTime = null,
CheapToArrivalTime = null,
CheapToFlightNo = null,
CheapToDuration = null,
CheapToStopInfo = null,
CheapTotalFare = null,
PnlCheapToFlight = null,
QuickFromRecID = null,
QuickToRecID = null,
ImgQuickFromFlight = null,
QuickFromDest1 = null,
QuickToDest1 = null,
QuickFromDepartTime = null,
QuickFromArrivalTime = null,
QuickFromFlightNo = null,
QuickFromDuration = null,
QuickFromStopInfo = null,
PnlQuickFromFlight = null,
ImgQuickToFlight = null,
QuickFromDest2 = null,
QuickToDest2 = null,
QuickToDepartTime = null,
QuickToArrivalTime = null,
QuickToFlightNo = null,
QuickToDuration = null,
QuickToStopInfo = null,
QuickTotalFare = null,
PnlQuickToFlight = null,
SelFromRecID = null,
SelToRecID = null,
ImgSelFromFlight = null,
SelFromDest1 = null,
SelToDest1 = null,
SelFromDepartTime = null,
SelFromArrivalTime = null,
SelFromFlightNo = null,
SelFromDuration = null,
SelFromStopInfo = null,
PnlSelFromFlight = null,
ImgSelToFlight = null,
SelFromDest2 = null,
SelToDest2 = null,
SelToDepartTime = null,
SelToArrivalTime = null,
SelToFlightNo = null,
SelToDuration = null,
SelToStopInfo = null,
SelTotalFare = null,
PnlSelToFlight = null,
SlFlightDuration = null,
SlPriceRange = null;

var DepartFlightList = null,
    ReturnFlightList = null,
    DepartFlightListCV = new wijmo.collections.CollectionView(),
    ReturnFlightListCV = new wijmo.collections.CollectionView();

var MinDurationInMin = 0,
    MaxDurationInMin = 0,
    MinPrice = 0,
    MaxPrice = 0;

var SortColumns = [{ 'name': 'Airlines', 'text': 'Airline' }, { 'name': 'DepartTime', 'text': 'Depart' }, { 'name': 'ArriveTime', 'text': 'Arrive' }, { 'name': 'DurationMin', 'text': 'Duration' }, { 'name': 'Fare', 'text': 'Price' }];
var DepSortOrder = [null, null, null, null, null];
var RetSortOrder = [null, null, null, null, null];

$(document).ready(function () {
    TripTypeSingle = document.getElementById('TripTypeSingle');
    TripTypeRound = document.getElementById('TripTypeRound');
    ACFromDest = wijmo.Control.getControl("#ACFromDest");
    ACToDest = wijmo.Control.getControl("#ACToDest");
    IDDepartDate = wijmo.Control.getControl("#IDDepartDate");
    IDDepartDateFF = wijmo.Control.getControl("#IDDepartDateFF");
    IDReturnDate = wijmo.Control.getControl("#IDReturnDate");
    IDReturnDateFF = wijmo.Control.getControl("#IDReturnDateFF");
    INAdult = wijmo.Control.getControl("#INAdult");
    INChild = wijmo.Control.getControl("#INChild");
    INInfant = wijmo.Control.getControl("#INInfant");
    CmbClass = wijmo.Control.getControl("#CmbClass");    
    LBAirlines = wijmo.Control.getControl("#LBAirlines");
    FCDepartFare = wijmo.Control.getControl("#FCDepartFare");
    FCReturnFare = wijmo.Control.getControl("#FCReturnFare");
    LBDepartList = wijmo.Control.getControl("#LBDepartList");
    LBReturnList = wijmo.Control.getControl("#LBReturnList");
    
    LblFromDest = document.getElementById('LblFromDest');
    LblFromCity = document.getElementById('LblFromCity');
    LblToDest = document.getElementById('LblToDest');
    LblToCity = document.getElementById('LblToCity');
    PnlFromDepartTimesFilter = document.getElementById('PnlFromDepartTimesFilter');
    PnlToDepartTimesFilter = document.getElementById('PnlToDepartTimesFilter');
    LblDepartFromInFilter = document.getElementById('LblDepartFromInFilter');
    LblDepartToInFilter = document.getElementById('LblDepartToInFilter');
    LblSelMinDepartInFilter = document.getElementById('LblSelMinDepartInFilter');
    LblSelMaxDepartInFilter = document.getElementById('LblSelMaxDepartInFilter');
    LblSelMinReturnInFilter = document.getElementById('LblSelMinReturnInFilter');
    LblSelMaxReturnInFilter = document.getElementById('LblSelMaxReturnInFilter');
    PnlFlightDurationFilter = document.getElementById('PnlFlightDurationFilter');
    LblMinDurationInFilter = document.getElementById('LblMinDurationInFilter');
    LblMaxDurationInFilter = document.getElementById('LblMaxDurationInFilter');
    PnlAirlinesFilter = document.getElementById('PnlAirlinesFilter');
    PnlPriceFilter = document.getElementById('PnlPriceFilter');
    LblSelMaxPriceInFilter = document.getElementById('LblSelMaxPriceInFilter');
    LblMinPriceInFilter = document.getElementById('LblMinPriceInFilter');
    LblMaxPriceInFilter = document.getElementById('LblMaxPriceInFilter');
    LblDepartDestFF = document.getElementById('LblDepartDestFF');
    LblReturnDestFF = document.getElementById('LblReturnDestFF');
    PnlDepartGrid = document.getElementById('PnlDepartGrid');
    PnlReturnGrid = document.getElementById('PnlReturnGrid');
    LblPageMsg = document.getElementById('LblPageMsg');
    LblModifyPnlMsg = document.getElementById('LblModifyPnlMsg');
    LblFFPnlMsg = document.getElementById('LblFFPnlMsg');
    LnkFareFinder = document.getElementById('LnkFareFinder');
    PnlCheapToFlight = document.getElementById('PnlCheapToFlight');
    CheapFromRecID = document.getElementById('CheapFromRecID');
    CheapToRecID = document.getElementById('CheapToRecID');
    ImgCheapFromFlight = document.getElementById('ImgCheapFromFlight');
    CheapFromDest1 = document.getElementById('CheapFromDest1');
    CheapToDest1 = document.getElementById('CheapToDest1');
    CheapFromDepartTime = document.getElementById('CheapFromDepartTime');
    CheapFromArrivalTime = document.getElementById('CheapFromArrivalTime');
    CheapFromFlightNo = document.getElementById('CheapFromFlightNo');
    CheapFromDuration = document.getElementById('CheapFromDuration');
    CheapFromStopInfo = document.getElementById('CheapFromStopInfo');
    PnlCheapToFlight = document.getElementById('PnlCheapToFlight');
    ImgCheapToFlight = document.getElementById('ImgCheapToFlight');
    CheapFromDest2 = document.getElementById('CheapFromDest2');
    CheapToDest2 = document.getElementById('CheapToDest2');
    CheapToDepartTime = document.getElementById('CheapToDepartTime');
    CheapToArrivalTime = document.getElementById('CheapToArrivalTime');
    CheapToFlightNo = document.getElementById('CheapToFlightNo');
    CheapToDuration = document.getElementById('CheapToDuration');
    CheapToStopInfo = document.getElementById('CheapToStopInfo');
    CheapTotalFare = document.getElementById('CheapTotalFare');

    QuickFromRecID = document.getElementById('QuickFromRecID');
    QuickToRecID = document.getElementById('QuickToRecID');
    ImgQuickFromFlight = document.getElementById('ImgQuickFromFlight');
    QuickFromDest1 = document.getElementById('QuickFromDest1');
    QuickToDest1 = document.getElementById('QuickToDest1');
    QuickFromDepartTime = document.getElementById('QuickFromDepartTime');
    QuickFromArrivalTime = document.getElementById('QuickFromArrivalTime');
    QuickFromFlightNo = document.getElementById('QuickFromFlightNo');
    QuickFromDuration = document.getElementById('QuickFromDuration');
    QuickFromStopInfo = document.getElementById('QuickFromStopInfo');
    PnlQuickFromFlight = document.getElementById('PnlQuickToFlight');
    ImgQuickToFlight = document.getElementById('ImgQuickToFlight');
    QuickFromDest2 = document.getElementById('QuickFromDest2');
    QuickToDest2 = document.getElementById('QuickToDest2');
    QuickToDepartTime = document.getElementById('QuickToDepartTime');
    QuickToArrivalTime = document.getElementById('QuickToArrivalTime');
    QuickToFlightNo = document.getElementById('QuickToFlightNo');
    QuickToDuration = document.getElementById('QuickToDuration');
    QuickToStopInfo = document.getElementById('QuickToStopInfo');
    QuickTotalFare = document.getElementById('QuickTotalFare');
    PnlQuickToFlight = document.getElementById('PnlQuickToFlight');

    SelFromRecID = document.getElementById('SelFromRecID');
    SelToRecID = document.getElementById('SelToRecID');
    ImgSelFromFlight = document.getElementById('ImgSelFromFlight');
    SelFromDest1 = document.getElementById('SelFromDest1');
    SelToDest1 = document.getElementById('SelToDest1');
    SelFromDepartTime = document.getElementById('SelFromDepartTime');
    SelFromArrivalTime = document.getElementById('SelFromArrivalTime');
    SelFromFlightNo = document.getElementById('SelFromFlightNo');
    SelFromDuration = document.getElementById('SelFromDuration');
    SelFromStopInfo = document.getElementById('SelFromStopInfo');
    PnlSelFromFlight = document.getElementById('PnlSelFromFlight');
    ImgSelToFlight = document.getElementById('ImgSelToFlight');
    SelFromDest2 = document.getElementById('SelFromDest2');
    SelToDest2 = document.getElementById('SelToDest2');
    SelToDepartTime = document.getElementById('SelToDepartTime');
    SelToArrivalTime = document.getElementById('SelToArrivalTime');
    SelToFlightNo = document.getElementById('SelToFlightNo');
    SelToDuration = document.getElementById('SelToDuration');
    SelToStopInfo = document.getElementById('SelToStopInfo');
    SelTotalFare = document.getElementById('SelTotalFare');
    PnlSelToFlight = document.getElementById('PnlSelToFlight');

    SlFlightDuration = document.getElementById('SlFlightDuration');
    SlPriceRange = document.getElementById('SlPriceRange');

    $('#myTab a').click(function (e) {
        e.preventDefault()
        $(this).tab('show')
    })

    $('#LBAirlines .wj-listbox-item').hover(
        function () {
            if ($(this).find('input').attr('value') != 'All')
                $(this).find('.onlyitem').show();
        },
    function () {
        $(this).find('.onlyitem').hide();
    });

    $('#LBAirlines .onlyitem').click(function (event) {
        var SelCheckValue = $(this).parent().find('input').val();
        $('#LBAirlines input[type=checkbox]').each(function () {
            if (this.value == SelCheckValue)
                $(this).prop('checked', true);
            else
                $(this).prop('checked', false);
        });
        event.stopPropagation();
        CheckAllFilters();
    });

    $('#LBAirlines .wj-listbox-item').click(function () {
        var ctrl = $(this).find('input');
        $(ctrl).prop('checked', !$(ctrl).is(':checked'));
        event.stopPropagation();
        CheckChange(ctrl);
    });

    $('.AirlinesList label').click(function (event) {
        CheckChange(this);
        event.stopPropagation();
    });

    $('.AirlinesList input').click(function (event) {
        CheckChange(this);
        event.stopPropagation();
    });

    function CheckChange(ctrl) {
        if ($(ctrl).val() == 'All') {            
            var CheckedValue = $(ctrl).is(":checked");
            $('#LBAirlines input[value!="All"]').each(function () {
                this.checked = CheckedValue;
            });
        }
        else {
            var UnCheckCount = 0;
            $('#LBAirlines input[type=checkbox]').each(function () {
                var Temp = $(this).is(":checked");
                if ($(this).attr("value") != 'All' && $(this).is(":checked") == false) {
                    UnCheckCount++;
                }
            });
            if (UnCheckCount == 0) {
                $('#All').prop('checked', true);
            }
            else {
                $('#All').prop('checked', false);
            }
        }
        CheckAllFilters();
    }

    
    //function to handle changed event of TripTypes
    $('#TripTypeSingle,#TripTypeRound').change(function () {
        CallTripTypeClicked();        
    });    
    
    //Set values of controls accessed from Querystring
    TripType = GetQueryStringValue('TripType');
    if (TripType == 'SINGLE')
    {        
        TripTypeSingle.checked = true;
    }
    else
    {
        TripTypeRound.checked = true;
    }
    CallTripTypeClicked();
    var FromDest = GetQueryStringValue('FromDest');
    var FromCity = GetQueryStringValue('FromCity');
    var FromAirport=GetQueryStringValue('FromAirport');
    var ToDest = GetQueryStringValue('ToDest');
    var ToCity = GetQueryStringValue('ToCity');
    var ToAirport=GetQueryStringValue('ToAirport');
    ACFromDest.selectedValue = FromDest;
    ACToDest.selectedValue = ToDest;
    IDDepartDate.value = IDDepartDateFF.value = new Date(GetQueryStringValue('DepartDateStr'));
    var TempDate = GetQueryStringValue('ReturnDateStr');
    if (GetTripType() == 'ROUND' && TempDate != null)
        IDReturnDate.value = IDReturnDateFF.value = new Date(TempDate);
    INAdult.value = GetQueryStringValue('Adult');
    INChild.value = GetQueryStringValue('Child');
    INInfant.value = GetQueryStringValue('Infant');
    CmbClass.selectedValue = GetQueryStringValue('Class');

    var DepartDate = new Date(IDDepartDate.value);
    var ReturnDate = new Date(IDReturnDate.value);
    var DepartDateStr = DepartDate.getMonth() + 1 + "/" + DepartDate.getDate() + "/" + DepartDate.getFullYear();
    var ReturnDateStr = null;
    if (GetTripType() == 'ROUND')
        ReturnDateStr = ReturnDate.getMonth() + 1 + "/" + ReturnDate.getDate() + "/" + ReturnDate.getFullYear();
    var AirlinesList = ['All'];
    GetFlightsList(GetTripType(), FromDest, FromCity, FromAirport, ToDest, ToCity, ToAirport, DepartDateStr, ReturnDateStr, INAdult.value, INChild.value, INInfant.value, CmbClass.selectedValue, 0, 0, 0, AirlinesList);
});

function ResetSortColList(Type, SortOrderList, ColIndex, Ascending) {
    if (SortOrderList == null || SortOrderList.length <= 0)
        return SortOrderList;
    for (var i = 0; i < SortOrderList.length; i++) {
        var Btn = null;
        if (Type == 'Dep')
            Btn = 'DFSBtn_' + SortColumns[i].name, html = SortColumns[i].text;
        else
            Btn = 'RFSBtn_' + SortColumns[i].name, html = SortColumns[i].text;
        if (i == ColIndex) {
            if (Ascending)
                html += '&nbsp;<i class="fa fa-arrow-up"></i>';
            else
                html += '&nbsp;<i class="fa fa-arrow-down"></i>';
            SortOrderList[i] = Ascending;
        }
        else
            SortOrderList[i] = null;
        document.getElementById(Btn).innerHTML = html;
    }
    return SortOrderList;
};

function SortDepartFlights(ColIndex) {
    var ColumnName = SortColumns[ColIndex].name;
    var Ascending = DepSortOrder[ColIndex] == null ? true : !DepSortOrder[ColIndex];
    DepartFlightListCV = LBDepartList.collectionView;
    if (DepartFlightListCV != null) {
        var sd = DepartFlightListCV.sortDescriptions;
        var sdNew = new wijmo.collections.SortDescription(ColumnName, Ascending); // remove any old sort descriptors and add the new one 
        sd.splice(0, sd.length, sdNew);
        DepSortOrder = ResetSortColList('Dep', DepSortOrder, ColIndex, Ascending);
        var selitem = DepartFlightListCV.currentItem;
        if (selitem != null)
            document.getElementById('DepFlight' + selitem.Id).checked = true;
    }
};

function SortReturnFlights(ColIndex) {
    var ColumnName = SortColumns[ColIndex].name;
    var Ascending = RetSortOrder[ColIndex] == null ? true : !RetSortOrder[ColIndex];
    ReturnFlightListCV = LBReturnList.collectionView;
    if (ReturnFlightListCV != null) {
        var sd = ReturnFlightListCV.sortDescriptions;
        var sdNew = new wijmo.collections.SortDescription(ColumnName, Ascending); // remove any old sort descriptors and add the new one 
        sd.splice(0, sd.length, sdNew);
        RetSortOrder = ResetSortColList('Ret', RetSortOrder, ColIndex, Ascending);
        var selitem = ReturnFlightListCV.currentItem;
        if (selitem != null)
            document.getElementById('RetFlight' + selitem.Id).checked = true;
    }
};

//Reset controls according to Trip Type
function CallTripTypeClicked() {
    if (TripTypeSingle.checked) {
        document.getElementById("DivReturnDate").style.display = "none";
        document.getElementById("DivRetDateMsg").style.display = "block";
        document.getElementById("ImgFlightDir").src = HostPath + "images/SINGLE.PNG";
        TripType = 'SINGLE';
    }
    else {
        document.getElementById("DivReturnDate").style.display = "block";
        document.getElementById("DivRetDateMsg").style.display = "none";
        document.getElementById("ImgFlightDir").src = HostPath + "images/ROUND.PNG";
        TripType = 'ROUND';
    }
};

//selectedIndexChanged event handler of ACFromDest
function ACFromDest_SelectedIndexChanged(sender) {
    if (ACFromDest.selectedIndex >= 0 && ACFromDest.selectedValue != null && ACToDest.selectedIndex >= 0 && ACToDest.selectedValue != null && ACFromDest.selectedItem == ACToDest.selectedItem) {
        $('#LblMsg').attr('style', 'color:red;font-weight:bold;');
        $('#LblMsg').html('From and Destination place can\'t be same.');
        ACFromDest.selectedIndex = -1;
    }
};


//selectedIndexChanged event handler of ACToDest
function ACToDest_SelectedIndexChanged(sender) {
    if (ACFromDest.selectedIndex >= 0 && ACFromDest.selectedValue != null && ACToDest.selectedIndex >= 0 && ACToDest.selectedValue != null && ACFromDest.selectedItem == ACToDest.selectedItem) {
        $('#LblMsg').attr('style', 'color:red;font-weight:bold;');
        $('#LblMsg').html('From and Destination place can\'t be same.');
        ACToDest.selectedIndex = -1;
    }
};

//valueChanged event handler of IDDepartDate
function IDDepartDate_valueChanged(sender) {
    var DepartDate = IDDepartDate.value;
    IDReturnDate.min = DepartDate;
    if (IDReturnDate.value < DepartDate)
        IDReturnDate.value = DepartDate;
};

//valueChanged event handler of IDReturnDate
function IDReturnDate_valueChanged(sender) {

};

//valueChanged event handler of IDDepartDateFF
function IDDepartDateFF_valueChanged(sender) {
    var DepartDate = IDDepartDateFF.value;
    IDReturnDateFF.min = DepartDate;
    if (IDReturnDateFF.value < DepartDate)
        IDReturnDateFF.value = DepartDate;
    SetDepartFareFinder();
};

//valueChanged event handler of IDDepartDate
function IDReturnDateFF_valueChanged(sender) {
    SetReturnFareFinder();
};

//function to set Depart Fare Finder C1FlexChart-FCDepartFare
function SetDepartFareFinder() {
    if (DepartFlightList == null)
        return;
    var MinPrice = Math.min.apply(Math, DepartFlightList.map(function (val) { return val.Fare; }));
    var MaxPrice = Math.max.apply(Math, DepartFlightList.map(function (val) { return val.Fare; }));
    var SelDate = new Date(IDDepartDate.value);
    var FromDate = new Date(IDDepartDateFF.value), ToDate = new Date(IDDepartDateFF.value);
    FromDate.setDate(FromDate.getDate() - 3);
    ToDate.setDate(ToDate.getDate() + 3);

    var DepartFFChartData = [];
    var i = 0;
    var TempDate = new Date(FromDate);
    var FareAmt = MinPrice;
    while (TempDate <= ToDate) {
        if (TempDate.getDate() == SelDate.getDate() && TempDate.getMonth() == SelDate.getMonth() && TempDate.getFullYear() == SelDate.getFullYear())
            FareAmt = MinPrice;
        else
            FareAmt = FareAmt - Math.floor(Math.random() * 20 + 1) + Math.floor(Math.random() * 20 + 2) - Math.floor(Math.random() * 20 + 5);
        if (FareAmt > 0) {
            DepartFFChartData.push({ FareDate: new Date(TempDate), FareDateStr: TempDate.getDate() + '-' + (TempDate.getMonth() + 1 + '-' + TempDate.getFullYear()), Fare: FareAmt });
            TempDate.setDate(TempDate.getDate() + 1);
        }
    }
    if (DepartFFChartData != null && DepartFFChartData.length > 0) {
        FCDepartFare.itemsSource = new wijmo.collections.CollectionView(DepartFFChartData);//DepartFFChartData;
        FCDepartFare.selection = null;
    }
};

//function to set Return Fare Finder C1FlexChart-FCReturnFare
function SetReturnFareFinder() {
    if (ReturnFlightList == null)
        return;
    var MinPrice = Math.min.apply(Math, ReturnFlightList.map(function (val) { return val.Fare; }));
    var MaxPrice = Math.max.apply(Math, ReturnFlightList.map(function (val) { return val.Fare; }));
    var SelDate = new Date(IDReturnDate.value);
    var FromDate = new Date(IDReturnDateFF.value), ToDate = new Date(IDReturnDateFF.value);
    FromDate.setDate(FromDate.getDate() - 3);
    ToDate.setDate(ToDate.getDate() + 3);

    var ReturnFFChartData = [];
    var i = 0;
    var TempDate = new Date(FromDate);
    var FareAmt = MinPrice;
    while (TempDate <= ToDate) {
        if (TempDate.getDate() == SelDate.getDate() && TempDate.getMonth() == SelDate.getMonth() && TempDate.getFullYear() == SelDate.getFullYear())
            FareAmt = MinPrice;
        else
            FareAmt = FareAmt - Math.floor(Math.random() * 20 + 1) + Math.floor(Math.random() * 20 + 2) - Math.floor(Math.random() * 20 + 5);
        if (FareAmt > 0) {
            ReturnFFChartData.push({ FareDate: new Date(TempDate), FareDateStr: TempDate.getDate() + '-' + (TempDate.getMonth() + 1 + '-' + TempDate.getFullYear()), Fare: FareAmt });
            TempDate.setDate(TempDate.getDate() + 1);
        }
    }
    if (ReturnFFChartData != null && ReturnFFChartData.length > 0) {
        FCReturnFare.itemsSource = new wijmo.collections.CollectionView(ReturnFFChartData);
        FCReturnFare.selection = null;
    }
};

//function to refine search
function CallSearch() {
    var RetValue = false;
    LblModifyPnlMsg.innerHTML = null;
    LblModifyPnlMsg.style.color = "red";
    LblModifyPnlMsg.style.fontWeight = "bold";
    if (TripTypeSingle.checked == false && TripTypeRound.checked == false) {
        LblModifyPnlMsg.innerHTML = 'Select your Trip Type: One Way/Round Trip.';
        return RetValue;
    }
    if (ACFromDest.selectedIndex < 0) {
        LblModifyPnlMsg.innerHTML = 'Select Leaving From Airport.';
        $('#ACFromDest').focus();
        return RetValue;
    }
    if (ACToDest.selectedIndex < 0) {
        LblModifyPnlMsg.innerHTML = 'Select Going To Airport.';
        $('#ACToDest').focus();
        return RetValue;
    }
    if (IDReturnDate.value < IDDepartDate.value) {
        LblModifyPnlMsg.innerHTML = 'Return date can\'t be smaller than Departure Date. Select a valid Return date.';
        $('#IDReturnDate').focus();
        return RetValue;
    }
    if (CmbClass.selectedIndex < 0) {
        LblModifyPnlMsg.innerHTML = 'Select a Class.';
        $('#CmbClass').focus();
        return RetValue;
    }
    RetValue = true;
    var DepartDate = new Date(IDDepartDate.value);
    var ReturnDate = new Date(IDReturnDate.value);
    var DepartDateStr = DepartDate.getMonth() + 1 + "/" + DepartDate.getDate() + "/" + DepartDate.getFullYear();
    var ReturnDateStr = null;
    if (GetTripType() == 'ROUND')
        ReturnDateStr = ReturnDate.getMonth() + 1 + "/" + ReturnDate.getDate() + "/" + ReturnDate.getFullYear();
    var AirlinesList = ['All'];
    window.location.href = HostPath + 'Home/RefineSearch?TripType=' + GetTripType() + '&FromDest=' + ACFromDest.selectedItem.Dest + '&FromCity=' + ACFromDest.selectedItem.City + '&FromAirport=' + ACFromDest.selectedItem.AirportInfo + '&ToDest=' + ACToDest.selectedItem.Dest + '&ToCity=' + ACToDest.selectedItem.City + '&ToAirport=' + ACToDest.selectedItem.AirportInfo + '&DepartDateStr=' + DepartDateStr + '&ReturnDateStr=' + ReturnDateStr + '&Adult=' + (INAdult.selectedIndex + 1) + '&Child=' + INChild.selectedIndex + '&Infant=' + INInfant.selectedIndex + '&Class=' + CmbClass.selectedValue;
};

//funciton to get and show Search flights data
function GetFlightsList(TripType, FromDest, FromCity, FromAirport, ToDest, ToCity, ToAirport, DepartDateStr, ReturnDateStr, Adult, Child, Infant, Class, MaxPrice, MaxDepartTime, MaxReturnTime, AirlinesList) {
    //Reset Labels
    LblFromDest.innerHTML = FromDest;
    LblFromCity.innerHTML = FromCity;
    LblToDest.innerHTML = ToDest;
    LblToCity.innerHTML = ToCity;

    FCDepartFare.header = 'Fare(' + FromDest + '-' + ToDest + ')';
    FCReturnFare.header = 'Fare(' + ToDest + '-' + FromDest + ')';
    IDDepartDateFF.value = IDDepartDate.value;
    IDReturnDateFF.value = IDReturnDate.value;
    LblDepartDestFF.innerHTML = FromDest + '(' + FromCity + ')-' + ToDest + '(' + ToCity + ')';
    LblReturnDestFF.innerHTML = ToDest + '(' + ToCity + ')-' + FromDest + '(' + FromCity + ')';
    LblDepartDate.innerHTML = IDDepartDate.value.toString().substring(0, 15);
    if (TripType == 'ROUND') {
        LblReturnDate.innerHTML = IDReturnDate.value.toString().substring(0, 15);//ReturnDate.getMonth() + 1 + ' ' + ReturnDate.getDate() + ' ' + ReturnDate.getFullYear());//IDReturnDate.value.toString());//dateFormat(ReturnDate, 'MMM dd yyyy'));
        LnkFareFinder.style.display = "block";
    }
    else {
        LblReturnDate.innerHTML = 'N/A';
        LnkFareFinder.style.display = "none";
    }
    //Reset Filter Titles etc.
    LblDepartFromInFilter.innerHTML = FromCity;
    LblDepartToInFilter.innerHTML = ToCity;
    LblMinDurationInFilter.innerHTML = "0h 0m";
    LblMaxDurationInFilter.innerHTML = "0h 0m";
    LblSelMaxPriceInFilter.innerHTML = "0";
    LblMinPriceInFilter.innerHTML = "0";
    LblMaxPriceInFilter.innerHTML = "0";

    DepartFlightList = ReturnFlightList = null;
    PnlDepartGrid.style.display = "none";
    PnlReturnGrid.style.display = "none";
    //Search for Flights
    DepartFlightList = LBDepartList.collectionView.items;
        PnlDepartGrid.style.display = "block";
        DepartFlightListCV = LBDepartList.collectionView;
        if (TripType == 'ROUND') {
            ReturnFlightList = LBReturnList.collectionView.items;
            PnlReturnGrid.style.display = "block";
            ReturnFlightListCV = LBReturnList.collectionView;
            PnlDepartGrid.style.width = "50%";
            PnlReturnGrid.style.width = "50%";

            PnlToDepartTimesFilter.style.display = "block";
            PnlSelToFlight.style.display = "block";
            SetDepartFareFinder();
            SetReturnFareFinder();
        }
        else {
            PnlDepartGrid.style.width = "100%";
            PnlReturnGrid.style.width = "0%";
            PnlToDepartTimesFilter.style.display = "none";
            PnlSelToFlight.style.display = "none";
        }
            SetCheapestData();
            SetQuickestData();
            CheckAllFiltersForDepart();
            CheckAllFiltersForReturn();
            SetSelectedData();
};

//function to get selected trip type
function GetTripType(){
    if(TripTypeSingle.checked)
        return 'SINGLE';
    else
        return 'ROUND';
};

//function to submit selection of fare finder FlexChart
function CallSubmitFF() {
    LblFFPnlMsg.innerHTML = null;
    LblFFPnlMsg.style.color = "red";
    LblFFPnlMsg.style.fontWeight = "bold";
    if (FCDepartFare.selection == null || FCReturnFare.selection == null)
    {
        LblFFPnlMsg.innerHTML = 'Select a Depart and Return date to search flights.';
        return;
    }
    var DepartDate = new Date(FCDepartFare.selection.collectionView.currentItem.FareDate);
    var ReturnDate = new Date(FCReturnFare.selection.collectionView.currentItem.FareDate);
    if (ReturnDate<DepartDate) {
        LblFFPnlMsg.innerHTML = 'Select a valid return date. Return date can\'t be earliear than Departure date.';
        return;
    }
    var FFDepFare = parseFloat(FCDepartFare.selection.collectionView.currentItem.Fare), FFRetFare = parseFloat(FCReturnFare.selection.collectionView.currentItem.Fare);
    IDDepartDate.value = new Date(FCDepartFare.selection.collectionView.currentItem.FareDate);
    IDReturnDate.value = new Date(FCReturnFare.selection.collectionView.currentItem.FareDate);    
    var DepartDateStr = DepartDate.getMonth() + 1 + "/" + DepartDate.getDate() + "/" + DepartDate.getFullYear();
    var ReturnDateStr = null;
    if (GetTripType() == 'ROUND')
        ReturnDateStr = ReturnDate.getMonth() + 1 + "/" + ReturnDate.getDate() + "/" + ReturnDate.getFullYear();
    var AirlinesList = ['All'];
    window.location.href = HostPath + 'Home/RefineSearch?TripType=' + GetTripType() + '&FromDest=' + ACFromDest.selectedItem.Dest + '&FromCity=' + ACFromDest.selectedItem.City + '&FromAirport=' + ACFromDest.selectedItem.AirportInfo + '&ToDest=' + ACToDest.selectedItem.Dest + '&ToCity=' + ACToDest.selectedItem.City + '&ToAirport=' + ACToDest.selectedItem.AirportInfo + '&DepartDateStr=' + DepartDateStr + '&ReturnDateStr=' + ReturnDateStr + '&Adult=' + (INAdult.selectedIndex + 1) + '&Child=' + INChild.selectedIndex + '&Infant=' + INInfant.selectedIndex + '&Class=' + CmbClass.selectedValue + '&FFDepF=' + FFDepFare + '&FFRetF=' + FFRetFare;
};

//function to set quickest flights data of searched flights
function SetQuickestData() {
    if (DepartFlightList == null)
        return;
    MinDurationInMin = MaxDurationInMin = 0;
    var TempMinDurationStr = null, TempMaxDurationStr = null;    
    var TempDepartFlightList = DepartFlightList;
    var MinDepartDuration = Math.min.apply(Math, TempDepartFlightList.map(function (val) { return val.DurationMin; }));        
    var TotalFare = 0;
    var DepartelementPos = TempDepartFlightList.map(function (x) { return x.DurationMin; }).indexOf(MinDepartDuration);
    var QuickDepartFlight = TempDepartFlightList[DepartelementPos];

    MinDurationInMin = QuickDepartFlight.DurationMin;
    TempMinDurationStr = QuickDepartFlight.DurationStr;
    var MaxDepartDuration = Math.max.apply(Math, TempDepartFlightList.map(function (val) { return val.DurationMin; }));
    var TempMaxDepartFlight = TempDepartFlightList[TempDepartFlightList.map(function (x) { return x.DurationMin; }).indexOf(MaxDepartDuration)];
    MaxDurationInMin = TempMaxDepartFlight.DurationMin;
    TempMaxDurationStr = TempMaxDepartFlight.DurationStr;

    //Depart Flight Info
    QuickFromRecID.value = QuickDepartFlight.Id;
    ImgQuickFromFlight.src =HostPath + QuickDepartFlight.Airlines_Img;
    ImgQuickFromFlight.title = QuickDepartFlight.Airlines;
    QuickFromDest1.innerHTML = LblFromDest.innerHTML;
    QuickToDest1.innerHTML = LblToDest.innerHTML;
    var DepartTime = new Date(QuickDepartFlight.DepartTime);
    QuickFromDepartTime.innerHTML = (DepartTime.getHours() > 9 ? DepartTime.getHours() : '0' + DepartTime.getHours()) + ':' + (DepartTime.getMinutes() > 9 ? DepartTime.getMinutes() : '0' + DepartTime.getMinutes());
    var ArriveTime = new Date(QuickDepartFlight.ArriveTime);
    QuickFromArrivalTime.innerHTML = (ArriveTime.getHours() > 9 ? ArriveTime.getHours() : '0' + ArriveTime.getHours()) + ':' + (ArriveTime.getMinutes() > 9 ? ArriveTime.getMinutes() : '0' + ArriveTime.getMinutes());
    QuickFromFlightNo.innerHTML = QuickDepartFlight.Flight_No;
    QuickFromDuration.innerHTML = QuickDepartFlight.DurationStr;
    QuickFromStopInfo.innerHTML = 'Non Stop';
    TotalFare += QuickDepartFlight.Fare * (INAdult.selectedIndex + 1) + (QuickDepartFlight.Fare * 70 / 100) * INChild.selectedIndex + (QuickDepartFlight.Fare * 30 / 100) * INChild.selectedIndex;

    //Return Flight Info
    if (GetTripType() == 'ROUND') {
        document.getElementById('PnlQuickFare').className = 'FareBook_D';
        var TempReturnFlightList = ReturnFlightList;
        var MinReturnDuration = Math.min.apply(Math, TempReturnFlightList.map(function (val) { return val.DurationMin; }));
        var ReturnelementPos = TempReturnFlightList.map(function (x) { return x.DurationMin; }).indexOf(MinReturnDuration);
        var QuickReturnFlight = TempReturnFlightList[ReturnelementPos];

        var MaxReturnDuration = Math.max.apply(Math, TempReturnFlightList.map(function (val) { return val.DurationMin; }));
        var TempMaxReturnFlight = TempReturnFlightList[TempReturnFlightList.map(function (x) { return x.DurationMin; }).indexOf(MaxReturnDuration)];

        if (QuickReturnFlight.DurationMin < MinDurationInMin)
        {
            MinDurationInMin = QuickReturnFlight.DurationMin;
            TempMinDurationStr = QuickReturnFlight.DurationStr;
        }
        if (TempMaxReturnFlight.DurationMin > MaxDurationInMin) {
            MaxDurationInMin = TempMaxReturnFlight.DurationMin;
            TempMaxDurationStr = TempMaxReturnFlight.DurationStr;
        }

        QuickToRecID.value = QuickReturnFlight.Id;
        ImgQuickToFlight.src =HostPath + QuickReturnFlight.Airlines_Img;
        ImgQuickToFlight.title = QuickReturnFlight.Airlines;
        QuickFromDest2.innerHTML = LblFromDest.innerHTML;
        QuickToDest2.innerHTML = LblToDest.innerHTML;
        var DepartTime = new Date(QuickReturnFlight.DepartTime);
        QuickToDepartTime.innerHTML = (DepartTime.getHours() > 9 ? DepartTime.getHours() : '0' + DepartTime.getHours()) + ':' + (DepartTime.getMinutes() > 9 ? DepartTime.getMinutes() : '0' + DepartTime.getMinutes());
        var ArriveTime = new Date(QuickReturnFlight.ArriveTime);
        QuickToArrivalTime.innerHTML = (ArriveTime.getHours() > 9 ? ArriveTime.getHours() : '0' + ArriveTime.getHours()) + ':' + (ArriveTime.getMinutes() > 9 ? ArriveTime.getMinutes() : '0' + ArriveTime.getMinutes());
        QuickToFlightNo.innerHTML = QuickReturnFlight.Flight_No;
        QuickToDuration.innerHTML = QuickReturnFlight.DurationStr;
        QuickToStopInfo.innerHTML = 'Non Stop';
        PnlQuickToFlight.style.display = 'block';
        TotalFare += QuickDepartFlight.Fare * (INAdult.selectedIndex + 1) + (QuickDepartFlight.Fare * 70 / 100) * INChild.selectedIndex + (QuickDepartFlight.Fare * 30 / 100) * INChild.selectedIndex;
    }
    else {
        document.getElementById('PnlQuickFare').className = 'FareBook_S';
        QuickToRecID.value = '0';
        PnlQuickToFlight.style.display = 'none';
    }
    QuickTotalFare.innerHTML = TotalFare;
    LblMinDurationInFilter.innerHTML = TempMinDurationStr;
    LblMaxDurationInFilter.innerHTML = TempMaxDurationStr;
    SlFlightDuration.min = MinDurationInMin;
    SlFlightDuration.max = MaxDurationInMin;
    SlFlightDuration.step = (MaxDurationInMin - MinDurationInMin) / 10;
    SlFlightDuration.value = MaxDurationInMin;
};

//function to set cheapest flights data of searched flights
function SetCheapestData() {
    if (DepartFlightList == null)
        return;
    MinPrice = MaxPrice = 0;
    var MinDepartPrice = Math.min.apply(Math, DepartFlightList.map(function (val) { return val.Fare; }));
    var TotalFare = 0;
    var DepartelementPos = DepartFlightList.map(function (x) { return x.Fare; }).indexOf(MinDepartPrice);
    var CheapDepartFlight = DepartFlightList[DepartelementPos];

    var MaxDepartPrice = Math.max.apply(Math, DepartFlightList.map(function (val) { return val.Fare; }));
    var TempDepartFlightList = DepartFlightList;
    var TempMaxDepartFlight = TempDepartFlightList[TempDepartFlightList.map(function (x) { return x.Fare; }).indexOf(MaxDepartPrice)];
    MinPrice = MinDepartPrice;
    MaxPrice = TempMaxDepartFlight.Fare;

    //Depart Flight Info
    CheapFromRecID.value = CheapDepartFlight.Id;
    ImgCheapFromFlight.src = HostPath + CheapDepartFlight.Airlines_Img;
    ImgCheapFromFlight.title = CheapDepartFlight.Airlines;
    CheapFromDest1.innerHTML = LblFromDest.innerHTML;
    CheapToDest1.innerHTML = LblToDest.innerHTML;
    var DepartTime = new Date(CheapDepartFlight.DepartTime);
    CheapFromDepartTime.innerHTML = (DepartTime.getHours() > 9 ? DepartTime.getHours() : '0' + DepartTime.getHours()) + ':' + (DepartTime.getMinutes() > 9 ? DepartTime.getMinutes() : '0' + DepartTime.getMinutes());
    var ArriveTime = new Date(CheapDepartFlight.ArriveTime);
    CheapFromArrivalTime.innerHTML = (ArriveTime.getHours() > 9 ? ArriveTime.getHours() : '0' + ArriveTime.getHours()) + ':' + (ArriveTime.getMinutes() > 9 ? ArriveTime.getMinutes() : '0' + ArriveTime.getMinutes());
    CheapFromFlightNo.innerHTML = CheapDepartFlight.Flight_No;
    CheapFromDuration.innerHTML = CheapDepartFlight.DurationStr;
    CheapFromStopInfo.innerHTML = 'Non Stop';
    TotalFare += MinDepartPrice * (INAdult.selectedIndex + 1) + (MinDepartPrice * 70 / 100) * INChild.selectedIndex + (MinDepartPrice * 30 / 100) * INChild.selectedIndex;

    //Return Flight Info
    if (GetTripType() == 'ROUND') {
        document.getElementById('PnlCheapFare').className = 'FareBook_D';

        var MinReturnPrice = Math.min.apply(Math, ReturnFlightList.map(function (val) { return val.Fare; }));
        var ReturnelementPos = ReturnFlightList.map(function (x) { return x.Fare; }).indexOf(MinReturnPrice);
        var CheapReturnFlight = ReturnFlightList[ReturnelementPos];

        var MaxReturnPrice = Math.max.apply(Math, ReturnFlightList.map(function (val) { return val.Fare; }));
        var TempReturnFlightList = ReturnFlightList;
        var TempMaxReturnFlight = TempReturnFlightList[TempReturnFlightList.map(function (x) { return x.Fare; }).indexOf(MaxReturnPrice)];

        if (MinReturnPrice < MinPrice)
            MinPrice = MinReturnPrice;
        if (MaxReturnPrice > MaxPrice)
            MaxPrice = TempMaxReturnFlight.Fare;

        CheapToRecID.value = CheapReturnFlight.Id;
        ImgCheapToFlight.src = HostPath + CheapReturnFlight.Airlines_Img;
        ImgCheapToFlight.title = CheapReturnFlight.Airlines;
        CheapFromDest2.innerHTML = LblFromDest.innerHTML;
        CheapToDest2.innerHTML = LblToDest.innerHTML;
        var DepartTime = new Date(CheapReturnFlight.DepartTime);
        CheapToDepartTime.innerHTML = (DepartTime.getHours() > 9 ? DepartTime.getHours() : '0' + DepartTime.getHours()) + ':' + (DepartTime.getMinutes() > 9 ? DepartTime.getMinutes() : '0' + DepartTime.getMinutes());
        var ArriveTime = new Date(CheapReturnFlight.ArriveTime);
        CheapToArrivalTime.innerHTML = (ArriveTime.getHours() > 9 ? ArriveTime.getHours() : '0' + ArriveTime.getHours()) + ':' + (ArriveTime.getMinutes() > 9 ? ArriveTime.getMinutes() : '0' + ArriveTime.getMinutes());
        CheapToFlightNo.innerHTML = CheapReturnFlight.Flight_No;
        CheapToDuration.innerHTML = CheapReturnFlight.DurationStr;
        CheapToStopInfo.innerHTML = 'Non Stop';
        PnlCheapToFlight.style.display = 'block';
        TotalFare += MinReturnPrice * (INAdult.selectedIndex + 1) + (MinReturnPrice * 70 / 100) * INChild.selectedIndex + (MinReturnPrice * 30 / 100) * INChild.selectedIndex;
    }
    else {
        document.getElementById('PnlCheapFare').className = 'FareBook_S';
        CheapToRecID.innerHTML = '0';
        PnlCheapToFlight.style.display = 'none';
    }
    CheapTotalFare.innerHTML = TotalFare;
    LblMinPriceInFilter.innerHTML = MinPrice;
    LblMaxPriceInFilter.innerHTML = MaxPrice;
    LblSelMaxPriceInFilter.innerHTML = MaxPrice;
    SlPriceRange.min = MinPrice;
    SlPriceRange.max = MaxPrice;
    var TempStep = Math.round((MaxPrice - MinPrice) / 10);
    if (TempStep < ((MaxPrice - MinPrice) / 10))
        TempStep++;
    SlPriceRange.step = TempStep;
    SlPriceRange.value = MaxPrice;
};

//function to check and set filtered data for Depart Flights
function CheckDepartTimeFilter() {
    CheckAllFiltersForDepart();
    setTimeout(function () {
        SetDepartSelectedData();
    }, 2000);
};

//function to check and set filtered data for Return Flights
function CheckReturnTimeFilter() {
    CheckAllFiltersForReturn();
    setTimeout(function () {
        SetReturnSelectedData();
    }, 2000);
};

//function to set selected flights data
function SetSelectedData() {
    setTimeout(function () {
        SetDepartSelectedData();
        if (GetTripType() == 'ROUND') {
            document.getElementById('PnlSelFare').className = 'FareBook_D';
            SetReturnSelectedData();
        }
        else
            document.getElementById('PnlSelFare').className = 'FareBook_S';
    }, 2000);
};

//function to set selected depart flights data
function SetDepartSelectedData() {
    var DepartSelRadioBtnID = null;
    DepartFlightListCV = LBDepartList.collectionView;
    var TotalFare = 0;
    var DepartItem = null;
    if (DepartFlightListCV == null || DepartFlightListCV.items.length<=0) {
        return;
    }
    DepartItem = DepartFlightListCV.currentItem;
    if (DepartItem != null) {
        DepartSelRadioBtnID = 'DepFlight' + DepartItem.Id;
        if (document.getElementById(DepartSelRadioBtnID) != null)
            document.getElementById(DepartSelRadioBtnID).checked = true;
        //Depart Flight Info
        SelFromRecID.value = DepartItem.Id;
        ImgSelFromFlight.src =HostPath + DepartItem.Airlines_Img;
        ImgSelFromFlight.title = DepartItem.Airlines;
        SelFromDest1.innerHTML = LblFromDest.innerHTML;
        SelToDest1.innerHTML = LblToDest.innerHTML;
        var DepartTime = new Date(DepartItem.DepartTime);
        SelFromDepartTime.innerHTML = (DepartTime.getHours() > 9 ? DepartTime.getHours() : '0' + DepartTime.getHours()) + ':' + (DepartTime.getMinutes() > 9 ? DepartTime.getMinutes() : '0' + DepartTime.getMinutes());
        var ArriveTime = new Date(DepartItem.ArriveTime);
        SelFromArrivalTime.innerHTML = (ArriveTime.getHours() > 9 ? ArriveTime.getHours() : '0' + ArriveTime.getHours()) + ':' + (ArriveTime.getMinutes() > 9 ? ArriveTime.getMinutes() : '0' + ArriveTime.getMinutes());
        SelFromFlightNo.innerHTML = DepartItem.Flight_No;
        SelFromDuration.innerHTML = DepartItem.DurationStr;
        SelFromStopInfo.innerHTML = 'Non Stop';
        TotalFare += DepartItem.Fare * (INAdult.selectedIndex + 1) + (DepartItem.Fare * 70 / 100) * INChild.selectedIndex + (DepartItem.Fare * 30 / 100) * INChild.selectedIndex;

        if (GetTripType() == 'ROUND') {
            ReturnFlightListCV = LBReturnList.collectionView;
            var ReturnItem = null;
            ReturnItem = ReturnFlightListCV.currentItem;
            if (ReturnItem != null) {
                TotalFare += ReturnItem.Fare * (INAdult.selectedIndex + 1) + (ReturnItem.Fare * 70 / 100) * INChild.selectedIndex + (ReturnItem.Fare * 30 / 100) * INChild.selectedIndex;
            }
        }
        SelTotalFare.innerHTML = TotalFare;
    }
};

//function to set selected return flights data
function SetReturnSelectedData() {
    var TotalFare = 0;
    DepartFlightListCV = LBDepartList.collectionView;
    var DepartItem = null;
    if (DepartFlightListCV == null || DepartFlightListCV.items.length <= 0) {
        return;
    }
    DepartItem = DepartFlightListCV.currentItem;
    if (DepartItem != null)
        TotalFare += DepartItem.Fare * (INAdult.selectedIndex + 1) + (DepartItem.Fare * 70 / 100) * INChild.selectedIndex + (DepartItem.Fare * 30 / 100) * INChild.selectedIndex;

    ReturnFlightListCV = LBReturnList.collectionView;
    var ReturnItem = null;
    ReturnItem = ReturnFlightListCV.currentItem;
    if (ReturnItem != null) {
        ReturnSelRadioBtnID = 'RetFlight' + ReturnItem.Id;
        if (document.getElementById(ReturnSelRadioBtnID) != null)
            document.getElementById(ReturnSelRadioBtnID).checked = true;
        //Selected Return Flight Info
        SelToRecID.value = ReturnItem.Id;
        ImgSelToFlight.src = HostPath + ReturnItem.Airlines_Img;
        ImgSelToFlight.title = ReturnItem.Airlines;
        SelFromDest2.innerHTML = LblFromDest.innerHTML;
        SelToDest2.innerHTML = LblToDest.innerHTML;
        var DepartTime = new Date(ReturnItem.DepartTime);
        SelToDepartTime.innerHTML = (DepartTime.getHours() > 9 ? DepartTime.getHours() : '0' + DepartTime.getHours()) + ':' + (DepartTime.getMinutes() > 9 ? DepartTime.getMinutes() : '0' + DepartTime.getMinutes());
        var ArriveTime = new Date(ReturnItem.ArriveTime);
        SelToArrivalTime.innerHTML = (ArriveTime.getHours() > 9 ? ArriveTime.getHours() : '0' + ArriveTime.getHours()) + ':' + (ArriveTime.getMinutes() > 9 ? ArriveTime.getMinutes() : '0' + ArriveTime.getMinutes());
        SelToFlightNo.innerHTML = ReturnItem.Flight_No;
        SelToDuration.innerHTML = ReturnItem.DurationStr;
        SelToStopInfo.innerHTML = 'Non Stop';
        TotalFare += ReturnItem.Fare * (INAdult.selectedIndex + 1) + (ReturnItem.Fare * 70 / 100) * INChild.selectedIndex + (ReturnItem.Fare * 30 / 100) * INChild.selectedIndex;

        SelTotalFare.innerHTML = TotalFare;
    }
};

//function to handle selectionchanged event of C1FlexGrid of Depart Flights-FGDepartList
function FGDepartList_SelectionChanged(sender, args) {
    SetDepartSelectedData();
};

//function to handle selectionchanged event of C1FlexGrid of Return Flights-FGReturnList
function FGReturnList_SelectionChanged(sender, args) {
    SetReturnSelectedData();
};

function LBDepartList_SelectionChanged(sender) {
    SetDepartSelectedData();
};

//function to handle selectionchanged event of C1FlexGrid of Return Flights-FGReturnList
function LBReturnList_SelectionChanged(sender) {
    SetReturnSelectedData();
};

//function to apply filter of Flight Duration in Flights
function SlFlightDurationChanged() {
    CheckAllFilters();
};

//function to apply filter of Price in Flights
function SlPriceRangeChanged() {
    LblSelMaxPriceInFilter.innerHTML = SlPriceRange.value;
    CheckAllFilters();
};

//function to apply all filters and rebind Flights data
function CheckAllFilters() {
    CheckAllFiltersForDepart();
    CheckAllFiltersForReturn();
    SetSelectedData();
};

//function to apply all filters and rebind Depart Flights data
function CheckAllFiltersForDepart() {
    if (DepartFlightList == null)
        return;
    var SelDurationMin = SlFlightDuration.value;
    var SelPrice = SlPriceRange.value;
    var TempList = [];
    for (var i = 0; i < DepartFlightList.length; i++) {
        var DepartTime=new Date(DepartFlightList[i].DepartTime);
        var PassFilterCount = 0;
        var DepartTimeFilterCount = 0;
        if (document.getElementById('FromDepartTimeFilter1').checked || document.getElementById('FromDepartTimeFilter2').checked || document.getElementById('FromDepartTimeFilter3').checked || document.getElementById('FromDepartTimeFilter4').checked) {
            if (document.getElementById('FromDepartTimeFilter1').checked && DepartTime.getHours() <= 5)
                DepartTimeFilterCount++;
            if (document.getElementById('FromDepartTimeFilter2').checked && DepartTime.getHours() >= 6 && DepartTime.getHours() <= 11)
                DepartTimeFilterCount++;
            if (document.getElementById('FromDepartTimeFilter3').checked && DepartTime.getHours() >= 12 && DepartTime.getHours() <= 17)
                DepartTimeFilterCount++;
            if (document.getElementById('FromDepartTimeFilter4').checked && DepartTime.getHours() >= 18)
                DepartTimeFilterCount++;
            if (DepartTimeFilterCount > 0)
                PassFilterCount++;
        }
        else
            PassFilterCount++;

        //Filter for Flight Duration
        if (DepartFlightList[i].DurationMin <= SelDurationMin)
            PassFilterCount++;
        
        //Filter for Airlines
        $('#LBAirlines input[type=checkbox]').each(function () {
            if ($(this).is(":checked") == true && this.value == DepartFlightList[i].Airlines_Code)
                PassFilterCount++;
        });

        //Filter for Fare
        if (DepartFlightList[i].Fare <= SelPrice)
            PassFilterCount++;

        if (PassFilterCount >= 4)
            TempList.push(DepartFlightList[i]);
    }
    LBDepartList.itemsSource = new wijmo.collections.CollectionView(TempList);
};

//function to apply all filters and rebind Return Flights data
function CheckAllFiltersForReturn() {
    if (DepartFlightList == null)
        return;
    if (GetTripType() != 'ROUND')
        return;
    var SelDurationMin = SlFlightDuration.value;
    var SelPrice = SlPriceRange.value;
    var TempList = [];
    for (var i = 0; i < ReturnFlightList.length; i++) {
        var DepartTime = new Date(ReturnFlightList[i].DepartTime);
        var PassFilterCount = 0;
        //Filter for Departing time
        var DepartTimeFilterCount = 0;
        if (document.getElementById('ToDepartTimeFilter1').checked || document.getElementById('ToDepartTimeFilter2').checked || document.getElementById('ToDepartTimeFilter3').checked || document.getElementById('ToDepartTimeFilter4').checked) {
            if (document.getElementById('ToDepartTimeFilter1').checked && DepartTime.getHours() <= 5)
                DepartTimeFilterCount++;
            if (document.getElementById('ToDepartTimeFilter2').checked && DepartTime.getHours() >= 6 && DepartTime.getHours() <= 11)
                DepartTimeFilterCount++;
            if (document.getElementById('ToDepartTimeFilter3').checked && DepartTime.getHours() >= 12 && DepartTime.getHours() <= 17)
                DepartTimeFilterCount++;
            if (document.getElementById('ToDepartTimeFilter4').checked && DepartTime.getHours() >= 18)
                DepartTimeFilterCount++;
            if (DepartTimeFilterCount > 0)
                PassFilterCount++;
        }
        else
            PassFilterCount++;

        //Filter for Flight Duration
        if (ReturnFlightList[i].DurationMin <= SelDurationMin)
            PassFilterCount++;

        //Filter for Airlines
        $('#LBAirlines input[type=checkbox]').each(function () {
            if ($(this).is(":checked") == true && this.value == ReturnFlightList[i].Airlines_Code)
                PassFilterCount++;
        });

        //Filter for Fare
        if (ReturnFlightList[i].Fare <= SelPrice)
            PassFilterCount++;

        if (PassFilterCount >= 4)
            TempList.push(ReturnFlightList[i]);
    }
    LBReturnList.itemsSource = new wijmo.collections.CollectionView(TempList);
};

//function to proceed with selected flights
function CallSelBookNow() {
    CallBookNow(SelFromRecID.value, SelToRecID.value);
};

//function to proceed with cheapest flights
function CallCheapBookNow() {
    CallBookNow(CheapFromRecID.value, CheapToRecID.value);
};

//function to proceed with quickest flights
function CallQuickBookNow() {
    CallBookNow(QuickFromRecID.value, QuickToRecID.value);
};

//function to proceed for booking with flights
function CallBookNow(DepartId, ReturnID) {
    if (DepartId > 0) {
        window.location.href = HostPath + 'Home/BookFlight?Dep=' + DepartId + '&Ret=' + ReturnID;
    }
    else {
        LblPageMsg.className = 'NormalMsgClass';
        LblPageMsg.innerHTML = 'No Flight is selected to book. Please! First Search Flights, then Select flight to book.';
    }
};