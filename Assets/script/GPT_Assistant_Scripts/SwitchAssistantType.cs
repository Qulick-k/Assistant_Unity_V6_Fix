using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Extensions;

public class SwitchAssistantType : MonoBehaviour
{
    [SerializeField] private Transform comprehensiveAssistant;  //綜合型助手
    [SerializeField] private Transform sequentialAssistant;    //循序型助手
    //[SerializeField] private bool useComprehensivePrompt;       //決定是否使用綜合型提示詞的布林值，True就是使用綜合型提示詞，False就是使用一般提示詞

    //點擊按鈕呼叫OpenComprehensiveAssistant()，開啟綜合型助手，並關閉循序型助手，並且設置useComprehensivePrompt提示詞為True
    public void OpenComprehensiveAssistant()
    {
        sequentialAssistant.SetActive(false);
        comprehensiveAssistant.SetActive(true);        
        //useComprehensivePrompt = true;
    }

    //點擊按鈕呼叫OpenComprehensiveAssistant()，開啟循序型助手，並關閉綜合型助手，並且設置useComprehensivePrompt提示詞為false
    public void OpenSequentialAssistant()
    {
        comprehensiveAssistant.SetActive(false);
        sequentialAssistant.SetActive(true);
        //useComprehensivePrompt = false;
    }
}
