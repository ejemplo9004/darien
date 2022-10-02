using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlGeneral : MonoBehaviour
{
    public int              limiteFPS = 30;
    public Terrenizator[]   terrenos;
    public int              actual;
    public Transform        agua;
    public CamaraSeguirPivote camara;

    public void Siguiente()
    {
        terrenos[actual].Avanzar();
        actual = (actual + 1) % terrenos.Length;
        agua.Translate((terrenos[0].cuantoAvanzar / terrenos.Length) * Vector3.forward, Space.World);
        camara.objetivo = terrenos[actual].campamento;
    }
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = limiteFPS;
    }

}
