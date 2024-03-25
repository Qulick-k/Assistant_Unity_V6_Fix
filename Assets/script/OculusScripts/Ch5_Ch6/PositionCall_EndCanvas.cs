using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PositionCall_EndCanvas : MonoBehaviour
{
    //當Socket有東西時，就用Event呼叫Method把相對應的布林值調整為True
    //當兩個布林值皆為True時，就將單元總結畫面顯示出來
    bool bramd;
    bool Image_Statue;

    bool DisplayCanvas;
    public GameObject endCanvas;

    [SerializeField] private string MissionContent;
    [SerializeField] private Text PlayerText;

    // Start is called before the first frame update
    void Start()
    {
        bramd = false;
        Image_Statue = false;

        DisplayCanvas = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (bramd == true && Image_Statue == true)
        {
            CanvasOut();
        }
    }

    public void brand_True()
    {
        bramd = true;
    }
    public void brand_False()
    {
        bramd = false;
    }
    public void Image_Statue_True()
    {
        Image_Statue = true;
    }
    public void Image_Statue_False()
    {
        Image_Statue = false;
    }

    private void CanvasOut()
    {
        if (DisplayCanvas == true)
        {
            endCanvas.SetActive(true);
            endCanvas.GetComponent<EndCanvasManagerCh2>().ShowEndCanvas();
            PlayerText.text = MissionContent;
            DisplayCanvas = false;
        }
        
    }

}
