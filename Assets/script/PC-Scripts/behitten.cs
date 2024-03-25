using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class behitten : MonoBehaviour
{
    public GameObject HideObj;
    public GameObject ShowObj;
    public GameObject hand_paper_active;
    public GameObject colli_after;
    public GameObject colli_before;
    public GameObject hidepeprlookCanvas;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void HitByRaycast()
    {
        if(Input.GetButtonDown("E"))
        {
            ShowObj.SetActive(true);
            HideObj.SetActive(false);
            if(tag=="people")
            {
            // FindObjectOfType<DialogueTrigger>().TriggerDialogue();
            // people_colli.enabled=true;//把人物boxcollider 有is trigger 的打勾
            colli_after.SetActive(true);
            colli_before.SetActive(false);
            Invoke("after_1sec_close",0.1f);
            }
            if(tag=="people_sugarbefore")
            {
            colli_after.SetActive(false);
            colli_before.SetActive(true);
            Invoke("before_1sec_close",0.1f);
            }
            if(tag=="huai")
            {
            // FindObjectOfType<DialogueTrigger>().TriggerDialogue();
            // people_colli.enabled=true;
            colli_after.SetActive(false);
            colli_before.SetActive(true);
            Invoke("before_1sec_close",0.1f);
            }
            if(tag=="huai_after")
            {
            hand_paper_active.SetActive(true);
            colli_after.SetActive(true);
            colli_before.SetActive(false);
            Invoke("after_1sec_close",0.1f);
            }
            if(tag=="bluebefore")
            {
            colli_after.SetActive(false);
            colli_before.SetActive(true);
            Invoke("before_1sec_close",0.1f);
            }

            if(tag=="blueafter")
            {
            // FindObjectOfType<DialogueTrigger>().TriggerDialogue();
            // people_colli.enabled=true;
            colli_after.SetActive(true);
            colli_before.SetActive(false);
            Invoke("after_1sec_close",0.1f);
            quest_manager.Netsircount=1;
            hidepeprlookCanvas.SetActive(false);
            }
            if(tag=="blue_stranger")
            {
            colli_after.SetActive(true);
            colli_before.SetActive(false);
            Invoke("after_1sec_close",0.1f);
            }
            if(tag=="green_stranger")
            {
            colli_after.SetActive(true);
            colli_before.SetActive(false);
            Invoke("after_1sec_close",0.1f);
            }
            if(tag=="pink_stranger")
            {
            colli_after.SetActive(true);
            colli_before.SetActive(false);
            Invoke("after_1sec_close",0.1f);
            }
            if(tag=="red_stranger")
            {
            colli_after.SetActive(true);
            colli_before.SetActive(false);
            Invoke("after_1sec_close",0.1f);
            }
        }
    }

    void before_1sec_close()
    {
        colli_before.SetActive(false);
    }
    void after_1sec_close()
    {
        colli_after.SetActive(false);
    }
}
