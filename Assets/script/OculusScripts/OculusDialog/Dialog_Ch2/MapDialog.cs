using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapDialog : MonoBehaviour
{
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
    }
    public void PickYuan()    //currentindex = 1
    {
        PickMapSound[currentindex].Stop();
        currentindex = 1;
        PickMapSound[currentindex].Play();
        dialogBox.SetActive(true);
        dialogMessage.text = "哦，建築師，這邊就是大員市，已經是你們荷蘭人的土地了。";
    }
    public void PickRight()
    {
        SceneManager.LoadScene("2-3");
        //dialogBox.SetActive(true);
        //dialogMessage.text = "哦，這土地已經是你們荷蘭人的管轄範圍了。";
    }
    public void CloseDialog()
    {
        dialogBox.SetActive(false);
    }
}
