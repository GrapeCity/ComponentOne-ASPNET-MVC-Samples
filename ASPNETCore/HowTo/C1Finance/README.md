## ASP.NET Core MVC Finance
#### [Download as zip](https://downgit.github.io/#/home?url=https://github.com/GrapeCity/ComponentOne-ASPNET-MVC-Samples/tree/master/ASPNETCore/HowTo/C1Finance)
____
#### A stock portfolio application written using the MVC pattern.
____
The application allows users to select stocks and build portfolios, optionally including 
purchase quantity and price paid for each stock.

The application retrieves financial data from Quandl and calculates the current 
value and change for each stock. It also creates charts comparing the evolution in stock
value for each company.

The portfolios built are stored locally using static class.

The main ASP.NET MVC controls used in this application are:

FlexChart: Used to show the evolution of the stock values over time. The user may select the
stocks that should be displayed and the period (from 6 months to all data since 2008).

AutoComplete: Used to search for companies to add to the portfolio. The control provides
as-you-type searching with query highlighting. The AutoComplete control is better than
a simple ComboBox in this case because the number of items is large.

In addition to controls, this application uses a CollectionView to hold the portfolio
items at client side. Using a CollectionView provides a standard way to handle sorting and currency.
We set 'AllowSorting' property of FlexGrid to true, that allows users to sort the 
portfolio items by name, symbol, value, etc.
We also updates itemsSource of FlexChart at client side in this application, which allows users to 
move to required views of FlexChart.
