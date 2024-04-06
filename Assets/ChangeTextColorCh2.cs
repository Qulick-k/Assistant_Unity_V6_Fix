using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChangeTextColorCh2 : MonoBehaviour
{
    //使用者跟接待員說完話後，當saveCounterMissionEvent有訂閱者的話，就呼叫訂閱者的LogMissionComplete()記錄使用者完成了什麼任務
    public UnityEvent<string> saveCounterMissionEvent;
    
    //設置一個布林值，當玩家跟接待員說完話，更新完任務後，這個布林值會變成false
    public bool counterSwich;

    public Text mission2;
    public Text mission3;
    public Text mission2Com;
    public GameObject xrOrigin;

    private void Start()
    {
        counterSwich = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (counterSwich)
        {
            //如果玩家的tag是TalkedToA，且guideSwitch為true，則任務2的文字變為綠色，並且任務3的文字變為可見
            if (xrOrigin.CompareTag("TalkedToA"))
            {
                if (KeepData.guideSwitch)
                {
                    mission2.text = "<color=#00FF00>✓ 2.了解荷蘭購買原住民土地的位置(找藍色原住民對話)</color>";
                    mission3.color = new Color(mission3.color.r, mission3.color.g, mission3.color.b, 1);
                    saveCounterMissionEvent.Invoke("成功完成，2.了解荷蘭購買原住民土地的位置(找藍色原住民對話)");
                    counterSwich = false;
                }
                //如果guideSwitch是false，那就是綜合型，只顯示完成的任務名字
                else
                {
                    mission2Com.text = "<color=#00FF00>✓ 2.了解荷蘭購買原住民土地的位置(找藍色原住民對話)</color>";
                    saveCounterMissionEvent.Invoke("成功完成，2.了解荷蘭購買原住民土地的位置(找藍色原住民對話)");
                    counterSwich = false;
                }

            }
        }
        
    }
}
