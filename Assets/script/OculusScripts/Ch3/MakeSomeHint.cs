using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeSomeHint : MonoBehaviour
{
    [SerializeField] private GameObject RouteCanvas;


    //碰撞體接觸到玩家的船，就開啟提示
    void OnTriggerEnter(Collider other)
    {
        RouteCanvas.SetActive(true);
    }
}
