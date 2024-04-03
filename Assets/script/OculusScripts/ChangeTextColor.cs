using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTextColor : MonoBehaviour
{
    [SerializeField] private GameObject sugarTable;
    [SerializeField] private Text Mission3;
    private void Start()
    {
        SugarSpawnSpot changeColorTag = sugarTable.GetComponent<SugarSpawnSpot>();        
        changeColorTag.changeColor += ChangeColorTag_changeColor;
    }

    private void ChangeColorTag_changeColor(object sender, System.EventArgs e)
    {
        changeColor();
    }
    
    public void changeColor()                                   //透過SugarTable的Enent通知quest2改變顏色
    {
        if (KeepData.guideSwitch)
        {
            Text text = GetComponent<Text>();
            text.text = "<color=green>✓ 2.了解荷蘭開墾台灣的規劃(尋找甘蔗)</color>";
            Mission3.color = new Color(Mission3.color.r, Mission3.color.g, Mission3.color.b, 1);  //撿取到甘蔗後，讓任務3的顏色從透明變成原本的顏色
        }
        else
        {
            Text text = GetComponent<Text>();            
            text.text = "<color=green>✓ 2.了解荷蘭開墾台灣的規劃(尋找甘蔗)</color>";
            text.color = new Color(text.color.r, text.color.g, text.color.b, 1);        //撿取到甘蔗後，讓任務2的顏色從透明變成完成任務的顏色
        }
    }
    
}
