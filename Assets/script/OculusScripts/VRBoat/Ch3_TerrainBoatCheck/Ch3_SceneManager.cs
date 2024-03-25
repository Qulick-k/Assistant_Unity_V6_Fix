using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ch3_SceneManager : MonoBehaviour
{
    public void BackToLobby()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void BackToCh3()
    {
        SceneManager.LoadScene("Chapter3_ChooseArea_Scene");
    }
}
