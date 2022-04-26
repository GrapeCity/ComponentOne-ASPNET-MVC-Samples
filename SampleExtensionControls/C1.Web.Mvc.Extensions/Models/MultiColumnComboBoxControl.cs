using System.ComponentModel;
using C1.Web.Mvc;
using System.Web.Mvc;
using C1.JsonNet;
using C1.JsonNet.Converters;

namespace C1.Web.Mvc.Extensions
{
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

        private void Initialize()
        {
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
        /// Gets or sets the value of the SelectedItem, obtained using the SelectedValuePath.
        /// </summary>
        public virtual object SelectedValues
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value of The page size.
        /// </summary>
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

    #if MODEL
            [C1Ignore]
    #endif
    #if !MODEL
        [JsonConverter(typeof(FunctionConverter))]
    #endif
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

}
