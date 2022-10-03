using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoTimon : MonoBehaviour
{
    public LauncherController timon;
    public float velocidad;
    public AudioSource[] audioSource;
    public float margen=1;
    int i = 0;
    void Start()
    {
        StartCoroutine(VerificadorSonido());
    }

    IEnumerator VerificadorSonido()
    {
        float f1, f2;

        velocidad = margen;
        while (true)
        {
            f1 = timon.angle;
            yield return new WaitForSeconds(1f / 10f);
            f2 = timon.angle;
            velocidad -= Mathf.Abs(f1 - f2);
            //velocidad = Mathf.Abs(f1 - f2);
            if (velocidad < 0)
            {
                audioSource[i].Play(1);
                i = (i + 1) % audioSource.Length;
                velocidad = margen;
            }
            
            
        }
    }
}
