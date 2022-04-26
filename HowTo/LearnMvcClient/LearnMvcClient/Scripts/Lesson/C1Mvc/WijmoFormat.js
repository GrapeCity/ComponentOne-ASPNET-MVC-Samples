c1.documentReady(function () {
    // first example
    document.getElementById('btnFormat1').addEventListener('click', function () {
        var msg = wijmo.format('Welcome {name}! You have {miles:n0} miles in your account.', {
            name: 'Joe',
            miles: 2332123
        });
        alert(msg);
    });

    // second example
    document.getElementById('btnFormat2').addEventListener('click', function () {
        var msg = wijmo.format('{name}, thanks for being a customer since {date:D}.', {
            name: 'Joe',
            date: new Date()
        });
        alert(msg);
    });
});