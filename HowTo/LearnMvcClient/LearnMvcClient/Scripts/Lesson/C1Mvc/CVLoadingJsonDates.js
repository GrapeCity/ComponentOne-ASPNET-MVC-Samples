c1.documentReady(function () {
    // start with some data encoded as JSON 
    // (as it would arrive from a server)
    var json = '[' +
      '{"id":0,"country":"US","sales":489.51,"expenses":2358.24,"date":"2017-02-08T12:47:06.405Z"},' +
      '{"id":1,"country":"Germany","sales":7803.20,"expenses":2513.54,"date":"2017-02-09T12:47:06.405Z"},' +
      '{"id":2,"country":"UK","sales":9996.58,"expenses":2616.71,"date":"2017-02-10T12:47:06.405Z"},' +
      '{"id":3,"country":"Japan","sales":9351.68,"expenses":3030.59,"date":"2017-02-10T12:47:06.405Z"},' +
      '{"id":4,"country":"Spain","sales":349.51,"expenses":7358.24,"date":"/Date(1486561758556)/"}' +
      ']';

    // decode the data
    // no special parsing for dates, the date field will contain strings
    var dataBad = JSON.parse(json);

    // show the bad data in a grid
    var theGridBad = wijmo.Control.getControl('#theGridBad');
    theGridBad.itemsSource = dataBad;

    // decode the data
    // use with a Date reviver to restore date fields
    var dataGood = JSON.parse(json, function(key, value) {
        if (typeof value === 'string') {
      
            // parse dates saved as JSON-strings
            var m = value.match(/^(\d{4})-(\d{2})-(\d{2})T(\d{2}):(\d{2}):(\d{2}(?:\.\d*)?)Z$/);
            if (m) {
                return new Date(Date.UTC(+m[1], +m[2] - 1, +m[3], +m[4], +m[5], +m[6]));
            }

            // parse dates saved as OData-style strings
            m = value.match(/^\/Date\((\d+)\)\/$/);
            if (m) {
                return new Date(parseInt(m[1]));
            }
        }
        return value;  
    });

    // show the good data in a grid
    var theGridGood = wijmo.Control.getControl('#theGridGood');
    theGridGood.itemsSource = dataGood;
});