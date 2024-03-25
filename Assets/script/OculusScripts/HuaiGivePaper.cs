using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuaiGivePaper : MonoBehaviour
{
    [SerializeField] private GameObject paper;

    public void SetPaperActive()
    {
        paper.SetActive(true);
    }
}
