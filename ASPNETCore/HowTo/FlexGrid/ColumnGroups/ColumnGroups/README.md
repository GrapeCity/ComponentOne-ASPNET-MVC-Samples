## ASP.NET Core MVC Column Groups
#### [Download as zip](https://downgit.github.io/#/home?url=https://github.com/GrapeCity/ComponentOne-ASPNET-MVC-Samples/tree/master/ASPNETCore/HowTo/FlexGrid/ColumnGroups/ColumnGroups)
____
#### Shows how you can show structured data with multi-level, merged headers.
____
Most tables and grids include a single header row that shows the name of 
the field that the column refers to.

In many cases, the table data has a hierarchical nature, and it may be 
desirable to add several levels of column headers to expose the hierarchy.

This sample uses the "ColumnGroupProvider" class to create multi-level,
merged headers based on a JavaScript object that describes the column
hierarchy.

For example, you could create a table like this one:

    /-----------------------------------------\
    |          |      Average      |   Red    |
    |          |-------------------|  eyes    |
    |          |  height |  weight |          |
    |-----------------------------------------|
    |  Males   | 1.9     | 0.003   |   40%    |
    |-----------------------------------------|
    | Females  | 1.7     | 0.002   |   43%    |
    \-----------------------------------------/


Using this code:

  // define the data
  // http://www.w3.org/TR/html401/struct/tables.html
  var w3Data = [
    { gender: 'Males', height: 1.9, weight: 0.003, red: .4 },
    { gender: 'Females', height: 1.7, weight: 0.002, red: .43 },
  ];

  // define the columns
  var w3Columns = [
    { header: ' ', binding: 'gender' },
    {
      header: 'Average', columns: [
        { header: 'Height', binding: 'height', format: 'n1' },
        { header: 'Weight', binding: 'weight', format: 'n3' }
      ]
    },
    { header: 'Red Eyes', binding: 'red', format: 'p0' }
  ];

  // bind columns and data to a FlexGrid
  bindColumnGroups(flex, w3Columns);


Notice how the w3Data object describes the columns as an array where 
each element may correspond to an individual column or to a column
group.
