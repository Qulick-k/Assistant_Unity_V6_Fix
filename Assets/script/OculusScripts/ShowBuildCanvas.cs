using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBuildCanvas : MonoBehaviour
{
    public GameObject buildCanvas;
    private void OnTriggerEnter(Collider other)
    {
        buildCanvas.SetActive(true);
    }
}
