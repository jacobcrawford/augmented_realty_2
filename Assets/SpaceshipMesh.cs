using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]

public class SpaceshipMesh : MonoBehaviour
{
    // read up on meshes for Unity here:
    // https://docs.unity3d.com/ScriptReference/Mesh.html

    private MeshFilter meshFilter;

    private static int numberOfVertices = 11;

    private Mesh mesh = null;
    private Vector3[] vertices = new Vector3[numberOfVertices];
    private Color[] colors = new Color[numberOfVertices];
    private Vector2[] uvs = new Vector2[numberOfVertices];
    private Vector3[] normals = new Vector3[numberOfVertices]; // not super important right now
    private int[] indices = new int[30];

    public float alpha1 = 0f;

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
        vertices[0] = new Vector3(0, 0, -0.5f);
        vertices[1] = new Vector3(0, 1, 0);
        vertices[2] = new Vector3(1, 0, 0);
        vertices[3] = new Vector3(-1, 0, -0.5f);
        vertices[4] = new Vector3(-1, 1, 0);
        vertices[5] = new Vector3(-2, 1, 0);
        vertices[6] = new Vector3(0, -1, 0);
        vertices[7] = new Vector3(-1, -1, 0);
        vertices[8] = new Vector3(-2, -1, 0);
        vertices[9] = new Vector3(-2, 2, 0);
        vertices[10] = new Vector3(-2, -2, 0);

        // vertex colors
        for (int i = 0; i < numberOfVertices; i++) {
            colors[i] = Color.blue;;
            // Only render front of the normals
            normals[i] = Vector3.back;
        }

        // Make nose red
        colors[2] = Color.red;
        colors[9] = colors[10] = Color.green;

        // vertex indices - defining an array of all triangles (in this case, only one)
        indices[0] = 0;
        indices[1] = 1;
        indices[2] = 2;

        indices[3] = 0;
        indices[4] = 3;
        indices[5] = 1;

        indices[6] = 3;
        indices[7] = 4;
        indices[8] = 1;

        indices[9] = 3;
        indices[10] = 5;
        indices[11] = 4;

        indices[12] = 0;
        indices[13] = 2;
        indices[14] = 6;

        indices[15] = 0;
        indices[16] = 6;
        indices[17] = 3;

        indices[18] = 3;
        indices[19] = 6;
        indices[20] = 7;

        indices[21] = 3;
        indices[22] = 7;
        indices[23] = 8;

        indices[24] = 4;
        indices[25] = 5;
        indices[26] = 9;

        indices[27] = 7;
        indices[28] = 10;
        indices[29] = 8;


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
