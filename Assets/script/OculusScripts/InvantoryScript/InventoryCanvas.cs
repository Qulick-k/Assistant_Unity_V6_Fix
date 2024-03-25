using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class InventoryCanvas : MonoBehaviour
{
    public event EventHandler callOpenMeshRenderer;
    public event EventHandler callCloseMeshRenderer;


    public Transform head;
    public float spawnDistance = 0.25f;
    public GameObject invantoryCanvas;
    public InputActionProperty showInvantoryCanvasPressX;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (showInvantoryCanvasPressX.action.WasPressedThisFrame())  //當按下X鍵
        {
            if (invantoryCanvas.GetComponent<Canvas>().enabled == false)   //要打開canvas之前，canvas是關起來的時候(false)
            {
                callOpenMeshRenderer?.Invoke(this, EventArgs.Empty);
                invantoryCanvas.GetComponent<Canvas>().enabled = true;      //並打開canvas掛載的Canvas載件
                invantoryCanvas.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;         //叫canvas歸位

            }
            else
            {
                callCloseMeshRenderer?.Invoke(this, EventArgs.Empty);
                invantoryCanvas.GetComponent<Canvas>().enabled = false;
                invantoryCanvas.transform.position = head.position + new Vector3(head.up.x, 0, head.up.z).normalized * 10;   //讓canvas移動到玩家身後

            }

        }
        invantoryCanvas.transform.LookAt(new Vector3(head.position.x, invantoryCanvas.transform.position.y, head.position.z)); //面向玩家
        invantoryCanvas.transform.forward *= -1;
    }

}
