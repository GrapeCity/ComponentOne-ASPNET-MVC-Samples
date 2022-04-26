c1.documentReady(function () {
    // customize the FlexGridFilter conditions
    var filter = wijmo.culture.FlexGridFilter,
  		Operator = wijmo.grid.filter.Operator;
    filter.stringOperators = [
  	{ name: '(doesn\'t matter)', op: null },
    { name: 'Is', op: Operator.EQ },
    { name: 'Is not', op: Operator.NE },
    { name: 'Is bigger than', op: Operator.GT }, // added
    { name: 'Is smaller than', op: Operator.LT }, // added
    //{ name: 'Begins with', op: Operator.BW },
    //{ name: 'Ends with', op: Operator.EW },
    //{ name: 'Has', op: Operator.CT },
    //{ name: 'Hasn\'t', op: Operator.NC }
    ];
    filter.numberOperators = [
  	{ name: '(doesn\'t matter)', op: null },
    { name: 'Is', op: Operator.EQ },
    { name: 'Is not', op: Operator.NE },
    { name: 'Is bigger than', op: Operator.GT },
    //{ name: 'Is Greater than or equal to', op: Operator.GE },
    { name: 'Is smaller than', op: Operator.LT },
    //{ name: 'Is Less than or equal to', op: Operator.LE }
    ];
    filter.dateOperators = [
      { name: '(doesn\'t matter)', op: null },
      { name: 'Is', op: Operator.EQ },
      { name: 'Is earlier than', op: Operator.LT },
      { name: 'Is later than', op: Operator.GT }
    ];
    filter.booleanOperators = [
      { name: '(not set)', op: null },
      { name: 'Is', op: Operator.EQ },
      { name: 'Is not', op: Operator.NE },
    ];

    var theGrid = wijmo.Control.getControl('#theGrid');

});