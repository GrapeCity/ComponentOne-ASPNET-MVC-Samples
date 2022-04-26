ASP.NET MVC MaterialDesignLite
-------------------------------------------
Shows how to use ComponentOne MVC for ASP.NET with Google's Material Design Lite.

Material Design Lite (MDL) is Google's implementation of their Material Design 
specification for web developers. It is framework-agnostic and provides beautiful
layouts for modern applications. MDL is a good alternative to Bootstrap CSS,
which we use extensively in our MVC samples.

For more information about MDL, please see these links:

https://medium.com/google-developers/introducing-material-design-lite-3ce67098c031#.hdd60hunf

https://www.getmdl.io/

This sample implements a ColorWheel control similar to the one available in 
MDL's customization page. Users may click the wheel to select a primary and
an accent colors; these define a theme that is automatically downloaded
and applied to the page.

The application shows CDN links to the selected themes and allows users to
download the minified CSS. ComponentOne MVC includes 286 Material themes 
that represent all valid combinatios of primary and accent colors.

The sample also includes a wijmo.material.js file that provides a "bootstrap"
function. This function is called automatically when the document loads and
performs two tasks:

1) It monitors MDL tab elements and invalidates ComponentOne MVC controls 
when the user switches tabs. This allows ComponentOne MVC controls to update 
their layout when they become visible.

2) It adds event handlers for ComponentOne MVC input elements contained 
in MDL TextField elements to update their state attributes (focus, dirty, invalid). 
This allows you to use ComponentOne Mvc input elements in MDL TextFields 
in exactly the same way you would use native input elements.

