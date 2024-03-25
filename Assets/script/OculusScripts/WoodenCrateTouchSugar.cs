using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class WoodenCrateTouchSugar : MonoBehaviour
{
    public UnityEvent sugarSpawnedAmountChange;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Sugar")
        {
            //other.gameObject.SetActive(false);
            Debug.Log("碰到髒木箱");
            Destroy(other.gameObject,0f);
            sugarSpawnedAmountChange?.Invoke();            //通知SugarSpawnSpot腳本，把Amount -1
        }
    }
}
