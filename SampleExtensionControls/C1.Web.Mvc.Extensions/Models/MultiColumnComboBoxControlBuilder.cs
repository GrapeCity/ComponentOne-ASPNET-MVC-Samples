using C1.Web.Mvc.Fluent;

namespace C1.Web.Mvc.Extensions.Fluent
{

    #region MultiColumnComboBox<T>

    /// <summary>
    /// Defines a builder to configurate <see cref="MultiColumnComboBox{T}" />.
    /// </summary>
    public partial class MultiColumnComboBoxBuilder<T>
        : DropDownBuilder<T, MultiColumnComboBox<T>, MultiColumnComboBoxBuilder<T>>
    {
        /// <summary>
        /// Creates one <see cref="MultiColumnComboBoxBuilder{T}" /> instance to configurate <paramref name="component"/>.
        /// </summary>
        /// <param name="component">The <see cref="MultiColumnComboBox{T}" /> object to be configurated.</param>
        public MultiColumnComboBoxBuilder(MultiColumnComboBox<T> component) : base(component)
        {
            Object = component;
        }
        /// <summary>
        /// Configurates <see cref="MultiColumnComboBox{T}.DisplayMemberPath" />.
        /// Sets the name of the property to use as the visual representation of the items.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Current builder.</returns>
        public MultiColumnComboBoxBuilder<T> DisplayMemberPath(string value)
        { 
            Object.DisplayMemberPath = value;
            return this;
        }

        /// <summary>
        /// Configurates <see cref="MultiColumnComboBox{T}.SelectedValuePath" />.
        /// Sets the name of the property used to get the SelectedValue from the SelectedItem.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Current builder.</returns>
        public MultiColumnComboBoxBuilder<T> SelectedValuePath(string value)
        { 
            Object.SelectedValuePath = value;
            return this;
        }

        /// <summary>
        /// Configurates <see cref="MultiColumnComboBox{T}.PageSize" />.
        /// The page size.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Current builder.</returns>
        public MultiColumnComboBoxBuilder<T> PageSize(int value)
        { 
            Object.PageSize = value;
            return this;
        }

        /// <summary>
        /// Configurates <see cref="MultiColumnComboBox{T}.IsEditable" />.
        /// Sets a value that enables or disables editing of the text in the input element of the ComboBox (defaults to false).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Current builder.</returns>
        public MultiColumnComboBoxBuilder<T> IsEditable(bool value)
        { 
            Object.IsEditable = value;
            return this;
        }

        /// <summary>
        /// Configurates <see cref="MultiColumnComboBox{T}.GridItemFormater" />.
        /// Sets a function used to customize the values shown in the drop-down list.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Current builder.</returns>
        public MultiColumnComboBoxBuilder<T> GridItemFormater(string value)
        { 
            Object.GridItemFormater = value;
            return this as MultiColumnComboBoxBuilder<T>;
        }

        /// <summary>
        /// Configurates <see cref="MultiColumnComboBox{T}.SelectedValues" />.
        /// Sets the value of the SelectedItem, obtained using the SelectedValuePath.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Current builder.</returns>
        public virtual MultiColumnComboBoxBuilder<T> SelectedValues(object value)
        {
            Object.SelectedValues = value;
            return this as MultiColumnComboBoxBuilder<T>;
        }
  }

#endregion
}