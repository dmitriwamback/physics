using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

class Program {


    public void Initialize() {

        GameWindowSettings gameWindowSettings = new GameWindowSettings();

        NativeWindowSettings nativeWindowSettings = new NativeWindowSettings() {
            APIVersion      = new Version(4, 1),
            Profile         = ContextProfile.Core,
            Flags           = ContextFlags.ForwardCompatible,
            Title           = "Atmosphere",
            Size            = new Vector2i(1200, 800),
        };

        new Display(gameWindowSettings, nativeWindowSettings);
    }


    public static void Main() {

        new Program().Initialize();
    }
}