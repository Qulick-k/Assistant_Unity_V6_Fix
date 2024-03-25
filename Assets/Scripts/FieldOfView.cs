using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;

    public GameObject playerRef, xrOrigin, originFuf, finalFuf, taskPanel, lin, toLin;
    public TMP_Text taskText;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;

    private void Start()
    {
        //playerRef = GameObject.Find("XR Origin (XR Rig)");
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask); //為林爽文設置一個OverlapSphere檢測是否有物件
        //             OverlapSphere設置在林爽文的座標上、半徑可以在編輯器設定成50、OverlapSphere檢測目標的layer

        if (rangeChecks.Length != 0)  //OverlapSphere裡面有東西的話
        {
            Transform target = rangeChecks[0].transform;  //就把那東西的transform賦予給target
            Vector3 directionToTarget = (target.position - transform.position).normalized;  //將target的座標和林爽文的座標用normalized算出新的數值，賦予給directionToTarget

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2) //如果從林的面前座標到directionToTarget的座標，小於angle/2，也就是73度/2
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position); //算出林爽文和玩家的距離，賦予給distanceToTarget

                //射線起始位置設在林爽文座標、directionToTarget決定檢測方向、distanceToTarget決定檢測射線的最大距離、obstructionMask決定要忽略的layer物件
                bool hit = Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask);
                if (!hit) //當林爽文沒檢測到牆壁時hit = false，也就是檢測到玩家了。因此在if判斷式判斷如果!hit = !false = true的時候，就代表能執行下面的方法了。
                {
                    Debug.Log(hit);
                    canSeePlayer = true;
                    Debug.Log("偵測到了，任務失敗!!");
                    xrOrigin.transform.position = new Vector3((float)-108.8, 0, (float)18.8);
                    taskPanel.SetActive(true);
                    toLin.SetActive(true);
                    
                    //this.gameObject.transform.position = new Vector3(-113, 0, 185);
                    taskText.text = "任務失敗，請重新捕捉林爽文";
                    lin.GetComponent<NPCwalk>().enabled = false;
                    lin.GetComponent<RandomAgent>().enabled = false;
                    lin.GetComponent<FieldOfView>().enabled = false;   
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Stick" || other.gameObject.name == "Stick(Clone)")
        {
            xrOrigin.transform.position = new Vector3((float)-108.8, 0, (float)18.8);
            originFuf.SetActive(false);
            finalFuf.SetActive(true);
            Debug.Log("hit");
            xrOrigin.gameObject.tag = "hit";
            taskPanel.SetActive(true);
            taskText.text = "任務成功，請跟福康安進行對話";
        }
    }
    public void CloseTaskPanel()
    {
        taskPanel.SetActive(false);
    }
}
