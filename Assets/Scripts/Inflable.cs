using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inflable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            /// Victoria
        }
    }
}
