using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Events;

public class SugarSpawnSpot : MonoBehaviour
{
    public static bool SugarCallChangeTagAndColor =false;


    public event EventHandler changeTag;
    public event EventHandler changeColor;
    //[SerializeField ]public static UnityEvent changeTagOrMissionText;

    [SerializeField] private Transform tableTopPoint;
    [SerializeField] private GameObject sugarPrefab;

    //[SerializeField] private GameObject Sugar;

    //private float spawnSugarTimer;
    //private float spawnSugarTimerMax = 1f;
    private int sugarSpawnedAmount;
    private int sugarSpawnedAmountMax = 3;

    //Queue<GameObject> queue;

    private void Awake()
    {
        //queue = new Queue<GameObject>();

        for ( var i =0; i < sugarSpawnedAmountMax; i++)
        {
            sugarSpawned();
        }

        //PickUp pickUp = GetComponentInChildren<PickUp>();
        //PickUp pickUp = GameObject.FindObjectOfType <PickUp>();
        //pickUp.sugarHover += PickUp_sugarHover;               //訂閱PickUp腳本的Event
        sugarSpawnedAmount = 3;

    }

    public static void PickUp_sugarHover()  //甘蔗被拿起來的時候，會通知過來
    {
        Debug.Log("傳過來了");
        SugarCallChangeTagAndColor = true;
        //changeTagOrMissionText?.Invoke();
    }

  

    // Update is called once per frame
    void Update()
    {
        if (sugarSpawnedAmount < sugarSpawnedAmountMax) //如果當前甘蔗數低於上限數3的話，增加甘蔗數
        {
            sugarSpawnedAmount++;
            sugarSpawned();
            //queue.Enqueue(preparedObject(tableTopPoint.position));

            //changeTagOrMissionText?.Invoke();
        }
        if (SugarCallChangeTagAndColor)
        {
            changeTag?.Invoke(this, EventArgs.Empty);
            changeColor?.Invoke(this, EventArgs.Empty);
            SugarCallChangeTagAndColor = false;
        }
        //spawnSugarTimer += Time.deltaTime;         //計時器每秒+1
        //if ( spawnSugarTimer > spawnSugarTimerMax) //當計時器超過3秒時，歸零計時器
        //{
        //    spawnSugarTimer = 0f;

            
        //}
    }
    
    void sugarSpawned()
    {
        GameObject sugarPrefabTrasform = Instantiate(sugarPrefab, tableTopPoint);
        Debug.Log("生成甘蔗~");
    }

    public void minusAmount()     //骯髒木箱，會通知minusAmount，讓Amount少一個物件
    {
        sugarSpawnedAmount = sugarSpawnedAmount - 1;
        
    }




    //物件池備用方法
    //GameObject Copy()    //生成物件
    //{
    //    GameObject sugar = Instantiate(sugarPrefab, tableTopPoint);

    //    return sugar;
    //}


    //GameObject AvaliableObject()
    //{
    //    GameObject sugarPrefabLocal = null;
    //    sugarPrefabLocal = queue.Dequeue();
    //    //queue.Enqueue(sugarPrefabLocal);

    //    return sugarPrefabLocal;
    //}

    //GameObject preparedObject(Vector3 position)
    //{
    //    GameObject preparedSugar = AvaliableObject();
    //    preparedSugar.transform.position = position;
    //    preparedSugar.SetActive(true);
    //    return preparedSugar;
    //}
}
