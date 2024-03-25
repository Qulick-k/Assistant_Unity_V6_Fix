using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DetectObject : MonoBehaviour
{
    //此腳本給一般物件使用，不能給海神廟牌坊、文昌閣牌坊使用
    private XRGrabInteractable GameObject;

    string cache; //快取
    public bool inCollider;

    //用來存放指定的Layer，以及自身的Layer
    private int LayerOnly_0_5_8_layer;
    private int LayerSelf;

    private void Awake()
    {
        //gameObject.layer uses only integers, but we can turn a layer name into a layer integer using LayerMask.NameToLayer()
        LayerOnly_0_5_8_layer = LayerMask.NameToLayer("Only_0_5_8_layer");
        LayerSelf = LayerMask.NameToLayer("ObjectNoTouchPlayer");


    }


    private void Start()
    {
        GameObject = GetComponent<XRGrabInteractable>();
        inCollider = false;
        cache = tag;
        //print("");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Border")
        {
            gameObject.layer = LayerOnly_0_5_8_layer;
            Debug.Log("Current layer: " + gameObject.layer);
        }
        
         //當地板碰到物件
        if (other.gameObject.tag == "Ground")
        {
            GameObject.interactionLayers = InteractionLayerMask.GetMask("Default");//把GameObject的layer設為Default            
            this.tag = "Untagged"; //如果碰到terrain或建築物的底部就把自身的tag改成Untagged
        }            
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Border")
        {
            gameObject.layer = LayerSelf;
            Debug.Log("Current layer: " + gameObject.layer);
        }
        //當物件離開地板
        if (other.gameObject.tag == "Ground")
        {
            GameObject.interactionLayers = InteractionLayerMask.GetMask("Default", "Socket");//把GameObject的layer設為Default跟Socket
                                                                                             //this.tag = "Sugar";//如果離開terrain或建築物的底部就把自身的tag改成Sugar
            this.tag = cache;
        }
    }

}
