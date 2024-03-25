using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class questList : MonoBehaviour
{
    public GameObject panel_list;
    public bool isopen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        openlist();
        // closelist();
    }

    void openlist()
    {
        if(Input.GetKeyDown(KeyCode.Q))
            {
                isopen= !isopen;
                panel_list.SetActive(isopen);
            }
    }

    void closelist()
    {
        // if(Input.GetKeyDown(KeyCode.Q))
        //     {
        //         isopen= !isopen;
        //         panel_list.SetActive(true);
        //     }
    }
}
