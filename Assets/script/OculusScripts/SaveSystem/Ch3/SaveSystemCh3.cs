using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[System.Serializable]
public class SaveSystemCh3 : MonoBehaviour
{
    //public List<PlayerDataCh2> pldataCh2= new List<PlayerDataCh2>();
    public List<PlayerDataCh2> pldataCh2;

    [SerializeField] public string filename;
    [SerializeField] public string playerName;
    [SerializeField] public string type;
    [SerializeField] PlayerDataCh2 playerDataCh2;

    string FILENAME;

    private void Start()
    {
        playerName = KeepData.loginName;
        pldataCh2 = FileHandler.ReadFromJSON<PlayerDataCh2>(filename);

        FILENAME = Application.persistentDataPath+ "/playerdatach3.csv";
    }
    public void Save()
    {
        pldataCh2.Add(new PlayerDataCh2(playerName, DateTime.Now.ToString(), type) { playerName = playerName, playerTime = DateTime.Now.ToString(), playerActionType = type });
        FileHandler.SaveToJSON<PlayerDataCh2>(pldataCh2, filename);
        WriteToCsv(FILENAME, pldataCh2);
    }
    public void WrongCubeSouth()
    {
        type = "¿ù»~¸ô½u";
        Save();
        
    }
    public void WrongCubeCastle()
    {
        type = "¼öÄõ¾B«°";
        Save();
        
    }
    public void CorrectCube()
    {
        type = "´¶Ã¹¥Á¾B«°";
        Save();
        
    }
    public void WriteToCsv(string FILENAME, List<PlayerDataCh2> pldata2)
    {
        using (var dataFile = new StreamWriter(FILENAME, false, System.Text.Encoding.UTF8))
        {
            dataFile.WriteLine(returnDataRowName());
            foreach (var playerDataCh2 in pldata2)
            {
                dataFile.WriteLine($"{playerDataCh2.playerName}, {playerDataCh2.playerTime}, {playerDataCh2.playerActionType}");
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
            WriteToCsv(FILENAME, pldataCh2);
            Debug.Log(FILENAME);
        }
    }

}
