using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayAudio : MonoBehaviour
{
    ///使用者每走入單元任務說明區域時，當saveAreaEvent有訂閱者的話，就呼叫訂閱者的LogMissionComplete()記錄使用者走到哪個區域
    public UnityEvent<string> saveAreaEvent;

    public AudioSource taskAudio;
    

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name == "XR Origin")
        {
            taskAudio.Play();
            saveAreaEvent.Invoke("進入單元任務說明區域");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name =="XR Origin")
        {
            taskAudio.Stop();
        }
    }
}
