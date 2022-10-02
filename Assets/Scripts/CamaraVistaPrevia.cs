using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraVistaPrevia : MonoBehaviour
{
    public Transform objetivo;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(objetivo);
    }
}
