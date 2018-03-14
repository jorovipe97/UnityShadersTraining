using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformationGrid : MonoBehaviour {

    public Transform Prefab;
    public int GridResolution;

    private Transform[] grid;
    private List<Transformation> transformations;
    private Matrix4x4 transformation;

	// Use this for initialization
	void Awake () {
        grid = new Transform[GridResolution * GridResolution * GridResolution];
        for (int i = 0, z = 0; z < GridResolution; z++)
        {
            for (int y = 0; y < GridResolution; y++)
            {
                for (int x = 0; x < GridResolution; x++, i++)
                {
                    grid[i] = CreateGridPoint(x, y, z);
                }
            }
        }

        transformations = new List<Transformation>();
    }   

    // Update is called once per frame
    void Update () {
        UpdateTransformation();
        for (int i = 0, z = 0; z < GridResolution; z++)
        {
            for (int y = 0; y < GridResolution; y++)
            {
                for (int x = 0; x < GridResolution; x++, i++)
                {
                    grid[i].localPosition = TransformPoint(x, y, z);
                }
            }
        }
    }

    void UpdateTransformation()
    {
        GetComponents<Transformation>(transformations);
        if (transformations.Count > 0)
        {
            transformation = transformations[0].Matrix;
            for (int i = 1; i < transformations.Count; i++)
            {
                transformation = transformations[i].Matrix * transformation;
            }
        }
    }

    private Transform CreateGridPoint(int x, int y, int z)
    {
        Transform point = Instantiate<Transform>(Prefab);
        point.localPosition = GetCoordinates(x, y, z);
        point.GetComponent<MeshRenderer>().material.color = new Color(
            (float)x / GridResolution,
            (float)y / GridResolution,
            (float)z / GridResolution
        );
        return point;
    }

    private Vector3 GetCoordinates(int x, int y, int z)
    {
        return new Vector3(
            x - (GridResolution - 1) * 0.5f,
            y - (GridResolution - 1) * 0.5f,
            z - (GridResolution - 1) * 0.5f
        );
    }

    private Vector3 TransformPoint(int x, int y, int z)
    {
        Vector3 coordinates = GetCoordinates(x, y, z);
        return transformation.MultiplyPoint(coordinates); ;
    }
}
