using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerDataCh2
{
    [SerializeField] public string playerName;
    [SerializeField] public string playerTime;
    [SerializeField] public string playerActionType;
    [SerializeField] public string conversationRecordCh2;

    public PlayerDataCh2(string name, string time, string actiontype, string record)
    {
        playerName = name;
        playerTime = time;
        playerActionType = actiontype;
        conversationRecordCh2 = record;
    }
}
