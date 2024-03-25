using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AllSceneManager : MonoBehaviour
{
    public string differentScene;
    public void BackToLobby()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void RestartScene()
    {
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
