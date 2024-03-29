using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssistanSwitchCanvas : MonoBehaviour
{
    [SerializeField] private Transform canvas;
    [SerializeField] private ChatGPTManager chatGPTManager;
    
    /// <summary>
    /// 當按下按鈕時，切換Canvas的顯示，並且創建或刪除Thread
    /// </summary>
    public void SwitchCanvas()
    {
        canvas.gameObject.SetActive(!canvas.gameObject.activeSelf);
        
        if (canvas.gameObject.activeSelf)
        {
            chatGPTManager.CreateThread();
        }
        else
        {
            chatGPTManager.DeleteThread();
        }
    }

}
