using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRotation : MonoBehaviour
{
    public GameObject playerCamera, xrOrigin;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = playerCamera.transform.position;
        var camForward = playerCamera.transform.rotation.z;
        Vector3 symbol;
        symbol.z = camForward;
        symbol.y = xrOrigin.transform.rotation.z;
        symbol.x = 90;
        this.transform.rotation = Quaternion.Euler(symbol.x, symbol.y, symbol.z);

        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log(this.transform.rotation);
        }

    }
        
}
