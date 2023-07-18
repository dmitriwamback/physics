using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;

class Display : GameWindow {
    public Display(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
    {
        Run();
    }


    List<Cloth.Point> samplePoints;
    List<Cloth.Constraint> sampleConstraints;
    Shader shader;

    const float uniformConstraintLength = 0.01f;
    Cloth sampleCloth;

    protected override void OnLoad() {
        base.OnLoad();

        shader = Shader.CreateShader("Blank");

        int pointCount = 45;
        int constraintCount = pointCount - 1;

        samplePoints = new List<Cloth.Point>();
        sampleConstraints = new List<Cloth.Constraint>();

        for (int i = 0; i < pointCount; i++) {

            Cloth.Point point = new Cloth.Point();
            point.position = new Vector2(0.005f * (i), 0.6f);
            point.previousPosition = point.position;
            if (i == 0) point.isMobile = true;
            samplePoints.Add(point);
        }

        for (int i = 0; i < constraintCount; i++) {

            Cloth.Point A = samplePoints[i], B = samplePoints[i+1];
            Cloth.Constraint constraint = new Cloth.Constraint();
            constraint.A = A;
            constraint.B = B;
            constraint.length = uniformConstraintLength * (float)(new Random().NextDouble() * 5f);
            sampleConstraints.Add(constraint);
        }

        GL.Enable(EnableCap.ProgramPointSize);

        sampleCloth = new Cloth();
        sampleCloth.Initialize();
    }

    protected override void OnUpdateFrame(FrameEventArgs args) {
        base.OnUpdateFrame(args);

        float deltaTime = (float)args.Time;
        GL.Clear(ClearBufferMask.ColorBufferBit);
        GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);

        sampleCloth.Update(deltaTime, samplePoints, sampleConstraints);
        sampleCloth.Draw(sampleConstraints, shader);

        SwapBuffers();
    }
}