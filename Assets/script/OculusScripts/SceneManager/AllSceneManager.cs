using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class AllSceneManager : MonoBehaviour
{
    //�s�Wunityevent��player�ΨӦs�ɡA�p�G������q�\�A�N�q���L�s��
    public UnityEvent saveLobbyEvent;
    public UnityEvent restartEvent;

    public string differentScene;
    public void BackToLobby()
    {
        //�p�G������q�\�A�N�I�s�q�\�̰���ۤv����k
        saveLobbyEvent.Invoke();
        SceneManager.LoadScene("Lobby");
    }

    public void RestartScene()
    {
        //�p�G������q�\�A�N�I�s�q�\�̰���ۤv����k
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
