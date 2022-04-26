## ASP.NET MVC Core AccessibilityExtender
#### [Download as zip](https://downgit.github.io/#/home?url=https://github.com/GrapeCity/ComponentOne-ASPNET-MVC-Samples/tree/master/ASPNETCore/HowTo/FlexGrid/AccessibilityExtender/AccessibilityExtender)
____
#### Implements a class that provides additional accessibility support to FlexGrid controls.
____
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
