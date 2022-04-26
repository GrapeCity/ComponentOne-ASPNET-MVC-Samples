using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using C1.Web.Mvc.Fluent;

namespace ImageCombo.Controls
{
    public static class HtmlHelperExtension
    {
        public static ImageComboBuilderFactory Custom(this HtmlHelper helper)
        {
            return new ImageComboBuilderFactory(helper);
        }
    }

    public class ImageComboBuilder<T> : ComboBoxBaseBuilder<T, ImageCombo<T>, ImageComboBuilder<T>>
    {
        private readonly ImageCombo<T> _control;

        public ImageComboBuilder(ImageCombo<T> control)
            : base(control)
        {
            _control = control;
        }

        /// <summary>
        /// Sets the ImageMemberPath property.
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns>Current builder</returns>
        public ImageComboBuilder<T> ImageMemberPath(string value)
        {
            _control.ImageMemberPath = value;
            return this;
        }

        /// <summary>
        /// Sets the ImageType property.
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns>Current builder</returns>
        public ImageComboBuilder<T> ImageType(ImageType value)
        {
            _control.ImageType = value;
            return this;
        }
    }

    public class ImageComboBuilderFactory : ControlBuilderFactory
    {
        private readonly HtmlHelper _helper;
        public ImageComboBuilderFactory(HtmlHelper helper)
            : base(helper)
        {
            if (helper == null)
            {
                throw new ArgumentNullException("helper");
            }

            _helper = helper;
        }

        public ImageComboBuilder<object> ImageCombo(string selector = null)
        {
            return new ImageComboBuilder<object>(new ImageCombo<object>(_helper, selector));
        }
    }
}