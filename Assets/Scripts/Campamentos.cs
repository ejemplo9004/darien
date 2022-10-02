using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campamentos : MonoBehaviour
{
    public GameObject   catapulta;
    public GameObject   inflable;
    public GameObject   prInmigrante;
    public Transform    padreInmigrante;
    public Inmigrante   inmigrante;
    private void Awake()
    {
        ActivarCampamento();
    }
    public void ActivarCatapulta()
    {
        catapulta.SetActive(true);
        inflable.SetActive(false);
        InstanciarInmigrante();
    }

    public void InstanciarInmigrante()
    {
        inmigrante = (Instantiate(prInmigrante, padreInmigrante) as GameObject).GetComponent<Inmigrante>();
        inmigrante.transform.localPosition = Vector3.zero;
        inmigrante.transform.localScale = Vector3.one;
        inmigrante.transform.localEulerAngles = Vector3.zero;
    }
    public void ActivarCampamento()
    {
        catapulta.SetActive(false);
        inflable.SetActive(true);
    }
}
