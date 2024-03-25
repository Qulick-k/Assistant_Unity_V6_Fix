using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BambooDialogManager4_5_6 : MonoBehaviour
{
    public UnityEngine.UI.Text actorName;
    public UnityEngine.UI.Text messageText;
    public RectTransform backgroundBox;
    public GameObject DB;
    
    // Mission2放在蔗糖trigger的script裡面


    public AudioSource[] typingSound_A;
    public AudioSource[] typingSound_B;
    private AudioSource[] currentSoundList;
    public bool _change_A_or_B_Sonud;   //true的話，執行A串列的語音；false的話，執行B串列的語音


    Message2_2[] currentMessages;
    Actor2_2[] currentActors;
    int activeMessage = 0;

    private void Start()
    {
        _change_A_or_B_Sonud = true;
    }
    public void openDialogue(Message2_2[] messages, Actor2_2[] actors)
    {        
        currentActors = actors;
        currentMessages = messages;
        activeMessage = 0;                  //每打開一次對話，就將活動訊息"activeMessage"設為0，
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
        Message2_2 messageToDisplay = currentMessages[activeMessage];
        messageText.text = messageToDisplay.meassage;
        //Debug.Log(messageText.text);

        Actor2_2 actorToDisplay = currentActors[messageToDisplay.actorId];
        actorName.text = actorToDisplay.name;
        //Debug.Log(actorToDisplay.name);
        DB.SetActive(true);

    }

    public void NextMessage()
    {
        currentSoundList[activeMessage].Stop();     //正在說的語句，因為按了下一句的按鈕，所以把當前語音切斷
        activeMessage++;                            //之後activeMessage + 1，執行下一句話的語音
        if (activeMessage < currentMessages.Length)
        {
            displayMessage();
            currentSoundList[activeMessage].Play();

        }      
        else
        {
            DB.SetActive(false);                
        }
    }
 
    public void ClickNextMessage()
    {
        NextMessage();
    }

}
