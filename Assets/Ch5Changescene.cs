using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ch5Changescene : MonoBehaviour
{

    public string sceneName;
    

    private void OnTriggerEnter(Collider other)
    {
        
        SceneManager.LoadScene(sceneName);
        
    }

}
