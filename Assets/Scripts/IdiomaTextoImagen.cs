using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IdiomaTextoImagen : MonoBehaviour
{
    public Image    imInterfaz;
    public Sprite   sprIngles;
    public Sprite   sprEspa�ol;
    public Text     txtInterfaz;
    public string   textoIngles;
    public string   textoEspa�ol;
     
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
                txtInterfaz.text = textoEspa�ol;
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
                imInterfaz.sprite = sprEspa�ol;
            }
        }
    }

    public void CambiarIdioma(int cual)
    {
        PlayerPrefs.SetInt("idioma", cual);
    }
}
