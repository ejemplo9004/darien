using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arbol : MonoBehaviour
{
    public GameObject[] arboles;
     
    public void Aleatorizar()
    {
        int c = Random.Range(0, arboles.Length);
        for (int i = 0; i < arboles.Length; i++)
        {
            arboles[i].SetActive(i == c);
        }
    }
}
