using UnityEngine;
using System.Collections;
using System.IO;

namespace PointCloud
{
    public class PointCloud2Prefab
    {
        // File
        private Material _matVertex;

        // PointCloud
        private GameObject _pointCloud;

        // have in mind that you have a limit of 65k points
        // per mesh, so if you want to render more points, 
        // you need to split them.
        private int _limitPoints = 65000;

        public PointCloud2Prefab(PointCloud<PointXYZRGB> cloud, int idx, Material matVertex)
        {
            Debug.Log("PointCloud2Prefab constructor");
            _matVertex = matVertex;
            CreateFolders();
            Load(cloud, idx);
        }

        public GameObject GetPrefab()
        {
            return _pointCloud;
        }

        private void CreateFolders()
        {
            Debug.Log("PointCloud2Prefab CreateFolders");
            if (!Directory.Exists(Application.dataPath + "/Temp/"))
            {
                Debug.Log("Checking if folder " + Application.dataPath + "/Temp/" + " exists...");
                UnityEditor.AssetDatabase.CreateFolder("Assets", "Temp");
            }
            if (!Directory.Exists(Application.dataPath + "/Temp/Clouds/"))
            {
                UnityEditor.AssetDatabase.CreateFolder("Assets/Temp", "Clouds");
            }
        }

        private void Load(PointCloud<PointXYZRGB> cloud, int global_idx)
        {
            Debug.Log("PointCloud2Prefab Load");
            Vector3[] points = new Vector3[cloud.Size];
            Color[] colors = new Color[cloud.Size];

            for (int i = 0; i < cloud.Size; i++)
            {
                points[i] = new Vector3(cloud[i].X, -cloud[i].Z, cloud[i].Y);
                colors[i] = new Color((float)cloud[i].R / 255.0f, (float)cloud[i].G / 255.0f, (float)cloud[i].B / 255.0f);

                //if ( i % 1000 == 0)
                //{
                //    yield return null;
                //}
            }

            // Instantiate Point Groups
            int numGroups = Mathf.CeilToInt(cloud.Size * 1.0f / _limitPoints * 1.0f);

            _pointCloud = new GameObject("cloud_" + global_idx.ToString());

            for (int i = 0; i < numGroups - 1; i++)
            {
                InstantiateMesh(points, colors, global_idx, i);
                if (i % 10 == 0)
                {
                    Debug.Log(i.ToString() + " out of " + numGroups.ToString() + " PointGroups loaded");
                }
                //yield return null;
            }

            //Store PointCloud
            Debug.Log("CreatePrefab...");
            UnityEditor.PrefabUtility.CreatePrefab("Assets/Temp/Clouds/" + "cloud_" + global_idx.ToString() + ".prefab", _pointCloud);
            Debug.Log("return");
        }

        private void InstantiateMesh(Vector3[] points, Color[] colors, int global_idx, int local_idx)
        {
            Debug.Log("InstantiateMesh");
            // Create Mesh
            GameObject pointGroup = new GameObject("cloud_" + global_idx.ToString() + "_" + local_idx);
            pointGroup.AddComponent<MeshFilter>();
            pointGroup.AddComponent<MeshRenderer>();
            pointGroup.GetComponent<Renderer>().material = _matVertex;

            pointGroup.GetComponent<MeshFilter>().mesh = CreateMesh(points, colors, local_idx);
            pointGroup.transform.parent = _pointCloud.transform;

            // Store Mesh
            if (!Directory.Exists(Application.dataPath + "/Temp/Clouds/" + "cloud_" + global_idx.ToString()))
            {
                UnityEditor.AssetDatabase.CreateFolder("Assets/Temp/Clouds", "cloud_" + global_idx.ToString());
            }
            UnityEditor.AssetDatabase.CreateAsset(pointGroup.GetComponent<MeshFilter>().mesh, "Assets/Temp/Clouds/" + "cloud_" + global_idx.ToString() + @"/" + "cloud_" + global_idx.ToString() + "_" + local_idx + ".asset");
            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.AssetDatabase.Refresh();
        }

        private Mesh CreateMesh(Vector3[] points, Color[] colors, int id)
        {
            Debug.Log("CreateMesh");
            Mesh mesh = new Mesh();

            int nPoints = _limitPoints;
            if (_limitPoints * (id + 1) > points.Length)
                nPoints = points.Length - _limitPoints * id;

            Vector3[] localPoints = new Vector3[nPoints];
            int[] localIndices = new int[nPoints];
            Color[] localColors = new Color[nPoints];

            for (int i = 0; i < nPoints; ++i)
            {
                localPoints[i] = points[id * _limitPoints + i];
                localIndices[i] = i;
                localColors[i] = colors[id * _limitPoints + i];
            }

            mesh.vertices = localPoints;
            mesh.colors = localColors;
            mesh.SetIndices(localIndices, MeshTopology.Points, 0);
            mesh.uv = new Vector2[nPoints];
            mesh.normals = new Vector3[nPoints];

            return mesh;
        }
    }
}
