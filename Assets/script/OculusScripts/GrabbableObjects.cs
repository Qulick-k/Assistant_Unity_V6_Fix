using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GrabbableObjects : MonoBehaviour
{
    public Material Light;

   void OnTriggerStay(Collider other)
   {
    if(other.gameObject.tag == "Player"){
        Light = this.GetComponent<MeshRenderer>().material;
    }
    
   }

}
