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
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class TabOrderRes {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal TabOrderRes() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MvcExplorer.Localization.TabOrderRes", typeof(TabOrderRes).Assembly);
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
        ///   Looks up a localized string similar to This view shows how to use TabOrder for controls.
        /// </summary>
        public static string TabOrder_Description_Text0 {
            get {
                return ResourceManager.GetString("TabOrder_Description_Text0", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;b&gt;TabIndex&lt;/b&gt; attribute value can be defined statically for a Wijmo control by specifying it on the control&apos;s host HTML element. But this value can&apos;t be changed later during application lifecycle, because Wijmo controls have complex structure, and the control may need to propagate this attribute value to its internal element to work properly..
        /// </summary>
        public static string TabOrder_Description_Text1 {
            get {
                return ResourceManager.GetString("TabOrder_Description_Text1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Because of this, to read or change control&apos;s tabindex dynamically, you should do it using &lt;b&gt;TabOrder&lt;/b&gt; property..
        /// </summary>
        public static string TabOrder_Description_Text2 {
            get {
                return ResourceManager.GetString("TabOrder_Description_Text2", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This view shows how to use TabOrder..
        /// </summary>
        public static string TabOrder_Text0 {
            get {
                return ResourceManager.GetString("TabOrder_Text0", resourceCulture);
            }
        }
    }
}
