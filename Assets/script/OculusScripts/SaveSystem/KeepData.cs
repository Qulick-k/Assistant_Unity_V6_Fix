using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepData : MonoBehaviour
{
    public static string loginName;
    public static bool guideSwitch; // true: 有引導，給循序型 ；false: 沒引導，給綜合型

    private void Start()
    {
        guideSwitch = true;        // 預設為循序型
    }
}
