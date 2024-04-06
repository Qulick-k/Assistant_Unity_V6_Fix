using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayAudio : MonoBehaviour
{
    ///�ϥΪ̨C���J�椸���Ȼ����ϰ�ɡA��saveAreaEvent���q�\�̪��ܡA�N�I�s�q�\�̪�LogMissionComplete()�O���ϥΪ̨�����Ӱϰ�
    public UnityEvent<string> saveAreaEvent;

    public AudioSource taskAudio;
    

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name == "XR Origin")
        {
            taskAudio.Play();
            saveAreaEvent.Invoke("�i�J�椸���Ȼ����ϰ�");
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
