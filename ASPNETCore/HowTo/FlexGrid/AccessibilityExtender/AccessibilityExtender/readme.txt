ASP.NET MVC Core AccessibilityExtender
---------------------------------------------------------------------
Implements a class that provides additional accessibility support to FlexGrid controls.

The FlexGrid has extensive built-in accessibility support. But there
are situations where you may want to extend that and provide additional
support that is specific to your application or user base.

The AccessibilityExtender shows how you can easily extend accessibility support
in two ways:

1) By allowing users to resize columns using the keyboard (alt+left/right keys).
This is done by handling the keydown event and changing the width property
on selected columns.

2) By providing an "alert" method that changes the content of an invisible
element with role "alert". In this sample, the alert method is used to 
announce grid actions such as sorting and filtering.

For net5.0 and above, when do deploying, should change the Deployment mode from Framework dependent value to Self-Contained for success hosting.