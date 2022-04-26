String.prototype.replaceAll = function(s1, s2){
    return this.replace(new RegExp(s1, "gm"), s2);
}

$scope = {
    theBorderRaius: null,
    theHeaderBgColor: null,
    theHeaderColor: null,
    theContentBgColor: null,
    theContentColor: null,
    theSelectedBgColor: null,
    theSelectedColor: null,
    theTooltipBgColor: null,
    theTooltipColor: null,

    theStyle: null,
    theCss: null,

    borderRadius: 4,
    headerBgColor: "#eaeaea",
    headerColor: "#444444",
    contentBgColor: "#ffffff",
    contentColor: "#333333",
    selectedBgColor: "#0085c7",
    selectedColor: "#ffffff",
    tooltipBgColor: "#ffffe5",
    tooltipColor: "#333333",

    multiSelectedBgColor: "#80ADBF",
    altCellBgColor: "#f9f9f9",
    cellBorderColor: "#c6c6c6",

    apply: null,
};

var CSS_TEMPLATE = '.wj-content {\r\n' +
'    border-radius: {{borderRadius}}px;\r\n' +
'    background-color: {{contentBgColor}};\r\n' +
'    color: {{contentColor}};\r\n' +
'}\r\n' +
'.wj-header {\r\n' +
'    background-color: {{headerBgColor}};\r\n' +
'    color: {{headerColor}};\r\n' +
'}\r\n' +
'.wj-state-selected {\r\n' +
'    background-color: {{selectedBgColor}};\r\n' +
'    color: {{selectedColor}};\r\n' +
'}\r\n' +
'.wj-state-multi-selected {\r\n' +
'    background-color: {{multiSelectedBgColor}};\r\n' +
'    color: {{selectedColor}};\r\n' +
'}\r\n' +
'.wj-tooltip {\r\n' +
'    background-color: {{tooltipBgColor}};\r\n' +
'    color: {{tooltipColor}};\r\n' +
'}\r\n' +
'.wj-cell:not(.wj-header):not(.wj-group):not(.wj-alt):not(.wj-state-selected):not(.wj-state-multi-selected) {\r\n' +
'    background-color: {{contentBgColor}};\r\n' +
'    color: {{contentColor}};\r\n' +
'}\r\n' +
'.wj-alt:not(.wj-header):not(.wj-group):not(.wj-state-selected):not(.wj-state-multi-selected) {\r\n' +
'    background-color: {{altCellBgColor}};\r\n' +
'    color: {{contentColor}};\r\n' +
'}\r\n' +
'.wj-cell {\r\n' +
'    border-color: {{cellBorderColor}};\r\n' +
'}\r\n';

c1.documentReady(function () {
    initControls();
    $scope.apply();
});

function initControls() {
    $scope.theBorderRadius = wijmo.Control.getControl('#theBorderRadius');
    $scope.theBorderRadius.value = $scope.borderRadius;
    $scope.theBorderRadius.valueChanged.addHandler(function (s, e) {
        $scope.borderRadius = s.value;
        $scope.apply();
    });

    $scope.theHeaderBgColor = wijmo.Control.getControl('#theHeaderBgColor');
    $scope.theHeaderBgColor.value = $scope.headerBbColor;
    $scope.theHeaderBgColor.valueChanged.addHandler(function (s, e) {
        $scope.headerBgColor = s.value;
        $scope.apply();
    });

    $scope.theHeaderColor = wijmo.Control.getControl('#theHeaderColor');
    $scope.theHeaderColor.value = $scope.headerColor;
    $scope.theHeaderColor.valueChanged.addHandler(function (s, e) {
        $scope.headerColor = s.value;
        $scope.apply();
    });

    $scope.theContentBgColor = wijmo.Control.getControl('#theContentBgColor');
    $scope.theContentBgColor.value = $scope.contentBgColor;
    $scope.theContentBgColor.valueChanged.addHandler(function (s, e) {
        $scope.contentBgColor = s.value;
        $scope.altCellBgColor = darken(s.value, 0.025);
        $scope.cellBorderColor = darken(s.value, 0.2);
        $scope.apply();
    });

    $scope.theContentColor = wijmo.Control.getControl('#theContentColor');
    $scope.theContentColor.value = $scope.contentColor;
    $scope.theContentColor.valueChanged.addHandler(function (s, e) {
        $scope.contentColor = s.value;
        $scope.apply();
    });

    $scope.theSelectedBgColor = wijmo.Control.getControl('#theSelectedBgColor');
    $scope.theSelectedBgColor.value = $scope.selectedBgColor;
    $scope.theSelectedBgColor.valueChanged.addHandler(function (s, e) {
        $scope.selectedBgColor = s.value;
        $scope.multiSelectedBgColor = saturate(darken(s.value, 0.05), 0.3);
        $scope.apply();
    });

    $scope.theSelectedColor = wijmo.Control.getControl('#theSelectedColor');
    $scope.theSelectedColor.value = $scope.selectedColor;
    $scope.theSelectedColor.valueChanged.addHandler(function (s, e) {
        $scope.selectedColor = s.value;
        $scope.apply();
    });

    $scope.theTooltipBgColor = wijmo.Control.getControl('#theTooltipBgColor');
    $scope.theTooltipBgColor.value = $scope.tooltipBgColor;
    $scope.theTooltipBgColor.valueChanged.addHandler(function (s, e) {
        $scope.tooltipBgColor = s.value;
        $scope.apply();
    });

    $scope.theTooltipColor = wijmo.Control.getControl('#theTooltipColor');
    $scope.theTooltipColor.value = $scope.tooltipColor;
    $scope.theTooltipColor.valueChanged.addHandler(function (s, e) {
        $scope.tooltipColor = s.value;
        $scope.apply();
    });

    var theGrid = wijmo.Control.getControl('#theGrid');
    theGrid.itemsSource = getData();

    var tt = new wijmo.Tooltip();
    tt.setTooltip('#theTooltip', '#ttSample');

    $scope.theStyle = document.getElementById('theStyle');
    $scope.theCss = document.getElementById('theCss');
}

$scope.apply = function() {
    var css = CSS_TEMPLATE
        .replaceAll("{{borderRadius}}", $scope.borderRadius)
        .replaceAll("{{contentBgColor}}", $scope.contentBgColor)
        .replaceAll("{{contentColor}}", $scope.contentColor)
        .replaceAll("{{headerBgColor}}", $scope.headerBgColor)
        .replaceAll("{{headerColor}}", $scope.headerColor)
        .replaceAll("{{selectedBgColor}}", $scope.selectedBgColor)
        .replaceAll("{{selectedColor}}", $scope.selectedColor)
        .replaceAll("{{tooltipBgColor}}", $scope.tooltipBgColor)
        .replaceAll("{{tooltipColor}}", $scope.tooltipColor)
        .replaceAll("{{multiSelectedBgColor}}", $scope.multiSelectedBgColor)
        .replaceAll("{{altCellBgColor}}", $scope.altCellBgColor)
        .replaceAll("{{cellBorderColor}}", $scope.cellBorderColor);

    $scope.theCss.innerHTML = css;
    $scope.theStyle.innerHTML = css;
}

function lighten(color, amount) {
    var myColor = new wijmo.Color(color);
    var hsb = myColor.getHsb();
    hsb[2] = hsb[2] + amount <= 1 ? hsb[2] + amount : 1;
    myColor = wijmo.Color.fromHsb(hsb[0], hsb[1], hsb[2]);
    return myColor.toString();
}

function darken(color, amount) {
    var myColor = new wijmo.Color(color);
    var hsb = myColor.getHsb();
    hsb[2] = hsb[2] - amount <= 0 ? 0 : hsb[2] - amount;
    myColor = wijmo.Color.fromHsb(hsb[0], hsb[1], hsb[2]);
    return myColor.toString();
}

function saturate(color, saturation) {
    var myColor = new wijmo.Color(color);
    var hsb = myColor.getHsb();
    hsb[1] = saturation;
    myColor = wijmo.Color.fromHsb(hsb[0], hsb[1], hsb[2]);
    return myColor.toString();
}

function getData() {
    // create some random data
    var countries = 'US,Germany,UK,Japan,Italy,Greece'.split(','),
        data = [];
    for (var i = 0; i < countries.length; i++) {
        data.push({
            country: countries[i],
            downloads: Math.round(Math.random() * 20000),
            sales: Math.random() * 10000,
            expenses: Math.random() * 5000
        });
    }
    return data;
}