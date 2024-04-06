using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AreaColliderCall : MonoBehaviour
{
    ///�ϥΪ̨C���@���y���ɡA��saveAreaEvent���q�\�̪��ܡA�N�I�s�q�\�̪�LogMissionComplete()�O���ϥΪ̨�����Ӱϰ�
    public UnityEvent<string> saveAreaColliderEvent;
    [SerializeField] private string areaName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "XR Origin")
        {
            saveAreaColliderEvent.Invoke(areaName);
        }
    }
}
