using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class DialogTrigger : MonoBehaviour
{
    public Message2_2[] messages_1, messages_2;
    public Actor2_2[] actors;
    public GameObject dialogueBox;
    public AudioSource typingSound;


    
    [SerializeField] private GameObject ObjectTag;

    private void Start()
    {
        
    }

    void StartDialogue1()
    {
        dialogueBox.GetComponent<DialogManager>()._change_A_or_B_Sonud = true;
        dialogueBox.GetComponent<DialogManager>().openDialogue(messages_1, actors);
    }

    void StartDialogue2() 
    {
        dialogueBox.GetComponent<DialogManager>()._change_A_or_B_Sonud = false;
        dialogueBox.GetComponent<DialogManager>().openDialogue(messages_2, actors);
    }

    void OnTriggerEnter(Collider other) {


        //初始狀態碰到驚嘆號
        if (other.gameObject.tag == "Player" && ObjectTag.tag == "thing")
        {
            Debug.Log("碰到驚嘆號");
            dialogueBox.SetActive(true);
            StartDialogue1();
        }
        //拿到蔗糖後，跟荷蘭人A對襪
        else if (other.gameObject.tag == "PlayerWithSugar" && ObjectTag.tag == "people")
        {
            Debug.Log("拿到蔗糖碰到荷蘭人A");
            dialogueBox.SetActive(true);
            StartDialogue1();
            ObjectTag.GetComponent<RandomPathTrolling>().SetWalkFalse(); //呼叫RandomPathTrolling腳本的方法，停止NPC移動
        }
        //玩家的tag還在初始階段時，碰到各個角色時
        else if (other.gameObject.tag == "Player")
        {
            Debug.Log("沒拿蔗糖碰到荷蘭人、或是碰到路人");
            dialogueBox.SetActive(true);
            StartDialogue2();
            ObjectTag.GetComponent<RandomPathTrolling>().SetWalkFalse(); //呼叫RandomPathTrolling腳本的方法，停止NPC移動
        }
        //跟荷蘭人A對話完後，跟郭懷一對話
        else if (other.gameObject.tag == "PlayerWithNerthland_A" && ObjectTag.tag == "huai")
        {
            Debug.Log("跟荷蘭人A聊過了");
            dialogueBox.SetActive(true);
            StartDialogue1();
            ObjectTag.GetComponent<RandomPathTrolling>().SetWalkFalse(); //呼叫RandomPathTrolling腳本的方法，停止NPC移動
        }
        //跟郭懷一對話完後，跟荷蘭人B對話
        else if (other.gameObject.tag == "PlayerWithGou" && ObjectTag.tag == "People_Blue")
        {
            Debug.Log("跟郭懷一聊過了");
            dialogueBox.SetActive(true);
            StartDialogue1();
            ObjectTag.GetComponent<RandomPathTrolling>().SetWalkFalse(); //呼叫RandomPathTrolling腳本的方法，停止NPC移動
        }
        else if (other.gameObject.tag != "PlayerWithGou" && other.gameObject.tag != "PlayerWithNerthland_A" && other.gameObject.tag != "Player" && other.gameObject.tag != "PlayerWithSugar")
        {
            Debug.Log("不是玩家，別展開對話窗");
        }
        else
        {
            Debug.Log("碰到路人");
            dialogueBox.SetActive(true);
            StartDialogue2();
            ObjectTag.GetComponent<RandomPathTrolling>().SetWalkFalse(); //呼叫RandomPathTrolling腳本的方法，停止NPC移動
        }
        
        
    }
    private void OnTriggerExit(Collider other)
    {
        //設判斷條件
        //呼叫NPC_animate
        //dialogueBox.SetActive(false);
        ObjectTag.GetComponent<RandomPathTrolling>().SetWalkTrue();
        ObjectTag.GetComponent<NPC_animate>().BackAnimate();
    }
}

[System.Serializable]
public class Message2_2 {
    public int actorId;
    public string meassage;
}



[System.Serializable]
public class Actor2_2 {
    public string name;
}