using System;
using System.ComponentModel;
using System.Reflection;
using C1.Web.Mvc;
using HtmlHelper = Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper;

namespace C1.Web.Mvc.Extensions
{

    #region MultiColumnComboBox<T>

    /// <summary>
    /// MultiColumnComboBox
    /// </summary>
    public partial class MultiColumnComboBox<T> : DropDown<T>
    {

        /// <summary>
        /// Creates one <see cref="MultiColumnComboBox{T}" /> instance.
        /// </summary>
        /// <param name="helper">The html helper</param>
        /// <param name="selector">The selector</param>
        public MultiColumnComboBox(HtmlHelper helper, string selector = null) : base(helper, selector)
        {
            Initialize();
        }

        /// <summary>
        /// create default value for parameters
        /// </summary>
        private void Initialize()
        {
            PageSize = 10;
        }

        /// <summary>
        /// Gets or sets the name of the property to use as the visual representation of the items.
        /// </summary>
        public string DisplayMemberPath
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the property used to get the SelectedValue from the SelectedItem.
        /// </summary>
        public string SelectedValuePath
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value of The page size.
        /// </summary>
        [DefaultValue(10)]
        public int PageSize
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value that enables or disables editing of the text in the input element of the ComboBox (defaults to false).
        /// </summary>

        public bool IsEditable
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a function used to customize the values shown in the drop-down list.
        /// </summary>
        public string GridItemFormater
        {
            get;
            set;
        }

        /// <summary>
        ///  Gets or sets the name of the ComboBox control.
        /// </summary>
        public string Name
        {
            get { return HtmlAttributes["wj-name"]; }
            set { HtmlAttributes["wj-name"] = value; }
        }

        // an internal property for ComboxFor and the for attribute.
        internal object[] Values
        {
            get;
            set;
        }
    }

    #endregion

}
