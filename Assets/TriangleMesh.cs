using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]

public class TriangleMesh : MonoBehaviour
{
    // read up on meshes for Unity here:
    // https://docs.unity3d.com/ScriptReference/Mesh.html

    private MeshFilter meshFilter;

    private Mesh mesh = null;
    private Vector3[] vertices = new Vector3[3];
    private Color[] colors = new Color[3];
    private Vector2[] uvs = new Vector2[3];
    private Vector3[] normals = new Vector3[3]; // not super important right now
    private int[] indices = new int[3];

    public float alpha = 1.0f;

    void Start()
    {      

        // Instantiate the mesh

        meshFilter = GetComponent<MeshFilter>();

        if (mesh == null)
        {
            mesh = new Mesh();
        }        

        // set data values per vertex

        // vertex positions
        vertices[0] = new Vector3(0, 0, 0);
        vertices[1] = new Vector3(0, 1, 0);
        vertices[2] = new Vector3(1, 0, 0);

        // vertex UV coordinates (for texture mapping)        
        uvs[0] = new Vector2(0, 0);
        uvs[1] = new Vector2(0, 1);
        uvs[2] = new Vector2(1, 0);

        // vertex normals
        // the surface normal of the triangle becomes the average
        normals[0] = Vector3.back;        
        normals[1] = Vector3.back; 
        normals[2] = Vector3.back; 

        // vertex colors
        colors[0] = Color.red;
        colors[1] = Color.green;
        colors[2] = Color.blue;
        colors[0].a = alpha;

        // vertex indices - defining an array of all triangles (in this case, only one)
        indices[0] = 0;
        indices[1] = 1;
        indices[2] = 2;

        // setting the data on the corresponding mesh properties
        mesh.vertices = vertices;
        mesh.colors = colors;
        mesh.normals = normals;
        mesh.uv = uvs;
        mesh.triangles = indices;


        // set the instantiated mesh on the mesh filter for the game object
        meshFilter.mesh = mesh;
    }

    void Update()
    {       
    }
}
