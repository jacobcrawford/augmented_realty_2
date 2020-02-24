using System.Collections;
using System.Collections.Generic;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgcodecsModule;
using OpenCVForUnity.ImgprocModule;
using UnityEngine;
using Vuforia;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]

public class TerrainMesh : MonoBehaviour
{
    // read up on meshes for Unity here:
    // https://docs.unity3d.com/ScriptReference/Mesh.html

    public bool drawGismoz;
    private Mat heightMap;
    private byte[] data = new byte[1];


    public float patch_size;
    private GameObject controllerTarget;
    private GameObject baseTarget;
    private MeshFilter meshFilter;
    private float height;

    public Mat heightmap;

    private Mesh mesh = null;
    private Vector3[] vertices;
    private Color[] colors;
    private Vector2[] uvs;
    private Vector3[] normals;
    private int[] indices;

    public float alpha1 = 0f;

    void Start()
    {
        patch_size = 0.01f;
        controllerTarget = GameObject.Find("ControllerTarget");
        baseTarget = GameObject.Find("BaseTarget");

        meshFilter = GetComponent<MeshFilter>();

        Mat heightMapImg = Imgcodecs.imread("Assets/Textures/HeightMaps/mars_height_2.jpg");

        heightmap = new Mat();

        Imgproc.cvtColor(heightMapImg, heightmap, Imgproc.COLOR_RGB2GRAY);
    }

    void Update()
    {
        makeMesh();

        height = controllerTarget.transform.transform.eulerAngles.y / 360;
        Debug.Log(controllerTarget.transform.transform.eulerAngles.y);
    }


    private void makeMesh()
    {
        // Instantiate the mesh

        if (mesh == null)
        {
            mesh = new Mesh();
        }

        var trackable = controllerTarget.GetComponent<TrackableBehaviour>();

        if (trackable.CurrentStatus != TrackableBehaviour.Status.TRACKED)
        {
            return;
        }

        // get center of controller (x,z)
        float x = controllerTarget.transform.position.x;
        float z = controllerTarget.transform.position.z;

        int rows = (int) System.Math.Ceiling(z / patch_size);
        int columns = (int) System.Math.Ceiling(x / patch_size);

        int size = rows * columns * 2;

        vertices = new Vector3[size];
        colors = new Color[size];
        uvs = new Vector2[size];
        normals = new Vector3[size];

        indices = new int[size * 6];

        // Set the uv coordinates in [0,1]
        float uv_row = 1f / columns;
        float uv_columns = 1f / rows;

        int vertex_nr = 0;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                // Get height from map
                heightmap.get(i, j, data);
                float heightVal = (data[0] - 128) / 255f;

                vertices[vertex_nr] = new Vector3(patch_size * j, heightVal * height, patch_size * i);
                uvs[vertex_nr] = new Vector2(uv_columns * i, uv_row * j);
                vertex_nr += 1;
            }
        }

        int index_nr = 0;
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                // Lower triangle
                indices[index_nr] = r * columns + c;
                index_nr += 1;
                indices[index_nr] = (r + 1) * columns + c;
                index_nr += 1;
                indices[index_nr] = r * columns + c + 1;
                index_nr += 1;

                // Upper triangle
                indices[index_nr] = r * columns + c + 1;
                index_nr += 1;
                indices[index_nr] = (r + 1) * columns + c;
                index_nr += 1;
                indices[index_nr] = (r + 1) * columns + c + 1;
                index_nr += 1;
            }
        }

        // vertex colors
        for (int i = 0; i < size; i++)
        {
            colors[i] = Color.blue; // Not used
            // Only render front of the normals
            normals[i] = Vector3.back;
        }

        // setting the data on the corresponding mesh properties
        mesh.vertices = vertices;
        mesh.colors = colors;
        mesh.normals = normals;
        mesh.uv = uvs;
        mesh.triangles = indices;

        // set the instantiated mesh on the mesh filter for the game object
        meshFilter.mesh = mesh;
    }
}
