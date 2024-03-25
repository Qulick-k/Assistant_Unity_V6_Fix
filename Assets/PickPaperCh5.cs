using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickPaperCh5 : MonoBehaviour
{
    //¼o±ó¸}¥»

    public int times = 0;
    public GameObject endCanvas;

    private void Start()
    {
        times = 0;
    }

    public void PickPaperTimesCh5()
    {
        times++;
        if (times >= 5)
        {
            endCanvas.SetActive(true);
            endCanvas.GetComponent<EndCanvasManagerCh2>().ShowEndCanvas();
            
        }
    }
}
