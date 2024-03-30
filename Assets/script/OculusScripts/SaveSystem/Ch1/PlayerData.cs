using System;
using UnityEngine;
using UnityEngine.Timeline;
using System.IO;

[Serializable]
public class PlayerData
{
    [SerializeField] public string playerName;
    [SerializeField] public string playerTime;
    [SerializeField] public string playerActionType;
    [SerializeField] public int playerPickSugar;
    [SerializeField] public string conversationRecord;


    public PlayerData(string name, string time, string actiontype, int times, string record) {  //,string record
        
        playerName = name;
        playerTime = time;
        playerActionType = actiontype;
        playerPickSugar = times;
        conversationRecord = record;
    }
}
