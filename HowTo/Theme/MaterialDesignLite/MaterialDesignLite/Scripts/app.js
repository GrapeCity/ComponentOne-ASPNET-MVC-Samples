$scope = {
    linkMaterial: null,
    linkWijmo: null,
    codeLinks: null,
    gauge1: null,
    gauge2: null,
    gauge3: null,
    gauge4: null,

    palette: null,
    wijmoUrl: "",
    materialUrl: "",
    codes: "",
    primary: "",
    accent: "",
    $apply: null,
    downloadCss: null,
    checkEven: null,
};

c1.documentReady(function () {
    $scope.linkMaterial = document.getElementById("linkMaterial");
    $scope.linkWijmo = document.getElementById("linkWijmo");
    $scope.codeLinks = document.getElementById("codeLinks");

    initColorTheme();
    initInputs();
    initGauges();
    initGrid();
});

function initColorTheme() {
    var colorWheel = new wijmo.material.ColorWheel('#colorWheel', {
        palette: $scope.palette,
        primary: 'Indigo',
        accent: 'Pink',
        themeChanged: function (s, e) {
            $scope.themeChanged(s, e);
        }
    });

    $scope.themeChanged(colorWheel);
}

function initInputs() {
    var evenNumber = wijmo.Control.getControl('#evenNumber');
    evenNumber.hostElement.wj_validation_error=function(){
        return $scope.checkEven(evenNumber.value);
    };
}

function initGauges() {
    $scope.gauge1 = wijmo.Control.getControl("#gauge1");
    $scope.gauge2 = wijmo.Control.getControl("#gauge2");
    $scope.gauge3 = wijmo.Control.getControl("#gauge3");
    $scope.gauge4 = wijmo.Control.getControl("#gauge4");

    var gaugeValueChanged = function (s, e) {
        $scope.gauge1.value = s.value;
        $scope.gauge2.value = s.value;
        $scope.gauge3.value = s.value;
        $scope.gauge4.value = s.value;
    }

    $scope.gauge1.valueChanged.addHandler(gaugeValueChanged);
    $scope.gauge2.valueChanged.addHandler(gaugeValueChanged);
    $scope.gauge3.valueChanged.addHandler(gaugeValueChanged);
}

function initGrid() {
    var grid = wijmo.Control.getControl('#theGrid');
    grid.itemsSource = $scope.palette;
}

// CSS url templates
var CSS_WIJMO = 'https://cdn.grapecity.com/wijmo/5.latest/styles/themes/material/wijmo.theme.material.{colors}.min.css';
var CSS_MDL = 'https://code.getmdl.io/1.1.1/material.{colors}.min.css';
var CODE_MDL = '&lt;link rel="stylesheet" href="https://code.getmdl.io/1.1.1/material.{colors}.min.css" /&gt;';
var CODE_WIJMO = '&lt;link rel="stylesheet" href="https://cdn.grapecity.com/wijmo/5.latest/styles/themes/material/wijmo.theme.material.{colors}.min.css" /&gt;';

// define the standard Material Design Lite color palette
$scope.palette = [
    { name: 'Cyan', outer: 'rgb(0, 188, 212)', inner: 'rgb(0, 151, 167)' },
    { name: 'Teal', outer: 'rgb(0, 150, 136)', inner: 'rgb(0, 121, 107)' },
    { name: 'Green', outer: 'rgb(76, 175, 80)', inner: 'rgb(56, 142, 60)' },
    { name: 'Light Green', outer: 'rgb(139, 195, 74)', inner: 'rgb(104, 159, 56)' },
    { name: 'Lime', outer: 'rgb(205, 220, 57)', inner: 'rgb(175, 180, 43)' },
    { name: 'Yellow', outer: 'rgb(255, 235, 59)', inner: 'rgb(251, 192, 45)' },
    { name: 'Amber', outer: 'rgb(255, 193, 7)', inner: 'rgb(255, 160, 0)' },
    { name: 'Orange', outer: 'rgb(255, 152, 0)', inner: 'rgb(245, 124, 0)' },
    { name: 'Brown', outer: 'rgb(121, 85, 72)', inner: 'rgb(93, 64, 55)', primary: true },
    { name: 'Blue Grey', outer: 'rgb(96, 125, 139)', inner: 'rgb(69, 90, 100)', primary: true },
    { name: 'Grey', outer: 'rgb(158, 158, 158)', inner: 'rgb(97, 97, 97)', primary: true },
    { name: 'Deep Orange', outer: 'rgb(255, 87, 34)', inner: 'rgb(230, 74, 25)' },
    { name: 'Red', outer: 'rgb(244, 67, 54)', inner: 'rgb(211, 47, 47)' },
    { name: 'Pink', outer: 'rgb(233, 30, 99)', inner: 'rgb(194, 24, 91)' },
    { name: 'Purple', outer: 'rgb(156, 39, 176)', inner: 'rgb(123, 31, 162)' },
    { name: 'Deep Purple', outer: 'rgb(103, 58, 183)', inner: 'rgb(81, 45, 168)' },
    { name: 'Indigo', outer: 'rgb(63, 81, 181)', inner: 'rgb(48, 63, 159)' },
    { name: 'Blue', outer: 'rgb(33, 150, 243)', inner: 'rgb(25, 118, 210)' },
    { name: 'Light Blue', outer: 'rgb(3, 169, 244)', inner: 'rgb(2, 136, 209)' }
];

// update URLs when the theme changes
$scope.themeChanged = function (s, e) {
    if (s.primary && s.accent) {
        var colors = s.primary.toLowerCase().replace(/ /g, '_') + '-' + s.accent.toLowerCase().replace(/ /g, '_');
        $scope.wijmoUrl = CSS_WIJMO.replace('{colors}', colors);
        $scope.materialUrl = CSS_MDL.replace('{colors}', colors);
        $scope.codes = CODE_MDL.replace('{colors}', colors) + '<br/>' + CODE_WIJMO.replace('{colors}', colors);
        $scope.primary = s.primary;
        $scope.accent = s.accent;
        $scope.$apply();
    }
}

$scope.$apply = function () {
    $scope.linkMaterial.href = $scope.materialUrl;
    $scope.linkWijmo.href = $scope.wijmoUrl;
    $scope.codeLinks.innerHTML = $scope.codes;
}

// download minified CSS for the current theme
$scope.downloadCss = function () {
    var a = document.createElement("a");
    a.target = 'blank';
    a.download = 'wijmo.theme.material.min.css';
    a.href = $scope.wijmoUrl;
    a.click();
}

// sample validation functions
$scope.checkEven = function (number) {
    return wijmo.isNumber(number) && number % 2 != 0
        ? 'not an even number...'
        : ''
}