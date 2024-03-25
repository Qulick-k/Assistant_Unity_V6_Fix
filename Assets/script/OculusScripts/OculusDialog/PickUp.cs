using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    //public event EventHandler sugarHover;

    //public GameObject PL;
    //[SerializeField] private Text Mission2;
    public GameObject Objects;
    //public static SaveSystem SaveSystem;
    

    public void changeOblectTag()
    {

        SugarSpawnSpot.PickUp_sugarHover();
        //sugarHover?.Invoke(this, EventArgs.Empty);
        Debug.Log("要傳過去了哦");
        //PL.tag = "PlayerWithSugar";
        //Mission2.text = "<color=green>2.尋找甘蔗 ✓</color>";
        BackColor();
    }
    public void ChangeColor()
    {
        Objects.GetComponent<TestGlowCollider>().setMaterialChange();
    }
    public void BackColor()
    {
        Objects.GetComponent<TestGlowCollider>().setMaterialOrigin();
    }

}
