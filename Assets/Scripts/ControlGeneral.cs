using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlGeneral : MonoBehaviour
{
    public int              limiteFPS = 30;
    public Terrenizator[]   terrenos;
    public int              actual;
    public Transform        agua;
    public CamaraSeguirPivote camara;
    public int maxInmigrantes;
    [HideInInspector] public int inmigrantesRestantes;

    public static ControlGeneral singleton;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    public void Siguiente()
    {
        terrenos[actual].Avanzar();
        terrenos[actual].ActivarCampamento();
        actual = (actual + 1) % terrenos.Length;
        agua.Translate((terrenos[0].cuantoAvanzar / terrenos.Length) * Vector3.forward, Space.World);
        camara.objetivo = terrenos[actual].campamento;
        terrenos[actual].ActivarCatapulta();

    }

    public void InstanciarInmigrante()
    {
        if (inmigrantesRestantes > 0)
        {
            inmigrantesRestantes--;
            Campamentos camp = GetCampamento();
            camp.InstanciarInmigrante();
            camp.catapulta.GetComponent<LauncherController>().UpdateCounter(inmigrantesRestantes + 1);
            
        }
        else
        {
            SceneManager.LoadScene("End");
        }
        
    }
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = limiteFPS;
        terrenos[actual].ActivarCatapulta();
        inmigrantesRestantes = maxInmigrantes - 1;
    }

    public Inmigrante GetInmigrante()
    {
        return terrenos[actual].sCampamentos.inmigrante;
    }

    public Campamentos GetCampamento()
    {
        return terrenos[actual].sCampamentos;
    }

}
