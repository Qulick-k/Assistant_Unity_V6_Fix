using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class CircleChangeQuestText : MonoBehaviour
{
    [SerializeField] private Text Mission1;
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name == "XR Origin")
        {
            Mission1.text = "<color=green>✓ 1.前往紅色圈圈</color>";
        }
    }
}
