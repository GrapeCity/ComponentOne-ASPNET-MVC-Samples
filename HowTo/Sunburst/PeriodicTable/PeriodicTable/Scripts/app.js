var mySunburst = null, myPropTile = null, group_info = null, subGroup_info = null, element_info = null;

c1.documentReady(function () {
    initHamburgerNav();
    InitialControls();
});

function initHamburgerNav() {
    var hamburgerNavBtn = document.querySelector('.hamburger-nav-btn');
    var hamburgerNavEle = document.querySelector('.narrow-screen.site-nav');
    new c1.sample.MultiLevelMenu(hamburgerNavEle, hamburgerNavBtn);
}

function InitialControls() {
    mySunburst = wijmo.Control.getControl("#sbPeriodicTbl");
    myPropTile = document.getElementById('properties-tile');
    group_info = document.getElementById('group-info');
    subGroup_info = document.getElementById('subGroup-info');
    element_info = document.getElementById('element-info');
    var chartDiv = document.getElementById("sbPeriodicTbl");
    var centerDiv = document.getElementById('divEleInfo');
    chartDiv.appendChild(centerDiv);
    centerDiv.style.visibility = "Visible";
    window.addEventListener('resize', function (e) {
        setCoordinates(centerDiv, mySunburst);
    });
    // Set up a function to listen for click events on the Sunburst Chart's parent DOM element
    mySunburst.hostElement.addEventListener('click', function (e) {
        try {
            var ht = mySunburst.hitTest(e.pageX, e.pageY);
            var GrpDiv = document.getElementById("divGrp");
            var SubGrpDiv = document.getElementById("divSubGrp");
            var EleDiv = document.getElementById("divEle");
            var chartDiv = wijmo.Control.getControl("#sbPeriodicTbl");
            var myPropTile = document.getElementById('divEleInfo');
            setCoordinates(myPropTile, mySunburst);
            if (ht.item.hasOwnProperty("ElementName")) {
                setElementInfos(myPropTile, GrpDiv, SubGrpDiv, EleDiv, ht);
            }
            else if (ht.item.hasOwnProperty("SubGroupName")) {
                setSubGroupInfos(myPropTile, GrpDiv, SubGrpDiv, EleDiv, ht);
            }
            else if (ht.item.hasOwnProperty("GroupName")) {
                setGroupInfos(myPropTile, GrpDiv, SubGrpDiv, EleDiv, ht);
            }
            else { }
        }
        catch (e) {
        }
    });
}

function setCoordinates(myPropTile, mySunburst) {
    if (myPropTile == null)
        return;
    myPropTile.style.width = myPropTile.style.height = "250px";
    myPropTile.style.left = ((mySunburst.hostElement.offsetWidth + 34) - myPropTile.offsetWidth) / 2 + "px";
    myPropTile.style.top = ((mySunburst.hostElement.offsetHeight) - myPropTile.offsetHeight) / 2 + "px";
}

function setSubGroupInfos(myPropTile, GrpDiv, SubGrpDiv, EleDiv, ht) {
    GrpDiv.style.visibility = "collapse";
    EleDiv.style.visibility = "collapse";
    SubGrpDiv.style.visibility = "Visible";
    var divSubGrpname = document.getElementById('divSubGrpName');
    divSubGrpname.innerHTML = ht.item.SubGroupName;
    var divNoOfEle = document.getElementById('divNoOfEle');
    divNoOfEle.innerHTML = "<b>No. of Elements: </b>" + ht.item.Elements.length;
    var divCharacterstics = document.getElementById('divCharacterstics');
    divCharacterstics.innerHTML = "";
    var characterstics = ht.item.Characteristics.split(',');
    for (var i = 0; i < characterstics.length; i++) {
        divCharacterstics.innerHTML += characterstics[i] + "<br/>";
    }
}

function setElementInfos(myPropTile, GrpDiv, SubGrpDiv, EleDiv, ht) {
    GrpDiv.style.visibility = "collapse";
    SubGrpDiv.style.visibility = "collapse";
    EleDiv.style.visibility = "visible";
    var EleSymbol = document.getElementById('divEleSymbol');
    EleSymbol.innerHTML = ht.item.Symbol;
    var divEle = document.getElementById('divEleName');
    divEle.innerHTML = ht.item.ElementName;
    var divAtomNo = document.getElementById('divAtomicNo');
    divAtomNo.innerHTML = "AtomicNumber: " + ht.item.AtomicNumber;
    var divAtomWeight = document.getElementById('divAtomicWeight');
    divAtomWeight.innerHTML = "AtomicWeight: " + ht.item.AtomicWeight;
}

function setGroupInfos(myPropTile, GrpDiv, SubGrpDiv, EleDiv, ht) {
    SubGrpDiv.style.visibility = "collapse";
    EleDiv.style.visibility = "collapse";
    GrpDiv.style.visibility = "visible";
    var divGrpname = document.getElementById('divGrpName');
    divGrpname.innerHTML = ht.item.GroupName;
    var divSubGrps = document.getElementById('divSubGrps');
    divSubGrps.innerHTML = "";
    var subGroups = ht.item.SubGroups;
    for (var i = 0; i < subGroups.length; i++) {
        divSubGrps.innerHTML += subGroups[i].SubGroupName + " (" + subGroups[i].Count + ")<br/>";
    }
}
