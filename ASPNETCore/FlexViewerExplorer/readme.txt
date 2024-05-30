ASP.NET Core MVC FlexViewer Explorer
---------------------------------------------
This sample demonstrates how to use MVC's ReportViewer and PdfViewer controls.

Note for FlexReport using SQLite database:
Currently, SQLite has this issue: https://stackoverflow.com/questions/13028069/unable-to-load-dll-sqlite-interop-dll/60176344.
Then SQLite 1.0.113 x86 package is required for the sample.

For net5.0 and above, when do deploying, should change the Deployment mode from Framework dependent value to Self-Contained for success hosting.