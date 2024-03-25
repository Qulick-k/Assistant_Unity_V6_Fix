using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _GeneratingStuffSystem : MonoBehaviour
{
    [SerializeField] private List<Transform> GeneratingList;
    [SerializeField] private Transform SpwanSpot;
    private int count;
    private void Start()
    {
        count = 0;
    }
    public void Generate1()
    {
        if(count < 10)
        {
            Transform GeneratingPrefab = Instantiate(GeneratingList[0], SpwanSpot);
            count++;
        }
    }

    public void Generate2()
    {
        if (count < 10)
        {
            Transform GeneratingPrefab = Instantiate(GeneratingList[1], SpwanSpot);
            count++;
        }
    }

    public void Generate3()
    {
        if (count < 10)
        {
            Transform GeneratingPrefab = Instantiate(GeneratingList[2], SpwanSpot);
            count++;
        }
    }

    public void Generate4()
    {
        if (count < 10)
        {
            Transform GeneratingPrefab = Instantiate(GeneratingList[3], SpwanSpot);
            count++;
        }
    }

    public void Generate5()
    {
        if (count < 10)
        {
            Transform GeneratingPrefab = Instantiate(GeneratingList[4], SpwanSpot);
            count++;
        }        
    }

    public void Generate6()
    {
        if (count < 10)
        {
            Transform GeneratingPrefab = Instantiate(GeneratingList[5], SpwanSpot);
            count++;
        }
        
    }

    public void Generate7()
    {
        if (count < 10)
        {
            Transform GeneratingPrefab = Instantiate(GeneratingList[6], SpwanSpot);
            count++;
        }
        
    }
}
