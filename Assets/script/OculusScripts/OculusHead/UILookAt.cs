using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookAt : MonoBehaviour
{
    public Transform head;    
        
    private void LateUpdate()
    {
        //transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
        transform.LookAt(new Vector3(head.position.x, transform.position.y, head.position.z)); //讓canvas實時面相鏡頭
        transform.forward *= -1;      //上面那行程式會變反的，所以forward*-1讓canvas轉正
    }

  
}
