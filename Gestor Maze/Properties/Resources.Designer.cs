//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gestor_Maze.Properties {
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Gestor_Maze.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to http://api.mazedeve.com/api/.
        /// </summary>
        internal static string baseUrl {
            get {
                return ResourceManager.GetString("baseUrl", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to http://api.mazedeve.com/api/orders/.
        /// </summary>
        internal static string baseUrlorders {
            get {
                return ResourceManager.GetString("baseUrlorders", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to http://api.mazedeve.com/api/products/.
        /// </summary>
        internal static string baseUrlproducts {
            get {
                return ResourceManager.GetString("baseUrlproducts", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to http://api.mazedeve.com/api/tables/.
        /// </summary>
        internal static string baseUrltables {
            get {
                return ResourceManager.GetString("baseUrltables", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to http://api.mazedeve.com/api/users.
        /// </summary>
        internal static string baseUrlusers {
            get {
                return ResourceManager.GetString("baseUrlusers", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Icon similar to (Icon).
        /// </summary>
        internal static System.Drawing.Icon Icon1 {
            get {
                object obj = ResourceManager.GetObject("Icon1", resourceCulture);
                return ((System.Drawing.Icon)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to version 0.0.1.
        /// </summary>
        internal static string version {
            get {
                return ResourceManager.GetString("version", resourceCulture);
            }
        }
    }
}
