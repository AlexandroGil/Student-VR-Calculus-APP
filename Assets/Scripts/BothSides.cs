using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BothSides : MonoBehaviour
{
    // Start is called before the first frame update
    void Start(){
        foreach (MeshFilter meshFilter in GameObject.FindObjectsOfType<MeshFilter>())
        {
            meshFilter.mesh.SetIndices(meshFilter.mesh.GetIndices(0).Concat(meshFilter.mesh.GetIndices(0).Reverse()).ToArray(), MeshTopology.Triangles, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
