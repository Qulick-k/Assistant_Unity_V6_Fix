using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class SwitchRayInteractor : MonoBehaviour
{
    public InputActionProperty PressA;
    [SerializeField] private GameObject RightGrabRay;

    // Update is called once per frame
    void Update()
    {
        if (PressA.action.WasPerformedThisFrame())
        {
            RightGrabRay.SetActive(!RightGrabRay.activeSelf);
        }
    }
}
