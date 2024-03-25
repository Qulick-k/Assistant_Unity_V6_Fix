using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScale : MonoBehaviour
{
    private Vector3 Scale;
    [SerializeField] private Transform NeedChangeObject;

    private bool allowToBack;

    void Awake()
    {
        Scale = NeedChangeObject.localScale;
        allowToBack = false;
    }
    // Start is called before the first frame update
    //void Awake()
    //{
       
    //}

    public void ChangeObjectScale()
    {
        if (allowToBack == false)
        {
            NeedChangeObject.localScale = new Vector3(5f, 5f, 5f);
            StartCoroutine(ChangeBoolToTrue());  //使用協程，等2秒再讓布林值變為true
            Debug.Log("變換體積");
        }
        else
        {
            NeedChangeObject.localScale = Scale;
            StartCoroutine(ChangeBoolToFalse()); // 使用協程，等2秒再讓布林值變為false
            Debug.Log("還原大小");
        }

    }

    IEnumerator ChangeBoolToTrue()
    {
        yield return new WaitForSeconds(2);
        allowToBack = true;
    }
    IEnumerator ChangeBoolToFalse()
    {

        yield return new WaitForSeconds(2);
        allowToBack = false;
    }

}
