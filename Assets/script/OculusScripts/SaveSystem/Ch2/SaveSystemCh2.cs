using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;

[System.Serializable]
public class SaveSystemCh2 : MonoBehaviour
{
    //第二單元各場景的名字填到下面的字串
    [SerializeField] private string sceneName;

    //顯示蚵殼灰、糯米漿、糖水的文字
    public Text shellText, riceText, sugarText, brickText; //循序型
    public Text shellTextCom, riceTextCom, sugarTextCom, brickTextCom; //綜合型

    //public List<PlayerDataCh2> pldataCh2= new List<PlayerDataCh2>();
    public List<PlayerDataCh2> pldataCh2;

    [SerializeField] public string filename;
    [SerializeField] public string playerName;
    [SerializeField] public string type;
    [SerializeField] PlayerDataCh2 playerDataCh2;
    private string conversationRecord; //存放對話紀錄

    string FILEPATH;
    public string FILENAME;

    private void Start()
    {
        playerName = KeepData.loginName;
        pldataCh2 = FileHandler.ReadFromJSON<PlayerDataCh2>(filename);

        FILEPATH = Application.persistentDataPath + "/" + FILENAME;

        LogBeginning();
    }

    public void LogBeginning()
    {
        //使用者進入第二單元，就紀錄使用者進入了第二單元，不需要使用UnityEvent
        if (sceneName != null)
        {
            type = "進入第二單元的" + sceneName;
            Save();            
        }
        else
        {
            print("場景名字為填寫進sceneName，如果不需紀錄則不用理會此訊息");
        }
        
    }

    /// <summary>
    /// 訂閱SceneManager的事件，當使用者離開第一單元或是重新遊玩第一單元，就紀錄使用者行為
    /// </summary>
    public void LogBackLobby()
    {
        //使用者離開第二單元，就紀錄使用者離開了第二單元
        type = "從第二單元回到關卡選擇大廳";
        Save();
    }

    public void LogRestart()
    {
        //使用者重新開始第二單元，就紀錄使用者重新開始了第二單元
        type = "重新開始第二單元";
        Save();
    }

    //重整type和conversationRecord的文字
    void ResetType_ConversationRecord()
    {
        type = "";
        conversationRecord = "";
    }

    public void Save()
    {
        pldataCh2.Add(new PlayerDataCh2(playerName, DateTime.Now.ToString(), type, conversationRecord) { playerName = playerName, playerTime = DateTime.Now.ToString(), playerActionType = type, conversationRecordCh2 = conversationRecord });
        //pldataCh2.Add(new PlayerDataCh2(playerName, DateTime.Now.ToString(), type));
        FileHandler.SaveToJSON<PlayerDataCh2>(pldataCh2, filename);
        WriteToCsv(FILEPATH, pldataCh2);

        //上面存完檔後，重設type和conversationRecord的文字為""。
        ResetType_ConversationRecord();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("blue_stranger"))
        {
            type = "和藍色衣服男子對話";
            Save();
        }
        else if (other.gameObject.CompareTag("red_stranger"))
        {
            type = "和紅色衣服男子對話";
            Save();
        }
        else if (other.gameObject.CompareTag("green_stranger"))
        {
            type = "和綠色衣服男子甲對話";
            Save();
        }
        else if (other.gameObject.CompareTag("green_stranger1"))
        {
            type = "和綠色衣服男子乙對話";
            Save();
        }
        else if (other.gameObject.CompareTag("pink_stranger"))
        {
            type = "和粉紅衣服男子對話";
            Save();
        }
        else if (other.gameObject.CompareTag("pinkgirl"))
        {
            type = "和粉紅衣服女子對話";
            Save();
        }
        else if (other.gameObject.CompareTag("bluegirl"))
        {
            type = "和藍色衣服女子對話";
            Save();
        }
        else if (other.gameObject.CompareTag("greengirl"))
        {
            type = "和綠色衣服女子對話";
            Save();
        }
        else if (other.gameObject.CompareTag("redgirl"))
        {
            type = "和紅色衣服女子對話";
            Save();
        }
      

    }
    public void PickNorth()
    {
        type = "選擇北線尾";
        Save();        
    }
    public void PickYuan() 
    {
        type = "選擇大員";
        Save();        
    }
    public void PickRight()
    {
        type = "選擇正確答案";
        Save();        
    }
    public void PickRice()
    {
        type = "撿取糯米漿";
        
        if (KeepData.guideSwitch)
        {
            if (riceText != null)
            {
                riceText.text = "<color=green>糯米漿</color>";
            }            
        }
        else
        {
            if (riceTextCom != null)
            {
                riceTextCom.text = "<color=green>糯米漿</color>";
            }            
        }                
        Save();        
    }
    public void PickShell()
    {
        type = "撿取蚵殼灰";
        
        if (KeepData.guideSwitch)
        {
            if (shellText != null)
            {
                shellText.text = "<color=green>蚵殼灰</color>";
            }
        }
        else
        {
            if (shellTextCom != null)
            {
                shellTextCom.text = "<color=green>蚵殼灰</color>";
            }            
        }
                
        Save();        
    }
    public void PickBrick()
    {
        type = "撿取紅磚石";
        
        if (KeepData.guideSwitch)
        {
            if (brickText != null)
            {
                brickText.text = "<color=green>紅磚石</color>";
            }            
        }
        else
        {
            if (brickTextCom != null)
            {
                brickTextCom.text = "<color=green>紅磚石</color>";
            }
           
        }
        Save();        
    }
    public void PickSugar()
    {
        type = "撿取糖水";
        if (KeepData.guideSwitch)
        {
            if (sugarText != null)
            {
                sugarText.text = "<color=green>糖水</color>";
            }
        }            
        else
        {
            if (sugarTextCom != null)
            {
                sugarTextCom.text = "<color=green>糖水</color>";
            }            
        }
        Save();  
    }
    public void PickPaper()
    {
        type = "撿取文件";
        Save();
    }
    public void TalkToFuf()
    {
        type = "和福康安對話";
        Save();
    }

    public void SaveConversationRecordCh2(string record)
    {
        conversationRecord = record;
        Save();
    }

    public void WriteToCsv(string FILENAME, List<PlayerDataCh2> pldata2)
    {
        using (var dataFile = new StreamWriter(FILENAME, false, System.Text.Encoding.UTF8))
        {
            dataFile.WriteLine(returnDataRowName());
            foreach (var playerDataCh2 in pldata2)
            {
                dataFile.WriteLine($"{playerDataCh2.playerName}, {playerDataCh2.playerTime}, {playerDataCh2.playerActionType}, , {playerDataCh2.conversationRecordCh2}");
            }
            dataFile.Close();
        }
        Debug.Log(FILENAME);
    }
    string returnDataRowName()
    {
        return "Name, Time, ActionType, 撿甘蔗次數, 玩家對話紀錄";
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            WriteToCsv(FILEPATH, pldataCh2);
            Debug.Log(FILEPATH);
        }
    }

}
