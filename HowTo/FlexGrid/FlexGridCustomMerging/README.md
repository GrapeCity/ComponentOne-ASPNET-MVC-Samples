## ASP.NET MVC Custom Merging
#### [Download as zip](https://downgit.github.io/#/home?url=https://github.com/GrapeCity/ComponentOne-ASPNET-MVC-Samples/tree/master/HowTo/FlexGrid/FlexGridCustomMerging)
____
#### Implement your own merging logic to create a TV-guide display.
____
The FlexGrid offers built-in cell merging based on cell content.

The built-in merging assumes you want to create merged ranges that
span either rows or columns, but not both.

This sample shows how you can overcome this limitation by implementing 
a custom version of the MergeManager class.

The sample creates a TV schedule and merges programs across weekdays
(columns) and show times (rows).
