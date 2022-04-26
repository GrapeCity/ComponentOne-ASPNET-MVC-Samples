## ASP.NET MVC Excel Import and Export
#### [Download as zip](https://downgit.github.io/#/home?url=https://github.com/GrapeCity/ComponentOne-ASPNET-MVC-Samples/tree/master/HowTo/FlexGrid/ExcelImportExport)
____
#### This sample shows how to import and export Excel files with FlexGrid.
____
You can use the xlsx.js library to import and export FlexGrid to and from Excel 
files using only JavaScript (no server-side code). This sample uses a modified version 
of the xlsx.js library (https://github.com/SheetJS/js-xlsx) that provides basic Excel 
export and import capabilities. You can find the modified version of the library in the 
scripts\c1xlsx.js file. The library uses the jszip.js library (https://github.com/Stuk/jszip) 
to read and write the Excel files.

** Export

The ExcelConverter.export function, implemented in the Scripts\ExcelConverter\ExcelConverter.js file, 
gets the FlexGrid instance as an input, converts its data and formatting to Excel format 
using the xlsx library, and returns an object representing the Excel file content. 
The exportExcel function in the controller that you can find in Scripts\app.js 
takes this object and saves it to a file on a local disk.

** Import

The ExcelConverter.import function takes an Excel file's content as an input, parses it using 
the xlsx library, and fills the specified grid instance with the data retrieved. The 
importExcel function in the Scripts\app.js  file reads the selected file content from 
disk and passes it to the ExcelConverter.import function, along with the grid instance 
defined on the html page.

** Adding it to your application

In order to add Excel import or export support to your application, please perform the following steps:


* Add the c1xlsx.js and ExcelConverter.ts(js) files to your application.
* In the html page, add references to:
* the jszip.js library (http://cdnjs.cloudflare.com/ajax/libs/jszip/2.2.1/jszip.min.js )
* c1xlsx.js
* ExcelConverter.js
* Add the code from the exportExcel function that saves the export results to a local file.
* Add the code from the importExcel function that reads an Excel file from disk.
