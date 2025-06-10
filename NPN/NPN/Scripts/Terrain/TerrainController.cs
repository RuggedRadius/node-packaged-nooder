using System.Collections.Generic;
using System.Diagnostics;
using System;
using Stride.Engine;
using Stride.Graphics;
using Stride.Rendering;
using Stride.Core.Mathematics;
using NPN.Noise;

namespace NPN.Scripts.Terrain
{
  public class TerrainController : StartupScript
  {
    public NoiseSettings Settings = new();

    private float[,] heightMap;
    private Model model;
    private ModelComponent modelComponent;

    public int Width = 500;
    public int Depth = 500;

    public float MaxHeight = 20f;

    public override void Start()
    {
      Debug.WriteLine("Start() is running");

      var generator = new NoiseGenerator(Settings);

      Debug.WriteLine($"Entity: {Entity.Name}");

      heightMap = new float[Width, Depth];

      for (int x = 0; x < Width; x++)
        for (int z = 0; z < Depth; z++)
        {
          float height = generator.GetNoise(x, z);

          heightMap[x, z] = (height + 1f) * 0.5f * MaxHeight;
        }

      GenerateMesh();
    }

    private void GenerateMesh()
    {
      var vertices = new List<VertexPositionNormalTexture>();
      var indices = new List<int>();

      for (int x = 0; x < Width; x++)
      {
        for (int z = 0; z < Depth; z++)
        {
          float y = heightMap[x, z];

          var position = new Vector3(x, y, z);
          var normal = CalculateNormal(x, z);

          var uv = new Vector2(x / (float)Width, z / (float)Depth);

          vertices.Add(new VertexPositionNormalTexture(position, normal, uv));
        }
      }

      for (int x = 0; x < Width - 1; x++)
      {
        for (int z = 0; z < Depth - 1; z++)
        {
          int topLeft = x + z * Width;
          int topRight = (x + 1) + z * Width;
          int bottomLeft = x + (z + 1) * Width;
          int bottomRight = (x + 1) + (z + 1) * Width;

          indices.Add(topLeft);
          indices.Add(bottomLeft);
          indices.Add(topRight);

          indices.Add(topRight);
          indices.Add(bottomLeft);
          indices.Add(bottomRight);
        }
      }

      var indexBuffer = Stride.Graphics.Buffer.Index.New(GraphicsDevice, indices.ToArray());
      var indexBufferBinding = new IndexBufferBinding(indexBuffer, true, indices.Count);

      var verticesBuffer = Stride.Graphics.Buffer.Index.New(GraphicsDevice, vertices.ToArray());

      var meshDraw = new MeshDraw
      {
        PrimitiveType = PrimitiveType.TriangleList,
        DrawCount = indices.Count,
        IndexBuffer = indexBufferBinding,
        VertexBuffers = new[] {
            new VertexBufferBinding(verticesBuffer, VertexPositionNormalTexture.Layout, vertices.Count, -1, 0) // true)
          }
      };

      var mesh = new Mesh { Draw = meshDraw };

      model = new Model();
      model.Meshes.Add(mesh);

      modelComponent = new ModelComponent(model);
      Entity.Add(modelComponent);
    }

    public void Update()
    {
    }

    private Vector3 CalculateNormal(int x, int z)
    {
      float hL = x > 0 ? heightMap[x - 1, z] : heightMap[x, z];
      float hR = x < Width - 1 ? heightMap[x + 1, z] : heightMap[x, z];
      float hD = z > 0 ? heightMap[x, z - 1] : heightMap[x, z];
      float hU = z < Depth - 1 ? heightMap[x, z + 1] : heightMap[x, z];

      var normal = new Vector3(hL - hR, 2f, hD - hU);
      normal.Normalize();
      return normal;
    }
  }
}