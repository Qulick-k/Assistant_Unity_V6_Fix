using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Ditzelgames;
using Unity.VisualScripting;
using System;
using UnityEngine.UIElements;

public class VRBoatController : MonoBehaviour
{
    [Header("Turn")]
    public HingeJoint steeringWheel;
    public float maxValue = 0.35f;
    public float minValue = -0.35f;
    public float turnThreshold = 0.2f;

    [Header("Input")]
    public float turnInput;    // STEP 5
    public float steering = 80f;  //STEP 11
    float rotate, currentRotate; //STEP 11
    [SerializeField] bool handOn = false;


    [Header("Power")]
    //visible Properties
    public float Power = 5f;
    public float MaxSpeed = 10f;

    //used Components
    public Rigidbody boatRigidbody;
    //public Quaternion StartRotation;

    public InputActionProperty SpeedTrigger;
    public InputActionProperty BreakTrigger;

    //
    public bool CanControll = false;

    private void Awake()
    {
        boatRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float steeringNormal = Mathf.InverseLerp(minValue, maxValue, steeringWheel.transform.localRotation.y);  // STEP 1
        float steeringRange = Mathf.Lerp(-1, 1, steeringNormal);            // STEP 2
        if (Mathf.Abs(steeringRange) < turnThreshold) // STEP 3
        {
            steeringRange = 0;
        }

        if (steeringRange == 0)   // STEP 4
        {
            turnInput = Input.GetAxis("Horizontal");
        }
        else
        {
            turnInput = steeringRange;
        }

        //Steer
        if (turnInput != 0)   // STEP 6
        {
            int dir = turnInput > 0 ? 1 : -1;        // STEP 7
            float amount = Mathf.Abs((turnInput));        // STEP 8
            Steer(dir, amount);    // STEP 9
        }

        currentRotate = rotate; rotate = 0;    //STEP 12
    }

    private void Steer(int direction, float amount) //10
    {
        rotate = (steering * direction) * amount;   // STEP 11
    }

   

    private void FixedUpdate()
    {
        //compute vectors
        var forward = Vector3.Scale(new Vector3(1, 0, 1), transform.forward);

        //forward/backward poewr
        float speedValue = SpeedTrigger.action.ReadValue<float>();
        float breakValue = BreakTrigger.action.ReadValue<float>();

        if (CanControll == true)
        {
            //Steering
            //Debug.LogWarning(Vector3.Lerp(transform.eulerAngles, new Vector3(0, transform.eulerAngles.y + currentRotate, 0), Time.deltaTime * 5f));            
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, transform.eulerAngles.y + currentRotate, 0), Time.deltaTime * 5f);    //STEP 13 finish
            //transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

            if (Input.GetKey(KeyCode.W) || speedValue > 0.3)
            {
                PhysicsHelper.ApplyForceToReachVelocity(boatRigidbody, forward * MaxSpeed, Power);
                Debug.Log("«e¶i");
            }
            
            if (Input.GetKey(KeyCode.S) || breakValue > 0.3)
            {
                PhysicsHelper.ApplyForceToReachVelocity(boatRigidbody, forward * -MaxSpeed, Power);
            }            
        }                         
    }
}
