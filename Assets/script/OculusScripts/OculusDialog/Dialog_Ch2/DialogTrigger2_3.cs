using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogTrigger2_3 : MonoBehaviour
{
    
    public Message2_3[] message2_3;
    public Actor2_3[] actor2_3;
    public GameObject dialog;

    [SerializeField] private GameObject ObjectTag;
    private void StartDialog1()
    {
        dialog.GetComponent<DialogManager2_3>().openDialogue(message2_3, actor2_3);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            ObjectTag.GetComponent<RandomPathTrolling>().SetWalkFalse();
            dialog.SetActive(true);
            StartDialog1();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //³]§PÂ_±ø¥ó
        //©I¥sNPC_animate
        //dialog.SetActive(false);
        ObjectTag.GetComponent<RandomPathTrolling>().SetWalkTrue();
        ObjectTag.GetComponent<NPC_animate>().BackAnimate();
    }
}


[System.Serializable]
public class Message2_3
{
    public int actorId;
    public string message;
}

[System.Serializable]
public class Actor2_3
{
    public string name;
}
