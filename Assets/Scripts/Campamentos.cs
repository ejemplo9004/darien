using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campamentos : MonoBehaviour
{
    public GameObject catapulta;
    public GameObject inflable;

    private void Awake()
    {
        ActivarCampamento();
    }
    public void ActivarCatapulta()
    {
        catapulta.SetActive(true);
        inflable.SetActive(false);
    }
    public void ActivarCampamento()
    {
        catapulta.SetActive(false);
        inflable.SetActive(true);
    }
}
