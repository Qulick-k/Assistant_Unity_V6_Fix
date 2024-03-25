using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class behitAnim : MonoBehaviour
{
    
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void HitByRaycast()
    {
        if(Input.GetButtonDown("E"))
        {

            if(tag=="green_stranger")
            {
            anim.SetInteger("stats",1);
            Invoke("after1sec_changeAni",0.1f);
            }

            if(tag=="red_stranger")
            {
            anim.SetInteger("stats",1);
            Invoke("after1sec_changeAni",0.1f);
            }

            if(tag=="pink_stranger")
            {
            anim.SetInteger("stats",1);
            Invoke("after1sec_changeAni",0.1f);
            }

            if(tag=="blue_stranger")
            {
            anim.SetInteger("stats",1);
            Invoke("after1sec_changeAni",0.1f);
            }

            if(tag=="huai")
            {
            anim.SetInteger("stats",1);
            Invoke("after1sec_changeAni",0.1f);
            }

            if(tag=="huai_after")
            {
            anim.SetInteger("stats",1);
            Invoke("after1sec_changeAni",0.1f);
            }

            if(tag=="people")
            {
            anim.SetInteger("stats",1);
            Invoke("after1sec_changeAni",0.1f);
            }

            if(tag=="people_sugarbefore")
            {
            anim.SetInteger("stats",1);
            Invoke("after1sec_changeAni",0.1f);
            }

            if(tag=="blueafter")
            {
            anim.SetInteger("stats",1);
            Invoke("after1sec_changeAni",0.1f);
            }

            if(tag=="bluebefore")
            {
            anim.SetInteger("stats",1);
            Invoke("after1sec_changeAni",0.1f);
            }

        }
        
    }

    void after1sec_changeAni()
    {
        anim.SetInteger("stats",0);
    }
}
