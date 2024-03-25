using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public AudioSource taskAudio;
    

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name == "XR Origin")
        {
            taskAudio.Play();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name =="XR Origin")
        {
            taskAudio.Stop();
        }
    }
}
