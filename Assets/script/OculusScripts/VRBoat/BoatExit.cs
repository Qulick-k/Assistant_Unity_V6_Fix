using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using UnityEngine.XR.Interaction.Toolkit;

public class BoatExit : MonoBehaviour
{
    public GameObject xrRig; // the XRRig
    public GameObject playerController; // the player controller
    public GameObject boat; //  the boat
    public GameObject boatEnter; //  the enter cube
    public GameObject ExitDestination;
    //public GameObject trackingSpace; // the tracking space

    //Quaternion initialRotation;
    //Vector3 initialPosition;

    //
    public bool intheCar = false;

    public InputActionProperty B_Button;

    // Start is called before the first frame update
    void Start()
    {
    //    initialRotation = trackingSpace.transform.localRotation; // store the initial rotation of the tracking space
    //    initialPosition = trackingSpace.transform.localPosition; //  store the initial position of the tracking space
    }

    // Update is called once per frame
    void Update()
    {
        if(intheCar == true && B_Button.action.WasPressedThisFrame())
        {
            BackPosition();
        }
    }

    public void BackPosition()
    {
        playerController.transform.position = ExitDestination.transform.position; //transport player out of the car

        playerController.transform.parent = xrRig.transform; // set parent to XRrig

        playerController.GetComponent<CharacterController>().enabled = true; //  enable character contoller
        playerController.GetComponent<LocomotionSystem>().enabled = true; //  enable LocomotionSystem
        playerController.GetComponent<DynamicMoveProvider>().enabled = true; //  enable DynamicMoveProvider
        playerController.GetComponent<CapsuleCollider>().enabled = true; //  enable CapsuleCollider
        playerController.GetComponent<ContinuousTurnProviderBase>().enabled = true; //  enable ContinuousTurnProviderBase
        playerController.GetComponent<CharacterControllerDriver>().enabled = true; //  enable CharacterControllerDriver
        playerController.GetComponent<TeleportationProvider>().enabled = true; //  enable TeleportationProvider

        boat.GetComponent<VRBoatController>().CanControll = false; // set bool let player allow to controll boat

        intheCar = false;

        boatEnter.SetActive(true); //enable the enter cube 
    }
}
