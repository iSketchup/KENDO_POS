using Avalonia;
using System;
using DotNetEnv;
using Avalonia.OpenGL;

namespace Main;

sealed class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);

    // Avalonia configuration, don't remove; also used by visual designer.

    public static AppBuilder BuildAvaloniaApp()
    {
        Env.TraversePath().Load();
        var builder = AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();

        if (OperatingSystem.IsWindows())
        {
            builder = builder.With(new Win32PlatformOptions
            {
                RenderingMode = new[]
                {
                    Win32RenderingMode.Wgl,
                    Win32RenderingMode.AngleEgl,
                    Win32RenderingMode.Software
                },

                WglProfiles = new[]
                {
                    new GlVersion(GlProfileType.OpenGL, 4, 6),
                    new GlVersion(GlProfileType.OpenGL, 4, 1),
                    new GlVersion(GlProfileType.OpenGL, 3, 3)
                }
            });
        }
        
#if DEBUG
    builder = builder.WithDeveloperTools();
#endif

    return builder;
    }
}