c1.documentReady(function () {
    var theChart = wijmo.Control.getControl('#theChart');

    // create a CollectionView containing data grouped by 
    // country, product, color, and type
    var view = new wijmo.collections.CollectionView(getData(), {
        groupDescriptions: 'country,product,color,type'.split(',')
    });

    theChart.itemsSource = getGroupData(view); // show first-level grouping
    theChart.selectionChanged.addHandler(function (s, e) {
        if (s.selection) {
            var point = s.selection.collectionView.currentItem;
            if (point && point.group && !point.group.isBottomLevel) {
                showGroup(point.group);
            }
        }
    })

    

    // show a group on the chart
    function showGroup(group) {

        // update titles
        updateChartHeader(group);
        var level = 'level' in group ? group.level + 1 : 0;
        theChart.axisX.title = wijmo.toHeaderCase(view.groupDescriptions[level].propertyName);

        // update the series color (use a different one for each level)
        var palette = theChart.palette || wijmo.chart.Palettes.standard;
        theChart.series[0].style = {
            fill: palette[level],
            stroke: palette[level]
        };

        // update data
        theChart.itemsSource = getGroupData(group);
        theChart.selection = null;
    }

    // update the chart header element
    var header = document.getElementById('header');
    function updateChartHeader(group) {
        var item = group.items[0],
          path = '',
          headers = [];
        for (var i = 0; i <= group.level; i++) {
            var prop = view.groupDescriptions[i].propertyName,
              hdr = wijmo.format('<a href="#{path}">{prop}</a>: {value}', {
                  path: path,
                  prop: wijmo.toHeaderCase(prop),
                  value: item[prop]
              });
            headers.push(hdr);
            path += '/' + item[prop];
        }
        header.innerHTML = headers.length > 0
            ? 'Sales for ' + headers.join(', ')
          : 'Sales';
    }

    // handle clicks on chart's header element to navigate back up
    header.addEventListener('click', function (e) {
        if (e.target instanceof HTMLAnchorElement) {
            e.preventDefault();

            // get the link path
            var path = e.target.href;
            path = path.substr(path.lastIndexOf('#') + 1);
            path = path.split('/');

            // find the group that matches the path
            var src = view;
            for (var i = 1; i < path.length; i++) {
                for (var j = 0; j < src.groups.length; j++) {
                    var group = src.groups[j];
                    if (group.name == path[i]) {
                        src = group;
                        break;
                    }
                }
            }

            // show the selected group
            showGroup(src);
        }
    });

    // get the group data for a selected point
    function getGroupData(group) {

        // get items for this group, aggregate by sales
        var arr = [];
        group.groups.forEach(function (g) {
            arr.push({
                name: g.name,
                sales: g.getAggregate('Sum', 'sales'),
                group: g
            });
        });

        // return a new collection view sorted by sales
        return new wijmo.collections.CollectionView(arr, {
            sortDescriptions: [
              new wijmo.collections.SortDescription('sales', false)
            ]
        });
    }

    // create some random data
    function getData() {
        var countries = 'US,Canada,Germany,UK,France,Italy,Japan,Korea,China'.split(',');
        var products = 'Piano,Violin,Flute,Guitar,Cello'.split(',');
        var colors = 'Red,Green,Blue,Brown,White,Black'.split(',');
        var types = 'Hobbyist,Average,Professional,Collector'.split(',');
        var data = [];
        for (var i = 0; i < 1000; i++) {
            data.push({
                id: i,
                country: randomItem(countries),
                product: randomItem(products),
                color: randomItem(colors),
                type: randomItem(types),
                sales: Math.random() * 10000,
                expenses: Math.random() * 5000
            });
        }
        return data;
    }

    function randomItem(items) {
        return items[Math.floor(Math.random() * items.length)];
    }
});