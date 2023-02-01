using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    public PolygonCollider2D hole2DCollider;
    public PolygonCollider2D ground2DCollider;
    public Collider groundCollider;
    public MeshCollider generatedMeshCollider;
    public float initialScale = 0.5f;
    private Mesh generatedMesh;

    private void Start()
    {
        GameObject[] allGOs = FindObjectsOfType(typeof(GameObject)) as GameObject[];

        foreach (var go in allGOs)
        {
            if (go.layer == LayerMask.NameToLayer("Obstacles"))
            {
                Physics.IgnoreCollision(go.GetComponent<Collider>(),generatedMeshCollider,true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Physics.IgnoreCollision(other,groundCollider,true);
        Physics.IgnoreCollision(other,generatedMeshCollider,false);
    }

    private void OnTriggerExit(Collider other)
    {
        Physics.IgnoreCollision(other,groundCollider,false);
        Physics.IgnoreCollision(other,generatedMeshCollider,true);
    }

    private void FixedUpdate()
    {
        if (transform.hasChanged)
        {
            transform.hasChanged = false;
            hole2DCollider.transform.position = new Vector2(transform.position.x, transform.position.z);
            hole2DCollider.transform.localScale = transform.localScale * initialScale;
            MakeHole2D();
            Make3DMeshCollider();
        }
    }

    private void MakeHole2D()
    {
        Vector2[] pointPositions = hole2DCollider.GetPath(0);

        for (int i = 0; i < pointPositions.Length; i++)
        {
            pointPositions[i] = hole2DCollider.transform.TransformPoint(pointPositions[i]);
        }

        ground2DCollider.pathCount = 2;
        ground2DCollider.SetPath(1, pointPositions);
    }

    private void Make3DMeshCollider()
    {
        if (generatedMesh != null)
        {
            Destroy(generatedMesh);
        }
        generatedMesh = ground2DCollider.CreateMesh(true,true);
        generatedMeshCollider.sharedMesh = generatedMesh;
    }
}
