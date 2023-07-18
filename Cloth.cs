using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

public class Cloth {

    public class Point {

        public Vector2 position, previousPosition;
        public bool isMobile;
    }

    public class Constraint {

        public Point A, B;
        public float length;
    }

    const float gravity = 9.81f;
    const int stableStateIterations = 500;


    int vertexArrayObject, vertexBufferObject;

    public void Initialize() {

        vertexArrayObject = GL.GenVertexArray();
        vertexBufferObject = GL.GenBuffer();
    }

    public void Update(float deltaTime, List<Point> points, List<Constraint> constraints) {

        foreach(Point point in points) {

            if (point.isMobile) continue;

            Vector2 currentPointPosition = point.position;
            point.position += point.position - point.previousPosition;
            point.position += -Vector2.UnitY * gravity * MathF.Pow(deltaTime, 2);
            point.previousPosition = currentPointPosition;
        }

        for (int i = 0; i < stableStateIterations; i++) {
            foreach(Constraint constraint in constraints) {

                Vector2 center = (constraint.A.position + constraint.B.position) / 2f;
                Vector2 direction = (constraint.A.position - constraint.B.position).Normalized();
                if (!constraint.A.isMobile) constraint.A.position = center + direction * constraint.length / 2f;
                if (!constraint.B.isMobile) constraint.B.position = center - direction * constraint.length / 2f;
            }
        }
    }

    public void Draw(List<Constraint> constraints, Shader shader) {

        List<float> tempVertices = new List<float>();

        foreach(Constraint constraint in constraints) {
            
            tempVertices.Add(constraint.A.position.X);
            tempVertices.Add(constraint.A.position.Y);
            tempVertices.Add(constraint.B.position.X);
            tempVertices.Add(constraint.B.position.Y);
        }

        float[] vertices = tempVertices.ToArray();

        GL.BindVertexArray(vertexArrayObject);
        GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.DynamicDraw);

        GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);

        shader.BindShaderProgram();
        Console.WriteLine(vertices.Length);
        GL.DrawArrays(PrimitiveType.Lines, 0, vertices.Length / 2);
        GL.DrawArrays(PrimitiveType.Points, 0, vertices.Length / 2);
    }
}