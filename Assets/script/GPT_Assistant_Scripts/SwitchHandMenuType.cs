using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchHandMenuType : MonoBehaviour
{
    [SerializeField] private Transform guideOnMenu;
    [SerializeField] private Transform guideOffMenu;
    [SerializeField] private Text switchAlpha;

    private void Start()
    {
        //KeepData.guideSwitch = true;
        //如果需要引導，就開啟循序型任務清單、關閉綜合型任務清單；如果不需要引導，就開啟綜合型任務清單、關閉循序型任務清單
        if (KeepData.guideSwitch == true)
        {
            guideOnMenu.gameObject.SetActive(true);
            guideOffMenu.gameObject.SetActive(false);
        }
        else
        {
            guideOnMenu.gameObject.SetActive(false);
            guideOffMenu.gameObject.SetActive(true);
        }
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.A))
    //    {
    //        //當我按下A鍵，就把文字調成正常顏色
    //        switchAlpha.color = new Color(switchAlpha.color.r, switchAlpha.color.g, switchAlpha.color.b, 1);

    //    }
    //}
}
