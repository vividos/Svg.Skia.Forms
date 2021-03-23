using System.Diagnostics.CodeAnalysis;
using UIKit;

[assembly: SuppressMessage(
    "StyleCop.CSharp.NamingRules",
    "SA1300:Element should begin with upper-case letter",
    Justification = "iOS is a proper noun",
    Scope = "namespace",
    Target = "~N:Svg.Skia.Forms.Sample.iOS")]

namespace Svg.Skia.Forms.Sample.iOS
{
    /// <summary>
    /// iOS application class
    /// </summary>
    public static class Application
    {
        /// <summary>
        /// This is the main entry point of the application.
        /// </summary>
        /// <param name="args">command line args</param>
        private static void Main(string[] args)
        {
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}
