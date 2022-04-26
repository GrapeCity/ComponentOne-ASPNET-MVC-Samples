ASP.NET Core MVC Financial
---------------------------------------------
This sample shows a simulated live data feed of stock quotes for the FTSE 100 companies.

The sample simulates batches of transactions at a given interval and updates
the information on a grid changing only the cells that have changed.

By default, the grid updates all cells when data changes. In this example,
changes happen very frequently but only affect a few cells. So it is
more efficient to keep track of the cell elements and update only those
that have changed.