using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoSenoidal : MonoBehaviour
{
    public float velocidad;
    public float amplitud;

    Vector3 posicionInicial;
    void Start()
    {
        posicionInicial = transform.localPosition;
    }
     
    // Update is called once per frame
    void Update()
    {
        transform.localPosition = posicionInicial + Vector3.forward * Mathf.Sin(velocidad * Time.time + transform.position.z) * amplitud;
    }
}
