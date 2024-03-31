using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[System.Serializable]
public class SaveSystemCh2 : MonoBehaviour
{
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
    }
    public void Save()
    {
        pldataCh2.Add(new PlayerDataCh2(playerName, DateTime.Now.ToString(), type, conversationRecord) { playerName = playerName, playerTime = DateTime.Now.ToString(), playerActionType = type, conversationRecordCh2 = conversationRecord });
        //pldataCh2.Add(new PlayerDataCh2(playerName, DateTime.Now.ToString(), type));
        FileHandler.SaveToJSON<PlayerDataCh2>(pldataCh2, filename);
        WriteToCsv(FILEPATH, pldataCh2);
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
        Save();        
    }
    public void PickShell()
    {
        type = "撿取蚵殼灰";
        Save();        
    }
    public void PickBrick()
    {
        type = "撿取紅磚石";
        Save();        
    }
    public void PickSugar()
    {
        type = "撿取糖水";
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
        return "Name, Time, ActionType";
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
