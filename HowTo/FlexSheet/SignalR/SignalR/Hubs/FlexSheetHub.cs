using C1.Web.Mvc.Sheet;
using Microsoft.AspNet.SignalR;
using SignalR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Script.Serialization;

namespace SignalR.Hubs
{
    public class FlexSheetHub : Hub
    {
        public void Update(string userId, object data)
        {
            var id = Context.ConnectionId;
            var success = true;

            try
            {
                //Update the static Workbook instace.
                UpdateWorkbook(data);
            }
            catch (Exception e)
            {
                success = false;
                data = e.ToString();
            }

            //Invoke caller's onUpdated method.
            Clients.Caller.onUpdated(success, data);

            //Update other clients.
            if (success)
            {
                Clients.AllExcept(id).update(id, userId, data);
            }
        }

        private void UpdateWorkbook(object data)
        {
            lock (this)
            {
                var serializer = new JavaScriptSerializer();
                var dic = (Dictionary<string, object>)serializer.DeserializeObject(data.ToString());
                var workbook = WorkbookOM.WORKBOOK;
                var type = (string)dic["type"];
                switch (type)
                {
                    case "cellValue":
                        UpdateCellValue(dic, workbook);
                        break;
                    case "cellsStyle":
                        UpdateCellsStyle(dic, workbook);
                        break;
                    case "addSheet":
                        AddEmptySheet(dic, workbook);
                        break;
                }
            }
        }

        public static void UpdateCellValue(Dictionary<string, object> data, Workbook workbook)
        {
            DateTime dt;
            var sheetIndex = (int)data["sheet"];
            var row = (int)data["row"];
            var col = (int)data["col"];
            var value = (object)data["value"];
            var isDate = (bool)data["isDate"];
            if (isDate)
            {
                DateTime.TryParse((string)value, out dt);
                workbook.Sheets[sheetIndex].Rows[row].Cells[col].Value = dt;
            }
            else
            {
                workbook.Sheets[sheetIndex].Rows[row].Cells[col].Value = value;
            }
        }

        public static void UpdateCellsStyle(Dictionary<string, object> data, Workbook workbook)
        {
            var sheetIndex = (int)data["sheet"];
            var row = (int)data["row"];
            var col = (int)data["col"];
            var row2 = (int)data["row2"];
            var col2 = (int)data["col2"];
            var formatType = (string)data["formatType"];
            var value = (string)data["value"];

            //Exchange them.
            if (col > col2)
            {
                col = col ^ col2;
                col2 = col ^ col2;
                col = col ^ col2;
            }

            if (row > row2)
            {
                row = row ^ row2;
                row2 = row ^ row2;
                row = row ^ row2;
            }

            for (var i = row; i <= row2; i++)
            {
                for (var j = col; j <= col2; j++)
                {
                    var cell = workbook.Sheets[sheetIndex].Rows[i].Cells[j];
                    cell.Style = cell.Style ?? new Style();
                    cell.Style.Font = cell.Style.Font ?? new Font();
                    switch (formatType)
                    {
                        case "fontWeight":
                            cell.Style.Font.Bold = value == "bold";
                            break;
                        case "fontStyle":
                            cell.Style.Font.Italic = value == "italic";
                            break;
                        case "textDecoration":
                            cell.Style.Font.Underline = value == "underline";
                            break;
                        case "textAlign":
                            cell.Style.HAlign = (HAlignType)Enum.Parse(typeof(HAlignType), value, true);
                            break;
                        case "fontFamily":
                            cell.Style.Font.Family = value;
                            break;
                        case "fontSize":
                            cell.Style.Font.Size = int.Parse(value.Substring(0, value.Length - 2));
                            break;
                        case "color":
                            cell.Style.Font.Color = value;
                            break;
                        case "backgroundColor":
                            cell.Style.Fill = cell.Style.Fill ?? new Fill();
                            cell.Style.Fill.Color = value;
                            break;
                        case "format":
                            cell.Style.Format = value;
                            break;
                    }
                }
            }
        }

        public static void AddEmptySheet(Dictionary<string, object> data, Workbook workbook)
        {
            var sheet = new Worksheet();
            const int COLS = 20;
            const int ROWS = 200;
            sheet.Name = (string)data["value"];
            for (var i = 0; i < ROWS; i++)
            {
                var row = new Row();
                for (var j = 0; j < COLS; j++)
                {
                    row.Cells.Add(new Cell());
                }
                sheet.Rows.Add(row);
            }
            workbook.Sheets.Add(sheet);
        }
    }
}