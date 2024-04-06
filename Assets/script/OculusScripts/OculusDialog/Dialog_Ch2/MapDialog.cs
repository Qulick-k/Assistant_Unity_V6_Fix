using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class MapDialog : MonoBehaviour
{
    //�ϥΪ̨C��ܤ@���g�a�A��saveMapEvent���q�\�̪��ܡA�N�I�s�q�\�̪�LogMissionComplete()�O���ϥΪ̿�F����g�a
    public UnityEvent<string> saveMapEvent;

    public Text actorName, dialogMessage;
    public GameObject dialogBox;
    [SerializeField] private AudioSource[] PickMapSound;
    private int currentindex;

    private void Start()
    {
        currentindex = 0;
        actorName.text = "����B";
    }
    public void PickNorth()   //currentindex = 0
    {
        PickMapSound[currentindex].Stop();
        currentindex = 0;
        PickMapSound[currentindex].Play();
        dialogBox.SetActive(true);
        dialogMessage.text = "�@�A�o�g�a�w�g�O�A�̲����H�����ҽd��F�C";
        saveMapEvent.Invoke("�ϥΪ̿�ܿ��~�A���_�u��");
    }
    public void PickYuan()    //currentindex = 1
    {
        PickMapSound[currentindex].Stop();
        currentindex = 1;
        PickMapSound[currentindex].Play();
        dialogBox.SetActive(true);
        dialogMessage.text = "�@�A�ؿv�v�A�o��N�O�j�����A�w�g�O�A�̲����H���g�a�F�C";
        saveMapEvent.Invoke("�ϥΪ̿�ܿ��~�A���j����");
    }
    public void PickRight()
    {
        saveMapEvent.Invoke("�ϥΪ̿�ܥ��T�A��쨪�r�ϰ�");
        SceneManager.LoadScene("2-3");
        //dialogBox.SetActive(true);
        //dialogMessage.text = "�@�A�o�g�a�w�g�O�A�̲����H�����ҽd��F�C";
    }
    public void CloseDialog()
    {
        dialogBox.SetActive(false);
    }
}
