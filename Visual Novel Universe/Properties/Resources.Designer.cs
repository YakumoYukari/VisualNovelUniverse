﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Visual_Novel_Universe.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Visual_Novel_Universe.Properties.Resources", typeof(Resources).Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Visual Novel Universe is an application for helping you catalogue, maintain, and discover Visual Novels. By using the Visual Novel Database as an engine, it can sort your folders, download relevant info to keep locally, and help you discover new visual novels via enhanced search functionality.\n\nMaintaining Visual Novels:\n1. Go into the file menu and select which directories you wish to have as your base VN directories.\n2. Put all your visual novel folders in there.\n3. Go to Data -&gt; Import VNs and your  [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string HelpText {
            get {
                return ResourceManager.GetString("HelpText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;VN Not Confirmed&gt;.
        /// </summary>
        internal static string MissingDeveloperInfo {
            get {
                return ResourceManager.GetString("MissingDeveloperInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No VN Selected!.
        /// </summary>
        internal static string StatusLabel_NoSelection {
            get {
                return ResourceManager.GetString("StatusLabel_NoSelection", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Not Owned!.
        /// </summary>
        internal static string StatusLabel_NotOwned {
            get {
                return ResourceManager.GetString("StatusLabel_NotOwned", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Owned!.
        /// </summary>
        internal static string StatusLabel_Owned {
            get {
                return ResourceManager.GetString("StatusLabel_Owned", resourceCulture);
            }
        }
    }
}
