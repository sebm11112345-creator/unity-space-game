using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Procedural Mesh Builder - Creates smooth, medium-poly geometric shapes for spaceship construction
/// Handles mesh generation, normal calculation, and vertex management
/// </summary>
public class ProceduralMeshBuilder
{
    private List<Vector3> vertices = new List<Vector3>();
    private List<int> triangles = new List<int>();
    private List<Vector2> uvs = new List<Vector2>();
    private List<Vector3> normals = new List<Vector3>();

    public Mesh Build()
    {
        Mesh mesh = new Mesh();
        mesh.name = "ProceduralMesh";
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.RecalculateTangents();
        return mesh;
    }

    public void Clear()
    {
        vertices.Clear();
        triangles.Clear();
        uvs.Clear();
        normals.Clear();
    }

    /// <summary>
    /// Creates a smooth cylinder with configurable segments for medium-poly quality
    /// </summary>
    public void AddCylinder(Vector3 position, float radius, float height, int segments = 16, bool capped = true)
    {
        int vertexOffset = vertices.Count;
        float angleStep = 360f / segments * Mathf.Deg2Rad;

        // Create top and bottom center vertices
        int topCenter = vertices.Count;
        vertices.Add(position + Vector3.up * height * 0.5f);
        uvs.Add(new Vector2(0.5f, 1f));

        int bottomCenter = vertices.Count;
        vertices.Add(position - Vector3.up * height * 0.5f);
        uvs.Add(new Vector2(0.5f, 0f));

        // Create side vertices (two rings - top and bottom)
        for (int i = 0; i < segments; i++)
        {
            float angle = i * angleStep;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;

            // Top ring
            Vector3 topPos = position + new Vector3(x, height * 0.5f, z);
            vertices.Add(topPos);
            uvs.Add(new Vector2((float)i / segments, 1f));

            // Bottom ring
            Vector3 bottomPos = position + new Vector3(x, -height * 0.5f, z);
            vertices.Add(bottomPos);
            uvs.Add(new Vector2((float)i / segments, 0f));
        }

        // Create side faces
        for (int i = 0; i < segments; i++)
        {
            int topA = vertexOffset + 2 + (i * 2);
            int bottomA = vertexOffset + 3 + (i * 2);
            int topB = vertexOffset + 2 + (((i + 1) % segments) * 2);
            int bottomB = vertexOffset + 3 + (((i + 1) % segments) * 2);

            // Two triangles per segment
            triangles.Add(topA);
            triangles.Add(bottomA);
            triangles.Add(topB);

            triangles.Add(topB);
            triangles.Add(bottomA);
            triangles.Add(bottomB);
        }

        // Add caps if requested
        if (capped)
        {
            // Top cap
            for (int i = 0; i < segments; i++)
            {
                int topVertex = vertexOffset + 2 + (i * 2);
                int nextTopVertex = vertexOffset + 2 + (((i + 1) % segments) * 2);
                triangles.Add(topCenter);
                triangles.Add(nextTopVertex);
                triangles.Add(topVertex);
            }

            // Bottom cap
            for (int i = 0; i < segments; i++)
            {
                int bottomVertex = vertexOffset + 3 + (i * 2);
                int nextBottomVertex = vertexOffset + 3 + (((i + 1) % segments) * 2);
                triangles.Add(bottomCenter);
                triangles.Add(bottomVertex);
                triangles.Add(nextBottomVertex);
            }
        }
    }

    /// <summary>
    /// Creates a smooth box/rectangular prism for ship hulls
    /// </summary>
    public void AddBox(Vector3 position, Vector3 size, int segmentsX = 2, int segmentsY = 2, int segmentsZ = 2)
    {
        Vector3 halfSize = size * 0.5f;
        int vertexOffset = vertices.Count;

        // Front face
        AddQuad(position + new Vector3(-halfSize.x, -halfSize.y, halfSize.z),
                position + new Vector3(halfSize.x, -halfSize.y, halfSize.z),
                position + new Vector3(halfSize.x, halfSize.y, halfSize.z),
                position + new Vector3(-halfSize.x, halfSize.y, halfSize.z),
                segmentsX, segmentsY, Vector3.forward);

        // Back face
        AddQuad(position + new Vector3(halfSize.x, -halfSize.y, -halfSize.z),
                position + new Vector3(-halfSize.x, -halfSize.y, -halfSize.z),
                position + new Vector3(-halfSize.x, halfSize.y, -halfSize.z),
                position + new Vector3(halfSize.x, halfSize.y, -halfSize.z),
                segmentsX, segmentsY, Vector3.back);

        // Right face
        AddQuad(position + new Vector3(halfSize.x, -halfSize.y, -halfSize.z),
                position + new Vector3(halfSize.x, -halfSize.y, halfSize.z),
                position + new Vector3(halfSize.x, halfSize.y, halfSize.z),
                position + new Vector3(halfSize.x, halfSize.y, -halfSize.z),
                segmentsZ, segmentsY, Vector3.right);

        // Left face
        AddQuad(position + new Vector3(-halfSize.x, -halfSize.y, halfSize.z),
                position + new Vector3(-halfSize.x, -halfSize.y, -halfSize.z),
                position + new Vector3(-halfSize.x, halfSize.y, -halfSize.z),
                position + new Vector3(-halfSize.x, halfSize.y, halfSize.z),
                segmentsZ, segmentsY, Vector3.left);

        // Top face
        AddQuad(position + new Vector3(-halfSize.x, halfSize.y, halfSize.z),
                position + new Vector3(halfSize.x, halfSize.y, halfSize.z),
                position + new Vector3(halfSize.x, halfSize.y, -halfSize.z),
                position + new Vector3(-halfSize.x, halfSize.y, -halfSize.z),
                segmentsX, segmentsZ, Vector3.up);

        // Bottom face
        AddQuad(position + new Vector3(-halfSize.x, -halfSize.y, -halfSize.z),
                position + new Vector3(halfSize.x, -halfSize.y, -halfSize.z),
                position + new Vector3(halfSize.x, -halfSize.y, halfSize.z),
                position + new Vector3(-halfSize.x, -halfSize.y, halfSize.z),
                segmentsX, segmentsZ, Vector3.down);
    }

    /// <summary>
    /// Adds a subdivided quad face to the mesh
    /// </summary>
    private void AddQuad(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4, int segX, int segY, Vector3 normal)
    {
        int vertexOffset = vertices.Count;

        // Create grid of vertices
        for (int y = 0; y <= segY; y++)
        {
            for (int x = 0; x <= segX; x++)
            {
                float xLerp = (float)x / segX;
                float yLerp = (float)y / segY;

                Vector3 pos = Vector3.Lerp(
                    Vector3.Lerp(v1, v2, xLerp),
                    Vector3.Lerp(v4, v3, xLerp),
                    yLerp
                );

                vertices.Add(pos);
                uvs.Add(new Vector2(xLerp, yLerp));
                normals.Add(normal);
            }
        }

        // Create triangles from grid
        for (int y = 0; y < segY; y++)
        {
            for (int x = 0; x < segX; x++)
            {
                int a = vertexOffset + y * (segX + 1) + x;
                int b = a + 1;
                int c = a + (segX + 1);
                int d = c + 1;

                triangles.Add(a);
                triangles.Add(c);
                triangles.Add(b);

                triangles.Add(b);
                triangles.Add(c);
                triangles.Add(d);
            }
        }
    }

    /// <summary>
    /// Creates a cone shape for ship noses/boosters
    /// </summary>
    public void AddCone(Vector3 position, float baseRadius, float height, int segments = 12)
    {
        int vertexOffset = vertices.Count;
        float angleStep = 360f / segments * Mathf.Deg2Rad;

        // Apex
        int apex = vertices.Count;
        vertices.Add(position + Vector3.up * height);
        uvs.Add(new Vector2(0.5f, 1f));

        // Base center
        int baseCenter = vertices.Count;
        vertices.Add(position);
        uvs.Add(new Vector2(0.5f, 0f));

        // Base ring
        for (int i = 0; i < segments; i++)
        {
            float angle = i * angleStep;
            float x = Mathf.Cos(angle) * baseRadius;
            float z = Mathf.Sin(angle) * baseRadius;
            vertices.Add(position + new Vector3(x, 0, z));
            uvs.Add(new Vector2((float)i / segments, 0f));
        }

        // Side triangles
        for (int i = 0; i < segments; i++)
        {
            int baseVertex = vertexOffset + 2 + i;
            int nextBaseVertex = vertexOffset + 2 + ((i + 1) % segments);
            triangles.Add(apex);
            triangles.Add(baseVertex);
            triangles.Add(nextBaseVertex);
        }

        // Base cap
        for (int i = 0; i < segments; i++)
        {
            int baseVertex = vertexOffset + 2 + i;
            int nextBaseVertex = vertexOffset + 2 + ((i + 1) % segments);
            triangles.Add(baseCenter);
            triangles.Add(nextBaseVertex);
            triangles.Add(baseVertex);
        }
    }

    /// <summary>
    /// Creates a sphere for cockpits and domes
    /// </summary>
    public void AddSphere(Vector3 position, float radius, int latitudeSegments = 12, int longitudeSegments = 24)
    {
        int vertexOffset = vertices.Count;

        // Create vertices
        for (int lat = 0; lat <= latitudeSegments; lat++)
        {
            float phi = Mathf.PI * lat / latitudeSegments;
            float sinPhi = Mathf.Sin(phi);
            float cosPhi = Mathf.Cos(phi);

            for (int lon = 0; lon <= longitudeSegments; lon++)
            {
                float theta = 2 * Mathf.PI * lon / longitudeSegments;
                float sinTheta = Mathf.Sin(theta);
                float cosTheta = Mathf.Cos(theta);

                Vector3 pos = position + new Vector3(
                    radius * sinPhi * cosTheta,
                    radius * cosPhi,
                    radius * sinPhi * sinTheta
                );

                vertices.Add(pos);
                uvs.Add(new Vector2((float)lon / longitudeSegments, (float)lat / latitudeSegments));
            }
        }

        // Create triangles
        for (int lat = 0; lat < latitudeSegments; lat++)
        {
            for (int lon = 0; lon < longitudeSegments; lon++)
            {
                int a = vertexOffset + lat * (longitudeSegments + 1) + lon;
                int b = a + 1;
                int c = a + (longitudeSegments + 1);
                int d = c + 1;

                triangles.Add(a);
                triangles.Add(c);
                triangles.Add(b);
                triangles.Add(b);
                triangles.Add(c);
                triangles.Add(d);
            }
        }
    }

    /// <summary>
    /// Creates a torus/ring shape for orbital mechanics visuals or design elements
    /// </summary>
    public void AddTorus(Vector3 position, float majorRadius, float minorRadius, int majorSegments = 24, int minorSegments = 12)
    {
        int vertexOffset = vertices.Count;

        for (int i = 0; i < majorSegments; i++)
        {
            float theta = 2 * Mathf.PI * i / majorSegments;
            float cosTheta = Mathf.Cos(theta);
            float sinTheta = Mathf.Sin(theta);

            for (int j = 0; j < minorSegments; j++)
            {
                float phi = 2 * Mathf.PI * j / minorSegments;
                float cosPhi = Mathf.Cos(phi);
                float sinPhi = Mathf.Sin(phi);

                Vector3 pos = position + new Vector3(
                    (majorRadius + minorRadius * cosPhi) * cosTheta,
                    minorRadius * sinPhi,
                    (majorRadius + minorRadius * cosPhi) * sinTheta
                );

                vertices.Add(pos);
                uvs.Add(new Vector2((float)i / majorSegments, (float)j / minorSegments));
            }
        }

        // Create triangles
        for (int i = 0; i < majorSegments; i++)
        {
            for (int j = 0; j < minorSegments; j++)
            {
                int a = vertexOffset + i * minorSegments + j;
                int b = vertexOffset + ((i + 1) % majorSegments) * minorSegments + j;
                int c = a + 1 >= vertexOffset + (i + 1) * minorSegments ? vertexOffset + i * minorSegments : a + 1;
                int d = b + 1 >= vertexOffset + ((i + 2) % majorSegments) * minorSegments ? vertexOffset + ((i + 1) % majorSegments) * minorSegments : b + 1;

                triangles.Add(a);
                triangles.Add(b);
                triangles.Add(c);
                triangles.Add(b);
                triangles.Add(d);
                triangles.Add(c);
            }
        }
    }

    /// <summary>
    /// Combines two meshes into one at specified positions
    /// </summary>
    public static Mesh CombineMeshes(Mesh mesh1, Mesh mesh2, Vector3 mesh2Offset)
    {
        ProceduralMeshBuilder builder = new ProceduralMeshBuilder();
        
        // Add first mesh
        builder.vertices.AddRange(mesh1.vertices);
        builder.uvs.AddRange(mesh1.uv);
        int offset1 = mesh1.vertices.Length;
        foreach (var tri in mesh1.triangles)
        {
            builder.triangles.Add(tri);
        }

        // Add second mesh with offset
        foreach (var vertex in mesh2.vertices)
        {
            builder.vertices.Add(vertex + mesh2Offset);
        }
        builder.uvs.AddRange(mesh2.uv);
        foreach (var tri in mesh2.triangles)
        {
            builder.triangles.Add(tri + offset1);
        }

        return builder.Build();
    }
}
