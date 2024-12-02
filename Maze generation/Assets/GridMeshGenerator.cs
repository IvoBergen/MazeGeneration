using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class GridMeshGenerator : MonoBehaviour
{
    public int width = 250; // Number of cells along the x-axis
    public int height = 250; // Number of cells along the z-axis
    public float cellSize = 1f; // Size of each cell

    private Mesh mesh;

    void Start()
    {
        GenerateMesh();
    }

    private void GenerateMesh()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        // Create vertices, triangles, and UVs
        Vector3[] vertices = new Vector3[(width + 1) * (height + 1)];
        int[] triangles = new int[width * height * 6];
        Vector2[] uvs = new Vector2[vertices.Length];

        int vertIndex = 0;
        int triIndex = 0;

        // Generate vertices and UVs
        for (int z = 0; z <= height; z++)
        {
            for (int x = 0; x <= width; x++)
            {
                vertices[vertIndex] = new Vector3(x * cellSize, 0, z * cellSize);
                uvs[vertIndex] = new Vector2((float)x / width, (float)z / height);
                vertIndex++;
            }
        }

        // Generate triangles
        for (int z = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                int topLeft = z * (width + 1) + x;
                int topRight = topLeft + 1;
                int bottomLeft = (z + 1) * (width + 1) + x;
                int bottomRight = bottomLeft + 1;

                // First triangle
                triangles[triIndex++] = topLeft;
                triangles[triIndex++] = bottomLeft;
                triangles[triIndex++] = topRight;

                // Second triangle
                triangles[triIndex++] = topRight;
                triangles[triIndex++] = bottomLeft;
                triangles[triIndex++] = bottomRight;
            }
        }

        // Assign data to the mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;

        // Optimize and recalculate normals
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;

        if (mesh != null && mesh.vertices.Length > 0)
        {
            // Draw grid for visualization in the editor
            foreach (var vertex in mesh.vertices)
            {
                Gizmos.DrawSphere(transform.TransformPoint(vertex), 0.05f);
            }
        }
    }
}
