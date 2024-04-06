using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AreaColliderCall : MonoBehaviour
{
    ///使用者每按一次語音時，當saveAreaEvent有訂閱者的話，就呼叫訂閱者的LogMissionComplete()記錄使用者走到哪個區域
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
