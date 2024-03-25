using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rayScript : MonoBehaviour
{
    Ray ray;
    float raylength =1.5f;
    RaycastHit hit;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ray=Camera.main.ScreenPointToRay(new Vector3(Screen.width/2,Screen.height/2,0));

        if(Physics.Raycast(ray,out hit,raylength))
        {
            hit.transform.SendMessage("HitByRaycast",gameObject,SendMessageOptions.DontRequireReceiver);
            Debug.DrawLine(ray.origin,hit.point,Color.yellow);
            print(hit.transform.name);
        }else{

        }
    }
}
