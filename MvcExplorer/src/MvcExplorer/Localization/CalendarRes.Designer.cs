﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcExplorer.Localization {
    using System;
    using System.Reflection;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class CalendarRes {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        internal CalendarRes() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MvcExplorer.Localization.CalendarRes", typeof(CalendarRes).GetTypeInfo().Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to MMM d, yyyy.
        /// </summary>
        public static string Index_DateFormat {
            get {
                return ResourceManager.GetString("Index_DateFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;i&gt;RepeatButtons&lt;/i&gt; = True: the calendar buttons (Previous and Next buttons) should act as repeat buttons, firing repeatedly as the button remains pressed..
        /// </summary>
        public static string Index_RepeatButtons {
            get {
                return ResourceManager.GetString("Index_RepeatButtons", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;i&gt;ShowYearPicker&lt;/i&gt; = True: the calendar should display a list of years when the user clicks the header element on the year calendar..
        /// </summary>
        public static string Index_ShowYearPicker {
            get {
                return ResourceManager.GetString("Index_ShowYearPicker", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This sample shows the basic usage of the Calendar control..
        /// </summary>
        public static string Index_Text0 {
            get {
                return ResourceManager.GetString("Index_Text0", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to to.
        /// </summary>
        public static string Index_To {
            get {
                return ResourceManager.GetString("Index_To", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Valid Range:.
        /// </summary>
        public static string Index_ValidRange {
            get {
                return ResourceManager.GetString("Index_ValidRange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Use this &lt;b&gt;Calendar&lt;/b&gt; control to select a date. 
        ///    Notice you won&apos;t be able to select weekends..
        /// </summary>
        public static string Validation_Text0 {
            get {
                return ResourceManager.GetString("Validation_Text0", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This sample shows how to set &lt;b&gt;item-validator&lt;/b&gt; attribute to a function to determine whether dates
        ///    are valid for selection..
        /// </summary>
        public static string Validation_Text1 {
            get {
                return ResourceManager.GetString("Validation_Text1", resourceCulture);
            }
        }
    }
}
