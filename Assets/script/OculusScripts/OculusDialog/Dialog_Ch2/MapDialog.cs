using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class MapDialog : MonoBehaviour
{
    //使用者每選擇一次土地，當saveMapEvent有訂閱者的話，就呼叫訂閱者的LogMissionComplete()記錄使用者選了什麼土地
    public UnityEvent<string> saveMapEvent;

    public Text actorName, dialogMessage;
    public GameObject dialogBox;
    [SerializeField] private AudioSource[] PickMapSound;
    private int currentindex;

    private void Start()
    {
        currentindex = 0;
        actorName.text = "原住民B";
    }
    public void PickNorth()   //currentindex = 0
    {
        PickMapSound[currentindex].Stop();
        currentindex = 0;
        PickMapSound[currentindex].Play();
        dialogBox.SetActive(true);
        dialogMessage.text = "哦，這土地已經是你們荷蘭人的管轄範圍了。";
        saveMapEvent.Invoke("使用者選擇錯誤，選到北線尾");
    }
    public void PickYuan()    //currentindex = 1
    {
        PickMapSound[currentindex].Stop();
        currentindex = 1;
        PickMapSound[currentindex].Play();
        dialogBox.SetActive(true);
        dialogMessage.text = "哦，建築師，這邊就是大員市，已經是你們荷蘭人的土地了。";
        saveMapEvent.Invoke("使用者選擇錯誤，選到大員市");
    }
    public void PickRight()
    {
        saveMapEvent.Invoke("使用者選擇正確，選到赤崁區域");
        SceneManager.LoadScene("2-3");
        //dialogBox.SetActive(true);
        //dialogMessage.text = "哦，這土地已經是你們荷蘭人的管轄範圍了。";
    }
    public void CloseDialog()
    {
        dialogBox.SetActive(false);
    }
}
