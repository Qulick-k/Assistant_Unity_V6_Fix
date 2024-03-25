using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseclip_glow : MonoBehaviour
{
    public GameObject HideObj;
    public GameObject ShowObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
                    HideObj.SetActive(false);
        ShowObj.SetActive(true);
        }
            
    }
    /*void OnMouseDown() {
        HideObj.SetActive(false);
        ShowObj.SetActive(true);
    }*/
}
