using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piso : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Inmigrante inmi = other.gameObject.GetComponent<Inmigrante>();
            //print(inmi);
            if (inmi!=null)
            {
                inmi.Morir();
            }
        }
    }
}
