using C1.JsonNet;
using C1.JsonNet.Converters;
using C1.Web.Mvc;
using C1.Web.Mvc.Fluent;
using C1.Web.Mvc.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace ImageCombo.Controls
{
    public enum ImageType
    {
        Bmp,
        Jpg,
        Gif,
        Png,
        Emf
    }

    /// <summary>
    /// ImageCombo is inherited from ComboBox. It can display databound image and text.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ImageCombo<T> : ComboBox<T>
    {
        public ImageCombo(HtmlHelper helper, string selector = null)
            : base(helper, selector)
        {
        }

        [Ignore]
        public string ImageMemberPath { get; set; }

        [Ignore]
        public ImageType ImageType { get; set; }

        public override void Render(HtmlTextWriter writer)
        {
            if (!String.IsNullOrEmpty(ImageMemberPath))
            {
                ItemTemplateContent = "<span>" +
                "<img style=\"width:75px;height:60px;\" src=\"data:image/" + ImageType.ToString().ToLower() +
                ";base64,{{" + ImageMemberPath + "}}\" />" +
                "<span>{{" + DisplayMemberPath + "}}</span>" +
                "</span>";
            }

            base.Render(writer);
        }

        protected override string ClientComponent
        {
            get
            {
                return "c1.mvc.input._ComboBoxWrapper";
            }
        }
    }
}