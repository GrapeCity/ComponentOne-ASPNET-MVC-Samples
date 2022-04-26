c1.documentReady(function () {
    // generate some data
    var data = [
      { rank: 1, title: 'The Wizard of Oz', rating: 4, attendance: getArr(40), revenue: getArr(20) },
      { rank: 2, title: 'Citizen Kane', rating: 5, attendance: getArr(40), revenue: getArr(20) },
      { rank: 3, title: 'The Godfather', rating: 5, attendance: getArr(40), revenue: getArr(20) },
      { rank: 4, title: 'Metropolis', rating: 4, attendance: getArr(40), revenue: getArr(20) },
      { rank: 5, title: 'Singing\' in the Rain', rating: 2, attendance: getArr(40), revenue: getArr(20) },
      { rank: 6, title: 'Casablanca', rating: 3, attendance: getArr(40), revenue: getArr(20) },
      { rank: 7, title: 'Alien', rating: 5, attendance: getArr(40), revenue: getArr(20) },
      { rank: 8, title: 'Vertigo', rating: 2, attendance: getArr(40), revenue: getArr(20) },
      { rank: 9, title: 'Gone with the Wind', rating: 4, attendance: getArr(40), revenue: getArr(20) },
      { rank: 10, title: 'Chinatown', rating: 2, attendance: getArr(40), revenue: getArr(20) }
    ];
    function getArr(len) {
        var arr = [];
        for (var i = 0; i < len; i++) {
            arr.push(Math.round(Math.random() * 100));
        }
        return arr;
    }

    var theGrid = wijmo.Control.getControl('#theGrid');
    theGrid.itemsSource = data;
    theGrid.formatItem.addHandler(function (s, e) {
        if (e.panel == s.cells) {
            var item = s.rows[e.row].dataItem;
            switch (s.columns[e.col].binding) {
                case 'rating':
                    formatRatingCell(e.cell, item.rating);
                    break;
                case 'attendance':
                    formatAttendanceCell(e.cell, item.attendance);
                    break;
                case 'revenue':
                    formatRevenueCell(e.cell, item.revenue);
                    break;
            }
        }
    });

    function formatRatingCell(cell, rating) {
        var html = '<div aria-label="rating:' + rating + ' stars">';
        for (var i = 0; i < rating; i++) {
            html += '<span class="glyphicon glyphicon-star"></span>';
        }
        html += '</div>';
        cell.innerHTML = html;
    }
    function formatAttendanceCell(cell, data) {
        cell.innerHTML = getSparklines(data)
    }
    function formatRevenueCell(cell, data) {
        cell.innerHTML = getSparkbars(data)
    }


    // generate sparklines as SVG
    function getSparklines(data) {
        var svg = '',
    	min = Math.min.apply(Math, data),
      max = Math.max.apply(Math, data),
      x1 = 0,
      y1 = scaleY(data[0], min, max),
      x2, y2;
        for (var i = 1; i < data.length; i++) {
            x2 = Math.round((i) / (data.length - 1) * 100);
            y2 = scaleY(data[i], min, max);
            svg += '<line x1=' + x1 + '% y1=' + y1 + '% x2=' + x2 + '% y2=' + y2 + '% />';
            x1 = x2;
            y1 = y2;
        }
        return wrapSvg(svg, 'sparklines');
    }
    function getSparkbars(data) {
        var svg = '',
                min = Math.min.apply(Math, data),
          max = Math.max.apply(Math, data),
          base = Math.min(max, Math.max(min, 0)),
          basey = scaleY(base, min, max),
          w = Math.round(100 / data.length) - 2,
          x, y;
        for (var i = 0; i < data.length; i++) {
            x = i * Math.round(100 / data.length) + 1;
            y = scaleY(data[i], min, max);
            svg += '<rect x=' + x + '% width=' + w + '% y=' + Math.min(y, basey) + '% height=' + Math.abs(y - basey) + '% />';
        }
        svg += '<rect x=0% width=100% height=1 y=' + basey + '% opacity=.5 />';
        return wrapSvg(svg, 'sparkbars');
    }

    function scaleY(value, min, max) {
        return 100 - Math.round((value - min) / (max - min) * 100);
    }
    function wrapSvg(svg, title) {
        return '<div aria-label="' + title + '" ' +
            'style="width:100%;height:100%;box-sizing:border-box;padding:4px">' +
            '<svg width="100%" height="100%" style="stroke:currentColor;"><g>' +
                svg +
                '</g></svg></div>';
    }
});