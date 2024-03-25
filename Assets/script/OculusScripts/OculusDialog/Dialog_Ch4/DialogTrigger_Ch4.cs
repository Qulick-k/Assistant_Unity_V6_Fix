using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;
using UnityEngine.UI;

public class DialogTrigger_Ch4 : MonoBehaviour
{
    public Message4[] messages_1, messages_2;
    public Actor4[] actors;
    public GameObject dialogueBox, dialogCanvas;
    public AudioSource typingSound;
    [SerializeField] private Text Missoon2, Missoon4, Missoon5;



    //[SerializeField] private GameObject ObjectTag;

    private void Start()
    {

    }

    void StartDialogue1()
    {
        dialogCanvas.GetComponent<DialogManager4>()._change_A_or_B_Sonud = true;
        dialogCanvas.GetComponent<DialogManager4>().openDialogue(messages_1, actors);
    }

    void StartDialogue2()
    {
        dialogCanvas.GetComponent<DialogManager4>()._change_A_or_B_Sonud = false;
        dialogCanvas.GetComponent<DialogManager4>().openDialogue(messages_2, actors);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "XR Origin")
        {
            //other.GetComponent<SaveSystemCh2>().TalkToFuf();
            if (other.gameObject.CompareTag("Player"))
            {
                Missoon2.text = "<color=green>✓ 2.尋找福康安</color>";
                StartDialogue1();
            }
            else if (other.gameObject.CompareTag("pick"))
            {
                Missoon4.text = "<color=green>✓ 4.和福康安回報</color>";
                StartDialogue2();
            }
            else if (other.gameObject.CompareTag("hit") || other.gameObject.CompareTag("gotolin"))
            {
                Missoon5.text = "<color=green>✓ 5.尋找林爽文</color>";
                StartDialogue2();
            }
        }
        
        Debug.Log(other.gameObject.tag);
    }
    private void OnTriggerExit(Collider other)
    {
        //設判斷條件
        //呼叫NPC_animate
        //dialogueBox.SetActive(false);
        //ObjectTag.GetComponent<RandomPathTrolling>().SetWalkTrue();
        //ObjectTag.GetComponent<NPC_animate>().BackAnimate();
    }
}

[System.Serializable]
public class Message4
{
    public int actorId;
    public string meassage;
}



[System.Serializable]
public class Actor4
{
    public string name;
}
   