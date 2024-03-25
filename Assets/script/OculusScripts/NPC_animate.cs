using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_animate : MonoBehaviour
{
    //記得找對話窗當referrence
    //[SerializeField]
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    public void ChangeAnimate()
    {
        Debug.Log("換動畫嚕");
        animator.SetInteger("stats", 1);  //執行說話時，比手勢
        //animator.Play("Take 001 0 0");   沒有複雜動畫切換，就不用狀態機了
    }
    public void BackAnimate()
    {
        animator.SetInteger("stats", 0);  //說話完，變回待機狀態
    }
}
