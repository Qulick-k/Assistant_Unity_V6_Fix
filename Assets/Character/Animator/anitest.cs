using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anitest : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        anim.SetInteger("stats",1);
    }

    private void OnTriggerExit(Collider other) {
        anim.SetInteger("stats",0);
    }
}
