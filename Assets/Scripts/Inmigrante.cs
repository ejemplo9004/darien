using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inmigrante : MonoBehaviour
{
    public GameObject[] cabezas;
    public GameObject[] cuerpos;
    public Animator     anim;
    public Rigidbody[]  rbs;
    public Rigidbody    rbPpal;
    public GameObject   partiSangre;
    public GameObject   camara;
    public float        delayCamara;
    public ThrowablePerson persona;

    bool vivo = true;
    Vector3 fuerza = Vector3.zero;
    void Start()
    {
        if (!vivo) return;
        rbPpal.isKinematic = true;
        int r = Random.Range(0, cabezas.Length);
        //anim.enabled = false;
        rbs = GetComponentsInChildren<Rigidbody>();
        for (int i = 0; i < cabezas.Length; i++)
        {
            cabezas[i].SetActive(i == r);
        }
        r = Random.Range(0, cuerpos.Length);
        for (int i = 0; i < cuerpos.Length; i++)
        {
            cuerpos[i].SetActive(i == r);
        }
    }

    void ActivarAnimator()
    {
        if (!vivo) return;
        anim.enabled = true;
    }

    IEnumerator CambioCamara(bool que, float cuanto)
    {
        yield return new WaitForSeconds(cuanto);
        camara.SetActive(que);
    }

    [ContextMenu("Mu�eco de trapo")]
    public void ActivarRagdoll()
    {
        rbPpal.isKinematic = false;
        if (!vivo) return;
        for (int i = 0; i < rbs.Length; i++)
        {
            rbs[i].velocity = Vector3.zero;
        }
        rbPpal.AddForce(fuerza * 5000);
        anim.enabled = false;
        StartCoroutine(CambioCamara(false, 5));
    }
    [ContextMenu("Lanzar!!!")]
    public void AnimarLanzamiento()
    {
        if (!vivo) return;
        anim.SetTrigger("lanzar");
        camara.transform.position = transform.position + camara.transform.localPosition;
        StartCoroutine(CambioCamara(true, delayCamara));
    }

    public void Morir()
    {
        if (!vivo) return;
        Destroy(Instantiate(partiSangre, transform.position, Quaternion.identity) as GameObject, 5);
        ActivarRagdoll();
        enabled = false;
        vivo = false;
        persona.EndAirTrip();
    }

    public void Morir(Vector3 _fuerza)
    {
        fuerza = _fuerza;
        Morir();
    }
}
