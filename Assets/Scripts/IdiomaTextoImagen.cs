using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IdiomaTextoImagen : MonoBehaviour
{
    public Image    imInterfaz;
    public Sprite   sprIngles;
    public Sprite   sprEspañol;
    public Text     txtInterfaz;
    public string   textoIngles;
    public string   textoEspañol;
     
    void Start()
    {
        int idioma = PlayerPrefs.GetInt("idioma", 0);
        if (txtInterfaz != null)
        {
            if (idioma ==0)
            {
                txtInterfaz.text = textoIngles;
            }
            else
            {
                txtInterfaz.text = textoEspañol;
            }
        }
        if (imInterfaz != null)
        {
            if (idioma == 0)
            {
                imInterfaz.sprite = sprIngles;
            }
            else
            {
                imInterfaz.sprite = sprEspañol;
            }
        }
    }

    public void CambiarIdioma(int cual)
    {
        PlayerPrefs.SetInt("idioma", cual);
    }
}
