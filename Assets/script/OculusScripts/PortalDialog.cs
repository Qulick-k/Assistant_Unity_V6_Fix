using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalDialog : MonoBehaviour
{
    public GameObject dialogBox;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "XR Origin")
        {
            dialogBox.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.name == "XR Origin")
        {
            dialogBox.SetActive(false);
        }
    }
}
