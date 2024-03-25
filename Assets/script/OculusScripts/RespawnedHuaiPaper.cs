using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnedHuaiPaper : MonoBehaviour
{
    [SerializeField] Transform respawnedSpot;                   //抓取生成位置，在Hierarchy>ThingGroup>TableMark>RespawnedSpot
    [SerializeField] GameObject huaiPaperPrefab;                //抓取郭懷一文件的Prefab，referrence在assets>Prefabs>HuaiPaper
   public void spawnedHauiPaper()
    {
        GameObject hauiPaper = Instantiate(huaiPaperPrefab, respawnedSpot);                //生成文件，父層位置放respawnedSpot
    }
}
