using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class DialogTrigger2_2 : MonoBehaviour
{
    public Message2_2_[] messages_1, messages_2;
    public Actor2_2_[] actors;
    public GameObject dialogueBox;


    [SerializeField] private GameObject ObjectTag;

    void StartDialogue1()
    {
        dialogueBox.GetComponent<DialogManager2_2>()._change_A_or_B_Sonud = true;
        dialogueBox.GetComponent<DialogManager2_2>().openDialogue(messages_1, actors);
        Debug.Log("first");
    }

    void StartDialogue2()
    {
        dialogueBox.GetComponent<DialogManager2_2>()._change_A_or_B_Sonud = false;
        dialogueBox.GetComponent<DialogManager2_2>().openDialogue(messages_2, actors);
        Debug.Log("second");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && this.gameObject.name == "Aboriginal_white_rig")
        {
            return;
        }        
        else if (other.gameObject.CompareTag("TalkedToA"))
        {
            Debug.Log("talked");
            ObjectTag.GetComponent<RandomPathTrolling>().SetWalkFalse();
            dialogueBox.SetActive(true) ;
            StartDialogue2();
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            ObjectTag.GetComponent<RandomPathTrolling>().SetWalkFalse();
            dialogueBox.SetActive(true);
            StartDialogue1();
        }
        else if (other.gameObject.CompareTag("hit"))
        {
            ObjectTag.GetComponent<RandomPathTrolling>().SetWalkFalse();
            dialogueBox.SetActive(true);
            StartDialogue1();
        }
        else if (other.gameObject.CompareTag("pick") || other.gameObject.CompareTag("gotolin"))
        {
            ObjectTag.GetComponent<RandomPathTrolling>().SetWalkFalse();
            dialogueBox.SetActive(true);
            StartDialogue1();
        }

    }
    private void OnTriggerExit(Collider other)
    {
        //³]§PÂ_±ø¥ó
        //©I¥sNPC_animate
        //dialogueBox.SetActive(false);
        ObjectTag.GetComponent<RandomPathTrolling>().SetWalkTrue();
        ObjectTag.GetComponent<NPC_animate>().BackAnimate();
    }
}

[System.Serializable]
public class Message2_2_
{
    public int actorId2_2;
    public string meassage2_2;
}



[System.Serializable]
public class Actor2_2_
{
    public string name2_2;
}