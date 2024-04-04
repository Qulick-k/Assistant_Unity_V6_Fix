using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.UI;

public class LoginScript : MonoBehaviour
{
    //
    [SerializeField] private TMP_Text guideWord;

    [SerializeField] public TMP_Text text, text2;

    public string PlName;

    public TMP_Text welcomeText;
    
    public void Login()
    {
        PlName = text.text + text2.text;
        KeepData.loginName = PlName;

        welcomeText.text = PlName + "，登入成功！";
        Debug.Log(PlName);

    }

    public void GuideOn()
    {
        KeepData.guideSwitch = true;
        guideWord.text = "成功開啟引導(循序型)";
        Debug.Log("循序型");
    }
    public void GuideOff()
    {
        KeepData.guideSwitch = false;
        guideWord.text = "成功關閉引導(綜合型)";
        Debug.Log("綜合型");
    }
}
