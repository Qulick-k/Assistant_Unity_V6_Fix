using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager2_3 : MonoBehaviour
{
    public UnityEngine.UI.Text actorName;
    public UnityEngine.UI.Text messageText;
    public RectTransform backgroundBox;
    public GameObject DB, XrOrigin;//, endPanel;
    public UnityEngine.UI.Text Mission3, Mission4;
    public Text Mission3Com;
    //public GameObject porTal;

    public GameObject CallObjectAnimatorOrCallMethodOrCheckTag;  //這欄位放各個對話窗所屬的NPC

    public AudioSource[] _typingSound_23_24;

    Message2_3[] currentMessages;
    Actor2_3[] currentActors;
    int activeMessage = 0;


    //[SerializeField] private GameObject CallObjectAnimatorOrCallMethodOrCheckTag;

    public void openDialogue(Message2_3[] messages, Actor2_3[] actors)
    {
        Debug.Log("here");        
        CallObjectAnimatorOrCallMethodOrCheckTag.GetComponent<NPC_animate>().ChangeAnimate(); //呼叫指定物件改成對話中動畫的方法
        currentActors = actors;
        currentMessages = messages;
        activeMessage = 0;
        _typingSound_23_24[activeMessage].Play();
        //Debug.Log(activeMessage);

        Debug.Log("started conversation! loaded messages: " + messages.Length);
        displayMessage();
    }

    void displayMessage()
    {
        Debug.Log("display message");
        Message2_3 messageToDisplay = currentMessages[activeMessage];
        messageText.text = messageToDisplay.message;
        //Debug.Log(messageText.text);

        Actor2_3 actorToDisplay = currentActors[messageToDisplay.actorId];
        actorName.text = actorToDisplay.name;
        //Debug.Log(actorToDisplay.name);
        DB.SetActive(true);

    }

    public void NextMessage()
    {
        _typingSound_23_24[activeMessage].Stop();
        activeMessage++;
        if (activeMessage < currentMessages.Length)
        {
            displayMessage();
            _typingSound_23_24[activeMessage].Play();

        }
        //任務2完成訊息，執行在掛載在甘蔗上的PickUp腳本
        else
        {
            CallObjectAnimatorOrCallMethodOrCheckTag.GetComponent<NPC_animate>().BackAnimate(); //呼叫指定物件改回待機動畫的方法
            CallObjectAnimatorOrCallMethodOrCheckTag.GetComponent<RandomPathTrolling>().SetWalkTrue(); //呼叫RandomPathTrolling腳本的方法，允許NPC移動

            DB.SetActive(false);
            
            if (KeepData.guideSwitch)
            {
                Mission3.text = "<color=green>✓ 3.認識各個材料的功用(找原住民對話收集材料)</color>";
                Mission4.color = new Color(Mission4.color.r, Mission4.color.g, Mission4.color.b, 1);
            }
            else
            {
                Mission3Com.text = "<color=green>✓ 3.認識各個材料的功用(找原住民對話收集材料)</color>";
            }
            


            //porTal.SetActive(true);

            //if (PL.tag == "Player")
            //{
            //    DB.SetActive(false);
            //    Mission1.text = "<color=green>1.前往黃色驚嘆號 ?</color>";
            //}

        }
    }

    public void ClickNextMessage()
    {
        NextMessage();
    }
}
