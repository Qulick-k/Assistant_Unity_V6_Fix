using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class DialogManager : MonoBehaviour
{
    //每完成一項任務時，當saveMissionEvent有訂閱者的話，就呼叫訂閱者記錄任務完成
    public UnityEvent<string> saveMissionEvent;

    public UnityEngine.UI.Text actorName;
    public UnityEngine.UI.Text messageText;
    public RectTransform backgroundBox;
    public GameObject DB, PL, endPanel;
    public UnityEngine.UI.Text Mission1, Mission2, Mission3, Mission4, Mission5;  //循序型用的任務清單
    // Mission2放在蔗糖trigger的script裡面

    //綜合型用的任務清單
    public Text Mission1Com, Mission3Com, Mission4Com, Mission5Com;  //Mission2Com暫時用不到

    public AudioSource[] typingSound_A;
    public AudioSource[] typingSound_B;
    private AudioSource[] currentSoundList;
    public bool _change_A_or_B_Sonud;   //true的話，執行A串列的語音；false的話，執行B串列的語音


    Message2_2[] currentMessages;
    Actor2_2[] currentActors;
    int activeMessage = 0;

    
    [SerializeField] private GameObject CallObjectAnimatorOrCallMethodOrCheckTag;
    
    
    public void openDialogue(Message2_2[] messages, Actor2_2[] actors) {
        
        CallObjectAnimatorOrCallMethodOrCheckTag.GetComponent<NPC_animate>().ChangeAnimate(); //呼叫指定物件改成對話中動畫的方法
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

        Debug.Log("started conversation! loaded messages: " +  messages.Length);
        displayMessage();
    }
    
    void displayMessage() {
        Debug.Log("display message");
        Message2_2 messageToDisplay = currentMessages[activeMessage];
        messageText.text = messageToDisplay.meassage;
        //Debug.Log(messageText.text);

        Actor2_2 actorToDisplay = currentActors[messageToDisplay.actorId];
        actorName.text = actorToDisplay.name;
        //Debug.Log(actorToDisplay.name);
        DB.SetActive(true);

    }

    public void NextMessage() {
        currentSoundList[activeMessage].Stop();     //正在說的語句，因為按了下一句的按鈕，所以把當前語音切斷
        activeMessage++;                            //之後activeMessage + 1，執行下一句話的語音
        if (activeMessage < currentMessages.Length){
            displayMessage();
            currentSoundList[activeMessage].Play();

        }
        //任務2完成訊息，執行在掛載在甘蔗上的PickUp腳本
        else {
            CallObjectAnimatorOrCallMethodOrCheckTag.GetComponent<NPC_animate>().BackAnimate(); //呼叫指定物件改回待機動畫的方法
            CallObjectAnimatorOrCallMethodOrCheckTag.GetComponent<RandomPathTrolling>().SetWalkTrue(); //呼叫RandomPathTrolling腳本的方法，允許NPC移動

            ///如果要找任務顯示文字是 2.了解荷蘭開墾台灣的規劃(尋找甘蔗)的話，請去找ChangeTextColor腳本
            ///在這裡分出綜合型跟循序型的差別
            ///如果是循序型，就每完成一個任務就顯示下一個任務
            if (KeepData.guideSwitch == true)
            {
                if (PL.tag == "Player")
                {
                    if (CallObjectAnimatorOrCallMethodOrCheckTag.name == "diaplace2_1")
                    {
                        DB.SetActive(false);
                        //spawnBool = true;
                        Mission1.text = "<color=green>✓ 1.認識抗荷的背景(前往竹簡)</color>";
                        Mission2.color = new Color(Mission2.color.r, Mission2.color.g, Mission2.color.b, 1);          //讓任務2的顏色從透明變成原本的顏色
                                                                                                                      //Mission1.color = Color.green;
                        saveMissionEvent.Invoke("成功完成，1.認識抗荷的背景(前往竹簡)");
                    }
                    DB.SetActive(false);                    
                }
                else if (PL.tag == "PlayerWithSugar")
                {
                    if (CallObjectAnimatorOrCallMethodOrCheckTag.tag != "people")   //拿到蔗糖後，碰到的人如果不是荷蘭人A的話，就關閉對話框就好
                    {
                        if(CallObjectAnimatorOrCallMethodOrCheckTag.name == "diaplace2_1")
                        {
                            DB.SetActive(false);
                            Mission1.text = "<color=green>✓ 1.認識抗荷的背景(前往竹簡)</color>";
                            Mission2.color = new Color(Mission2.color.r, Mission2.color.g, Mission2.color.b, 1);          //讓任務2的顏色從透明變成原本的顏色
                            saveMissionEvent.Invoke("成功完成，1.認識抗荷的背景(前往竹簡)");
                        }
                        DB.SetActive(false);
                    }
                    else   //拿到蔗糖後，確定碰到的人是荷蘭人A的話，就關閉對話窗+改變玩家的Tag+更新任務清單
                    {
                        DB.SetActive(false);
                        PL.tag = "PlayerWithNerthland_A";
                        Mission3.text = "<color=green>✓ 3.蔗糖是荷蘭人重要的經濟來源(找到荷蘭人並繳交甘蔗)</color>";
                        //讓任務4的顏色從透明變成原本的顏色
                        Mission4.color = new Color(Mission4.color.r, Mission4.color.g, Mission4.color.b, 1);
                        saveMissionEvent.Invoke("成功完成，3.蔗糖是荷蘭人重要的經濟來源(找到荷蘭人並繳交甘蔗)");
                    }
                }
                else if (PL.tag == "PlayerWithNerthland_A")
                {
                    //跟荷蘭人A對話後，碰到的人如果不是郭懷一的話，對話完就把對話窗關閉
                    if (CallObjectAnimatorOrCallMethodOrCheckTag.tag != "huai")
                    {
                        if (CallObjectAnimatorOrCallMethodOrCheckTag.name == "diaplace2_1")
                        {
                            DB.SetActive(false);
                            Mission1.text = "<color=green>✓ 1.認識抗荷的背景(前往竹簡)</color>";
                            Mission2.color = new Color(Mission2.color.r, Mission2.color.g, Mission2.color.b, 1);          //讓任務2的顏色從透明變成原本的顏色
                            saveMissionEvent.Invoke("成功完成，1.認識抗荷的背景(前往竹簡)");
                        }
                        DB.SetActive(false);
                        //PlayerTagDontChange = false;
                    }
                    else
                    {
                        //碰到的人如果是郭懷一的話，對話窗關閉+改變玩家的Tag+更新任務清單+顯示出文件
                        DB.SetActive(false);
                        PL.tag = "PlayerWithGou";
                        Mission4.text = "<color=green>✓ 4.認識郭懷一擔任使節的原由、郭懷一抗荷蘭的成因(獲得文件)</color>";
                        //讓任務5的顏色從透明變成原本的顏色
                        Mission5.color = new Color(Mission5.color.r, Mission5.color.g, Mission5.color.b, 1);
                        CallObjectAnimatorOrCallMethodOrCheckTag.GetComponent<HuaiGivePaper>().SetPaperActive();
                        saveMissionEvent.Invoke("成功完成，4.認識郭懷一擔任使節的原由、郭懷一抗荷蘭的成因(獲得文件)");
                    }
                }
                else if (PL.tag == "PlayerWithGou")
                {
                    //跟郭懷一對話後，碰到的人如果不是荷蘭人B的話，對話完就把對話窗關閉
                    if (CallObjectAnimatorOrCallMethodOrCheckTag.tag != "People_Blue")
                    {
                        if (CallObjectAnimatorOrCallMethodOrCheckTag.name == "diaplace2_1")
                        {
                            DB.SetActive(false);
                            Mission1.text = "<color=green>✓ 1.認識抗荷的背景(前往竹簡)</color>";
                            Mission2.color = new Color(Mission2.color.r, Mission2.color.g, Mission2.color.b, 1);          //讓任務2的顏色從透明變成原本的顏色
                            saveMissionEvent.Invoke("成功完成，1.認識抗荷的背景(前往竹簡)");
                        }
                        DB.SetActive(false);
                    }
                    else
                    {
                        //碰到的人如果是荷蘭人B的話，對話窗關閉+更新任務清單+隔兩秒顯示出單元總結畫面
                        DB.SetActive(false);
                        Mission5.text = "<color=green>✓ 5.荷蘭如何治理與處置反抗民眾(找藍色衣服荷蘭人的對話)\r\n</color>";
                        saveMissionEvent.Invoke("成功完成，5.荷蘭如何治理與處置反抗民眾(找藍色衣服荷蘭人的對話)");
                        EndCanvasManager.EndCanvas();
                    }
                }
            }
            ///如果是綜合型，就每完成一個任務，就只顯示完成了什麼任務
            else
            {
                switch (PL.tag)
                {
                    case "Player":
                        Debug.Log("進入switch迴圈");
                        if (CallObjectAnimatorOrCallMethodOrCheckTag.name == "diaplace2_1")
                        {
                            DB.SetActive(false);
                            //spawnBool = true;
                            Mission1Com.text = "<color=green>✓ 1.認識抗荷的背景(前往竹簡)</color>";
                            Mission1Com.color = new Color(Mission1Com.color.r, Mission1Com.color.g, Mission1Com.color.b, 1);          //讓任務1的顏色從透明變成任務完成的顏色
                            saveMissionEvent.Invoke("成功完成，1.認識抗荷的背景(前往竹簡)");
                        }
                        DB.SetActive(false);
                        break;
                    case "PlayerWithSugar":
                        if (CallObjectAnimatorOrCallMethodOrCheckTag.tag != "people")   //拿到蔗糖後，碰到的人如果不是荷蘭人A的話，就關閉對話框就好
                        {
                            if (CallObjectAnimatorOrCallMethodOrCheckTag.name == "diaplace2_1")
                            {
                                DB.SetActive(false);
                                Mission1Com.text = "<color=green>✓ 1.認識抗荷的背景(前往竹簡)</color>";
                                Mission1Com.color = new Color(Mission1Com.color.r, Mission1Com.color.g, Mission1Com.color.b, 1);          //讓任務1的顏色從透明變成任務完成的顏色
                                saveMissionEvent.Invoke("成功完成，1.認識抗荷的背景(前往竹簡)");
                            }
                            DB.SetActive(false);
                        }
                        else   //拿到蔗糖後，確定碰到的人是荷蘭人A的話，就關閉對話窗+改變玩家的Tag+更新任務清單
                        {
                            DB.SetActive(false);
                            PL.tag = "PlayerWithNerthland_A";
                            Mission3Com.text = "<color=green>✓ 3.蔗糖是荷蘭人重要的經濟來源(找到荷蘭人並繳交甘蔗)</color>";
                            Mission3Com.color = new Color(Mission3Com.color.r, Mission3Com.color.g, Mission3Com.color.b, 1);          //讓任務3的顏色從透明變成任務完成的顏色
                            saveMissionEvent.Invoke("成功完成，3.蔗糖是荷蘭人重要的經濟來源(找到荷蘭人並繳交甘蔗)");
                        }
                        break;
                    case "PlayerWithNerthland_A":
                        //跟荷蘭人A對話後，碰到的人如果不是郭懷一的話，對話完就把對話窗關閉
                        if (CallObjectAnimatorOrCallMethodOrCheckTag.tag != "huai")
                        {
                            if (CallObjectAnimatorOrCallMethodOrCheckTag.name == "diaplace2_1")
                            {
                                DB.SetActive(false);
                                Mission1Com.text = "<color=green>✓ 1.認識抗荷的背景(前往竹簡)</color>";
                                Mission1Com.color = new Color(Mission1Com.color.r, Mission1Com.color.g, Mission1Com.color.b, 1);          //讓任務1的顏色從透明變成任務完成的顏色
                                saveMissionEvent.Invoke("成功完成，1.認識抗荷的背景(前往竹簡)");
                            }
                            DB.SetActive(false);
                            //PlayerTagDontChange = false;
                        }
                        else
                        {
                            //碰到的人如果是郭懷一的話，對話窗關閉+改變玩家的Tag+更新任務清單+顯示出文件
                            DB.SetActive(false);
                            PL.tag = "PlayerWithGou";
                            Mission4Com.text = "<color=green>✓ 4.認識郭懷一擔任使節的原由、郭懷一抗荷蘭的成因(獲得文件)</color>";
                            Mission4Com.color = new Color(Mission4Com.color.r, Mission4Com.color.g, Mission4Com.color.b, 1);          //讓任務4的顏色從透明變成任務完成的顏色
                            CallObjectAnimatorOrCallMethodOrCheckTag.GetComponent<HuaiGivePaper>().SetPaperActive();
                            saveMissionEvent.Invoke("成功完成，4.認識郭懷一擔任使節的原由、郭懷一抗荷蘭的成因(獲得文件)");
                        }
                        break;
                    case "PlayerWithGou":
                        //跟郭懷一對話後，碰到的人如果不是荷蘭人B的話，對話完就把對話窗關閉
                        if (CallObjectAnimatorOrCallMethodOrCheckTag.tag != "People_Blue")
                        {
                            if (CallObjectAnimatorOrCallMethodOrCheckTag.name == "diaplace2_1")
                            {
                                DB.SetActive(false);
                                //spawnBool = true;
                                Mission1Com.text = "<color=green>✓ 1.認識抗荷的背景(前往竹簡)</color>";
                                Mission1Com.color = new Color(Mission1Com.color.r, Mission1Com.color.g, Mission1Com.color.b, 1);          //讓任務1的顏色從透明變成任務完成的顏色
                                saveMissionEvent.Invoke("成功完成，1.認識抗荷的背景(前往竹簡)");
                            }
                            DB.SetActive(false);
                        }
                        else
                        {
                            //碰到的人如果是荷蘭人B的話，對話窗關閉+更新任務清單+隔兩秒顯示出單元總結畫面
                            DB.SetActive(false);
                            Mission5Com.text = "<color=green>✓ 5.荷蘭如何治理與處置反抗民眾(找藍色衣服荷蘭人的對話)\r\n</color>";
                            Mission5Com.color = new Color(Mission5Com.color.r, Mission5Com.color.g, Mission5Com.color.b, 1);          //讓任務5的顏色從透明變成任務完成的顏色
                            saveMissionEvent.Invoke("成功完成，5.荷蘭如何治理與處置反抗民眾(找藍色衣服荷蘭人的對話)");
                            EndCanvasManager.EndCanvas();
                            
                        }
                        break;
                }
            }
           
        }
    }
    //void ShowEndCanvas()
    //{
    //    Vector3 forward = PL.transform.forward;
    //    forward.y = 1;
    //    Vector3 x = PL.transform.forward;
    //    x.y = 0;
    //    endPanel.transform.position = PL.transform.position + forward * 3;
    //    endPanel.transform.rotation = Quaternion.LookRotation(x,Vector3.up);
        
    //    pldata = FileHandler.ReadFromJSON<PlayerData>(filename); //讀取json的內容
    //    //pldata10 = pldata.GetRange(pldata.Count - 10, 10);
    //    Debug.Log(pldata10);
    //    if (pldata.Count >= 10)
    //    {
    //        foreach (var item in pldata.GetRange(pldata.Count - 10, 10))
    //        {
    //            logText.text += item.playerName + " " + item.playerTime + " " + item.playerActionType + "\n"; //列出log紀錄
    //        }
    //    }
    //    else
    //    {
    //        foreach (var item in pldata.GetRange(0, pldata.Count))
    //        {
    //            logText.text += item.playerName + " " + item.playerTime + " " + item.playerActionType + "\n"; //列出log紀錄
    //        }
    //    }
        
    //    Debug.Log(logText.text);

    //    endPanel.SetActive(true);
    //}

    public void ClickNextMessage(){
        NextMessage();
    }
    
}
