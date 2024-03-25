using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class PickPaper : MonoBehaviour
{
    int count;
    public GameObject portal, xrOrigin;
    [SerializeField] private Text Mission3;

    public void PickUpTimes()
    {
        count++;
        
        if (count == 5)
        {
            Mission3.text = "<color=green>✓ 3.收集文件</color>";
            portal.SetActive(true);
            xrOrigin.tag = "pick";
        }
    }
}
