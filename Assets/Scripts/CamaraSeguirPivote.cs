using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraSeguirPivote : MonoBehaviour
{
    public Transform    objetivo;
    public float        velocidad = 5;
    public Transform    miniMap;
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, objetivo.position, velocidad * Time.deltaTime);
        miniMap.position = Vector3.Lerp(miniMap.position, objetivo.position.z*Vector3.forward, velocidad * Time.deltaTime*2);
    }
}
