using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class place2_1 : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //enter繼續對話
        if(Input.GetKeyDown(KeyCode.Return))
        {
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
        }
    }


}
