using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Measure : MonoBehaviour
{
    private float xLength, yLength, zLength;
    void Start()
    {
        xLength = GetComponent<MeshFilter>().mesh.bounds.extents.x;
        yLength = GetComponent<MeshFilter>().mesh.bounds.extents.y;
        zLength = GetComponent<MeshFilter>().mesh.bounds.extents.z;
        Debug.Log("X: " + xLength);
        Debug.Log("Y: " + yLength);
        Debug.Log("Z: " + zLength);
    }
}
