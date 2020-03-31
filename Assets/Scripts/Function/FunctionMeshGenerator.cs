using System;
using UnityEngine;
using LinqyCalculator;
using System.Linq.Expressions;

public class FunctionMeshGenerator
{
    private Mesh mesh;

    private Vector3[] vertices;
    private int[] triangles;
    private Vector2[] uv;

    private int n;
    private double xMin, xMax;
    private double zMin, zMax;

    private Expression<Func<double>> function;

    public FunctionMeshGenerator()
    {
        mesh = new Mesh();
    }

    public void SetExpression(string expr)
    {
        function = ExpressionParser.ParseExpression(expr);
    }

    public void SetBoundaries(double xMin, double xMax, double zMin, double zMax)
    {
        this.xMin = xMin;
        this.xMax = xMax;
        this.zMin = zMin;
        this.zMax = zMax;
    }

    public void SetN(int n)
    {
        this.n = n;
    }

    public void RecalculateMesh()
    {
        vertices = new Vector3[(n + 1) * (n + 1)];
        uv = new Vector2[vertices.Length];
        triangles = new int[6 * n * n];

        for (int i = 0, zi = 0; zi <= n; zi++)
        {
            for (int xi = 0; xi <= n; xi++)
            {
                ExpressionParser.x = xMin + ((xMax - xMin) * xi) / n;
                ExpressionParser.y = zMin + ((zMax - zMin) * zi) / n;
                double z = function.Compile()();
                if(double.IsNaN(z)){
                    z = 0;
                }

                vertices[i] = new Vector3((float)ExpressionParser.x, (float)z, (float)ExpressionParser.y);
                uv[i] = new Vector2((1.0f * xi) / n, (1.0f * zi) / n);

                i++;
            }
        }

        for (int z = 0, vert = 0, tris = 0; z < n; z++)
        {
            for (int x = 0; x < n; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + n + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + n + 1;
                triangles[tris + 5] = vert + n + 2;

                vert++;
                tris += 6;
            }

            vert++;
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        mesh.RecalculateNormals();
    }

    public Mesh GetMesh()
    {
        return mesh;
    }
}
