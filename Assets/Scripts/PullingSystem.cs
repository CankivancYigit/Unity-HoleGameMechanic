using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullingSystem : MonoBehaviour
{
    
    [Range(1f,1000f)]public float power = 1f; // gravity power
    [Range(-10f, 10f)] public float upOrDown; // direction of gravity
    [Range(1f,10f)]public float forceRange = 1f; // range of gravity

    public LayerMask layerMask;
    
    private void FixedUpdate()
    {
        Gravity(transform.position,forceRange,layerMask);
    }

    private void Gravity(Vector3 gravitySource, float range, LayerMask layerMask)
    {
        Collider[] objs = Physics.OverlapSphere(gravitySource, range, layerMask);

        for (int i = 0; i < objs.Length; i++)
        {
            Rigidbody rbs = objs[i].GetComponent<Rigidbody>();
            
            Vector3 forceDirection = new Vector3(gravitySource.x,upOrDown,gravitySource.z) - objs[i].transform.position;
            
            rbs.AddForceAtPosition(power * forceDirection.normalized,gravitySource);
        }
    }
}
