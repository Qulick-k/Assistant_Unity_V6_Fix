using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;

[System.Serializable]
public class SaveSystemCh2 : MonoBehaviour
{
    //�ĤG�椸�U�������W�r���U�����r��
    [SerializeField] private string sceneName;

    //��ܳH�ߦǡB�z�̼ߡB�}������r
    public Text shellText, riceText, sugarText, brickText; //�`�ǫ�
    public Text shellTextCom, riceTextCom, sugarTextCom, brickTextCom; //��X��

    //public List<PlayerDataCh2> pldataCh2= new List<PlayerDataCh2>();
    public List<PlayerDataCh2> pldataCh2;

    [SerializeField] public string filename;
    [SerializeField] public string playerName;
    [SerializeField] public string type;
    [SerializeField] PlayerDataCh2 playerDataCh2;
    private string conversationRecord; //�s���ܬ���

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
        //�ϥΪ̶i�J�ĤG�椸�A�N�����ϥΪ̶i�J�F�ĤG�椸�A���ݭn�ϥ�UnityEvent
        if (sceneName != null)
        {
            type = "�i�J�ĤG�椸��" + sceneName;
            Save();            
        }
        else
        {
            print("�����W�r����g�isceneName�A�p�G���ݬ����h���βz�|���T��");
        }
        
    }

    /// <summary>
    /// �q�\SceneManager���ƥ�A��ϥΪ����}�Ĥ@�椸�άO���s�C���Ĥ@�椸�A�N�����ϥΪ̦欰
    /// </summary>
    public void LogBackLobby()
    {
        //�ϥΪ����}�ĤG�椸�A�N�����ϥΪ����}�F�ĤG�椸
        type = "�q�ĤG�椸�^�����d��ܤj�U";
        Save();
    }

    public void LogRestart()
    {
        //�ϥΪ̭��s�}�l�ĤG�椸�A�N�����ϥΪ̭��s�}�l�F�ĤG�椸
        type = "���s�}�l�ĤG�椸";
        Save();
    }

    //����type�MconversationRecord����r
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

        //�W���s���ɫ�A���]type�MconversationRecord����r��""�C
        ResetType_ConversationRecord();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("blue_stranger"))
        {
            type = "�M�Ŧ��A�k�l���";
            Save();
        }
        else if (other.gameObject.CompareTag("red_stranger"))
        {
            type = "�M�����A�k�l���";
            Save();
        }
        else if (other.gameObject.CompareTag("green_stranger"))
        {
            type = "�M����A�k�l�ҹ��";
            Save();
        }
        else if (other.gameObject.CompareTag("green_stranger1"))
        {
            type = "�M����A�k�l�A���";
            Save();
        }
        else if (other.gameObject.CompareTag("pink_stranger"))
        {
            type = "�M������A�k�l���";
            Save();
        }
        else if (other.gameObject.CompareTag("pinkgirl"))
        {
            type = "�M������A�k�l���";
            Save();
        }
        else if (other.gameObject.CompareTag("bluegirl"))
        {
            type = "�M�Ŧ��A�k�l���";
            Save();
        }
        else if (other.gameObject.CompareTag("greengirl"))
        {
            type = "�M����A�k�l���";
            Save();
        }
        else if (other.gameObject.CompareTag("redgirl"))
        {
            type = "�M�����A�k�l���";
            Save();
        }
      

    }
    public void PickNorth()
    {
        type = "��ܥ_�u��";
        Save();        
    }
    public void PickYuan() 
    {
        type = "��ܤj��";
        Save();        
    }
    public void PickRight()
    {
        type = "��ܥ��T����";
        Save();        
    }
    public void PickRice()
    {
        type = "�ߨ��z�̼�";
        
        if (KeepData.guideSwitch)
        {
            if (riceText != null)
            {
                riceText.text = "<color=green>�z�̼�</color>";
            }            
        }
        else
        {
            if (riceTextCom != null)
            {
                riceTextCom.text = "<color=green>�z�̼�</color>";
            }            
        }                
        Save();        
    }
    public void PickShell()
    {
        type = "�ߨ��H�ߦ�";
        
        if (KeepData.guideSwitch)
        {
            if (shellText != null)
            {
                shellText.text = "<color=green>�H�ߦ�</color>";
            }
        }
        else
        {
            if (shellTextCom != null)
            {
                shellTextCom.text = "<color=green>�H�ߦ�</color>";
            }            
        }
                
        Save();        
    }
    public void PickBrick()
    {
        type = "�ߨ����j��";
        
        if (KeepData.guideSwitch)
        {
            if (brickText != null)
            {
                brickText.text = "<color=green>���j��</color>";
            }            
        }
        else
        {
            if (brickTextCom != null)
            {
                brickTextCom.text = "<color=green>���j��</color>";
            }
           
        }
        Save();        
    }
    public void PickSugar()
    {
        type = "�ߨ��}��";
        if (KeepData.guideSwitch)
        {
            if (sugarText != null)
            {
                sugarText.text = "<color=green>�}��</color>";
            }
        }            
        else
        {
            if (sugarTextCom != null)
            {
                sugarTextCom.text = "<color=green>�}��</color>";
            }            
        }
        Save();  
    }
    public void PickPaper()
    {
        type = "�ߨ����";
        Save();
    }
    public void TalkToFuf()
    {
        type = "�M�ֱd�w���";
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
        return "Name, Time, ActionType, �ߥ̽�����, ���a��ܬ���";
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
