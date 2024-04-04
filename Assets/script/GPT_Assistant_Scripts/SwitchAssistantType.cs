using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Extensions;

public class SwitchAssistantType : MonoBehaviour
{
    [SerializeField] private Transform comprehensiveAssistant;  //綜合型助手
    [SerializeField] private Transform sequentialAssistant;    //循序型助手
    //[SerializeField] private bool useComprehensivePrompt;       //決定是否使用綜合型提示詞的布林值，True就是使用綜合型提示詞，False就是使用一般提示詞

    private void Start()
    {
        //如果需要引導，就開啟循序型任務清單、關閉綜合型任務清單；如果不需要引導，就開啟綜合型任務清單、關閉循序型任務清單
        if (KeepData.guideSwitch == true)
        {
            sequentialAssistant.gameObject.SetActive(true);
            comprehensiveAssistant.gameObject.SetActive(false);
        }
        else
        {
            sequentialAssistant.gameObject.SetActive(false);
            comprehensiveAssistant.gameObject.SetActive(true);
        }
    }


    //點擊按鈕呼叫OpenComprehensiveAssistant()，開啟綜合型助手，並關閉循序型助手，並且設置useComprehensivePrompt提示詞為True
    public void OpenComprehensiveAssistant()
    {
        if (KeepData.guideSwitch != true)
        {
            sequentialAssistant.SetActive(false);
            comprehensiveAssistant.SetActive(true);
        }
        else
        {
            print("已開啟循序型判斷，不能開啟綜合型助手");
        }          
        //useComprehensivePrompt = true;
    }

    //點擊按鈕呼叫OpenSequentialAssistant()，開啟循序型助手，並關閉綜合型助手，並且設置useComprehensivePrompt提示詞為false
    public void OpenSequentialAssistant()
    {
        if (KeepData.guideSwitch)
        {
            comprehensiveAssistant.SetActive(false);
            sequentialAssistant.SetActive(true);
        }
        else
        {
            print("已開啟綜合型判斷，不能開啟循序型助手");
        }        
        //useComprehensivePrompt = false;
    }
}
