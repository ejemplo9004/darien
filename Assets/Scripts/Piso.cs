using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piso : MonoBehaviour
{
    public bool aplicarFuerzaChoque = true;
    public AudioSource audio;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Inmigrante inmi = other.gameObject.GetComponent<Inmigrante>();
            //print(inmi);
            if (inmi!=null)
            {
                inmi.Morir(aplicarFuerzaChoque? (inmi.transform.position - transform.position).normalized:Vector3.zero);
                if (audio != null)
                {
                    audio.Play();
                }
            }
        }
    }
}
