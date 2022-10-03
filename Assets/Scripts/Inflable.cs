using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inflable : MonoBehaviour
{
    public AudioSource audio;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Inmigrante inmi = other.gameObject.GetComponent<Inmigrante>();
            if (inmi != null)
            {
                Destroy(other.gameObject);
                ControlGeneral.singleton.Siguiente();
            }
        }
    }
}
