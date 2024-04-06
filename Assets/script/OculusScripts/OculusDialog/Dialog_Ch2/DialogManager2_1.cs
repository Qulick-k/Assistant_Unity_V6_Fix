using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class DialogManager2_1 : MonoBehaviour
{
    //使用者每和系統竹簡對話，當saveSystemDialogMissionEvent有訂閱者的話，就呼叫訂閱者的LogMissionComplete()記錄使用者完成什麼任務
    public UnityEvent<string> saveSystemDialogMissionEvent;

    public UnityEngine.UI.Text actorName;
    public UnityEngine.UI.Text messageText;
    public RectTransform backgroundBox;
    public GameObject DB;//, PL, endPanel;
    public UnityEngine.UI.Text Mission1, Mission2;
    public Text Mission1Com;
    public GameObject porTal;


    public AudioSource [] typingSound;

    Message2_1[] currentMessages;
    Actor2_1[] currentActors;
    int activeMessage = 0;


    //[SerializeField] private GameObject CallObjectAnimatorOrCallMethodOrCheckTag;


    public void openDialogue(Message2_1[] messages, Actor2_1[] actors)
    {
        Debug.Log("here");
        
        //CallObjectAnimatorOrCallMethodOrCheckTag.GetComponent<NPC_animate>().ChangeAnimate(); //呼叫指定物件改成對話中動畫的方法
        currentActors = actors;
        currentMessages = messages;
        activeMessage = 0;
        typingSound[activeMessage].Play();
        //Debug.Log(activeMessage);

        Debug.Log("started conversation! loaded messages: " + messages.Length);
        displayMessage();
    }

    void displayMessage()
    {
        Debug.Log("display message");
        Message2_1 messageToDisplay = currentMessages[activeMessage];
        messageText.text = messageToDisplay.meassage2_1;
        //Debug.Log(messageText.text);

        Actor2_1 actorToDisplay = currentActors[messageToDisplay.actorId2_1];
        actorName.text = actorToDisplay.name2_1;
        //Debug.Log(actorToDisplay.name);
        DB.SetActive(true);

    }

    public void NextMessage()
    {
        typingSound[activeMessage].Stop();
        activeMessage++;
        if (activeMessage < currentMessages.Length)
        {
            displayMessage();
            typingSound[activeMessage].Play();

        }
        //任務2完成訊息，執行在掛載在甘蔗上的PickUp腳本
        else
        {
            ///在這裡分出綜合型跟循序型的差別
            ///如果是循序型，就每完成一個任務就顯示下一個任務
            ///如果是綜合型，就每完成一個任務，就只顯示完成了什麼任務
            if (KeepData.guideSwitch)
            {
                //CallObjectAnimatorOrCallMethodOrCheckTag.GetComponent<NPC_animate>().BackAnimate(); //呼叫指定物件改回待機動畫的方法
                //CallObjectAnimatorOrCallMethodOrCheckTag.GetComponent<RandomPathTrolling>().SetWalkTrue(); //呼叫RandomPathTrolling腳本的方法，允許NPC移動

                DB.SetActive(false);
                porTal.SetActive(true);
                DB.SetActive(false);
                Mission1.text = "<color=green>✓ 1.認識建造普羅民遮城的背景(前往竹簡)</color>";
                Mission2.color = new Color(Mission2.color.r, Mission2.color.g, Mission2.color.b, 1);
                saveSystemDialogMissionEvent.Invoke("成功完成，1.認識建造普羅民遮城的背景(前往竹簡)");
            }
            else
            {
                DB.SetActive(false);
                porTal.SetActive(true);
                DB.SetActive(false);
                Mission1Com.text = "<color=green>✓ 1.認識建造普羅民遮城的背景(前往竹簡)</color>";
                saveSystemDialogMissionEvent.Invoke("成功完成，1.認識建造普羅民遮城的背景(前往竹簡)");
            }
            
            
        }
    }

    public void ClickNextMessage()
    {
        NextMessage();
    }

}
