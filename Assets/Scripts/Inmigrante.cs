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

    bool vivo = true;
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

    [ContextMenu("Muñeco de trapo")]
    public void ActivarRagdoll()
    {
        rbPpal.isKinematic = false;
        if (!vivo) return;
        for (int i = 0; i < rbs.Length; i++)
        {
            rbs[i].velocity = Vector3.zero;
        }
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
    }

    public void ActivarFake()
    {
        StartCoroutine(FakeAvanzar());
    }

    IEnumerator FakeAvanzar()
    {
        float gravedad = -2;
        while (vivo)
        {
            transform.Translate((Vector3.forward * 2 + Vector3.down * gravedad) * Time.deltaTime);
            gravedad += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
