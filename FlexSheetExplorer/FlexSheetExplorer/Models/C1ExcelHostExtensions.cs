using C1.C1Excel;
using C1.Web.Mvc.Sheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlexSheetExplorer.Models
{
    public static class C1ExcelHostExtensions
    {
        private const int DEFAULT_DPI = 96;
        private const string CHARS_COUNT_SUFFIX = "ch";
        private const float PIXELS_TO_TWIPS_FACTOR = 1440F / DEFAULT_DPI;
        private const float PIXELS_TO_POINTS_FACTOR = 72F / DEFAULT_DPI;
        private const int CELL_PANDING = 6;

        public static C1XLBook ToC1XLBook(this Workbook workbook)
        {
            var c1Book = new C1XLBook();
            c1Book.Author = workbook.Creator;
            c1Book.Sheets.Clear();
            workbook.Sheets.ForEach(sheet => c1Book.AddXLSheet(sheet, workbook));
            if (workbook.ActiveWorksheet >= 0 && workbook.ActiveWorksheet < c1Book.Sheets.Count)
            {
                c1Book.Sheets.SelectedIndex = workbook.ActiveWorksheet;
            }
            return c1Book;
        }

        public static Workbook ToWorkbook(this C1XLBook c1Book)
        {
            var workbook = new Workbook();
            workbook.Creator = c1Book.Author;
            workbook.ActiveWorksheet = c1Book.Sheets.SelectedIndex;

            var c1StyleConverter = new CachedXLStyleConverter<XLStyle, Style>();

            c1Book.Sheets.Cast<XLSheet>()
                .Select(sheet => sheet.ToWorksheet(c1StyleConverter)).ToList()
                .ForEach(sheet => workbook.Sheets.Add(sheet));
            return workbook;
        }

        private static bool IsMergedCell(int row, int col, List<XLCellRange> mergedRanges)
        {
            foreach (var range in mergedRanges)
            {
                if (range.Contains(null, row, col))
                {
                    return true;
                }
            }
            return false;
        }

        internal static XLSheet AddXLSheet(this C1XLBook c1Book, Worksheet worksheet, Workbook workbook)
        {
            int nCols = worksheet.Columns.Count;
            var mergedRanges = new List<XLCellRange>();

            // Set sheet-wide properties
            var c1Sheet = c1Book.Sheets.Add();
            c1Sheet.Name = string.IsNullOrWhiteSpace(worksheet.Name)
                ? "Sheet" + (workbook.Sheets.IndexOf(worksheet) + 1)
                : worksheet.Name;
            c1Sheet.OutlinesBelow = worksheet.SummaryBelow;

            // Columns
            int j = 0;
            foreach (var col in worksheet.Columns)
            {
                if (col != null)
                {
                    var c1Col = c1Sheet.Columns[j];
                    c1Col.Visible = col.Visible;
                    c1Col.Style = col.Style.ToXLStyle(c1Book);
                    if (c1Col.Style != null)
                    {
                        c1Col.Width = (int)ColumnWidthToTwips(col.Width, c1Col.Style.Font);
                    }
                }
                j++;
            }

            // Fill the table
            int i = 0;
            foreach (var row in worksheet.Rows)
            {
                if (row != null)
                {
                    var c1Row = c1Sheet.Rows[i];
                    c1Row.Visible = row.Visible;
                    c1Row.Style = row.Style.ToXLStyle(c1Book);
                    c1Row.Collapsed = row.Collapsed;
                    c1Row.Height = row.Height != -1 ? (int)PixelsToTwips(row.Height) : -1;
                    c1Row.OutlineLevel = row.GroupLevel;

                    var cellCount = Math.Min(nCols, row.Cells.Count);
                    for (j = 0; j < cellCount; j++)
                    {
                        if (IsMergedCell(i, j, mergedRanges))
                        {
                            continue;
                        }
                        var cell = row.Cells[j];
                        if (cell == null)
                        {
                            continue;
                        }

                        if (cell.RowSpan > 1 || cell.ColSpan > 1)
                        {
                            mergedRanges.Add(new XLCellRange(i, cell.RowSpan - 1 + i, j, cell.ColSpan - 1 + j));
                        }

                        var c1Cell = c1Sheet[i, j];

                        DateTime datetime;
                        if (cell.IsDate && DateTime.TryParse(cell.Value as string, out datetime))
                        {
                            c1Cell.Value = datetime;
                        }
                        else
                        {
                            c1Cell.Value = cell.Value;
                        }

                        c1Cell.Formula = cell.Formula;
                        c1Cell.Style = cell.Style.ToXLStyle(c1Book);
                    }
                }
                i++;
            }

            // Merged cells
            mergedRanges.ForEach(r => c1Sheet.MergedCells.Add(r.RowFrom, r.ColumnFrom, r.RowCount, r.ColumnCount));

            // Frozen Columns & Frozen Rows
            if (worksheet.FrozenPane != null)
            {
                c1Sheet.Columns.Frozen = worksheet.FrozenPane.Columns;
                c1Sheet.Rows.Frozen = worksheet.FrozenPane.Rows;
            }
            return c1Sheet;
        }

        internal static Worksheet ToWorksheet(this XLSheet c1Sheet, CachedXLStyleConverter<XLStyle, Style> c1StyleConverter)
        {
            int nCols = c1Sheet.Columns.Count;

            // Set sheet-wide properties
            var worksheet = new Worksheet();
            worksheet.Name = c1Sheet.Name;
            worksheet.SummaryBelow = c1Sheet.OutlinesBelow;

            // Columns
            foreach (XLColumn c1Col in c1Sheet.Columns)
            {
                var col = new Column { Visible = c1Col.Visible };
                if (c1Col.Width != -1)
                {
                    col.Width = (TwipsToPixels(c1Col.Width) + CELL_PANDING).ToString();
                }
                col.Style = c1StyleConverter.ConvertToCommonStyle(c1Col.Style, s => s.ToStyle());
                worksheet.Columns.Add(col);
            }

            // Fill the table
            int i = 0;
            var mergedRanges = c1Sheet.MergedCells.Cast<XLCellRange>().ToList();
            foreach (XLRow c1Row in c1Sheet.Rows)
            {
                // "Visible" has different meaning in C1Excel and in wijmo.
                // In C1Excel, a collapsed data row's "Visible" is false, but the row is still there.
                // In wijmo, FlexGrid will not generate a "Visible=false" row.
                // The collapse state of a group is not imported in FlexSheet sample. All groups are expanded.
                var row = new Row { Visible = c1Row.OutlineLevel > 0 ? true : c1Row.Visible, GroupLevel = c1Row.OutlineLevel };
                if (c1Row.Height != -1)
                {
                    row.Height = (int)TwipsToPixels(c1Row.Height) + CELL_PANDING;
                }
                row.Style = c1StyleConverter.ConvertToCommonStyle(c1Row.Style, s => s.ToStyle());
                row.Collapsed = c1Row.Collapsed;

                // skip invalid cells.
                var isValidCell = false;
                for (int j = nCols - 1; j >= 0; j--)
                {
                    var c1Cell = c1Sheet[i, j];

                    if (isValidCell || c1Cell.Formula != null || c1Cell.Value != null || IsMergedCell(i, j, mergedRanges))
                    {
                        isValidCell = true;

                        var cellValue = c1Cell.Value;
                        if (cellValue is System.Exception)
                        {
                            cellValue = (cellValue as System.Exception).Message;
                        }

                        var cell = new Cell { Value = cellValue, Formula = c1Cell.Formula, IsDate = cellValue is DateTime };
                        cell.Style = c1StyleConverter.ConvertToCommonStyle(c1Cell.Style, s => s.ToStyle());
                        row.Cells.Add(cell);
                    }
                }

                row.Cells.Reverse();
                worksheet.Rows.Add(row);
                i++;
            }

            var maxColsCount = worksheet.Rows.Count > 0 ? worksheet.Rows.Select(r => { return r.Cells.Count; }).Max() : 0;
            worksheet.Columns.RemoveRange(maxColsCount, nCols - maxColsCount);

            // Merged cells
            foreach (var range in mergedRanges)
            {
                var cell = worksheet.Rows[range.RowFrom].Cells[range.ColumnFrom];
                cell.ColSpan = range.ColumnCount;
                cell.RowSpan = range.RowCount;

                //Clear the empty merged cells
                for (var index = 1; index < cell.RowSpan; index++)
                {
                    worksheet.Rows[range.RowFrom + index].Cells[range.ColumnFrom] = null;
                }

                for (var index = 1; index < cell.ColSpan; index++)
                {
                    worksheet.Rows[range.RowFrom].Cells[range.ColumnFrom + index] = null;
                }
            }

            // Frozen columns & rows
            if (c1Sheet.Rows.Frozen > 0 || c1Sheet.Columns.Frozen > 0)
            {
                worksheet.FrozenPane = new FrozenPane { Rows = c1Sheet.Rows.Frozen, Columns = c1Sheet.Columns.Frozen };
            }

            return worksheet;
        }

        private static float ColumnWidthToTwips(string width, System.Drawing.Font font)
        {
            if (!string.IsNullOrEmpty(width))
            {
                float pixWidth;
                float charsCount;
                if (width.EndsWith(CHARS_COUNT_SUFFIX))
                {
                    // Column width measured as the number of characters, see https://msdn.microsoft.com/en-us/library/documentformat.openxml.spreadsheet.column(v=office.14).aspx
                    if (float.TryParse(width.Substring(0, width.Length - 2), out charsCount))
                    {
                        var maxDigitWidth = MeasureMaxDigitWidth(font);
                        return PixelsToTwips(((256 * charsCount + 128 / maxDigitWidth) / 256) * maxDigitWidth);
                    }
                }
                else if (float.TryParse(width, out pixWidth))
                {
                    return PixelsToTwips(pixWidth);
                }
            }
            return -1;
        }

        private static float MeasureMaxDigitWidth(System.Drawing.Font font)
        {
            using (var img = new System.Drawing.Bitmap(1, 1))
            using (var g = System.Drawing.Graphics.FromImage(img))
            {
                return new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" }.Select(n => g.MeasureString(n, font).Width).Max();
            }
        }

        private static float PixelsToTwips(float pixels)
        {
            return pixels * PIXELS_TO_TWIPS_FACTOR;
        }

        private static float TwipsToPixels(float twips)
        {
            return twips / PIXELS_TO_TWIPS_FACTOR;
        }

        private static float PixelsToPoints(float pixels)
        {
            return pixels * PIXELS_TO_POINTS_FACTOR;
        }

        private static float PointsToPixels(float points)
        {
            return points / PIXELS_TO_POINTS_FACTOR;
        }

        private static XLStyle ToXLStyle(this Style style, C1XLBook c1Book)
        {
            if (style == null)
            {
                return null;
            }

            var c1Style = new XLStyle(c1Book);
            if (style.Format != null)
            {
                c1Style.Format = style.Format;
            }
            c1Style.SetXLText(style.Font);
            c1Style.SetXLBackground(style.Fill);
            if (style.Border == null)
            {
                style.Border = new BorderCollection("#CCCCCC");
            }
            c1Style.SetXLBorder(style.Border);
            c1Style.AlignHorz = style.HAlign.ConvertEnum<HAlignType, XLAlignHorzEnum>();
            c1Style.AlignVert = style.VAlign.ConvertEnum<VAlignType, XLAlignVertEnum>();
            c1Style.Indent = style.Indent;
            c1Style.Rotation = style.TextRotation;
            c1Style.Direction = style.TextDirection.ConvertEnum<TextDirectionType, XLReadingDirection>();
            c1Style.WordWrap = style.WrapText;
            c1Style.ShrinkToFit = style.ShrinkToFit;
            c1Style.Locked = style.Locked;
            return c1Style;
        }

        private static void SetXLText(this XLStyle c1Style, Font font)
        {
            if (font == null)
            {
                return;
            }
            var fontStyle = System.Drawing.FontStyle.Regular;
            if (font.Bold)
            {
                fontStyle |= System.Drawing.FontStyle.Bold;
            }
            if (font.Italic)
            {
                fontStyle |= System.Drawing.FontStyle.Italic;
            }
            if (font.Underline) // Underline type not supported yet
            {
                fontStyle |= System.Drawing.FontStyle.Underline;
            }
            if (font.Strikethrough)
            {
                fontStyle |= System.Drawing.FontStyle.Strikeout;
            }
            // Superscript/subscript not supported
            c1Style.Font = new System.Drawing.Font(font.Family.ToSafeFontName(), PixelsToPoints(font.Size), fontStyle);
            if (!string.IsNullOrEmpty(font.Color))
            {
                c1Style.ForeColor = font.Color.ToColor();
            }
        }

        private static string ToSafeFontName(this string cssFontFamily)
        {
            var systemFonts = new HashSet<string>(
                System.Drawing.FontFamily.Families.Select(x => x.Name).ToList());
            var names = cssFontFamily.Split(',').Select(x => x.Trim(' ', '\'', '\"')).ToList();
            foreach (var name in names)
            {
                if (systemFonts.Contains(name))
                {
                    return name;
                }
            }
            return "Arial";
        }

        private static void SetXLBackground(this XLStyle c1Style, Fill fill)
        {
            if (fill == null)
            {
                return;
            }

            // Set BackColor will lead to BackPattern=Solid when BackPattern is None, so make sure set BackPattern first.
            c1Style.BackPattern = fill.Pattern.ConvertEnum<PatternStyle, XLPatternEnum>(1);

            if (!string.IsNullOrEmpty(fill.Color))
            {
                c1Style.BackColor = fill.Color.ToColor();
            }

        }

        private static void SetXLBorder(this XLStyle c1Style, BorderCollection border)
        {
            if (border == null)
            {
                return;
            }
            if (border.Top != null)
            {
                if (!string.IsNullOrEmpty(border.Top.Color))
                {
                    c1Style.BorderColorTop = border.Top.Color.ToColor();
                }

                c1Style.BorderTop = border.Top.Type.ConvertEnum<BorderLinetype, XLLineStyleEnum>();
            }

            if (border.Bottom != null)
            {
                if (!string.IsNullOrEmpty(border.Bottom.Color))
                {
                    c1Style.BorderColorBottom = border.Bottom.Color.ToColor();
                }

                c1Style.BorderBottom = border.Bottom.Type.ConvertEnum<BorderLinetype, XLLineStyleEnum>();
            }

            if (border.Left != null)
            {
                if (!string.IsNullOrEmpty(border.Left.Color))
                {
                    c1Style.BorderColorLeft = border.Left.Color.ToColor();
                }
                c1Style.BorderLeft = border.Left.Type.ConvertEnum<BorderLinetype, XLLineStyleEnum>();
            }

            if (border.Right != null)
            {
                if (!string.IsNullOrEmpty(border.Right.Color))
                {
                    c1Style.BorderColorRight = border.Right.Color.ToColor();
                }

                c1Style.BorderRight = border.Right.Type.ConvertEnum<BorderLinetype, XLLineStyleEnum>();
            }
        }

        private static bool IsNumber(this object obj)
        {
            return obj is int || obj is float || obj is double || obj is decimal;
        }

        private static Style ToStyle(this XLStyle c1Style)
        {
            if (c1Style == null)
            {
                return null;
            }
            var style = new Style();
            style.Format = c1Style.Format;
            style.Indent = c1Style.Indent;
            style.Locked = c1Style.Locked;
            style.ShrinkToFit = c1Style.ShrinkToFit;
            style.TextRotation = c1Style.Rotation;
            style.WrapText = c1Style.WordWrap;

            style.Border = c1Style.ToBorder();
            style.Fill = c1Style.ToFill();
            style.Font = c1Style.ToFont();
            style.HAlign = (c1Style.AlignHorz == XLAlignHorzEnum.Undefined ? XLAlignHorzEnum.General : c1Style.AlignHorz).ConvertEnum<XLAlignHorzEnum, HAlignType>();
            style.VAlign = (c1Style.AlignVert == XLAlignVertEnum.Undefined ? XLAlignVertEnum.Bottom : c1Style.AlignVert).ConvertEnum<XLAlignVertEnum, VAlignType>();
            style.TextDirection = c1Style.Direction.ConvertEnum<XLReadingDirection, TextDirectionType>();

            return style;
        }

        private static BorderCollection ToBorder(this XLStyle c1Style)
        {
            if (c1Style.BorderTop == XLLineStyleEnum.None
                && c1Style.BorderBottom == XLLineStyleEnum.None
                && c1Style.BorderLeft == XLLineStyleEnum.None
                && c1Style.BorderRight == XLLineStyleEnum.None)
            {
                return null;
            }
            var border = new BorderCollection();
            border.Top = new Border
            {
                Color = c1Style.BorderColorTop.HandleAutomaticBorderColor().ToHex(),
                Type = c1Style.BorderTop.ConvertEnum<XLLineStyleEnum, BorderLinetype>(1)
            };
            border.Bottom = new Border
            {
                Color = c1Style.BorderColorBottom.HandleAutomaticBorderColor().ToHex(),
                Type = c1Style.BorderBottom.ConvertEnum<XLLineStyleEnum, BorderLinetype>(1)
            };
            border.Left = new Border
            {
                Color = c1Style.BorderColorLeft.HandleAutomaticBorderColor().ToHex(),
                Type = c1Style.BorderLeft.ConvertEnum<XLLineStyleEnum, BorderLinetype>(1)
            };
            border.Right = new Border
            {
                Color = c1Style.BorderColorRight.HandleAutomaticBorderColor().ToHex(),
                Type = c1Style.BorderRight.ConvertEnum<XLLineStyleEnum, BorderLinetype>(1)
            };
            return border;
        }

        private static System.Drawing.Color HandleAutomaticBorderColor(this System.Drawing.Color color)
        {
            if (color.R == 255 && color.G == 255 && color.B == 255)
            {
                return System.Drawing.Color.Black;
            }
            return color;
        }

        private static System.Drawing.Color ToColor(this string str)
        {
            int value;
            if (str.StartsWith("rgb"))
            {
                var arr = str.Replace("rgb(", "").Replace("rgba(", "").Replace(")", "").Split(',')
                    .Select(s => Convert.ToInt32(s.Trim())).ToArray();
                return System.Drawing.Color.FromArgb(arr[0], arr[1], arr[2]);
            }
            if (int.TryParse(str, System.Globalization.NumberStyles.HexNumber, null, out value))
            {
                str = "#" + str;
            }
            return System.Drawing.ColorTranslator.FromHtml(str);
        }

        // ColorTranslator may return color name.
        private static string ToHex(this System.Drawing.Color color, bool hashSign = true)
        {
            return (hashSign ? "#" : null) + color.ToArgb().ToString("X8").Substring(2);
        }

        private static Fill ToFill(this XLStyle c1Style)
        {
            if (c1Style.BackColor != null && c1Style.BackColor.A == 0)
            {
                return null;
            }
            var fill = new Fill();
            var color = c1Style.BackColor.ToHex();
            fill.Color = color;
            if (c1Style.BackPattern != XLPatternEnum.Solid && c1Style.BackPattern != XLPatternEnum.None)
            {
                fill.Pattern = c1Style.BackPattern.ConvertEnum<XLPatternEnum, PatternStyle>(1);
            }
            return fill;
        }

        private static Font ToFont(this XLStyle c1Style)
        {
            var font = new Font();
            font.Bold = c1Style.Font.Bold;
            font.Color = c1Style.ForeColor.ToHex();
            font.Family = c1Style.Font.Name;
            font.Italic = c1Style.Font.Italic;
            font.Size = PointsToPixels(c1Style.Font.Size);
            font.Strikethrough = c1Style.Font.Strikeout;
            font.Underline = c1Style.Font.Underline; // underline style not supported.
            // superscript/subscript not supported.
            return font;
        }

        private static TOutEnum ConvertEnum<TInEnum, TOutEnum>(this TInEnum inEnum, int defaultOutValue = 0)
            where TInEnum : struct
            where TOutEnum : struct
        {
            TOutEnum result;
            if (Enum.TryParse(inEnum.ToString(), out result))
            {
                return result;
            }
            else
            {
                return (TOutEnum)Enum.ToObject(typeof(TOutEnum), defaultOutValue);
            }
        }
    }
    public class CachedXLStyleConverter<TXLStyle, TCommonStyle>
    {
        private Dictionary<TXLStyle, TCommonStyle> ConvertedStyles = new Dictionary<TXLStyle, TCommonStyle>();

        internal TCommonStyle ConvertToCommonStyle(TXLStyle style, Func<TXLStyle, TCommonStyle> convert)
        {
            if (style == null)
            {
                return default(TCommonStyle);
            }
            else
            {
                if (ConvertedStyles.ContainsKey(style))
                {
                    return ConvertedStyles[style];
                }
                else
                {
                    var commonStyle = convert(style);
                    ConvertedStyles.Add(style, commonStyle);
                    return commonStyle;
                }
            }
        }
    }
}