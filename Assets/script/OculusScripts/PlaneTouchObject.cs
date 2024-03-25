using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlaneTouchObject : MonoBehaviour
{
    public UnityEvent fallingSugarDestroy;              //參考放SugarTable>SugarSpawnSpot>minusAmount
    public UnityEvent fallingHuaiPaperDestroy;          //參考放TableMark>RespawnedHuaiPaper>spawnedHuaiPaper
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sugar")
        {
            //other.gameObject.SetActive(false);
            Debug.Log("碰到髒木箱");
            Destroy(other.gameObject, 0f);
            fallingSugarDestroy?.Invoke();            //通知SugarSpawnSpot腳本，把Amount -1
        }
        if (other.tag == "huaiPaper")
        {
            Debug.Log("碰到郭懷一文件了");
            Destroy(other.gameObject, 0f);
            fallingHuaiPaperDestroy?.Invoke();       //通知RespawnedHuaiPaper腳本，把郭懷一文件重生
        }
    }
}
