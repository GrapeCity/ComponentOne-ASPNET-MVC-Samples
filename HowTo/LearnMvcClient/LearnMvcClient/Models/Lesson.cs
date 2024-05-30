using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.IO;
using System;
using System.Text.RegularExpressions;

namespace LearnMvcClient.Models
{
    /// <summary>
    /// Defines some methods.
    /// </summary>
    public static class LessonHelper
    {
        private const string DefaultDocumentation = "https://developer.mescius.com/componentone/docs/mvc/online-mvc/overview.html";
        private const string DefaultDocumentationJa = "https://docs.mescius.com/help/c1/aspnet-mvc/aspmvc_helpers/";
        private static List<Lesson> _lessons;
        private static readonly object _locker = new object();
        private const string LessonsInServer = "~/Content/Lessons.xml";
        private const string LessonScriptsFolderPath = "~/Scripts/Lesson";
        private const string LessonCssFolderPath = "~/Content/css/Lesson";
        private const string LessonControllerFolderPath = "~/Controllers";
        private const string LessonViewFolderPath = "~/Views";
        private const string ActionNameAttribute = "actionname";
        private const string ControllerNameAttribute = "controllername";
        private const string TextAttribute = "text";
        private const string TextJaAttribute = "text.ja";
        private const string DocumentationAttribute = "documentation";
        private const string DocumentationJaAttribute = "documentation.ja";
        private const string IsNewAttribute = "isnew";
        private const string IdAttribute = "id";
        private const string LessonTagName = "Lesson";
        private static readonly Regex SharedView = new Regex("@Html\\.Partial\\(\"(\\S+?)\",");

        /// <summary>
        /// Gets the lesson collection.
        /// </summary>
        public static IEnumerable<Lesson> Lessons
        {
            get
            {
                EnsureLessons();
                return _lessons;
            }
        }

        /// <summary>
        /// Gets the pages' sources of the lesson specified by the controller name and the action name.
        /// </summary>
        /// <param name="controllerName">The controller name used to specify the lesson.</param>
        /// <param name="actionName">The action name used to specify the lesson.</param>
        /// <returns>The sources of all the related pages: controller, view and javascript.</returns>
        public static IDictionary<string, string> GetPageSources(string controllerName, string actionName)
        {
            var pageSources = new Dictionary<string, string>();

            var jsFileName = string.Format("{0}/{1}.js", controllerName, actionName);
            var jsFilePath = HttpContext.Current.Server.MapPath(GetScriptRelativePath(controllerName, actionName));
            if (File.Exists(jsFilePath))
            {
                var jsPrefix = string.Format("// This file locates: \"Scripts/Lesson/{0}\".\r\n", jsFileName);
                var jsFileHtml = GetFileAsHtmlContent(jsFilePath);
                pageSources.Add(jsFileName, jsPrefix + jsFileHtml);
            }

            var cssFileName = string.Format("{0}/{1}.css", controllerName, actionName);
            var cssFilePath = HttpContext.Current.Server.MapPath(GetCssRelativePath(controllerName, actionName));
            if (File.Exists(cssFilePath))
            {
                var cssFilePrefix = string.Format("// This file locates: \"Content/css/Lesson/{0}\".\r\n", cssFileName);
                var cssFileHtml = GetFileAsHtmlContent(cssFilePath);
                pageSources.Add(cssFileName, cssFilePrefix + cssFileHtml);
            }

            var controllerFileName = actionName + "Controller.cs";
            var controllerFilePath = HttpContext.Current.Server.MapPath(
                string.Format("{0}/{1}/{2}", LessonControllerFolderPath, controllerName, controllerFileName));
            var controllerFileHtml = GetFileAsHtmlContent(controllerFilePath);
            pageSources.Add(controllerFileName, controllerFileHtml);

            var viewFileName = actionName + ".cshtml";
            var viewFilePath = HttpContext.Current.Server.MapPath(
                string.Format("{0}/{1}/{2}", LessonViewFolderPath, controllerName, viewFileName));
            var viewFileHtml = GetFileAsHtmlContent(viewFilePath);
            pageSources.Add(viewFileName, viewFileHtml);

            var matches = SharedView.Matches(viewFileHtml);
            if(matches != null)
            {
                var count = matches.Count;
                for(var i=0; i<count; i++)
                {
                    var match = matches[i];
                    if (match.Success)
                    {
                        if(match.Groups != null && match.Groups.Count >= 2)
                        {
                            var sharedName = match.Groups[1].Value;
                            if(!string.IsNullOrEmpty(sharedName) && sharedName.Length > 0)
                            {
                                sharedName += ".cshtml";
                                var sharedViewPath = HttpContext.Current.Server.MapPath(
                                    string.Format("{0}/{1}/{2}", LessonViewFolderPath, "Shared", sharedName));
                                var sharedFileContent = GetFileAsHtmlContent(sharedViewPath);
                                pageSources.Add(sharedName, sharedFileContent);
                            }
                        }
                    }
                }
            }

            return pageSources;
        }

        public static string GetScriptRelativePath(string controllerName, string actionName)
        {
            return string.Format("{0}/{1}/{2}.js", LessonScriptsFolderPath, controllerName, actionName);
        }

        public static string GetCssRelativePath(string controllerName, string actionName)
        {
            return string.Format("{0}/{1}/{2}.css", LessonCssFolderPath, controllerName, actionName);
        }

        /// <summary>
        /// Gets the documentation url of a lesson specified by the controller name and the action name.
        /// </summary>
        /// <param name="controllerName">The controller name used to specify the lesson.</param>
        /// <param name="actionName">The action name used to specify the lesson.</param>
        /// <returns>The documentation url.</returns>
        public static string GetLessonDocumentationUrl(string controllerName, string actionName, int id = 0)
        {
            var lesson = FindLesson(controllerName, actionName, Lessons, id);
            if (lesson != null)
            {
                return IsjaCulture()
                    ? (lesson.DocumentationJa ?? lesson.Documentation ?? DefaultDocumentationJa)
                    : (lesson.Documentation ?? DefaultDocumentation);
            }

            return null;
        }

        public static Lesson FindLesson(string controllerName, string actionName, int id = 0)
        {
            return FindLesson(controllerName, actionName, Lessons, id);
        }

        private static Lesson FindLesson(string controllerName, string actionName, IEnumerable<Lesson> lessons, int id)
        {
            foreach (var lesson in lessons)
            {
                var lActionName = lesson.ActionName;
                if (string.Compare(controllerName, lesson.ControllerName, true) == 0
                    && string.Compare(lActionName, actionName, true) == 0
                    && lesson.Id == id)
                {
                    return lesson;
                }

                if (lesson.SubLessons != null)
                {
                    var result = FindLesson(controllerName, actionName, lesson.SubLessons, id);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }

            return null;
        }

        private static string GetFileAsHtmlContent(string controllerFilePath)
        {
            return File.ReadAllText(controllerFilePath);
        }

        private static void EnsureLessons()
        {
            lock (_locker)
            {
                if (_lessons != null)
                {
                    return;
                }

                var root = XElement.Load(HttpContext.Current.Server.MapPath(LessonsInServer));
                _lessons = GetLessons(root);
            }
        }

        private static Lesson ConvertFrom(XElement lessonElement, Lesson parent)
        {
            var lesson = new Lesson();
            var actionName = lessonElement.Attribute(ActionNameAttribute);
            if(actionName != null)
            {
                lesson.ActionName = actionName.Value;
            }
            else
            {
                lesson.ActionName = "Index";
            }

            var controllerName = lessonElement.Attribute(ControllerNameAttribute);
            if (controllerName != null)
            {
                lesson.ControllerName = controllerName.Value;
            }
            else if (parent != null)
            {
                lesson.ControllerName = parent.ControllerName;
            }

            var text = lessonElement.Attribute(TextAttribute);
            if (text != null)
            {
                lesson.Text = text.Value;
            }

            var textJa = lessonElement.Attribute(TextJaAttribute);
            if (textJa != null)
            {
                lesson.TextJa = textJa.Value;
            }

            var documentation = lessonElement.Attribute(DocumentationAttribute);
            if (documentation != null)
            {
                lesson.Documentation = documentation.Value;
            }

            var documentationJa = lessonElement.Attribute(DocumentationJaAttribute);
            if (documentationJa != null)
            {
                lesson.DocumentationJa = documentationJa.Value;
            }

            var isnew = lessonElement.Attribute(IsNewAttribute);
            if (isnew != null)
            {
                lesson.IsNew = System.Convert.ToBoolean(isnew.Value);
            }

            var id = lessonElement.Attribute(IdAttribute);
            if (id != null)
            {
                lesson.Id = System.Convert.ToInt32(id.Value);
            }

            if (lessonElement.Elements().Count() > 0)
            {
                lesson.SubLessons = GetLessons(lessonElement, lesson);
            }

            return lesson;
        }

        private static List<Lesson> GetLessons(XElement xElement, Lesson parent = null)
        {
            var lessons = new List<Lesson>();
            foreach (var subElement in xElement.Elements(LessonTagName))
            {
                lessons.Add(ConvertFrom(subElement, parent));
            }
            return lessons;
        }

        internal static bool IsjaCulture()
        {
            var culture = System.Threading.Thread.CurrentThread.CurrentUICulture;
            return culture.Name.StartsWith("ja", StringComparison.OrdinalIgnoreCase);
        }
    }


    /// <summary>
    /// Defines a class to descript the lesson information.
    /// </summary>
    public class Lesson
    {
        private string _text;
        private string _textJa;

        /// <summary>
        /// Gets or sets the text displayed in the list for one lesson.
        /// </summary>
        public string Text
        {
            get
            {
                return _text ?? (_text = GetTitleText(ActionName));
            }
            set
            {
                _text = value;
            }
        }

        /// <summary>
        /// Gets or sets the text of JP culture displayed in the list for one lesson.
        /// </summary>
        public string TextJa
        {
            get { return _textJa ?? Text; }
            set { _textJa = value; }
        }

        /// <summary>
        /// Gets the display text for one lesson.
        /// </summary>
        public string DisplayText
        {
            get
            {
                return LessonHelper.IsjaCulture() ? TextJa : Text;
            }
        }

        /// <summary>
        /// Gets or sets the sub-lessons which this lesson has.
        /// </summary>
        public List<Lesson> SubLessons { get; set; }

        /// <summary>
        /// Gets or sets the controller name which this lesson page belongs to.
        /// </summary>
        public string ControllerName { get; set; }

        /// <summary>
        /// Gets or sets the action name which this lesson page shows in.
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// Gets or sets the id of the lession.
        /// </summary>
        /// <remarks>
        /// Some lessons link to same action name, use Id property to make the lesson unique. 
        /// </remarks>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the href link for this lesson.
        /// </summary>
        /// <remarks>
        /// If it is not set, it could be obtained from ControllerName and Name.
        /// </remarks>
        public string Href { get; set; }

        /// <summary>
        /// Gets or sets the documentation url for this lesson.
        /// </summary>
        public string Documentation { get; set; }

        /// <summary>
        /// Gets or sets the documentation url of JP culture for this lesson.
        /// </summary>
        public string DocumentationJa { get; set; }

        /// <summary>
        /// Gets or sets whether this lesson is new.
        /// </summary>
        public bool IsNew { get; set; }

        /// <summary>
        /// Gets a text with title format.
        /// </summary>
        /// <param name="text">The original text to be formatted.</param>
        /// <returns>A text with title format.</returns>
        public static string GetTitleText(string text)
        {
            var result = text;
            if (!string.IsNullOrEmpty(text))
            {
                result = "";
                foreach (var ch in text)
                {
                    if (char.IsUpper(ch) && result.Length > 0)
                    {
                        result += " ";
                    }

                    result += ch;
                }
            }
            return result;
        }
    }
}