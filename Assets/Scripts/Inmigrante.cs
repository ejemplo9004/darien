using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inmigrante : MonoBehaviour
{
    public GameObject[] cabezas;
    public GameObject[] cuerpos;
    public Animator anim;
    public Rigidbody[] rbs;

    void Start()
    {
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
        Invoke("ActivarAnimator", 0.1f);
    }

    void ActivarAnimator()
    {
        anim.enabled = true;
    }

    [ContextMenu("Muñeco de trapo")]
    public void ActivarRagdoll()
    {
        for (int i = 0; i < rbs.Length; i++)
        {
            rbs[i].velocity = Vector3.zero;
        }
        anim.enabled = false;
    }
    [ContextMenu("Lanzar!!!")]
    public void AnimarLanzamiento()
    {
        anim.SetTrigger("lanzar");
    }
}
