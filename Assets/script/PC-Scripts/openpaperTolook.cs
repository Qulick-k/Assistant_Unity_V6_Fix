using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openpaperTolook : MonoBehaviour
{
    public GameObject paperCanvas;
    public GameObject paper_onhand;
    public bool isopenpaper;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(paper_onhand.activeSelf)
        {
            openpapr();
        }
    }

    void openpapr()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            isopenpaper= !isopenpaper;
            paperCanvas.SetActive(isopenpaper);
        }
    }
}
