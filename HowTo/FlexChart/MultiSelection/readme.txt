ASP.NET MVC FlexChart Multi-select
------------------------------------------------------------------------------
This sample shows how to support the selection of multiple plot elements in the FlexChart
by Ctrl+clicking on plot elements or by drawing a rectangle on the chart's
plot area.

Selection by drawing on a rectangle on the FlexChart works by handling various
mouse events on the FlexChart's host element and finding the plot elements within
the rectangle's bounds.  If the plot element is within the bounds of the drawn
rectangle, the 'wj-state-selected' CSS class is added to the plot element and
information about it is added to an internal array.

Selection by Ctrl+clicking on a plot element works by handling the click event
on the FlexChart's host element and calling the chart's hitTest method to find
the chart element that was clicked.  If the click is on an unselected plot element,
the code toggles the plot element's selected state by adding the 'wj-state-selected'
CSS class to the plot element and adds information about the it to an array.  If the
click is on a selected plot element, the item is deselected.  If the click is not
on a plot element, the array of selected items will be cleared completely.
