using System;
using UnityEngine;

public class EndUI : MonoBehaviour
{
    [SerializeField] private GameObject spanishCanvas;
    [SerializeField] private GameObject englishCanvas;

    private void Start()
    {
        int language = PlayerPrefs.GetInt("idioma", 0);
        if (language == 0)
        {
            englishCanvas.SetActive(true);
        }
        else if (language == 1)
        {
            spanishCanvas.SetActive(true);
        }
    }
}
