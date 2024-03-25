using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject close_3sec_open;

    void update()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        TriggerDialogue();
        close_3sec_open.SetActive(false);//關閉3秒 之後開啟
        Invoke("itself_coli_control",3.0f);
        if(tag=="2_1")
        {
            quest_manager.place2_1++;
        }
        if(tag=="coli_sugar_before")
        {
            quest_manager.netherland_before_dia_count++;
        }
        if(tag=="coli_sugar_after")
        {
            quest_manager.netherland_after_dia_count++;
        }
        if(tag=="huai_beforecoli")
        {
            quest_manager.huai_before_dia_count++;
        }
        if(tag=="huai_aftercoli")
        {
            quest_manager.huai_after_dia_count++;
        }
        if(tag=="blue_before_coli")
        {
            quest_manager.blue_before_dia_count++;
        }
        if(tag=="blue_after_coli")
        {
            quest_manager.blue_after_dia_count++;
        }
    }
    
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        Time.timeScale=0;
    }
    
        public void itself_coli_control()
    {
        close_3sec_open.SetActive(true);
    }
    
}
