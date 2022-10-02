using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrenizator : MonoBehaviour
{
    public MeshFilter malla;
    public MeshCollider collider;
    public float altura=3;
    public float frecuencia;
    public float offset;
    public List<Vector3> vertices = new List<Vector3>();
    public float minimo = 2;
    public float agua = 0.5f;
    public Transform campamento;
    public float radioMenor;
    public float radioMayor;
    public AnimationCurve curvas;
    [Header("...Arboles...")]
    public GameObject prefabArbol;
    public List<GameObject> arboles = new List<GameObject>();
    int arbolActual = 0;
    public float frecuenciaArboles = 3;
    public float cuadricula = 30;
    public float rangoAparecer;
    [Header("...Avances...")]
    public float cuantoAvanzar;

    void Start()
    {
        Actualizar();
    }

    public void Avanzar()
    {
        Invoke("AhoraAvanzar", 0.3f);
    }
    void AhoraAvanzar()
    {
        transform.Translate(Vector3.forward * cuantoAvanzar, Space.World);
        Actualizar();
    }

    void Actualizar()
    {
        campamento.localPosition = new Vector3(Random.Range(-15f,15f), campamento.localPosition.y, campamento.localPosition.z);
        malla.mesh.GetVertices(vertices);
        DesactivarArboles();
        for (int i = 0; i < vertices.Count; i++)
        {
            Vector3 v = vertices[i];
            float nx = offset + transform.position.x / 4 + (v.x + 10f) / 20f * frecuencia;
            float ny = offset + transform.position.z / 4 + (-v.y + 10f) / 20f * frecuencia;
            v.z = (Mathf.PerlinNoise(nx, ny)) * altura;
            if (v.z < agua)
            {
                v.z = agua;
            }else if (v.z < minimo)
            {
                v.z = minimo;
            }
            v.z = AlturaCampamento(v.z, v);
            v.z = v.z * (curvas.Evaluate((v.x + 20f) / 40f));
            vertices[i] = v;
        }
        DesactivarArboles();
        for (int i = -20; i < 20; i++)
        {
            for (int j = -20; j < 20; j++)
            {
                VerificarArbol(Vector3.one, i, j);
            }
        }
        malla.mesh.SetVertices(vertices);
        malla.mesh.RecalculateNormals();
        collider.sharedMesh = malla.mesh;
    }

    public void VerificarArbol(Vector3 posicion, float x, float y)
    {
        Vector3 npos = new Vector3(x, 0, y);

        Vector3 v = transform.position + npos;
        float nx = offset + transform.position.x / 4 + (v.x) / 20f * frecuencia;
        float ny = offset + transform.position.z / 4 + (-v.y) / 20f * frecuencia;
        v.z = (Mathf.PerlinNoise(nx, ny)) * altura;
        float probabilidad = Mathf.PerlinNoise(x + frecuenciaArboles, y + frecuenciaArboles);
        v.z = v.z * (curvas.Evaluate((v.x + 20f) / 40f));
        if (v.z < agua)
        {
            probabilidad = 0;
        }
        else if (v.z < minimo)
        {
            v.z = minimo;
        }
        
        npos += transform.position;
        npos.y = v.z;

        if ((npos-campamento.position).magnitude < radioMenor)
        {
            probabilidad = 0;
        }

        if (probabilidad > rangoAparecer)
        {
            Instanciar(npos);
        }
    }

    public float AlturaCampamento(float altura, Vector3 pos)
    {
        float distancia = (campamento.localPosition - pos).magnitude;
        if (distancia < radioMenor)
        {
            return minimo;
        }else if (distancia < radioMayor)
        {
            return Mathf.Lerp(minimo,altura, (distancia-radioMenor)/(radioMayor-radioMenor));
        }
        return altura;
    }

    private void OnDrawGizmosSelected()
    {
        if (campamento == null)
        {
            return;
        }
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(campamento.position, radioMenor);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(campamento.position, radioMayor);
    }

    public void Instanciar(Vector3 posicion)
    {
        Vector3 nv = posicion;// new Vector3(posicion.x, posicion.z, posicion.y) + transform.position;
        if (arbolActual >= arboles.Count)
        {
            GameObject go = Instantiate(prefabArbol, nv, Quaternion.identity, transform);
            arboles.Add(go);
            go.SendMessage("Aleatorizar");
            go.GetComponent<Arbol>().posicion = posicion;
            arbolActual++;
        }
        else
        {
            arboles[arbolActual].SetActive(true);
            arboles[arbolActual].SendMessage("Aleatorizar");
            arboles[arbolActual].transform.position = nv;
            arboles[arbolActual].GetComponent<Arbol>().posicion = nv;
            arbolActual++;
        }
    }

    public void DesactivarArboles()
    {
        for (int i = 0; i < arboles.Count; i++)
        {
            arboles[i].SetActive(false);
        }
        arbolActual = 0;
    }
}
