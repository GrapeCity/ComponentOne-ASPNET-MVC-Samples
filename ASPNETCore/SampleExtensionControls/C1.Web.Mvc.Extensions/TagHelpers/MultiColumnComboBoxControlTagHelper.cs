using C1.Web.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Reflection;
using System;
using System.Linq;
namespace C1.Web.Mvc.Extensions.TagHelpers
{

    #region MultiColumnComboBoxTagHelper

    /// <summary>
    /// <see cref="ITagHelper"/> implementation for <see cref="MultiColumnComboBox{T}" />.
    /// </summary>
    [RestrictChildren("c1-items-source", "c1-odata-source", "c1-odata-virtual-source", "c1-input-item-template")]
    [HtmlTargetElement("c1-multi-column-combo-box")]
    public class MultiColumnComboBoxTagHelper : DropDownTagHelper<object, MultiColumnComboBox<object>>
    {
        /// <summary>
        /// Configurates <see cref="MultiColumnComboBox{T}.DisplayMemberPath" />.
        /// Sets the name of the property to use as the visual representation of the items.
        /// </summary>
        public string DisplayMemberPath
        {
            get { return TObject.DisplayMemberPath; }
            set { TObject.DisplayMemberPath = value; }
        }

        /// <summary>
        /// Configurates <see cref="MultiColumnComboBox{T}.SelectedValuePath" />.
        /// Sets the name of the property used to get the SelectedValue from the SelectedItem.
        /// </summary>
        public string SelectedValuePath
        {
            get { return TObject.SelectedValuePath; }
            set { TObject.SelectedValuePath = value; }
        }

        /// <summary>
        /// Configurates <see cref="MultiColumnComboBox{T}.PageSize" />.
        /// The page size.
        /// </summary>
        public int PageSize
        {
            get { return TObject.PageSize; }
            set { TObject.PageSize = value; }
        }

        /// <summary>
        /// Configurates <see cref="MultiColumnComboBox{T}.IsEditable" />.
        /// Sets a value that enables or disables editing of the text in the input element of the ComboBox (defaults to false).
        /// </summary>
        public bool IsEditable
        {
            get { return TObject.IsEditable; }
            set { TObject.IsEditable = value; }
        }

        /// <summary>
        /// Configurates <see cref="MultiColumnComboBox{T}.GridItemFormater" />.
        /// Sets a function used to customize the values shown in the drop-down list.
        /// </summary>
        public string GridItemFormater
        {
            get { return TObject.GridItemFormater; }
            set { TObject.GridItemFormater = value; }
        }


        /// <summary>
        ///  Gets or sets the name of the ComboBox control.
        /// </summary>
        public string Name
        {
            get { return TObject.Name; }
            set { TObject.Name = value; }
        }

        /// <summary>
        /// An expression to be evaluated against the current model.
        /// </summary>
        public ModelExpression For
        {
            get;
            set;
        }

        /// <summary>
        /// Render the component result to the writer.
        /// </summary>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        protected override void Render(TagHelperOutput output)
        {
            TObject.HtmlAttributes["type"] = "text";
            base.Render(output);
        }

        /// <summary>
        /// Synchronously executes the MultiColumnComboBoxTagHelper with
        /// the given context and output.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        protected override void ProcessAttributes(TagHelperContext context, object parent)
        {
            base.ProcessAttributes(context, parent);
            if (For == null)
            {
                return;
            }
            TObject.Values = new object[1] { For.Model };
            TObject.Name = ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(For.Name);
        }
    }

    #endregion
}

