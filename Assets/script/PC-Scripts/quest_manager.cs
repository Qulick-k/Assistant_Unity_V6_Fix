using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class quest_manager : MonoBehaviour
{
    public static int netherland_after_dia_count=0;//給蔗糖後的對話次數 依樣3的倍數
    public static int netherland_before_dia_count=0;//給蔗糖前 會369跳
    public static int huai_before_dia_count=0;//郭懷一任務前 對話次數 依樣3的倍數
    public static int huai_after_dia_count=0;//郭懷一 任務後的對話次數  依樣3的倍數
    public static int blue_before_dia_count=0;//藍藍荷蘭 3倍數
    public static int blue_after_dia_count=0;//藍藍荷蘭  3倍數

    public static int huaicount=0;
    public static int Netsircount=0;

    public static int place2_1=0;//驚嘆號觸發次數
    public Text quest_1;

    public GameObject sugar;
    public Text quest_2;

    public GameObject netherlandpp;
    public Text quest_3;
    public GameObject huai_tagchange_forQuest4open;

    public Text quest_4;

    public GameObject blueNEtsir;
    public Text quest_5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        quest_onoff();
    }

    public void quest_onoff()
    {
        if(place2_1>=1)
        {
            // Debug.Log("boxcoli_false");
            //驚嘆號2_1對話完觸發 改變任務1 TEXT
            quest_1.text="<color=green>1.前往黃色驚嘆號 ✓</color>";
        }

        if(sugar.activeSelf)
        {
            quest_2.text="<color=green>2.尋找甘蔗 ✓</color>";
        }

        if(netherlandpp.tag=="people"&&netherland_after_dia_count>=1)
        {
            quest_3.text="<color=green>3.找到荷蘭人並繳交甘蔗 ✓</color>";
            huai_tagchange_forQuest4open.tag="huai_after";
        }

        if(huaicount==1)
        {
            quest_4.text="<color=green>4.找到郭懷一對話獲取文件 ✓</color>";
        }

        if(blueNEtsir.tag=="blueafter"&&Netsircount==1)
        {
            quest_5.text="<color=green>5.尋找荷蘭人繳回文件 ✓</color>";
        }
    }
}
