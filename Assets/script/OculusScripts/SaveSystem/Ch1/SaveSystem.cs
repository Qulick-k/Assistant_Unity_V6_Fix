using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;
using System.Text;
using System.Linq;
using UnityEngine.XR;

[System.Serializable]
public class SaveSystem : MonoBehaviour
{
    public List<PlayerData> pldata; //= new List<PlayerData>();
    [SerializeField] public string playerName;
    LoginScript loginText;
    
    [SerializeField] public PlayerData player;
    [SerializeField] string type;
    [SerializeField] public static int pickupTimes;
    private string conversationRecord; //存放對話紀錄
    
    [SerializeField] public string filename;

    public static bool change;
    [SerializeField] public static int numstatic;
    [SerializeField] private int numprivate;

    string FILENAME;

    private void Start()
    {
        playerName = KeepData.loginName;
        pldata = FileHandler.ReadFromJSON<PlayerData>(filename);
        FILENAME = Application.persistentDataPath + "/playerdatach1.csv";

        //使用者進入第一單元，就紀錄使用者進入了第一單元
        LogBeginning();
    }

    //重整type和conversationRecord的文字
    void ResetType_ConversationRecord()
    {
        type = "";
        conversationRecord = "";
    }

    public void LogBeginning()
    {
        //使用者進入第一單元，就紀錄使用者進入了第一單元
        type = "進入第一單元";
        Save();

    }

    /// <summary>
    /// 訂閱SceneManager的事件，當使用者離開第一單元或是重新遊玩第一單元，就紀錄使用者行為
    /// </summary>
    public void LogBackLobby()
    {
        //使用者離開第一單元，就紀錄使用者離開了第一單元
        type = "從第一單元回到回到關卡選擇大廳";
        Save();
    }

    public void LogRestart()
    {
        //使用者重新開始第一單元，就紀錄使用者重新開始了第一單元
        type = "重新開始第一單元";
        Save();
    }

    //紀錄完成了哪項任務
    public void LogMissionComplete(string Mission)
    {
        type = Mission;
        Save();
    }

    public void LogUserLookBoard()
    {
        type = "使用者查看了操作說明版";
        Save();
    }

    /// <summary>
    /// 碰到物件時，紀錄玩家的行為
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "huai")
        {
            type = "和郭懷一說話";

            Save();
            
        }
        else if (other.gameObject.tag == "people")
        {
            type = "和黑色荷蘭人說話";

            Save();
            
        }
        else if (other.gameObject.tag == "People_Blue")
        {
            type = "和藍色荷蘭人A長官說話";

            Save();

        }
        else if (other.gameObject.tag == "green_stranger")
        {
            type = "和綠色衣服男子對話";

            Save();
            
        }
        else if (other.gameObject.tag == "pink_stranger")
        {
            type = "和粉紅衣服男子甲對話";
            Save();
            
        }
        else if (other.gameObject.tag == "blue_stranger")
        {
            type = "和藍色衣服男子對話";

            Save();
            
        }
        else if (other.gameObject.tag == "red_stranger")
        {
            type = "和紅色衣服男子對話";

            Save();
            
        }
        else if (other.gameObject.tag == "green_stranger1")
        {
            type = "和粉紅衣服男子乙對話";

            Save();

        }
        else if (other.gameObject.tag == "thing")
        {
            type = "碰到驚嘆號";

            Save();
            
        }



    }

    //ChatGPTManager腳本會呼叫這個方法，玩家提問、助手回答都會呼叫
    public void SaveConversationRecord(string record)
    {
        conversationRecord = record;
        Save();
    }

    public void PaperSelect()
    {
        type = "拾取文件";
        Save();
    }

    public void DollSelect()
    {
        type = "撿取郭懷一人偶";
        Save();

    }

    public void Save()
    {
        pldata.Add(new PlayerData(playerName, DateTime.Now.ToString(), type, pickupTimes, conversationRecord)); //, conversationRecord  補上對話紀錄的引數
        FileHandler.SaveToJSON<PlayerData>(pldata, filename);
        WriteToCsv(FILENAME, pldata);

        //上面存完檔後，重設type和conversationRecord的文字為""。
        ResetType_ConversationRecord();
    }


    //流程一
    public static void add()    //甘蔗呼叫這個method
    {
        numstatic++;   //靜態變數++
        change = true; //布林閥值改為True，讓update方法可以通過if判斷
        

    }
    //流程二
    private void Update()
    {
        if (change == true)   //要是被允許改變的話 。 可以直接寫if(change就可)，然後也不一定要用change，用可以清楚表達這個變數為哪個物件做事的命名就OK
        {
            numprivate = numstatic; //靜態變數的值，賦予給私有變數
            pickupTimes = numprivate;
            SugarSelect();  //呼叫SugarSelect方法紀錄log

            //流程四
            //把布林閥值改為false，關掉
            change = false; //
        }
    }
    //流程三
    public void SugarSelect()    //執行紀錄log
    {
        //type...;
        //pldata......(..... ,numprivate);
        //FileHandler......;

        type = "撿取甘蔗";

        pldata.Add(new PlayerData(playerName, DateTime.Now.ToString(), type, numprivate, conversationRecord));  //, conversationRecord  補上對話紀錄的引數
        FileHandler.SaveToJSON<PlayerData>(pldata, filename);
        WriteToCsv(FILENAME, pldata);
        Debug.Log(numprivate);

        //流程四的關掉布林閥值放到這裡也OK，也就是 change = false;這行。
    }
    public void WriteToCsv(string FILENAME, List<PlayerData> pldata)
    {
        using (var dataFile = new StreamWriter(FILENAME, false, System.Text.Encoding.UTF8))
        {
            dataFile.WriteLine(returnDataRowName());
            foreach (var playerData in pldata)
            {
                dataFile.WriteLine($"{playerData.playerName}, {playerData.playerTime}, {playerData.playerActionType}, {playerData.playerPickSugar}, {playerData.conversationRecord}"); //, {playerData.conversationRecord}  補上對話紀錄的引數
            }
            dataFile.Close();
        }
        Debug.Log(FILENAME);
    }
    string returnDataRowName()
    {
        return "Name, Time, ActionType, 撿甘蔗次數, 玩家對話紀錄"; //,撿甘蔗次數 ,玩家對話紀錄  補上CSV在首行的標題
    }

}
