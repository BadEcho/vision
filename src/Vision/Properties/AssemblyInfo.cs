using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Windows;
#if RELEASE
using BadEcho.Properties;

[assembly: InternalsVisibleTo("BadEcho.Vision.Tests,PublicKey="+BuildInfo.PublicKey)]
#else
[assembly: InternalsVisibleTo("BadEcho.Vision.Tests")]
#endif

[assembly: ThemeInfo(ResourceDictionaryLocation.None,
                     ResourceDictionaryLocation.SourceAssembly)]

[assembly: SuppressMessage("Style", 
                           "IDE0021:Use expression body for constructors",
                           Scope = "member", 
                           Target = "~M:BadEcho.Vision.Windows.VisionWindow.#ctor",
                           Justification = "This constructor makes a call to the base constructor, and expression bodies don't look appetizing at all next to such an invocation.")]

[assembly: SuppressMessage("Maintainability",
                           "CA1515:Consider making public types internal",
                           Scope = "type",
                           Target = "~T:BadEcho.Vision.App",
                           Justification = "WPF App classes are generated as public.")]

[assembly: SuppressMessage("Maintainability",
                           "CA1515:Consider making public types internal",
                           Scope = "type",
                           Target = "~T:BadEcho.Vision.Views.VisionTitleView",
                           Justification = "WPF window classes are generated as public.")]