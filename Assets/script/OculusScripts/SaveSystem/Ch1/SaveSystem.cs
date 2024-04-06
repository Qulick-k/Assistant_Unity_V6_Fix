using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;
using System.Text;
using System.Linq;
using UnityEngine.XR;
using UnityEditor.SearchService;

[System.Serializable]
public class SaveSystem : MonoBehaviour
{
    public List<PlayerData> pldata; //= new List<PlayerData>();
    [SerializeField] public string playerName;
    LoginScript loginText;
    
    [SerializeField] public PlayerData player;
    [SerializeField] string type;
    [SerializeField] public static int pickupTimes;
    private string conversationRecord; //�s���ܬ���
    
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

        //�ϥΪ̶i�J�Ĥ@�椸�A�N�����ϥΪ̶i�J�F�Ĥ@�椸
        LogBeginning();
    }

    //����type�MconversationRecord����r
    void ResetType_ConversationRecord()
    {
        type = "";
        conversationRecord = "";
    }

    public void LogBeginning()
    {
        //�ϥΪ̶i�J�Ĥ@�椸�A�N�����ϥΪ̶i�J�F�Ĥ@�椸
        type = "�i�J�Ĥ@�椸";
        Save();

    }

    /// <summary>
    /// �q�\SceneManager���ƥ�A��ϥΪ����}�Ĥ@�椸�άO���s�C���Ĥ@�椸�A�N�����ϥΪ̦欰
    /// </summary>
    public void LogBackLobby()
    {
        //�ϥΪ����}�Ĥ@�椸�A�N�����ϥΪ����}�F�Ĥ@�椸
        type = "�q�Ĥ@�椸�^��^�����d��ܤj�U";
        Save();
    }

    public void LogRestart()
    {
        //�ϥΪ̭��s�}�l�Ĥ@�椸�A�N�����ϥΪ̭��s�}�l�F�Ĥ@�椸
        type = "���s�}�l�Ĥ@�椸";
        Save();
    }

    //���������F��������
    public void LogMissionComplete(string Mission)
    {
        type = Mission;
        Save();
    }

    public void LogUserLookBoard()
    {
        type = "�ϥΪ̬d�ݤF�ާ@������";
        Save();
    }

    /// <summary>
    /// �I�쪫��ɡA�������a���欰
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "huai")
        {
            type = "�M���h�@����";

            Save();
            
        }
        else if (other.gameObject.tag == "people")
        {
            type = "�M�¦�����H����";

            Save();
            
        }
        else if (other.gameObject.tag == "green_stranger")
        {
            type = "�M����A�k�l���";

            Save();
            
        }
        else if (other.gameObject.tag == "pink_stranger")
        {
            type = "�M������A�k�l�ҹ��";
            Save();
            
        }
        else if (other.gameObject.tag == "blue_stranger")
        {
            type = "�M�Ŧ��A�k�l���";

            Save();
            
        }
        else if (other.gameObject.tag == "red_stranger")
        {
            type = "�M�����A�k�l���";

            Save();
            
        }
        else if (other.gameObject.tag == "green_stranger1")
        {
            type = "�M������A�k�l�A���";

            Save();

        }
        else if (other.gameObject.tag == "thing")
        {
            type = "�I����ĸ�";

            Save();
            
        }



    }

    //ChatGPTManager�}���|�I�s�o�Ӥ�k�A���a���ݡB�U��^�����|�I�s
    public void SaveConversationRecord(string record)
    {
        conversationRecord = record;
        Save();
    }

    public void PaperSelect()
    {
        type = "�B�����";
        Save();
        
    }
    public void Save()
    {
        pldata.Add(new PlayerData(playerName, DateTime.Now.ToString(), type, pickupTimes, conversationRecord)); //, conversationRecord  �ɤW��ܬ������޼�
        FileHandler.SaveToJSON<PlayerData>(pldata, filename);
        WriteToCsv(FILENAME, pldata);

        //�W���s���ɫ�A���]type�MconversationRecord����r��""�C
        ResetType_ConversationRecord();
    }


    //�y�{�@
    public static void add()    //�̽��I�s�o��method
    {
        numstatic++;   //�R�A�ܼ�++
        change = true; //���L�֭ȧאּTrue�A��update��k�i�H�q�Lif�P�_
        

    }
    //�y�{�G
    private void Update()
    {
        if (change == true)   //�n�O�Q���\���ܪ��� �C �i�H�����gif(change�N�i)�A�M��]���@�w�n��change�A�Υi�H�M����F�o���ܼƬ����Ӫ��󰵨ƪ��R�W�NOK
        {
            numprivate = numstatic; //�R�A�ܼƪ��ȡA�ᤩ���p���ܼ�
            pickupTimes = numprivate;
            SugarSelect();  //�I�sSugarSelect��k����log

            //�y�{�|
            //�⥬�L�֭ȧאּfalse�A����
            change = false; //
        }
    }
    //�y�{�T
    public void SugarSelect()    //�������log
    {
        //type...;
        //pldata......(..... ,numprivate);
        //FileHandler......;

        type = "�ߨ��̽�";

        pldata.Add(new PlayerData(playerName, DateTime.Now.ToString(), type, numprivate, conversationRecord));  //, conversationRecord  �ɤW��ܬ������޼�
        FileHandler.SaveToJSON<PlayerData>(pldata, filename);
        WriteToCsv(FILENAME, pldata);
        Debug.Log(numprivate);

        //�y�{�|���������L�֭ȩ��o�̤]OK�A�]�N�O change = false;�o��C
    }
    public void WriteToCsv(string FILENAME, List<PlayerData> pldata)
    {
        using (var dataFile = new StreamWriter(FILENAME, false, System.Text.Encoding.UTF8))
        {
            dataFile.WriteLine(returnDataRowName());
            foreach (var playerData in pldata)
            {
                dataFile.WriteLine($"{playerData.playerName}, {playerData.playerTime}, {playerData.playerActionType}, {playerData.playerPickSugar}, {playerData.conversationRecord}"); //, {playerData.conversationRecord}  �ɤW��ܬ������޼�
            }
            dataFile.Close();
        }
        Debug.Log(FILENAME);
    }
    string returnDataRowName()
    {
        return "Name, Time, ActionType, �ߥ̽�����, ���a��ܬ���"; //,�ߥ̽����� ,���a��ܬ���  �ɤWCSV�b���檺���D
    }

}
