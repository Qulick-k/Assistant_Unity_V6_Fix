using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingSpeaking : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

   
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        anim.SetInteger("Stats",1);
         /*
        if(tag == "npc")
        {
            anim.SetInteger("Stats",1);
            //Invoke("after1sec_changeAni", 0.1f);
        }
        */
    }
    
    private void OnTriggerExit(Collider other)
    {
        anim.SetInteger("Stats",0);
        /*
        if(tag == "npc")
        {
            anim.SetInteger("Stats",0);
            //Invoke("after1sec_changeAni",0.1f);
        }
        */
    }
    /*
    void after1sec_changeAni()
    {
        anim.SetInteger("Stats",1);
    }
    */
}
