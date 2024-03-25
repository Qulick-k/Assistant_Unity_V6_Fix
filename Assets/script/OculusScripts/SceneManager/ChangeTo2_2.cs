using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeTo2_2 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("enter");
        if(other.gameObject.name == "XR Origin")
        {
            SceneManager.LoadScene("2-2");
        }
    }
}
