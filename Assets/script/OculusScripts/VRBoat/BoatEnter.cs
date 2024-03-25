using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class BoatEnter : MonoBehaviour
{
    public GameObject playerController; // the player controller
    public GameObject boat; //boat
    public GameObject boatDestination; // the place the player goes in the car

    Quaternion seatRotation; // rotation of player once in the car
    Vector3 seatPosition; //  position of player in the car

    [SerializeField]private bool playerIsInTheCube = false;
    [SerializeField] private BoatExit _boatExit;
    public InputActionProperty B_Button;



    // Update is called once per frame
    void Update()
    {
        seatRotation = boatDestination.transform.rotation;
        seatPosition = boatDestination.transform.position;

        if((playerIsInTheCube == true && B_Button.action.WasPressedThisFrame()) || Input.GetKey(KeyCode.G))
        {
           
            playerController.transform.position = seatPosition; //  set position of player
            playerController.transform.rotation = seatRotation; //  set rotation of the player 
            

            playerController.GetComponent<CharacterController>().enabled = false; //  disable character contoller
            playerController.GetComponent<LocomotionSystem>().enabled = false; //  disable LocomotionSystem
            playerController.GetComponent<DynamicMoveProvider>().enabled = false; //  disable DynamicMoveProvider
            playerController.GetComponent<CapsuleCollider>().enabled = false; //  disable CapsuleCollider
            playerController.GetComponent<ContinuousTurnProviderBase>().enabled = false; //  disable CapsuleCollider
            playerController.GetComponent<CharacterControllerDriver>().enabled = false; //  disable CapsuleCollider
            playerController.GetComponent<TeleportationProvider>().enabled = false; //  disable CapsuleCollider

            playerController.transform.parent = boat.transform; //set player contoller parent from XRrig to the vehicle

            boat.GetComponentInChildren<BoatExit>().intheCar = true; // set bool on CarExit script to true
            _boatExit.enabled = true; //±Ò¥ÎboatExit¸}¥»
            boat.GetComponent<VRBoatController>().CanControll = true; // set bool let player allow to controll boat

            gameObject.SetActive(false); //  disable the enter cube            
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInTheCube = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInTheCube = false;

        }
    }
}
