using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class DialogManager_Ch3 : MonoBehaviour
{
    public UnityEngine.UI.Text actorName;
    public UnityEngine.UI.Text messageText;
    public RectTransform backgroundBox;
    public GameObject DB, PL;
    //public GameObject endPanel;
    public UnityEngine.UI.Text Mission1, Mission2, Mission3, Mission4, Mission5;
    // Mission2放在蔗糖trigger的script裡面

    public AudioSource[] typingSound_Ch3_A;
    public AudioSource[] typingSound_Ch3_B;
    private AudioSource[] currentSoundList;
    public bool _change_A_or_B_Sonud;   //true的話，執行A串列的語音；false的話，執行B串列的語音

    MessageCh3[] currentMessages;
    ActorCh3[] currentActors;
    int activeMessage = 0;

    public UnityEvent openShipCanvas;


    [SerializeField] private GameObject CallObjectAnimatorOrCallMethodOrCheckTag;


    public void openDialogue(MessageCh3[] messages, ActorCh3[] actors)
    {
        
        //CallObjectAnimatorOrCallMethodOrCheckTag.GetComponent<NPC_animate>().ChangeAnimate(); //呼叫指定物件改成對話中動畫的方法
        currentActors = actors;
        currentMessages = messages;
        activeMessage = 0;
        if (_change_A_or_B_Sonud == true)
        {
            currentSoundList = typingSound_Ch3_A;           //當_change_A_or_B_Sonud為true，把A LIST的語音賦予給currentSoundList存起來
            currentSoundList[activeMessage].Play();     //並且讓currentSoundList播放第activeMessage位的語音，也就是第0位的語音，因為activeMessage被設為0了
        }
        else
        {
            currentSoundList = typingSound_Ch3_B;           //當_change_A_or_B_Sonud為true，把B LIST的語音賦予給currentSoundList存起來
            currentSoundList[activeMessage].Play();     //並且讓currentSoundList播放第activeMessage位的語音，也就是第0位的語音，因為activeMessage被設為0了
        }        
        //Debug.Log(activeMessage);

        Debug.Log("started conversation! loaded messages: " + messages.Length);
        displayMessage();
    }

    void displayMessage()
    {
        Debug.Log("display message");
        MessageCh3 messageToDisplay = currentMessages[activeMessage];
        messageText.text = messageToDisplay.meassage;
        //Debug.Log(messageText.text);

        ActorCh3 actorToDisplay = currentActors[messageToDisplay.actorId];
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
        //任務2完成訊息，執行在掛載在甘蔗上的PickUp腳本
        else
        {
            //CallObjectAnimatorOrCallMethodOrCheckTag.GetComponent<NPC_animate>().BackAnimate(); //呼叫指定物件改回待機動畫的方法
            //CallObjectAnimatorOrCallMethodOrCheckTag.GetComponent<RandomPathTrolling>().SetWalkTrue(); //呼叫RandomPathTrolling腳本的方法，允許NPC移動
            if ((PL.tag == "Player" && CallObjectAnimatorOrCallMethodOrCheckTag.tag == "thing") || (PL.tag == "PlayerWithZen" && CallObjectAnimatorOrCallMethodOrCheckTag.tag == "thing"))
            {
                DB.SetActive(false);
                //spawnBool = true;
                Mission1.text = "<color=green>✓ 1.前往竹簡</color>";
                //Mission1.color = Color.green;
            }
            else if (PL.tag == "Player" && CallObjectAnimatorOrCallMethodOrCheckTag.tag == "zenhe")
            {      
                DB.SetActive(false);
                PL.tag = "PlayerWithZen";
                Mission2.text = "<color=green>✓ 2.尋找鄭成功</color>";
                
            }
            else if (PL.tag == "PlayerWithZen" && CallObjectAnimatorOrCallMethodOrCheckTag.tag == "shougun")
            {                
                //碰到的人如果是何斌的話，對話窗關閉+改變玩家的Tag+更新任務清單
                DB.SetActive(false);
                PL.tag = "PlayerWithShou";
                Mission3.text = "<color=green>✓ 3.尋找水手</color>";
                //CallObjectAnimatorOrCallMethodOrCheckTag.GetComponent<HuaiGivePaper>().SetPaperActive();
            }
            else if (PL.tag == "PlayerWithShou" && CallObjectAnimatorOrCallMethodOrCheckTag.tag == "zenhe")
            {    
                //碰到的人如果是荷蘭人B的話，對話窗關閉+更新任務清單+隔兩秒顯示出單元總結畫面
                DB.SetActive(false);
                PL.tag = "PlayerWithZenTwice";
                Mission4.text = "<color=green>✓ 4.和鄭成功回報</color>";
                //EndCanvasManager.EndCanvas();
            }
            else if (PL.tag == "PlayerWithZenTwice" && CallObjectAnimatorOrCallMethodOrCheckTag.tag == "shougun")
            {
                //碰到的人如果是荷蘭人B的話，對話窗關閉+更新任務清單+隔兩秒顯示出單元總結畫面
                DB.SetActive(false);                
                Mission5.text = "<color=green>✓ 5.找水手出航</color>";
                openShipCanvas?.Invoke();
                //換場景到開船場景()
                //EndCanvasManager.EndCanvas();
            }            
        }
    }
    

    public void ClickNextMessage()
    {
        NextMessage();
    }
}
