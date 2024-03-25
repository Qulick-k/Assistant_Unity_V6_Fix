using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{

    //透過 Dimensions在Start()生成 Mesh。
    public int Dimension = 10;
    public float UVScale;
    public Octave[] Octaves;

    //為了生成Mesh還需要設置兩個變數
    protected MeshFilter MeshFilter;
    protected Mesh Mesh;


    // Start is called before the first frame update
    void Start()
    {
        //Mesh的設置
        Mesh = new Mesh(); //賦予Mesh內容給變數Mesh
        Mesh.name = gameObject.name;

        Mesh.vertices = GenerateVerts();
        Mesh.triangles = GenerateTries();
        Mesh.uv = GenerateUVs();
        Mesh.RecalculateBounds();
        Mesh.RecalculateNormals();

        MeshFilter = gameObject.AddComponent<MeshFilter>();
        MeshFilter.mesh = Mesh;
    }

    public float GetHeight(Vector3 position)
    {
        //scale factor and position in local space
        var scale = new Vector3(1 / transform.lossyScale.x, 0, 1 / transform.lossyScale.z);
        var localPos = Vector3.Scale((position - transform.position), scale);

        //get edge points
        var p1 = new Vector3(Mathf.Floor(localPos.x), 0, Mathf.Floor(localPos.z));
        var p2 = new Vector3(Mathf.Floor(localPos.x), 0, Mathf.Ceil(localPos.z));
        var p3 = new Vector3(Mathf.Ceil(localPos.x), 0, Mathf.Floor(localPos.z));
        var p4 = new Vector3(Mathf.Ceil(localPos.x), 0, Mathf.Ceil(localPos.z));

        //clamp if the position is outside the plane
        p1.x = Mathf.Clamp(p1.x, 0, Dimension);
        p1.z = Mathf.Clamp(p1.z, 0, Dimension);
        p2.x = Mathf.Clamp(p2.x, 0, Dimension);
        p2.z = Mathf.Clamp(p2.z, 0, Dimension);
        p3.x = Mathf.Clamp(p3.x, 0, Dimension);
        p3.z = Mathf.Clamp(p3.z, 0, Dimension);
        p4.x = Mathf.Clamp(p4.x, 0, Dimension);
        p4.z = Mathf.Clamp(p4.z, 0, Dimension);

        //get the max distance to one of the edges and take that to compute max - dist
        var max = Mathf.Max(Vector3.Distance(p1, localPos), Vector3.Distance(p2, localPos), Vector3.Distance(p3, localPos), Vector3.Distance(p4, localPos) + Mathf.Epsilon);
        var dist = (max - Vector3.Distance(p1, localPos))
                 + (max - Vector3.Distance(p2, localPos))
                 + (max - Vector3.Distance(p3, localPos))
                 + (max - Vector3.Distance(p4, localPos) + Mathf.Epsilon);
        //weight sum
        var height = Mesh.vertices[index(p1.x, p1.z)].y * (max - Vector3.Distance(p1, localPos))
                   + Mesh.vertices[index(p2.x, p2.z)].y * (max - Vector3.Distance(p2, localPos))
                   + Mesh.vertices[index(p3.x, p3.z)].y * (max - Vector3.Distance(p3, localPos))
                   + Mesh.vertices[index(p4.x, p4.z)].y * (max - Vector3.Distance(p4, localPos));

        //scale
        return height * transform.lossyScale.y / dist;
    }

    private Vector3[] GenerateVerts()
    {
        var verts = new Vector3[(Dimension + 1) * (Dimension + 1)];      //  11 * 11 個頂點的陣列

        //equaly distributed verts 同等的分布頂點
        for (int x = 0; x <= Dimension; x++)
        {
            for(int z = 0; z <= Dimension; z++)
            {
                verts[index(x, z)] = new Vector3(x, 0, z);  //賦予每一輪的Vector3(x, 0, z))給verts頂點陣列中的Index(x, z)方法
            }
        }

        return verts;
    }

    private int index(int x, int z)
    {
        //x = 0 z = 0 就回傳0  ， x = 0 z = 9 就回傳9 ， x = 0 z = 10 就回傳10 ， x = 1 z = 0 就回傳11，因為有11個頂點
        return x * (Dimension + 1) + z;
    }
    private int index(float x, float z)
    {
        return index((int)x, (int)z);
    }

    private int[] GenerateTries()
    {
        var tries = new int[Mesh.vertices.Length * 6]; //一個正方形，需要分成兩個三角形，因此總共需要6個邊長

        //two triangle are one tile 2個三角形組合成一個方形
        for(int x = 0; x < Dimension; x++)
        {
            for(int z = 0; z < Dimension; z++)
            {
                tries[index(x, z) * 6 + 0] = index(x, z);
                tries[index(x, z) * 6 + 1] = index(x + 1, z + 1);
                tries[index(x, z) * 6 + 2] = index(x + 1, z);
                tries[index(x, z) * 6 + 3] = index(x, z);
                tries[index(x, z) * 6 + 4] = index(x, z + 1);
                tries[index(x, z) * 6 + 5] = index(x + 1, z + 1);
            }
        }

        return tries;
    }

    private Vector2[] GenerateUVs()
    {
        var uvs = new Vector2[Mesh.vertices.Length];

        //always set one uv over n tiles than flip the uv and set it again
        for (int x = 0; x <= Dimension; x++)
        {
            for (int z = 0; z <= Dimension; z++)
            {
                var vec = new Vector2((x / UVScale) % 2, (z / UVScale) % 2);
                uvs[index(x, z)] = new Vector2(vec.x <= 1 ? vec.x : 2 - vec.x, vec.y <= 1 ? vec.y : 2 - vec.y);
            }
        }

        return uvs;
    }

    // Update is called once per frame
    void Update()
    {
        var verts = Mesh.vertices;    //先抓取我們自己設置的頂點，賦予到變數verts上
        for (int x = 0; x <= Dimension; x++) //訪問所有頂點，並且設置高度參數
        {
            for(int z = 0; z <= Dimension; z++)
            {
                var y = 0f;
                for(int o = 0; o < Octaves.Length; o++)
                {
                    if (Octaves[o].alternate)  //如果alternate為true
                    {
                        var perl = Mathf.PerlinNoise((x * Octaves[o].scale.x) / Dimension, (z * Octaves[o].scale.y) / Dimension) * Mathf.PI * 2f;
                        y += Mathf.Cos(perl + Octaves[o].speed.magnitude * Time.time) * Octaves[o].height;  //透過Cos和經過的時間，讓magnitude去乘與height
                    }
                    else
                    {
                        var perl = Mathf.PerlinNoise((x * Octaves[o].scale.x + Time.time * Octaves[o].speed.x) / Dimension, (z * Octaves[o].scale.y + Time.time * Octaves[o].speed.y) / Dimension) - 0.5f;
                        y += perl * Octaves[o].height;
                    }
                }

                verts[index(x, z)] = new Vector3(x, y, z);
            }
        }

        Mesh.vertices = verts;
        Mesh.RecalculateNormals();
    }


    [Serializable]
    public struct Octave
    {
        public Vector2 speed;
        public Vector2 scale;
        public float height;
        public bool alternate;
    }
}
