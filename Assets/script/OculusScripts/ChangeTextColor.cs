using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTextColor : MonoBehaviour
{
    [SerializeField] private GameObject sugarTable;
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
        Text text = GetComponent<Text>();
        text.text = "<color=green>✓ 2.了解荷蘭開墾台灣的規劃(尋找甘蔗)</color>";
    }
    
}
