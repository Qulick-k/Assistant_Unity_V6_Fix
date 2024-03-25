using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class BuildHouse : MonoBehaviour
{
    public GameObject[] houses;
    public int index;
    public GameObject endCanvas, PL, buildCanvas;
    public EndCanvasManagerCh2 endCanvasManager;

    public UnityEngine.UI.Text Mission5;

    public void Build()
    {
        houses[index].SetActive(true);
        index++;
        if(index >= houses.Length)
        {
            endCanvasManager.ShowEndCanvas();
            buildCanvas.SetActive(false);
            Mission5.text = "<color=green>✓ 5.為荷蘭人造出普羅民遮城(建造普羅民遮城)</color>";
        }
    }
    private void Start()
    {
        endCanvasManager = endCanvas.GetComponent<EndCanvasManagerCh2>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Build();
        }
    }
}
