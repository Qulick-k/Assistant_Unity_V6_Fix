using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestGlowCollider : MonoBehaviour
{
    public Material Material_Change, Material_Origin;
    public GameObject Grabbles;

    //public Text MissionTwo;

    //void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.tag == "Player"){
    //        Grabbles.GetComponent<MeshRenderer>().material = Material_Change;
    //        //MissionTwo.text = "2.尋找甘蔗 √";
    //        //MissionTwo.color = Color.green;
    //        //Debug.Log("enter!!!");
    //    }
    //}
    //void OnTriggerExit(Collider other)
    //{
    //    if(other.gameObject.tag == "Player"){
    //        Grabbles.GetComponent<MeshRenderer>().material = Material_Origin;
    //        //Debug.Log("exit!!!");
    //    }
    //}
    public void setMaterialOrigin()
    {
        Grabbles.GetComponent<MeshRenderer>().material = Material_Origin;
    }
    public void setMaterialChange()
    {
        Grabbles.GetComponent<MeshRenderer>().material = Material_Change;
    }
}