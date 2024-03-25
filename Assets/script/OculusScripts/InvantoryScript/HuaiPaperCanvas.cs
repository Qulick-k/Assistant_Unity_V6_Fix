using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuaiPaperCanvas : MonoBehaviour
{
    public static bool ShowCanvas;
    [SerializeField] private GameObject huaipapercanvas;

    private void Start()
    {
        ShowCanvas = true;
    }

    private void Update()
    {
        if (ShowCanvas == false)
        {
            huaipapercanvas.GetComponent<Canvas>().enabled = false;
        }
    }

    public void closeCanvas()
    {
        huaipapercanvas.GetComponent<Canvas>().enabled = false;
    }

    public void openCanvas()
    {
        if (ShowCanvas == true)
        {
            huaipapercanvas.GetComponent<Canvas>().enabled = true;
        }
        else
        {
            huaipapercanvas.GetComponent<Canvas>().enabled = false;
        }
    }
}
