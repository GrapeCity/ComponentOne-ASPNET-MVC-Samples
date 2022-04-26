using C1.Web.Mvc.Fluent;

namespace C1.Web.Mvc.Extensions.Fluent
{
    /// <summary>
    /// Extends ControlBuilderFactory for MultiColumnComboBox related controls creation.
    /// </summary>   
    public partial class MultiColumnComboBoxBuilder<T>
      : DropDownBuilder<T, MultiColumnComboBox<T>, MultiColumnComboBoxBuilder<T>>
      {
          MultiColumnComboBox<T> Object;


        /// <summary>
        /// Sets the Name property.
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns>Current builder</returns>
        public MultiColumnComboBoxBuilder<T> Name(string value)
        {
            Object.Name = value;
            return this as MultiColumnComboBoxBuilder<T>;
        }

        //An internal fluent command to set the internal property "Value".
        internal MultiColumnComboBoxBuilder<T> Value(object value)
        {
            Object.Values = new object[1] { value };
            return this;
        }
    }
}
