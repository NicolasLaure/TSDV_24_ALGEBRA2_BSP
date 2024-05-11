using UnityEngine;
using CustomMath;
public class VectorHandler : MonoBehaviour
{
    public Vec3 A;
    public Vec3 B;
    public Vec3 result;
    private Material mat;

    private void OnEnable()
    {
        Shader shader = Shader.Find("Standard");
        mat = new Material(shader);
    }
    private void OnPostRender()
    {
        mat.SetPass(0);
        GL.PushMatrix();

        DrawLine(new Vec3(0, 0, 0), A, Color.white);
        DrawLine(new Vec3(0, 0, 0), B, Color.white);
        GL.PopMatrix();
    }

    private void DrawLine(Vec3 start, Vec3 end, Color color)
    {
        GL.Begin(GL.LINES);
        GL.Color(color);
        GL.Vertex(start);
        GL.Vertex(end);
        GL.End();
    }
    private void DrawCone()
    {

    }
}
