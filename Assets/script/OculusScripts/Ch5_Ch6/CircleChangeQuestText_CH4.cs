using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleChangeQuestText_CH4 : MonoBehaviour
{
    [SerializeField] private Text Mission1;
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name == "XR Origin")
        {
            Mission1.text = "<color=green>✓ 1.前往藍色光圈</color>";
        }
    }

    public void Guai()
    {
        Mission1.text = "<color=green>✓ 3.認識大士殿裡的古物(放回觀音神像)</color>";
    }
}
