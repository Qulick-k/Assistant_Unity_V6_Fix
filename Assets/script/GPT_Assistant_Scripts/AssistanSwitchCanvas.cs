using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssistanSwitchCanvas : MonoBehaviour
{
    [SerializeField] private Transform canvas;
    [SerializeField] private ChatGPTManager chatGPTManager;
    
    /// <summary>
    /// ����U���s�ɡA����Canvas����ܡA�åB�ЫةΧR��Thread
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
