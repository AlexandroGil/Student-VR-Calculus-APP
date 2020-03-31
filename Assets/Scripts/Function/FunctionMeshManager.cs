using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FunctionMeshManager : MonoBehaviour
{
    public string expr;
    public double xMin, xMax;
    public double zMin, zMax;
    public int n;

    public bool more = false, less = false;

    private FunctionMeshGenerator fmg;

    public MeshFilter meshFilter;
    public MeshCollider meshCollider;

    void Awake()
    {
        foreach (MeshFilter meshFilter in GameObject.FindObjectsOfType<MeshFilter>())
        {
            meshFilter.mesh.SetIndices(meshFilter.mesh.GetIndices(0).Concat(meshFilter.mesh.GetIndices(0).Reverse()).ToArray(), MeshTopology.Triangles, 0);
        }
        fmg = new FunctionMeshGenerator();
    }

    public void ScaleMesh(bool more) {
        if(more) {
            this.gameObject.transform.localScale += new Vector3(1,1,1);
            more = false;
        } else if(!more) {
            this.gameObject.transform.localScale += new Vector3(-1,-1,-1);
            more = true;
        }

    }
    public void RecalculateMesh()
    {
        fmg.SetBoundaries(xMin, xMax, zMin, zMax);
        fmg.SetN(n);
        fmg.SetExpression(expr);
        fmg.RecalculateMesh();

        var mesh = fmg.GetMesh();

        if (meshFilter != null)
        {
            meshFilter.mesh = null;
            meshFilter.mesh = mesh;
        }

        if (meshCollider != null)
        {
            meshCollider.sharedMesh = null;
            meshCollider.sharedMesh = mesh;
        }
    }
}
