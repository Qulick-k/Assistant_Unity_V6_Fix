using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger_Ch3 : MonoBehaviour
{
    public MessageCh3[] messages_1, messages_2;
    public ActorCh3[] actors;
    public GameObject dialogueBox;
    public AudioSource typingSound;

    [SerializeField] private GameObject ObjectTag;



    void StartDialogue1()
    {
        dialogueBox.GetComponent<DialogManager_Ch3>()._change_A_or_B_Sonud = true;
        dialogueBox.GetComponent<DialogManager_Ch3>().openDialogue(messages_1, actors);
    }

    void StartDialogue2()
    {
        dialogueBox.GetComponent<DialogManager_Ch3>()._change_A_or_B_Sonud = false;
        dialogueBox.GetComponent<DialogManager_Ch3>().openDialogue(messages_2, actors);
    }

    void OnTriggerEnter(Collider other)
    {
        //初始狀態碰到驚嘆號
        if ((other.gameObject.CompareTag("Player") && ObjectTag.CompareTag("thing")) || (other.gameObject.CompareTag("PlayerWithZen") && ObjectTag.CompareTag("thing")))
        {
            Debug.Log("碰到驚嘆號");
            dialogueBox.SetActive(true);
            StartDialogue1();
        }
        //第一次跟鄭成功對話的話
        else if (other.gameObject.CompareTag("Player") && ObjectTag.CompareTag("zenhe"))
        {
            Debug.Log("第一次跟鄭成功對話");
            dialogueBox.SetActive(true);
            StartDialogue1();
            //ObjectTag.GetComponent<RandomPathTrolling>().SetWalkFalse(); //呼叫RandomPathTrolling腳本的方法，停止NPC移動
        }
        //玩家的tag還在初始階段時，碰到各個角色時
        else if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("沒拿蔗糖碰到荷蘭人、或是碰到路人");
            //dialogueBox.SetActive(true);
            //StartDialogue2();
            //ObjectTag.GetComponent<RandomPathTrolling>().SetWalkFalse(); //呼叫RandomPathTrolling腳本的方法，停止NPC移動
        }
        //第一次跟何斌對話的話
        else if (other.gameObject.CompareTag("PlayerWithZen") && ObjectTag.CompareTag("shougun"))
        {
            Debug.Log("第一次跟何斌對話");
            dialogueBox.SetActive(true);
            StartDialogue1();
            //ObjectTag.GetComponent<RandomPathTrolling>().SetWalkFalse(); //呼叫RandomPathTrolling腳本的方法，停止NPC移動
        }
        //第二次跟鄭成功對話的話
        else if (other.gameObject.CompareTag("PlayerWithShou") && ObjectTag.CompareTag("zenhe"))
        {
            Debug.Log("第二次跟鄭成功對話");
            dialogueBox.SetActive(true);
            StartDialogue2();
            //ObjectTag.GetComponent<RandomPathTrolling>().SetWalkFalse(); //呼叫RandomPathTrolling腳本的方法，停止NPC移動
        }
        //第二次跟何斌對話的話
        else if (other.gameObject.CompareTag("PlayerWithZenTwice") && ObjectTag.CompareTag("shougun"))
        {
            Debug.Log("第二次跟何斌對話");
            dialogueBox.SetActive(true);
            StartDialogue2();
            //ObjectTag.GetComponent<RandomPathTrolling>().SetWalkFalse(); //呼叫RandomPathTrolling腳本的方法，停止NPC移動
        }
        else if (other.gameObject.tag != "Player" && other.gameObject.tag != "PlayerWithZen" && other.gameObject.tag != "PlayerWithShou" && other.gameObject.tag != "PlayerWithZenTwice")
        {
            Debug.Log("不是玩家，別展開對話窗");
        }
        else
        {
            Debug.Log("碰到路人");
            //dialogueBox.SetActive(true);
            //StartDialogue2();
            //ObjectTag.GetComponent<RandomPathTrolling>().SetWalkFalse(); //呼叫RandomPathTrolling腳本的方法，停止NPC移動
        }
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
public class MessageCh3
{
    public int actorId;
    public string meassage;
}



[System.Serializable]
public class ActorCh3
{
    public string name;
}