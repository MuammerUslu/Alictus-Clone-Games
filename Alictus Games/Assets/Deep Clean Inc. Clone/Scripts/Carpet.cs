using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeepCleanInc
{
    public class Carpet : MonoBehaviour
    {
        //MeshFilter meshFilter;
        //Mesh mesh;
        //Transform waterVacuum;
        //Vector3[] initialVerticeCoordinates;
        //Vector3[] tempCoor;

        //public float minDist = 2f;
        //public float speed = 20f;
        //private void Start()
        //{
        //    DOTween.Init();
        //    meshFilter = GetComponent<MeshFilter>();
        //    mesh = meshFilter.sharedMesh;
        //    initialVerticeCoordinates = mesh.vertices;
        //    tempCoor = initialVerticeCoordinates;
        //    waterVacuum = FindObjectOfType<WaterVacuum>().transform;
        //    //Debug.Log(initialVerticeCoordinates.Length);
        //    //for (int i = 0; i < mesh.vertices.Length; i++)
        //    //Debug.Log("Vertex #" + i + " = " + mesh.vertices[i].ToString("F7"));
        //}

        //private void Update()
        //{

        //    Vector3 vacuumPos = new Vector3(waterVacuum.transform.position.x, transform.position.y, waterVacuum.transform.position.z);

        //    for (int i = 0; i < mesh.vertices.Length; i++)
        //    {
        //        //if (Vector3.Distance(vacuumPos, mesh.vertices[i]) * 1000f <= minDist)
        //        {
        //            mesh.vertices[i] = Vector3.Lerp(mesh.vertices[i], vacuumPos, Time.deltaTime * speed);
        //        }
        //        //else
        //        {
        //            //tempCoor[i] = Vector3.Lerp(mesh.vertices[i], initialVerticeCoordinates[i], Time.deltaTime * speed);
        //        }
        //    }
        //}
    }
}
