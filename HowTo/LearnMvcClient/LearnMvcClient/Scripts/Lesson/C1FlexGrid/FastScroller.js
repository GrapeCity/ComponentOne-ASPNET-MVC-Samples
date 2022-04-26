c1.documentReady(function () {
    // create the data
    data = [];
    var first = 'Adeline,Alexander,Alice,Amelia,Andrew,Arabella,Aria,Aryan,Asher,Astrid,Audrey,Aurelia,Aurora,Austin,Ava,Axel,Benjamin,Bodhi,Caleb,Callum,Caroline,Charlotte,Chloe,Claire,Cora,Daniel,Declan,Eleanor,Eli,Elijah,Elise,Elizabeth,Ella,Eloise,Emily,Emma,Esme,Ethan,Evangeline,Ezra,Felix,Finn,Genevieve,Grace,Hazel,Henry,Imogen,Isaac,Isabella,Isla,Ivy,Jack,Jacob,James,Jane,Jasper,John,Julian,Khaleesi,Leo,Levi,Liam,Lucas,Lucy,Luke,Luna,Lydia,Maeve,Matthew,Maxwell,Maya,Mia,Mila,Miles,Milo,Nathaniel,Nora,Oliver,Olivia,Ophelia,Oscar,Penelope,Roman,Rose,Sadie,Samuel,Scarlett,Sebastian,Silas,Soren,Stella,Thea,Theodore,Thomas,Violet,William,Wyatt,Xavier,Zachary'.split(',');
    var last = 'Adams,Alexander,Allen,Anderson,Bailey,Baker,Barnes,Bell,Bennett,Black,Boyd,Brooks,Brown,Bryant,Burns,Butler,Campbell,Carter,Clark,Cole,Coleman,Collins,Cook,Cooper,Cox,Crawford,Cruz,Daniels,Davis,Diaz,Dixon,Edwards,Ellis,Evans,Fisher,Flores,Ford,Foster,Freeman,Garcia,Gibson,Gomez,Gonzales,Gonzalez,Gordon,Graham,Gray,Green,Griffin,Hall,Hamilton,Harris,Harrison,Hayes,Henderson,Henry,Hernandez,Hicks,Hill,Holmes,Howard,Hughes,Hunt,Hunter,Iachetta,Jackson,James,Jenkins,Johnson,Jones,Jordan,Kelly,Kennedy,King,Lee,Lewis,Long,Lopez,Marshall,Martin,Martinez,Mason,Mcdonald,Miller,Mitchell,Moore,Morales,Morgan,Morris,Murphy,Murray,Myers,Nelson,Ortiz,Owens,Parker,Patterson,Perez,Perry,Peterson,Phillips,Porter,Powell,Price,Quaid,Ramirez,Ramos,Reed,Reyes,Reynolds,Rice,Richardson,Rivera,Roberts,Robertson,Robinson,Rodriguez,Rogers,Ross,Russell,Sanchez,Sanders,Scott,Shaw,Simmons,Simpson,Smith,Stevens,Stewart,Sullivan,Taylor,Thomas,Thompson,Torres,Tucker,Turner,Udell,Valentine,Vaughan,Vickers,Walker,Wallace,Ward,Warren,Washington,Watson,Webb,Wells,West,White,Williams,Wilson,Wood,Woods,Wright,Xanders,Xavier,Young,Zabinski,Zacharias'.split(',');
    for (var i = 0; i < 1000; i++) {
        data.push({
            first: first[Math.floor(Math.random() * first.length)],
            last: last[Math.floor(Math.random() * last.length)],
        });
    }

    // group and sort it with a CollectionView
    var view = new wijmo.collections.CollectionView(data, {
        sortDescriptions: ['last', 'first'],
        groupDescriptions: [ // group by last name's initial
            new wijmo.collections.PropertyGroupDescription('last',
                function (item, propName) {
                    return item[propName][0];
                })
        ]
    });
    view.moveCurrentToFirst();

    // show the data in a FlexGrid
    var theGrid = wijmo.Control.getControl('#theGrid');
    theGrid.itemsSource = view;
    theGrid.formatItem.addHandler(function (s, e) {
        if (e.panel == s.cells && s.columns[e.col].binding == 'last') {
            var row = s.rows[e.row];
            var item = s.rows[e.row].dataItem;
            e.cell.innerHTML = row instanceof wijmo.grid.GroupRow
                ? wijmo.format('&nbsp;&nbsp;<b>{name}</b>', item)
                : wijmo.format('{first} <b>{last}</b>', item);
        }
    });

    // create the fast scroller element
    var theScroller = document.createElement('div');
    theScroller.classList.add('fast-scroller');
    for (var i = 65; i <= 90; i++) {
        var initial = wijmo.createElement('<div>' + String.fromCharCode(i) + '</div>');
        theScroller.appendChild(initial);
    }
    theGrid.hostElement.firstElementChild.appendChild(theScroller);
    theScroller.addEventListener('mousedown', function (e) {

        // get initial we're looking for
        var initial = e.target.textContent;
        scrollTo(initial);
        e.preventDefault();
    });

    // allow scrolling by typing the initial as well
    theGrid.hostElement.addEventListener('keypress', function (e) {
        scrollTo(String.fromCharCode(e.keyCode));
    });

    // scroll the grid to show a given initial
    function scrollTo(initial) {
        if (initial.length == 1) {
            initial = initial.toUpperCase();
            for (var i = 0; i < theGrid.rows.length; i++) {
                var row = theGrid.rows[i];
                if (row instanceof wijmo.grid.GroupRow && row.dataItem.name == initial) {
                    var rc = theGrid.cells.getCellBoundingRect(i, 0, true);
                    var pt = theGrid.scrollPosition;

                    // scroll with animation
                    if (true) {
                        wijmo.animate(function (pct) {
                            var y = (pt.y * (1 - pct)) + (-rc.top * pct);
                            theGrid.scrollPosition = new wijmo.Point(pt.x, y);
                        }, 300);
                    } else { // or without
                        theGrid.scrollPosition = new wijmo.Point(pt.x, -rc.top);
                    }
                    break;
                }
            }
        }
    }
});