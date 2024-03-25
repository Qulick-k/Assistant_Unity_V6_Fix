using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class DialogTrigger2_1 : MonoBehaviour
{
    public Message2_1[] messages2_1_1, messages2_1_2;
    public Actor2_1[] actors2_1;
    public GameObject dialogueBox;


    [SerializeField] private GameObject ObjectTag;

    void StartDialogue1()
    {
        dialogueBox.GetComponent<DialogManager2_1>().openDialogue(messages2_1_1, actors2_1);
        Debug.Log("first");
    }

    void StartDialogue2()
    {
        dialogueBox.GetComponent<DialogManager2_1>().openDialogue(messages2_1_2, actors2_1);
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
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
public class Message2_1
{
    public int actorId2_1;
    public string meassage2_1;
}



[System.Serializable]
public class Actor2_1
{
    public string name2_1;
}