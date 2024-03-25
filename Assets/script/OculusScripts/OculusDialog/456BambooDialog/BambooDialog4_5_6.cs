using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class BambooDialog4_5_6 : MonoBehaviour
{
    public Message2_2[] messages_1, messages_2;
    public Actor2_2[] actors;
    public GameObject dialogueBox;

    void StartDialogue1()
    {
        dialogueBox.GetComponent<BambooDialogManager4_5_6>().openDialogue(messages_1, actors);
    }

    

    void OnTriggerEnter(Collider other)
    {


        //初始狀態碰到驚嘆號
        if (other.gameObject.name == "XR Origin" )
        {
            Debug.Log("碰到驚嘆號");
            dialogueBox.SetActive(true);
            StartDialogue1();
        }


    }
  
}

