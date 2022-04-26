using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using C1.C1Pdf;
using System.IO;
using System.Drawing;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers.PDF
{
    public partial class PDFController : Controller
    {
        //
        // GET: /Acroform/

        public ActionResult Acroform()
        {
            return View();
        }

        public ActionResult CreateAcroformPdf()
        {
            try
            {
                InitialAcroformPdfDocument();
                var stream = new MemoryStream();
                _c1Pdf.Save(stream);
                stream.Position = 0;
                return File(stream, "application/pdf");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        private int _textBoxCount;
        private int _checkBoxCount;
        private int _pushButtonCount;
        private int _comboBoxCount;
        private int _listBoxCount;
        private int _rowCount;

        private void InitialAcroformPdfDocument()
        {
            _c1Pdf = new C1PdfDocument();
            // create pdf document
            _c1Pdf.Clear();
            _c1Pdf.DocumentInfo.Title = "PDF Acroform";
            _tempDir = Server.MapPath("../Temp");
            if (Directory.Exists(_tempDir))
            {

            }
            else
            {
                Directory.CreateDirectory(_tempDir);

            }
            // calculate page rect (discounting margins)
            RectangleF rcPage = GetPageRect();
            RectangleF rc = rcPage;


            // add title
            Font titleFont = new Font("Tahoma", 24, FontStyle.Bold);
            rc = RenderParagraph(_c1Pdf.DocumentInfo.Title, titleFont, rcPage, rc);

            // render Employees table
            rc = RenderTable(rc, rcPage);

            // Render Buttons
            Font btnFont = new Font("Tahoma", 14, FontStyle.Bold);
            PdfPushButton button1 = RenderPushButton("Submit", btnFont, new RectangleF(new PointF(rc.X, rc.Y + 10), new SizeF(90, 25)), ButtonLayout.TextLeftImageRight);
            button1.Actions.LostFocus.Add(new PdfPushButton.Action(ButtonAction.GotoPage, "Bmark"));
            button1.BorderStyle = FieldBorderStyle.Inset;
            button1.BorderWidth = FieldBorderWidth.Medium;
            button1.BorderColor = Color.Black;

            PdfPushButton button2 = RenderPushButton("Clear Fields", btnFont, new RectangleF(new PointF(rc.X + 110, rc.Y + 10), new SizeF(110, 25)), ButtonLayout.TextLeftImageRight);
            button2.Actions.Pressed.Add(new PdfPushButton.Action(ButtonAction.ClearFields));
            button2.BorderStyle = FieldBorderStyle.Dashed;
            button2.BorderWidth = FieldBorderWidth.Medium;
            button2.BorderColor = Color.Black;

            // second pass to number pages
            AddFooters();

            _c1Pdf.Compression = CompressionEnum.None;
        }

        private RectangleF RenderTable(RectangleF rc, RectangleF rcPage)
        {
            // get data
            List<Employee> employees = _dataBase.Employees.ToList();
            //  select fonts
            Font hdrFont = new Font("Tahoma", 12, FontStyle.Bold);
            Font txtFont = new Font("Tahoma", 10);

            // build table
            rc = RenderParagraph("NorthWind Employees", hdrFont, rcPage, rc);

            // build table
            rc = RenderTableHeader(hdrFont, rc, new [] { "TextBoxes", "RadioButtons", "CheckBoxes", "ComboBoxes", "ListBoxes" });
            foreach (Employee employee in employees)
                rc = RenderTableRow(txtFont, hdrFont, rcPage, rc, employee);

            // done
            return rc;
        }

        private RectangleF RenderTableHeader(Font font, RectangleF rc, string[] fields)
        {
            // calculate cell width (same for all columns)
            RectangleF rcCell = rc;
            rcCell.Width = rc.Width / fields.Length;
            rcCell.Height = 0;

            //  calculate cell height (max of all columns)
            foreach (string field in fields)
            {
                float height = _c1Pdf.MeasureString(field, font, rcCell.Width).Height;
                rcCell.Height = Math.Max(rcCell.Height, height);
            }

            // render header cells
            foreach (string field in fields)
            {
                _c1Pdf.FillRectangle(Brushes.Black, rcCell);
                _c1Pdf.DrawString(field, font, Brushes.White, rcCell);
                rcCell.Offset(rcCell.Width, 0);
            }

            // update rectangle and return it
            rc.Offset(0, rcCell.Height);
            return rc;
        }

        private RectangleF RenderTableRow(Font font, Font hdrFont, RectangleF rcPage, RectangleF rc, Employee employee)
        {
            _rowCount += 1;
            RectangleF rcCell = rc;
            rcCell.Height = 100;

            // break page if we have to
            if (rcCell.Bottom > rcPage.Bottom)
            {
                _c1Pdf.NewPage();
                rc = RenderTableHeader(hdrFont, rcPage, new [] { "TextBoxes", "RadioButtons", "CheckBoxes", "ComboBoxes", "ListBoxes" });
                rcCell.Y = rc.Y;
            }


            // render data cells
            Pen pen = new Pen(Brushes.Gray, 0.1f);

            _c1Pdf.DrawRectangle(pen, rcCell);
            rcCell.Inflate(-4, 0);
            // Render name
            float nameWidth = (float)(rc.Width * .5);
            float colWidth = rc.Width / 2;
            float x1 = rc.Location.X + 2;
            float x2 = rc.Location.X + rc.Width / 2;
            float y1 = rc.Location.Y + 2;

            // Declare possible locations for row controls
            PointF r1c1 = new PointF(rc.Location.X + 2, rc.Location.Y + 2);

            // Render String
            string employeeName = employee.FirstName + " " + employee.LastName;
            _c1Pdf.DrawString(employeeName, new Font(font.FontFamily, 20, FontStyle.Bold), Brushes.Black, new RectangleF(r1c1, new SizeF(nameWidth, 25)));

            // Render ComboBox
            _c1Pdf.DrawString("Reports to:", font, Brushes.Black, new PointF(x1, y1 + 30));
            int reportsTo = string.IsNullOrEmpty(employee.ReportsTo.ToString()) ? 0 : (int)employee.ReportsTo;
            RenderComboBox(new [] { "Nancy Davolio", "Andrew Fuller", "Janet Leverling", "Margaret Peacock", "Steven Buchanan", "Michael Suyama", "Robert King" }, reportsTo, font, new RectangleF(new PointF(x1 + 55, y1 + 29), new SizeF(colWidth - 70, 15)));

            // Render Radiobuttons
            _c1Pdf.DrawString("Product Division:", font, Brushes.Black, new PointF(x1, y1 + 50));
            RenderRadioButton(true, "GroupDivision" + _rowCount, "One", font, new RectangleF(new PointF(x1 + 80, y1 + 50), new SizeF(colWidth - 85, 15)));
            RenderRadioButton(true, "GroupDivision" + _rowCount, "Two", font, new RectangleF(new PointF(x1 + 120, y1 + 50), new SizeF(colWidth - 125, 15)));
            RenderRadioButton(true, "GroupDivision" + _rowCount, "Three", font, new RectangleF(new PointF(x1 + 160, y1 + 50), new SizeF(colWidth - 165, 15)));

            // Render Checkbox
            RenderCheckBox(true, " Receives Email Notifications", font, new RectangleF(new PointF(x2 + 10, y1 + 5), new SizeF(colWidth - 5, 15)));
            _c1Pdf.DrawString("Email Address:", font, Brushes.Black, new PointF(x2 + 10, y1 + 30));

            // Render Textbox
            RenderTextBox(employee.LastName.ToLower() + "@nwind.com", font, new RectangleF(new PointF(x2 + 80, y1 + 29), new SizeF(colWidth - 95, 15)), Color.FromKnownColor(KnownColor.Info), "Enter Email Address", false);

            // Render Listbox
            _c1Pdf.DrawString("Title:", font, Brushes.Black, new PointF(x2 + 10, y1 + 60));
            RenderListBox(new [] { "Sales Representative", "Sales Manager", "Vice President, Sales", "Inside Sales Coordinator" }, employee.Title, font, new RectangleF(new PointF(x2 + 50, y1 + 55), new SizeF(colWidth - 65, 30)));

            rcCell.Inflate(4, 0);


            pen.Dispose();

            // update rectangle and return it
            rc.Offset(0, rcCell.Height);
            return rc;
        }

        // add text box field for fields of the PDF document
        // with common parameters and default names.
        //  
        private PdfTextBox RenderTextBox(string text, Font font, RectangleF rc, Color back, string toolTip, bool multiline)
        {
            //  create
            string name = string.Format("ACFTB{0}", _textBoxCount + 1);
            PdfTextBox textBox = new PdfTextBox();

            //  default border
            // textBox.BorderWidth = 3f / 4;
            textBox.BorderStyle = FieldBorderStyle.Solid;
            textBox.BorderColor = SystemColors.ControlDarkDark;

            // parameters
            textBox.Font = font;
            textBox.Name = name;
            textBox.DefaultText = text;
            textBox.Text = text;
            textBox.ToolTip = string.IsNullOrEmpty(toolTip) ? string.Format("{0} ({1})", text, name) : toolTip;
            if (back != Color.Transparent && !back.IsEmpty)
            {
                textBox.BackColor = back;
            }
            textBox.IsMultiline = multiline;
            // add
            _c1Pdf.AddField(textBox, rc);
            _textBoxCount++;

            // done
            return textBox;
        }

        // add combo box field for fields of the PDF document
        // with common parameters and default names.
        //  
        private PdfComboBox RenderComboBox(string[] list, int activeIndex, Font font, RectangleF rc, Color back, string toolTip)
        {
            // create
            string name = string.Format("ACFCLB{0}", _comboBoxCount + 1);
            PdfComboBox comboBox = new PdfComboBox();

            // default border
            comboBox.BorderWidth = FieldBorderWidth.Thin;
            comboBox.BorderStyle = FieldBorderStyle.Solid;
            comboBox.BorderColor = SystemColors.ControlDarkDark;

            // array
            foreach (string text in list)
            {
                comboBox.Items.Add(text);
            }

            // parameters
            comboBox.Font = font;
            comboBox.Name = name;
            comboBox.DefaultValue = activeIndex;
            comboBox.Value = activeIndex;
            comboBox.ToolTip = string.IsNullOrEmpty(toolTip) ? string.Format("{0} ({1})", string.Format("Count = {0}", comboBox.Items.Count), name) : toolTip;
            if (back != Color.Transparent && !back.IsEmpty)
            {
                comboBox.BackColor = back;
            }

            // add
            _c1Pdf.AddField(comboBox, rc);
            _comboBoxCount++;

            // done
            return comboBox;
        }

        private PdfComboBox RenderComboBox(string[] list, int activeIndex, Font font, RectangleF rc)
        {
            return RenderComboBox(list, activeIndex, font, rc, Color.Transparent, null);
        }

        // add list box field for fields of the PDF document
        // with common parameters and default names.
        //  
        private PdfListBox RenderListBox(string[] list, string text, Font font, RectangleF rc, Color back, string toolTip)
        {
            // create
            string name = string.Format("ACFLB{0}", _listBoxCount + 1);
            PdfListBox listBox = new PdfListBox();

            // default border
            listBox.BorderWidth = FieldBorderWidth.Thin;
            listBox.BorderStyle = FieldBorderStyle.Solid;
            listBox.BorderColor = SystemColors.ControlDarkDark;

            // array
            foreach (string item in list)
            {
                listBox.Items.Add(item);
            }

            // parameters
            listBox.Font = font;
            listBox.Name = name;
            listBox.Text = text;
            listBox.ToolTip = string.IsNullOrEmpty(toolTip) ? string.Format("{0} ({1})", string.Format("Count = {0}", listBox.Items.Count), name) : toolTip;
            if (back != Color.Transparent && !back.IsEmpty)
            {
                listBox.BackColor = back;
            }

            // add
            _c1Pdf.AddField(listBox, rc);
            _listBoxCount++;

            // done
            return listBox;
        }

        private PdfListBox RenderListBox(string[] list, string text, Font font, RectangleF rc)
        {
            return RenderListBox(list, text, font, rc, Color.Transparent, null);
        }

        // add radio button box field for fields of the PDF document
        // with common parameters and default names.
        //  
        private PdfRadioButton RenderRadioButton(bool value, string group, string text, Font font, RectangleF rc, Color back, string toolTip)
        {
            // create
            string name = string.IsNullOrEmpty(group) ? "ACFRGR" : group;
            PdfRadioButton radioButton = new PdfRadioButton();

            // parameters
            radioButton.Name = name;
            radioButton.DefaultValue = value;
            radioButton.Value = value;
            radioButton.ToolTip = string.IsNullOrEmpty(toolTip) ? string.Format("{0} ({1})", text, name) : toolTip;
            if (back != Color.Transparent && !back.IsEmpty)
            {
                radioButton.BackColor = back;
            }

            // add
            float radioSize = font.Size;
            float radioTop = rc.Top + (rc.Height - radioSize) / 2;
            _c1Pdf.AddField(radioButton, new RectangleF(rc.Left, radioTop, radioSize, radioSize));

            // text for radio button field
            using (SolidBrush brush = new SolidBrush(Color.Black))
            {
                float x = rc.Left + radioSize + 1.0f;
                float y = rc.Top + (rc.Height - radioSize - 1.0f) / 2;
                _c1Pdf.DrawString(text, new Font(font.Name, radioSize, font.Style), brush, new PointF(x, y));
            }

            // done
            return radioButton;
        }

        private PdfRadioButton RenderRadioButton(bool value, string group, string text, Font font, RectangleF rc)
        {
            return RenderRadioButton(value, group, text, font, rc, Color.Transparent, null);
        }

        // add check box field for fields of the PDF document
        // with common parameters and default names.
        //  
        private PdfCheckBox RenderCheckBox(bool value, string text, Font font, RectangleF rc, Color back, string toolTip)
        {
            // create
            string name = string.Format("ACFCB{0}", _checkBoxCount + 1);
            PdfCheckBox checkBox = new PdfCheckBox();

            // default border
            checkBox.BorderWidth = FieldBorderWidth.Thin;
            checkBox.BorderStyle = FieldBorderStyle.Solid;
            checkBox.BorderColor = SystemColors.ControlDarkDark;

            // parameters
            checkBox.Name = name;
            checkBox.DefaultValue = value;
            checkBox.Value = value;
            checkBox.ToolTip = string.IsNullOrEmpty(toolTip) ? string.Format("{0} ({1})", text, name) : toolTip;
            if (back != Color.Transparent && !back.IsEmpty)
            {
                checkBox.BackColor = back;
            }

            // add
            float checkBoxSize = font.Size;
            float checkBoxTop = rc.Top + (rc.Height - checkBoxSize) / 2;
            _c1Pdf.AddField(checkBox, new RectangleF(rc.Left, checkBoxTop, checkBoxSize, checkBoxSize));
            _checkBoxCount++;

            // text for check box field
            using (SolidBrush brush = new SolidBrush(Color.Black))
            {
                float x = rc.Left + checkBoxSize + 1.0f;
                float y = rc.Top + (rc.Height - checkBoxSize - 1.0f) / 2;
                _c1Pdf.DrawString(text, new Font(font.Name, checkBoxSize, font.Style), brush, new PointF(x, y));
            }

            // done
            return checkBox;
        }

        private PdfCheckBox RenderCheckBox(bool value, string text, Font font, RectangleF rc)
        {
            return RenderCheckBox(value, text, font, rc, Color.Transparent, null);
        }

        // add push button box field for fields of the PDF document
        // with common parameters and default names.
        //  
        private PdfPushButton RenderPushButton(string text, Font font, RectangleF rc, Color back, Color fore, string toolTip, Image image, ButtonLayout layout)
        {
            // create
            string name = string.Format("ACFPB{0}", _pushButtonCount + 1);
            PdfPushButton pushButton = new PdfPushButton();

            // parameters
            pushButton.Name = name;
            pushButton.DefaultValue = text;
            pushButton.Value = text;
            pushButton.Font = font;
            pushButton.ToolTip = string.IsNullOrEmpty(toolTip) ? string.Format("{0} ({1})", text, name) : toolTip;
            if (back != Color.Transparent && !back.IsEmpty)
            {
                pushButton.BackColor = back;
            }
            if (fore != Color.Transparent && !fore.IsEmpty)
            {
                pushButton.ForeColor = fore;
            }

            // icon
            if (image != null)
            {
                pushButton.Image = image;
                pushButton.Layout = layout;
            }

            // add
            _c1Pdf.AddField(pushButton, rc);
            _pushButtonCount++;

            // done
            return pushButton;
        }

        private PdfPushButton RenderPushButton(string text, Font font, RectangleF rc, ButtonLayout layout)
        {
            return RenderPushButton(text, font, rc, Color.Transparent, Color.Transparent, null, null, layout);
        }
    }
}
