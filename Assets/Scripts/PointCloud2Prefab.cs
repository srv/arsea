/* 
 * @brief ARSEA Project
 * @author Miquel Massot Campos
 * @author System, Robotics and Vision
 * @author University of the Balearic Islands
 */

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
         
        public PointCloud2Prefab(Vector3[] points, Color[] colors, int idx, Material matVertex)
        {
            _matVertex = matVertex;
            CreateFolders();
            Load(points, colors, idx);
        }

        public GameObject GetPrefab()
        {
            return _pointCloud;
        }

        private void CreateFolders()
        {
            if (!Directory.Exists(Application.dataPath + "/Temp/"))
            {
                UnityEditor.AssetDatabase.CreateFolder("Assets", "Temp");
            }
            if (!Directory.Exists(Application.dataPath + "/Temp/Clouds/"))
            {
                UnityEditor.AssetDatabase.CreateFolder("Assets/Temp", "Clouds");
            }
        }

        private void Load(Vector3[] points, Color[] colors, int global_idx)
        {
            // Instantiate Point Groups. Number of groups depending on the limit on the number of points to be renderized for each point cloud. 
            int numGroups = Mathf.CeilToInt(points.Length * 1.0f / _limitPoints * 1.0f) + 1;
            _pointCloud = new GameObject("cloud_" + global_idx.ToString());
            _pointCloud.SetActive(false);
            for (int i = 0; i < numGroups - 1; i++)
            {
                InstantiateMesh(points, colors, global_idx, i);
            }
        }

        private void InstantiateMesh(Vector3[] points, Color[] colors, int global_idx, int local_idx)
        {
            // Create Mesh
            GameObject pointGroup = new GameObject("cloud_" + global_idx.ToString() + "_" + local_idx);
            pointGroup.AddComponent<MeshFilter>();
            pointGroup.AddComponent<MeshRenderer>();  
            pointGroup.GetComponent<Renderer>().material = _matVertex;

            pointGroup.GetComponent<MeshFilter>().mesh = CreateMesh(points, colors, local_idx);
            pointGroup.transform.parent = _pointCloud.transform;

            // Store Mesh
            //if (!Directory.Exists(Application.dataPath + "/Temp/Clouds/" + "cloud_" + global_idx.ToString()))
            //{
            //    UnityEditor.AssetDatabase.CreateFolder("Assets/Temp/Clouds", "cloud_" + global_idx.ToString());
            //}
            //UnityEditor.AssetDatabase.CreateAsset(pointGroup.GetComponent<MeshFilter>().mesh, "Assets/Temp/Clouds/" + "cloud_" + global_idx.ToString() + @"/" + "cloud_" + global_idx.ToString() + "_" + local_idx + ".asset");
            //UnityEditor.AssetDatabase.SaveAssets();
            //UnityEditor.AssetDatabase.Refresh();
        }

        private Mesh CreateMesh(Vector3[] points, Color[] colors, int id)
        {    
            Mesh mesh = new Mesh(); 

            int nPoints = _limitPoints;
            int cloud_size = points.Length;
            if (_limitPoints * (id + 1) > cloud_size)
                nPoints = cloud_size - _limitPoints * id;

            Vector3[] localPoints = new Vector3[nPoints];
            int[] localIndices = new int[nPoints];
            Color[] localColors = new Color[nPoints];

            for (int i = 0; i < nPoints; ++i)
            {
                int idx = id * _limitPoints + i;
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
