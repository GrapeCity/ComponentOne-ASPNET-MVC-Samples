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
    ///    A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class InputDateTimeRes {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        internal InputDateTimeRes() {
        }
        
        /// <summary>
        ///    Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MvcExplorer.Localization.InputDateTimeRes", typeof(InputDateTimeRes).GetTypeInfo().Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///    Overrides the current thread's CurrentUICulture property for all
        ///    resource lookups using this strongly typed resource class.
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
        ///    Looks up a localized string similar to Select a date and a time.
        /// </summary>
        public static string Index_Text0 {
            get {
                return ResourceManager.GetString("Index_Text0", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to This sample shows the basic usage of the InputDateTime control..
        /// </summary>
        public static string Index_Text1 {
            get {
                return ResourceManager.GetString("Index_Text1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select a date and a time.
        /// </summary>
        public static string Validation_Text0 {
            get {
                return ResourceManager.GetString("Validation_Text0", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This sample shows how to implement validation in the InputDateTime control. The &lt;b&gt;ItemValidator&lt;/b&gt; function is used
        ///    to determine whether dates are valid for selection and typing..
        /// </summary>
        public static string Validation_Text1 {
            get {
                return ResourceManager.GetString("Validation_Text1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to It also show how to use InvalidInput event to check invalid date time and keep focus for correcting it..
        /// </summary>
        public static string Validation_Text2 {
            get {
                return ResourceManager.GetString("Validation_Text2", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The following sample checks invalid date and time using InvalidInput event..
        /// </summary>
        public static string Validation_Text3 {
            get {
                return ResourceManager.GetString("Validation_Text3", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid input, the value must be date and time in the current year..
        /// </summary>
        public static string Validation_Text4 {
            get {
                return ResourceManager.GetString("Validation_Text4", resourceCulture);
            }
        }
    }
}
