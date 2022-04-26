using C1.Web.Mvc.Fluent;
using C1.Web.Mvc.Grid;
using C1.Web.Mvc.MultiRow;
using C1.Web.Mvc.MultiRow.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MultiRowExplorer.Models
{
    public class LayoutDefinitionsBuilders
    {
        public static Action<ListItemFactory<CellGroup, CellGroupBuilder>> OneLine
        {
            get
            {
                return ld =>
                {
                    ld.Add().Colspan(15).Cells(cells =>
                    {
                        cells.Add(cell => cell.Binding("Id").Header("ID").CssClass("id"))
                            .Add(cell => cell.Binding("Date").Header("Ordered"))
                            .Add(cell => cell.Binding("ShippedDate").Header("Shipped"))
                            .Add(cell => cell.Binding("Amount").Header("Amount").Format("c").CssClass("amount"))
                            .Add(cell => cell.Binding("Customer.Name").Name("CustomerName").Header("Customer"))
                            .Add(cell => cell.Binding("Customer.Address").Name("CustomerAddress").Header("Address"))
                            .Add(cell => cell.Binding("Customer.City").Name("CustomerCity").Header("City")
                                .DataMap(dm => { dm.DisplayMemberPath("Value").SelectedValuePath("Value").Bind(Orders.GetCities().ToValues()); }))
                            .Add(cell => cell.Binding("Customer.State").Name("CustomerState").Header("State"))
                            .Add(cell => cell.Binding("Customer.Zip").Name("CustomerZip").Header("Zip"))
                            .Add(cell => cell.Binding("Customer.Email").Name("CustomerEmail").Header("Customer Email").CssClass("email"))
                            .Add(cell => cell.Binding("Customer.Phone").Name("Customerphone").Header("Customer Phone"))
                            .Add(cell => cell.Binding("Shipper.Name").Name("ShipperName").Header("Shipper"))
                            .Add(cell => cell.Binding("Shipper.Email").Name("ShipperEmail").Header("Shipper Email").CssClass("email"))
                            .Add(cell => cell.Binding("Shipper.Phone").Name("ShipperPhone").Header("Shipper Phone"))
                            .Add(cell => cell.Binding("Shipper.Express").Name("ShipperExpress").Header("Express"));
                    });
                };
            }
        }

        public static Action<ListItemFactory<CellGroup, CellGroupBuilder>> TwoLines
        {
            get
            {
                return ld =>
                {
                    ld.Add().Header("Order").Colspan(2).Cells(cells =>
                    {
                        cells.Add(cell => cell.Binding("Id").Header("ID").CssClass("id").Width("150"))
                            .Add(cell => cell.Binding("Date").Header("Ordered").Width("150"))
                            .Add(cell => cell.Binding("Amount").Header("Amount").Format("c").CssClass("amount"))
                            .Add(cell => cell.Binding("ShippedDate").Header("Shipped"));
                    });
                    ld.Add().Header("Customer").Colspan(3).Cells(cells =>
                    {
                    cells.Add(cell => cell.Binding("Customer.Name").Name("CustomerName").Header("Customer").Width("250"))
                        .Add(cell => cell.Binding("Customer.Email").Name("CustomerEmail").Header("Customer Email").Colspan(2).CssClass("email"))
                        .Add(cell => cell.Binding("Customer.Address").Name("CustomerAddress").Header("Address"))
                        .Add(cell => cell.Binding("Customer.City").Name("CustomerCity").Header("City").DataMapEditor(DataMapEditor.DropDownList).Width("300")
                                .DataMap(dm => { dm.DisplayMemberPath("Value").SelectedValuePath("Value").Bind(Orders.GetCities().ToValues()); }))
                        .Add(cell => cell.Binding("Customer.State").Name("CustomerState").Header("State").Width("100"));
                    });
                    ld.Add().Header("Shipper").Colspan(2).Cells(cells =>
                    {
                        cells.Add(cell => cell.Binding("Shipper.Name").Name("ShipperName").Header("Shipper").Colspan(2))
                            .Add(cell => cell.Binding("Shipper.Email").Name("ShipperEmail").Header("Shipper Email").CssClass("email").Width("300"))
                            .Add(cell => cell.Binding("Shipper.Express").Name("ShipperExpress").Header("Express").Width("150"));
                    });
                };
            }
        }

        public static Action<ListItemFactory<CellGroup, CellGroupBuilder>> ThreeLines
        {
            get
            {
                return ld =>
                {
                    ld.Add().Header("Order").Colspan(2).Cells(cells =>
                    {
                        cells.Add(cell => cell.Binding("Id").Header("ID").Colspan(2).CssClass("id"))
                            .Add(cell => cell.Binding("Amount").Header("Amount").Format("c").Colspan(2).CssClass("amount"))
                            .Add(cell => cell.Binding("Date").Header("Ordered"))
                            .Add(cell => cell.Binding("ShippedDate").Header("Shipped"));
                    });
                    ld.Add().Header("Customer").Colspan(3).Cells(cells =>
                    {
                        cells.Add(cell => cell.Binding("Customer.Name").Name("CustomerName").Header("Customer"))
                            .Add(cell => cell.Binding("Customer.Email").Name("CustomerEmail").Header("Customer Email").Colspan(2).CssClass("email"))
                            .Add(cell => cell.Binding("Customer.Address").Name("CustomerAddress").Header("Address").Colspan(2))
                            .Add(cell => cell.Binding("Customer.Phone").Name("CustomerPhone").Header("Phone"))
                            .Add(cell => cell.Binding("Customer.City").Name("CustomerCity").Header("City")
                                .DataMap(dm => { dm.DisplayMemberPath("Value").SelectedValuePath("Value").Bind(Orders.GetCities().ToValues()); }))
                            .Add(cell => cell.Binding("Customer.State").Name("CustomerState").Header("State"))
                            .Add(cell => cell.Binding("Customer.Zip").Name("CustomerZip").Header("Zip"));
                    });
                    ld.Add().Header("Shipper").Cells(cells =>
                    {
                        cells.Add(cell => cell.Binding("Shipper.Name").Name("ShipperName").Header("Shipper").Width("*"))
                            .Add(cell => cell.Binding("Shipper.Email").Name("ShipperEmail").Header("Shipper Email").CssClass("email"))
                            .Add(cell => cell.Binding("Shipper.Express").Name("ShipperExpress").Header("Express"));
                    });
                };
            }
        }

        public static Action<ListItemFactory<CellGroup, CellGroupBuilder>> Sales
        {
            get
            {
                return ld =>
                {
                    ld.Add().Cells(cells =>
                    {
                        cells.Add(cell => cell.Binding("ID").Header("ID"));
                        cells.Add(cell => cell.Binding("Active").Header("Active"));
                    });
                    ld.Add().Cells(cells =>
                    {
                        cells.Add(cell => cell.Binding("Start").Header("Start").Format("MM/dd/yyyy"));
                        cells.Add(cell => cell.Binding("End").Header("End").Format("MM/dd/yyyy HH:mm:ss").Width("150"));
                    });
                    ld.Add().Colspan(2).Cells(cells =>
                    {
                        cells.Add(cell => cell.Binding("Country").Header("Country").Colspan(2));
                        cells.Add(cell => cell.Binding("Product").Header("Product"));
                        cells.Add(cell => cell.Binding("Color").Header("Color"));
                    });
                    ld.Add().Colspan(2).Cells(cells =>
                    {
                        cells.Add(cell => cell.Binding("Amount").Header("Amount"));
                        cells.Add(cell => cell.Binding("Amount2").Header("Amount2"));
                        cells.Add(cell => cell.Binding("Discount").Header("Discount").Colspan(2));
                    });
                };
            }
        }
    }
}