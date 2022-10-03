using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private float rayDistance;
    [SerializeField] private LayerMask terrainLayer;

    public bool CheckGround()
    {
        if (Physics.Raycast(transform.position, Vector3.down, rayDistance,terrainLayer))
        {
            return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position - new Vector3(0, rayDistance, 0));
    }
}
