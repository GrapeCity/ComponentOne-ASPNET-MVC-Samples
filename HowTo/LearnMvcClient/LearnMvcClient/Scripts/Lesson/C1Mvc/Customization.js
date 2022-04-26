c1.documentReady(function () {
    var theGrid = wijmo.Control.getControl('#theGrid');

    // customize grid filter conditions
    var filter = wijmo.culture.FlexGridFilter;
    var Operator = wijmo.grid.filter.Operator;
    filter.stringOperators = [
  	{ name: '(not set)', op: null },
    { name: 'Same', op: Operator.EQ },
    { name: 'Different', op: Operator.NE },
    { name: 'Bigger', op: Operator.GT }, // added
    { name: 'Smaller', op: Operator.LT }, // added
    //{ name: 'Begins with', op: Operator.BW },
    //{ name: 'Ends with', op: Operator.EW },
    //{ name: 'Has', op: Operator.CT },
    //{ name: 'Hasn\'t', op: Operator.NC }
    ];
    filter.numberOperators = [
    { name: '(not set)', Operator: null },
    { name: 'Same', op: Operator.EQ },
    { name: 'Different', op: Operator.NE },
    { name: 'Bigger', op: Operator.GT },
    //{ name: 'Is Greater than or equal to', op: Operator.GE },
    { name: 'Smaller', op: Operator.LT },
    //{ name: 'Is Less than or equal to', op: Operator.LE }
    ];
    filter.dateOperators = [
    { name: '(not set)', op: null },
    { name: 'Same', op: Operator.EQ },
    { name: 'Earlier', op: Operator.LT },
    { name: 'Later', op: Operator.GT }
    ];
    filter.booleanOperators = [
    { name: '(not set)', op: null },
    { name: 'Is', op: Operator.EQ },
    { name: 'Isn\'t', op: Operator.NE }
    ];
});