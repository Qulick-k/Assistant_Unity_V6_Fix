using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.UI;

public class LoginScript : MonoBehaviour
{
    
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
        Debug.Log("循序型");
    }
    public void GuideOff()
    {
        KeepData.guideSwitch = false;
        Debug.Log("綜合型");
    }
}
