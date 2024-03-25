using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndCanvasManagerCh2 : MonoBehaviour
{
    public GameObject PL, endCanvas;
    List<PlayerDataCh2> pldata2 ;

    public string filename;
    public Text log;

    private void Start()
    {
        //filename = GetComponent<SaveSystemCh2>().filename.ToString();
        //pldata2 = FileHandler.ReadFromJSON<PlayerDataCh2>(filename);
    }
    public void ShowEndCanvas()
    {
        Vector3 forward = PL.transform.forward;
        forward.y = 1;
        Vector3 x = PL.transform.forward;
        x.y = 0;
        endCanvas.transform.position = PL.transform.position + forward * 3;
        endCanvas.transform.rotation = Quaternion.LookRotation(x, Vector3.up);

        pldata2 = FileHandler.ReadFromJSON<PlayerDataCh2>(filename);

        if (pldata2.Count >= 10)
        {
            foreach (var item in pldata2.GetRange(pldata2.Count - 10, 10))
            {
                log.text += item.playerName + " " + item.playerTime + " " + item.playerActionType + "\n";
                
            }
           
        }
        else
        {
            foreach (var item in pldata2)
            {
                log.text += item.playerName + " " + item.playerTime + " " + item.playerActionType + "\n";
            }
        }

        endCanvas.SetActive(true);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("press s");
            //filename = "playerdatach2.json";
            //ShowEndCanvas();
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene("2-1");
    }
    public void ReadFile()
    {
        pldata2 = FileHandler.ReadFromJSON<PlayerDataCh2>(filename);

        if (pldata2.Count >= 10)
        {
            foreach (var item in pldata2.GetRange(pldata2.Count - 10, 10))
            {
                log.text += item.playerName + " " + item.playerTime + " " + item.playerActionType + "\n";

            }

        }
        else
        {
            foreach (var item in pldata2)
            {
                log.text += item.playerName + " " + item.playerTime + " " + item.playerActionType + "\n";
            }
        }
    }
}
