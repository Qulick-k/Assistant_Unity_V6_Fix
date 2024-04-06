using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuildHouse : MonoBehaviour
{
    //使用者每按一次語音時，當saveBuildMissionEvent有訂閱者的話，就呼叫訂閱者的LogMissionComplete()記錄使用者完成哪個任務
    public UnityEvent<string> saveBuildMissionEvent;

    public GameObject[] houses;
    public int index;
    public GameObject endCanvas, PL, buildCanvas;
    public EndCanvasManagerCh2 endCanvasManager;

    public UnityEngine.UI.Text Mission5;
    public Text Mission5Com;

    public void Build()
    {
        houses[index].SetActive(true);
        index++;
        if(index >= houses.Length)
        {
            endCanvasManager.ShowEndCanvas();
            buildCanvas.SetActive(false);
            if (KeepData.guideSwitch)
            {
                Mission5.text = "<color=green>✓ 5.為荷蘭人造出普羅民遮城(建造普羅民遮城)</color>";
                saveBuildMissionEvent.Invoke("成功完成，5.為荷蘭人造出普羅民遮城(建造普羅民遮城)");
            }
            else
            {
                Mission5Com.text = "<color=green>✓ 5.為荷蘭人造出普羅民遮城(建造普羅民遮城)</color>";
                saveBuildMissionEvent.Invoke("成功完成，5.為荷蘭人造出普羅民遮城(建造普羅民遮城)");
            }
            
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
