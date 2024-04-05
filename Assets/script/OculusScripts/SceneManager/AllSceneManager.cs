using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class AllSceneManager : MonoBehaviour
{
    //新增unityevent給player用來存檔，如果有物件訂閱，就通知他存檔
    public UnityEvent saveLobbyEvent;
    public UnityEvent restartEvent;

    public string differentScene;
    public void BackToLobby()
    {
        //如果有物件訂閱，就呼叫訂閱者執行自己的方法
        saveLobbyEvent.Invoke();
        SceneManager.LoadScene("Lobby");
    }

    public void RestartScene()
    {
        //如果有物件訂閱，就呼叫訂閱者執行自己的方法
        restartEvent.Invoke();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void GotoCh3Ship()
    {
        SceneManager.LoadScene("Chapter3_Zeelandia_Scene");
    }
    public void RestartDifferentScene()
    {
        SceneManager.LoadScene(differentScene);
    }
    
}
