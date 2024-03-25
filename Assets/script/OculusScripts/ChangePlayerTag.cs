using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayerTag : MonoBehaviour
{
    [SerializeField] private GameObject sugarTable;
    private void Start()
    {
        SugarSpawnSpot changeColorTag = sugarTable.GetComponent<SugarSpawnSpot>();
        changeColorTag.changeTag += ChangeColorTag_changeTag;       
    }       

    private void ChangeColorTag_changeTag(object sender, System.EventArgs e)
    {
        changePlayerTag();
    }
        
    public void changePlayerTag()                              //透過SugarTable的Enent通知玩家(XROrigin)改變顏色
    {
        //GameObject gameObject = GetComponent<>();
        gameObject.tag = "PlayerWithSugar";
    }
}
