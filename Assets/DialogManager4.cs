using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager4 : MonoBehaviour
{
    public UnityEngine.UI.Text actorName;
    public UnityEngine.UI.Text messageText;
    public RectTransform backgroundBox;
    public GameObject DB, XR, endPanel;
    //public UnityEngine.UI.Text Mission1,Mission2, Mission3, Mission4, Mission5;

    public GameObject portal, toLinPortal;



    public AudioSource[] typingSound_A;
    public AudioSource[] typingSound_B;
    private AudioSource[] currentSoundList;
    public bool _change_A_or_B_Sonud;   //true的話，執行A串列的語音；false的話，執行B串列的語音

    Message4[] currentMessages;
    Actor4[] currentActors;
    int activeMessage = 0;



    public void openDialogue(Message4[] messages, Actor4[] actors)
    {       
        currentActors = actors;
        currentMessages = messages;
        activeMessage = 0;
        if (_change_A_or_B_Sonud == true)
        {
            currentSoundList = typingSound_A;           //當_change_A_or_B_Sonud為true，把A LIST的語音賦予給currentSoundList存起來
            currentSoundList[activeMessage].Play();     //並且讓currentSoundList播放第activeMessage位的語音，也就是第0位的語音，因為activeMessage被設為0了
        }
        else
        {
            currentSoundList = typingSound_B;           //當_change_A_or_B_Sonud為true，把B LIST的語音賦予給currentSoundList存起來
            currentSoundList[activeMessage].Play();     //並且讓currentSoundList播放第activeMessage位的語音，也就是第0位的語音，因為activeMessage被設為0了
        }
        //Debug.Log(activeMessage);

        Debug.Log("started conversation! loaded messages: " + messages.Length);
        displayMessage();
    }

    void displayMessage()
    {
        Debug.Log("display message");
        Message4 messageToDisplay = currentMessages[activeMessage];
        messageText.text = messageToDisplay.meassage;
        //Debug.Log(messageText.text);

        Actor4 actorToDisplay = currentActors[messageToDisplay.actorId];
        actorName.text = actorToDisplay.name;
        //Debug.Log(actorToDisplay.name);
        DB.SetActive(true);

    }

    public void NextMessage()
    {
        currentSoundList[activeMessage].Stop();     //正在說的語句，因為按了下一句的按鈕，所以把當前語音切斷
        activeMessage++;
        if (activeMessage < currentMessages.Length)
        {
            displayMessage();
            currentSoundList[activeMessage].Play();

        }

        else
        {
            DB.SetActive(false);
            if (XR.gameObject.CompareTag("Player"))
            {
                portal.SetActive(true);
            }
            else if (XR.gameObject.CompareTag("pick")||XR.gameObject.CompareTag("gotolin"))
            {
                toLinPortal.SetActive(true);
                XR.gameObject.tag = "gotolin";
            }
            else if (XR.gameObject.CompareTag("hit"))
            {
                endPanel.SetActive(true);
                endPanel.GetComponent<EndCanvasManagerCh2>().ShowEndCanvas();
            }
        }
    }

    public void ClickNextMessage()
    {
        NextMessage();
    }

}
