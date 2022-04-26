using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace C1.Web.Mvc.Extensions.Fluent
{
    /// <summary>
    /// Defines a factory class to create different controls' builders.
    /// </summary>
    public class SampleControlBuilderFactory : C1.Web.Mvc.Fluent.ControlBuilderFactory
    {
        HtmlHelper _helper;

        /// <summary>
        /// Summary:
        ///     Default Constructor
        /// </summary>
        /// <param name="helper"></param>
        public SampleControlBuilderFactory(HtmlHelper helper) : base(helper)
        {
            _helper = helper;
        }

        /// <summary>
        /// Creates a <see cref="Extensions.MultiColumnComboBox{T}" /> control
        /// and attaches it to the dom elements matching against the specified selector.
        /// </summary>
        /// <typeparam name="T">The data item type.</typeparam>
        /// <param name="selector">
        /// Specifies a selector. It is optional.
        /// If it is not set, a default dom element will be generated.
        /// </param>
        /// <returns>A <see cref="MultiColumnComboBoxBuilder{T}" />.</returns>
        public MultiColumnComboBoxBuilder<T> MultiColumnComboBox<T>(string selector = null)
        {
            return new MultiColumnComboBoxBuilder<T>(new MultiColumnComboBox<T>(_helper, selector));
        }

        /// <summary>
        /// Creates a <see cref="Extensions.MultiColumnComboBox{T}" /> control
        /// and attaches it to the dom elements matching against the specified selector.
        /// </summary>
        /// <param name="selector">
        /// Specifies a selector. It is optional.
        /// If it is not set, a default dom element will be generated.
        /// </param>
        /// <returns>A <see cref="MultiColumnComboBoxBuilder{T}" />.</returns>
        public MultiColumnComboBoxBuilder<object> MultiColumnComboBox(string selector = null)
        {
            return new MultiColumnComboBoxBuilder<object>(new MultiColumnComboBox<object>(_helper, selector));
        }
    }

    /// <summary>
    /// Define a factory class to create different control builders, with model bindings.
    /// </summary>
    public class SampleControlBuilderFactory<TModel> : SampleControlBuilderFactory
    {
        private readonly HtmlHelper<TModel> _htmlHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="C1.Web.Mvc.Extensions.Fluent.SampleControlBuilderFactory"/> class 
        /// by using the specified html helper instance.
        /// </summary>
        /// <param name="helper"></param>
        public SampleControlBuilderFactory(HtmlHelper<TModel> helper) : base(helper)
        {
            _htmlHelper = helper;
        }

        /// <summary>
        /// Create a ComboBoxBuilder.
        /// </summary>
        /// <param name="expression">The model bind expression</param>
        /// <returns>The ComboBoxBuilder</returns>
        public MultiColumnComboBoxBuilder<object> MultiColumnComboBoxFor<TObject>(Expression<Func<TModel, TObject>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, _htmlHelper.ViewData);
            var name = _htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldName(ExpressionHelper.GetExpressionText(expression));
            var value = metadata.Model != null ? metadata.Model : null;
            var unobtrusiveValidationAttributes = _htmlHelper.GetUnobtrusiveValidationAttributes(name, metadata);
            return new MultiColumnComboBoxBuilder<object>(new MultiColumnComboBox<object>(_htmlHelper))
                .Id(NameToId(name)).Name(name).Value(value);
        }

        private string NameToId(string name)
        {
            if (string.IsNullOrEmpty(name)) return name;

            return name.Replace('.', '_').Replace('[', '_').Replace(']', '_').Replace('-', '_');
        }
    }

    /// <summary>
    /// Extends HtmlHelper for MultiColumnComboBox related controls creation.
    /// </summary>
    public static class HtmlHelperExtension
    {
        /// <summary>
        /// Summary:
        ///     Create the ControlBuilderFactory instance via HtmlHelper.
        /// </summary>
        /// <param name="helper">The specified HtmlHelper object</param>
        /// <returns>An instance of ControlBuilderFactory</returns>
        public static SampleControlBuilderFactory Custom(this HtmlHelper helper)
        {
            return new SampleControlBuilderFactory(helper);
        }

        /// <summary>
        /// Create the ControlBuilderFactory instance via html helper.
        /// </summary>
        /// <typeparam name="TModel">The specified model type</typeparam>
        /// <param name="helper">The specified html helper object</param>
        /// <returns>An instance of ControlBuilderFactory</returns>
        public static SampleControlBuilderFactory<TModel> Custom<TModel>(this HtmlHelper<TModel> helper)
        {
            return new SampleControlBuilderFactory<TModel>(helper);
        }
        

        /// <summary>
        /// Summary:
        ///     Register needed scripts for MultiColumnComboBox control
        /// </summary>
        /// <param name="html">The specified HtmlHelper object</param>
        /// <returns>scripts content</returns>
        public static MvcHtmlString RegisterMultiColumnComboBoxScripts(this HtmlHelper html)
        {
            var js = @"<script src=""/C1ExtensionResource/Scripts?f=input.js"" type></script>";
            js += @"<script src=""/C1ExtensionResource/Scripts?f=mvc.input.js"" type></script>";
            return MvcHtmlString.Create(js);
        }
    }
}