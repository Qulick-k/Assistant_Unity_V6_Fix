using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTextColorCh2 : MonoBehaviour
{
    public Text mission2;
    public GameObject xrOrigin;

    // Update is called once per frame
    void Update()
    {
        if (xrOrigin.CompareTag("TalkedToA"))
        {
            mission2.text = "<color=#00FF00>✓ 2.了解荷蘭購買原住民土地的位置(找藍色原住民對話)</color>";
        }
    }
}
